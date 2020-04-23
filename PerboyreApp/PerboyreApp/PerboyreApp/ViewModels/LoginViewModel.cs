using System;
using PerboyreApp.Interfaces;
using Prism.Navigation;
using Prism.Services;

namespace PerboyreApp.ViewModels
{
    public class LoginViewModel :ViewModelBase
    {
        public LoginViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IApiService ApiService) : base(navigationService, pageDialogService)
        {
        }
    }
}
