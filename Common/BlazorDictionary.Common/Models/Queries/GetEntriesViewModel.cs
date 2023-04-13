﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDictionary.Common.Models.Queries
{
    public class GetEntriesViewModel
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public int CommentCount { get; set; }
    }
}
