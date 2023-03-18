using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Api.Domain.Models;
using BlazorDictionary.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDictionary.Infrastructure.Persistence.Repositories
{
    internal class EntryCommentReporsitory : GenericRepository<EntryComment>, IEntryCommentRepository
    {
        public EntryCommentReporsitory(BlazorDictionaryContext dbContext) : base(dbContext)
        {
        }
    }
}
