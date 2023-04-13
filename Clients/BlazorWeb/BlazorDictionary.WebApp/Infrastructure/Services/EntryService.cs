using BlazorDictionary.Common.Models.Pages;
using BlazorDictionary.Common.Models.Queries;
using BlazorDictionary.Common.Models.RequestModels;
using BlazorDictionary.WebApp.Infrastructure.Services.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;

namespace BlazorDictionary.WebApp.Infrastructure.Services
{
    public class EntryService : IEntryService
    {
        private readonly HttpClient client;

        public EntryService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<GetEntriesViewModel>> GetEntries(string category=null)
        {
            
            var result = await client.GetFromJsonAsync<List<GetEntriesViewModel>>
                ($"/api/Entry?TodaysEntries=false&count=30&Category={category}");

            return result;
        }

        public async Task<GetEntryDetailViewModel> GetEntryDetail(Guid entryId)
        {
            var result = await client.GetFromJsonAsync<GetEntryDetailViewModel>($"/api/Entry/{entryId}");

            return result;
        }

        public async Task<PagedViewModel<GetEntryDetailViewModel>> GetMainPageEntries(int page, int pageSize)
        {
            var result = await client.GetFromJsonAsync<PagedViewModel<GetEntryDetailViewModel>>
                ($"/api/Entry/MainPageEntries?page={page}&pageSize={pageSize}");

            return result;
        }

        public async Task<PagedViewModel<GetEntryDetailViewModel>> GetProfilPageEntries(int page, int pageSize, string userName = null)
        {
            var result = await client.GetFromJsonAsync<PagedViewModel<GetEntryDetailViewModel>>
                ($"/api/entry/userEntries?userName={userName}&page={page}&pageSize={pageSize}");

            return result;
        }

        public async Task<PagedViewModel<GetEntryCommentsViewModel>> GetEntryComments(Guid entryId, int page, int pageSize)
        {
            var result = await client.GetFromJsonAsync<PagedViewModel<GetEntryCommentsViewModel>>
                ($"/api/Entry/Comments/id?id={entryId}&page={page}&pageSize={pageSize}");
           
               return result;
        }

        public async Task<Guid> CreateEntry(CreateEntryCommand command)
        {
            var res = await client.PostAsJsonAsync("/api/Entry/CreateEntry", command);

            if (!res.IsSuccessStatusCode)
                return Guid.Empty;

            var guidStr = await res.Content.ReadAsStringAsync();

            return new Guid(guidStr.Trim('"'));
        }

        public async Task<Guid> CreateEntryComment(CreateEntryCommentCommand command)
        {
            var res = await client.PostAsJsonAsync("/api/Entry/CreateEntryComment", command);

            if (!res.IsSuccessStatusCode)
                return Guid.Empty;

            var guidStr = await res.Content.ReadAsStringAsync();

            return new Guid(guidStr.Trim('"'));
        }

        public async Task<List<SearchEntryViewModel>> SearchBySubject(string searchText)
        {
            var result = await client.GetFromJsonAsync<List<SearchEntryViewModel>>($"/api/Entry/Search?SearchText={searchText}");

            return result;
        }
    }
}
