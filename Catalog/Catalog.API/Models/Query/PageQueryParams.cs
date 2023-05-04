using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Models.Query
{
    public class PageQueryParams
    {
        /// <summary>
        /// A page number having a numeric value of 1 or greater
        /// </summary>
        [FromQuery(Name = "page")]
        public int Page { get; init; }
    }
}
