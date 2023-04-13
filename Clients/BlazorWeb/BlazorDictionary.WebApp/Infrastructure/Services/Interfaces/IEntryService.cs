using BlazorDictionary.Common.Models.Pages;
using BlazorDictionary.Common.Models.Queries;
using BlazorDictionary.Common.Models.RequestModels;

namespace BlazorDictionary.WebApp.Infrastructure.Services.Interfaces
{
    public interface IEntryService
    {
        Task<Guid> CreateEntry(CreateEntryCommand command);
        Task<Guid> CreateEntryComment(CreateEntryCommentCommand command);
        Task<List<GetEntriesViewModel>> GetEntries(string category=null);
        Task<PagedViewModel<GetEntryCommentsViewModel>> GetEntryComments(Guid entryId, int page, int pageSize);
        Task<GetEntryDetailViewModel> GetEntryDetail(Guid entryId);
        Task<PagedViewModel<GetEntryDetailViewModel>> GetMainPageEntries(int page, int pageSize);
        Task<PagedViewModel<GetEntryDetailViewModel>> GetProfilPageEntries(int page, int pageSize, string userName = null);
        Task<List<SearchEntryViewModel>> SearchBySubject(string searchText);
    }
}