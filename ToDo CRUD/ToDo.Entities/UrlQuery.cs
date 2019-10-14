using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.Entities
{
    /// <summary>
    /// url query string
    /// </summary>
    public class UrlQuery
    {
        /// <summary>
        /// max page size
        /// </summary>
        private const int maxPageSize = 10;

        /// <summary>
        /// page number
        /// </summary>
        public long? PageNumber { get; set; }

        /// <summary>
        /// page size private
        /// </summary>

        private int _pageSize;

        /// <summary>
        /// page size public
        /// </summary>
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value < maxPageSize) ? value : maxPageSize; }
        }
    }
}
