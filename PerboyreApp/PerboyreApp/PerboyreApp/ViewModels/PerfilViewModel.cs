using System;
using PerboyreApp.Interfaces;
using Prism.Navigation;
using Prism.Services;

namespace PerboyreApp.ViewModels
{
    public class PerfilViewModel :ViewModelBase
    {
        public PerfilViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IApiService ApiService) : base(navigationService, pageDialogService)
        {
        }
    }
}
