namespace Catalog.Business.Models.Queries
{
    public class PageQueryEntity
    {
        private const int DEFAULT_PAGE_NUMBER = 1;
        private const int DEFAULT_PAGE_SIZE = 5;

        /// <summary>
        /// A page number having a numeric value of 1 or greater
        /// </summary>
        public int Page { get; set; } = DEFAULT_PAGE_NUMBER;

        /// <summary>
        /// A page size having a numeric value of 1 or greater.static Represents the number of tracks returned per page.
        /// </summary>
        public int Limit { get; set; } = DEFAULT_PAGE_SIZE;
    }
}
