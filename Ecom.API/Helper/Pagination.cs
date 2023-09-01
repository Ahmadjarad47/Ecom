namespace Ecom.API.Helper
{
    public class Pagination<T> where T : class
    {
        public Pagination(int pagenumber, int pagesize,int count, IReadOnlyList<T> data)
        {
            pageNumber = pagenumber;
            pageSize = pagesize;
           Count = count;
             Data=data;     
        }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data     { get; set; }
    }
}
