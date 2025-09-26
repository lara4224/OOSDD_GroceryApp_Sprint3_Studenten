using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.App.Views;
using Grocery.Core.Exceptions;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


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

        public ICommand CancelCommand { get; }

        public RegisterViewModel(IAuthService authService, GlobalViewModel global)
        {
            _authService = authService;
            _global = global;
            CancelCommand = new RelayCommand(OnCancel);
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

            if (ErrorMessage != "")
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
                ErrorMessage = "Wachtwoord moet minimaal 6 tekens bevatten, waaronder een hoofdletter een cijfer en speciaal teken.";
            }
            catch (Exception _)
            {
                ErrorMessage = "Registratie niet geslaagd.";
            }
        }

        private async void OnCancel()
        {
            if (Application.Current.MainPage is NavigationPage navPage)
            {
                await navPage.PopAsync();
            }
            else if (Application.Current.MainPage?.Navigation != null)
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }

    }
  }
 