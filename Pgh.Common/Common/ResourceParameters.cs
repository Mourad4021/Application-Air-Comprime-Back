namespace Pgh.Common.Common
{
    public class ResourceParameters
    {
        public const int MaxPageSize = 1000;

        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string OrderBy { get; set; }

        public string SearchQuery { get; set; }
    }
}
