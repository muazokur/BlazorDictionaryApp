using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDictionary.Common.Models.Pages
{
    public class PagedViewModel<T> where T : class
    {
        public PagedViewModel() : this(new List<T>(), new Page())
        {

        }
        public PagedViewModel(IList<T> results, Page pageINfo)
        {
            Results = results;
            PageInfo = pageINfo;
        }

        public IList<T> Results { get; set; }
        public Page PageInfo { get; set; }
    }
}
