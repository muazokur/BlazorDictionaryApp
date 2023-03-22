﻿using System;
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
        public const string CreateEntryCommentFavQueueName= "CreateEntryCommentFavQueue";


    }
}
