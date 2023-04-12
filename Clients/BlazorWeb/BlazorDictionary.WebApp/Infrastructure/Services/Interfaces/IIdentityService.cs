using BlazorDictionary.Common.Models.RequestModels;

namespace BlazorDictionary.WebApp.Infrastructure.Services.Interfaces
{
    public interface IIdentityService
    {
        bool IsLoggedIn { get; }

        Guid GetUserId();
        string GetUserName();
        string GetUserToken();
        Task<bool> Login(LoginUserCommand command);
        Task<bool> Create(CreateUserCommand command);
        void Logout();
    }
}