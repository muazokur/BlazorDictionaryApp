using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDictionary.Common
{
    public class DictionaryConstants
    {
        public const string RabbitMQHost = "localhost";
        public const string DefaultExchangeType= "direct";

        public const string UserExcangeName = "UserExchange";
        public const string UserEmailExcangedQueueName = "UserEmailExchangedQueue";

        public const string FavExcahangeName = "FavExcahangeName";
        public const string CreateEntryFavQueueName= "CreateEntryFavQueue";
        public const string CreateEntryCommentFavQueueName= "CreateEntryCommentFavQueue";

        public const string DeleteEntryFavQueueName= "DeleteEntryFavQueue";
        public const string DeleteEntryVoteQueueName = "DeleteEntrtVoteQueue";

        public const string CreateEntryCommentVoteQueueName = "CreateEntryCommentVoteQueue";
        public const string DeleteEntryCommentFavQueueName = "DeleteEntryCommentFavQueue";

        public const string DeleteEntryCommentVoteQueueName = "DeleteEntryCommentVoteQueue";

        public const string VoteExchangeName = "VoteExchange";
        public const string CreateEntryVoteQueueName = "CreateEntryVoteQueue";






    }
}
