using System;
using PerboyreApp.Interfaces;
using Prism.Navigation;
using Prism.Services;

namespace PerboyreApp.ViewModels
{
    public class MainPage2ViewModel : ViewModelBase
    {
        public MainPage2ViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IApiService ApiService) : base(navigationService, pageDialogService)
        {

        }
    }
}
