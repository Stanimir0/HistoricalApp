using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoricalApp.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> SignInAsync(string email, string password);
        Task<string> SignUpAsync(string email, string password, string username);
        Task SignOutAsync();
        
    }

    // Services/Firebase/AuthServiceFirebase.cs
    public class AuthServiceFirebase : IAuthService
    {
        // Firebase SDK usage here...
        public Task<string> SignInAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task SignOutAsync()
        {
            throw new NotImplementedException();
        }

        public Task<string> SignUpAsync(string email, string password, string username)
        {
            throw new NotImplementedException();
        }
    }
}
