using System;
using PerboyreApp.Interfaces;
using Prism.Navigation;
using Prism.Services;

namespace PerboyreApp.ViewModels
{
    public class LocalizacaoViewModel :ViewModelBase
    {
        public LocalizacaoViewModel(INavigationService navigationService, IPageDialogService pageDialogService,IApiService ApiService) : base(navigationService, pageDialogService)
        {

        }
    }
}
