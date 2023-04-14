using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDictionary.Api.Application.Features.Queries.GetEntries
{
    public class GetEntriesQueryHandler : IRequestHandler<GetEntriesQuery, List<GetEntriesViewModel>>
    {
        private readonly IEntryRepository entryRepository;
        private readonly IMapper mapper;

        public GetEntriesQueryHandler(IEntryRepository entryRepository, IMapper mapper)
        {
            this.entryRepository = entryRepository;
            this.mapper = mapper;
        }

        public async Task<List<GetEntriesViewModel>> Handle(GetEntriesQuery request, CancellationToken cancellationToken)
        {
            var query = entryRepository.AsQuaryable();

            if (request.TodaysEntries)
            {
                query = query.Where(i => i.CreateDate >= DateTime.Now.Date).Where(i => i.CreateDate <= DateTime.Now.AddDays(1).Date);
            }
            else
            {
                query = query.Where(x => x.Category == request.Category).OrderByDescending(y => y.CreateDate).Include(i => i.EntryComments);

            }
            //TODO
            //Category linq process
            //query = query.Where(x=>x.Category==request.Category).OrderBy(y=>y.CreateDate).Include(i => i.EntryComments).OrderBy(i => Guid.NewGuid()).Take(request.Count);

            return await query.ProjectTo<GetEntriesViewModel>(mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }
    }
}
