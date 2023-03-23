using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDictionary.Api.Application.Features.Queries.SearchBySucject
{
    public class SearchEntryQueryHandler : IRequestHandler<SearchEntryQuery, List<SearchEntryViewModel>>
    {
        private readonly IEntryRepository entryRepository;

        public SearchEntryQueryHandler(IEntryRepository entryRepository)
        {
            this.entryRepository = entryRepository;
        }

        public async Task<List<SearchEntryViewModel>> Handle(SearchEntryQuery request, CancellationToken cancellationToken)
        {
            //TODO validation, request.SearchText lenght should be checked

            #region optionalSearch
            entryRepository.Get(i => i.Subject.StartsWith(""));
            #endregion

            var result =entryRepository.Get(i=>EF.Functions.Like(i.Subject, $"{request.SearchText}%")).Select(i=>new SearchEntryViewModel()
            {
                Id=i.Id,
                Subject = i.Subject,
            });

            return await result.ToListAsync(cancellationToken);
        }
    }
}
