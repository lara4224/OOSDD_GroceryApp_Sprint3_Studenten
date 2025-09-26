using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Grocery.Core.Interfaces.Services;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Models;
using Grocery.Core.Exceptions;
using Grocery.App.Views;


namespace Grocery.App.ViewModels
{
    public partial class RegisterViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;
        private readonly GlobalViewModel _global;

        [ObservableProperty]
        private string username = "Lara Bijsterbosch";

        [ObservableProperty]
        private string email = "user3@mail.com";

        [ObservableProperty]
        private string password = "user3";

        [ObservableProperty]
        private string errorMessage = "";

        public RegisterViewModel(IAuthService authService, GlobalViewModel global)
        {
            _authService = authService;
            _global = global;
        }

        [RelayCommand]
        private void Register()
        {
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(Username))
                ErrorMessage += "Vul alle verplichte velden in. ";

            if (string.IsNullOrWhiteSpace(Email))
                ErrorMessage += "Vul alle verplichte velden in. ";

            if (string.IsNullOrWhiteSpace(Password))
                ErrorMessage += "Vul alle verplichte velden in. ";

            if (ErrorMessage != "'')
            return;

            try
            {
                Client client = _authService.Register(Username, Email, Password);

                _global.Client = client;
                Application.Current.MainPage = new AppShell();
            }
            catch (UsedEmailException _)
            {
                ErrorMessage = "E-mailadres al in gebruik.";
            }
            catch (InvalidEmailException _)
            {
                ErrorMessage = "E-Mailadres is ongeldig.";
            }
            catch (InvalidPasswordException _)
            {
                ErrorMessage = "Wachtwoord moet minimaal 6 tekens bevatten, waaronder een hoofdletter een cijfer en speciaal teken."
            }
            catch (Exception _)
            {
                ErrorMessage = "Registratie niet geslaagd.";
            }
        }

    }
  }
 