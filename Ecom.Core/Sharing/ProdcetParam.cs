namespace Ecom.Core.Sharing
{
    public class ProdcetParam
    {
        public int maxPageSize { get; set; } = 15;
        private int _pagesize=3;
        public int pageSize{
            get { return this._pagesize; }
            set { this._pagesize = value>maxPageSize?maxPageSize:value; }

            }
        public int pageNumber { get; set; } = 1;

        public string sort { get; set; }
        public int? categoryId { get; set; }
        private string _serach;

        public string serach
        {
            get { return _serach; }
            set { _serach = value.ToLower(); }
        }

    }
}
