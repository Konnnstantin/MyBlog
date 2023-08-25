using Microsoft.AspNetCore.Identity;
using MyBlog.Domains.Entity;
using MyBlog.Domains.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Service.Interfaces
{
    public interface IAccountService
    {
        Task<SignInResult> Login(LoginViewModel model);
        Task<IdentityResult> EditAccount(AccountEditViewModel model);
        Task<IdentityResult> Register(RegisterViewModel model);
        Task<AccountEditViewModel> EditAccount(Guid id);
        Task LogoutAccount();
        Task RemoveAccount(int id);
        Task<List<User>> GetAccounts();
    }
}
