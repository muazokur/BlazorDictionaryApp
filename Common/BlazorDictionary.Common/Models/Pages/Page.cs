using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDictionary.Common.Models.Pages
{
    public class Page
    {
        public Page() :this(0)
        {

        }
        public Page(int totolRowCount) : this(1, 10, totolRowCount)
        {

        }
        public Page(int pageSize, int totolRowCount) : this(1, pageSize, totolRowCount)
        {

        }
        public Page(int currentPage, int pageSize, int totolRowCount)
        {
            if (currentPage < 1)
                throw new ArgumentException("Invalid page number!");
            if (pageSize < 1)
                throw new ArgumentException("Invalid page size!");

            TotolRowCount = totolRowCount;
            CurrentPage = currentPage;
            PageSize = pageSize;
        }

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotolRowCount { get; set; }
        public int TotolPageCount => (int)Math.Ceiling((double)TotolRowCount / PageSize);
        public int Skip => (CurrentPage - 1) * PageSize;
    }
}
