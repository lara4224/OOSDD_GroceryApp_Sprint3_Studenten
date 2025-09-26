using Grocery.Core.Helpers;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using Grocery.Core.Exceptions;

namespace Grocery.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IClientService _clientService;
        public AuthService(IClientService clientService)
        {
            _clientService = clientService;
        }
        public Client? Login(string email, string password)
        {
            Client? client = _clientService.Get(email);
            if (client == null) return null;
            if (PasswordHelper.VerifyPassword(password, client.Password)) return client;
            return null;
        }

        public Client Register(string username, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password)) throw new ArgumentException();

            if (_clientService.Get(email.Trim()) != null) throw new UsedEmailException();

            if (!EmailHelper.ValidateEmail(email)) throw new InvalidEmailException();

            if (!PasswordHelper.ValidatePasswordComplexity(password)) throw new InvalidPasswordException();

            Client c = _clientService.Create(email, PasswordHelper.HashPassword(password), username);

            return c;
        }
    }
}
