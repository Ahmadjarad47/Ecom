using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Ecom.API.Error;
using Ecom.Core.Dto;
using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Ecom.infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ITokenService token;
        private readonly IMapper mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService token,IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.token = token;
            this.mapper = mapper;
        }
        [HttpPost("Login")]
        public async Task<IActionResult>Login(LoginDTO userDto)
        {
            var user=await userManager.FindByEmailAsync(userDto.Email);
            if (user is null) return Unauthorized(new BaseComonentResponse(401));
            var result = await signInManager.CheckPasswordSignInAsync(user, userDto.Password, false)
      ; if (result is null || result.Succeeded==false) return Unauthorized(new BaseComonentResponse(401));
      
            return Ok(new UserDto(
            
                 user.Email,
                user.displayName,
                token.CreateToken(user)
            ));
        }
        [HttpPost("Register")]
        public async Task<IActionResult>Register(RegisterDTO register)
        {
            if (checkEmailExisit(register.Email).Result.Value||string.IsNullOrEmpty(register.Email))
            {
                return new BadRequestObjectResult(new APIValidationError
                {
                    Error = new[]
                    {
                        "This Email Is Alread Exisit!!"
                    }
                });
            }
            var user = new AppUser
            {
                displayName = register.DisplayName,
                UserName = register.Email,
                Email= register.Email,
            };
            var res = await userManager.CreateAsync(user, register.Password);
            if (res.Succeeded==false)
            {
                return BadRequest(new BaseComonentResponse(400));
            }
            return Ok(new UserDto(register.Email, register.DisplayName, token.CreateToken(user)));
        }
        [Authorize]
        [HttpGet("get-current-user")]
        public async Task<IActionResult> getCurrentUser()
        {
            string email = HttpContext.User?.Claims?
                .FirstOrDefault(m => m.Type == ClaimTypes.Email).Value;
            AppUser user = await userManager.FindByEmailAsync(email);
            return Ok(new UserDto(user.Email,user.displayName,token.CreateToken(user)));
        }
        
      
      
        [Authorize]
        [HttpGet("get-user-address")]
        public async Task<IActionResult> GetAddress()
        {
            var email = HttpContext.User?.Claims?
               .FirstOrDefault(m => m.Type == ClaimTypes.Email).Value;

            AppUser user = await userManager.Users.Include(x=>x.Address).SingleOrDefaultAsync(x=>x.Email==email);
                var result=mapper.Map<Address,AddressDTO>(user.Address);
            return Ok(result);
        }
        [Authorize]
        [HttpGet("update-address")]
        public async Task<IActionResult>UpdateAddresDTos(AddressDTO addressDTO)
        {
            var email = HttpContext.User?.Claims?
               .FirstOrDefault(m => m.Type == ClaimTypes.Email).Value;

            AppUser user = await userManager.Users.Include(x => x.Address).SingleOrDefaultAsync(x => x.Email == email);
            user.Address = mapper.Map<AddressDTO, Address>(addressDTO);
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok(mapper.Map<Address, AddressDTO>(user.Address));

            }
            return BadRequest("problem while updating");
        }
        [HttpGet]
        public async Task<ActionResult<bool>>checkEmailExisit(string email)
        => Ok(await userManager.FindByEmailAsync(email));
    }
}
