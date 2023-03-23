using BlazorDictionary.Common.Models.Pages;
using BlazorDictionary.Common.Models.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDictionary.Api.Application.Features.Queries.GetUserEntries
{
    public class GetUserEntriesQuery : BasePagedQuery, IRequest<PagedViewModel<GetUserEntriesDetailViewModel>>
    {
        public Guid? UserId { get; set; } 
        public string UserName { get; set; }
        public GetUserEntriesQuery(int page=1, int pageSize=10, string userName = null, Guid? userId = null) : base(page, pageSize)
        {
            UserName = userName;
            UserId = userId;
        }
    }
}
