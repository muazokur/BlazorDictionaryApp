using BlazorDictionary.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDictionary.Common.Models.Queries
{
    public class BaseFooterRateViewModel
    {
        public VoteType VoteType { get; set; }
    }

    public class BaseFooterFavoriteViewModel
    {
        public bool IsFavorited { get; set; }
        public int FavoriteCount { get; set; }
    }

    public class BaseFooterRateFavoriteViewModel : BaseFooterFavoriteViewModel
    {
        public VoteType VoteType { get; set; }
    }
}
