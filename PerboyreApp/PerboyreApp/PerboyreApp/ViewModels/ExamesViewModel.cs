using System;
using System.Windows.Input;
using PerboyreApp.Interfaces;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace PerboyreApp.ViewModels
{
    public class ExamesViewModel : ViewModelBase, IInitialize
    {
        private ICommand _voltar;
        IApiService apiService;
        public ExamesViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IApiService ApiService) : base(navigationService, pageDialogService)
        {
        }
        public ICommand voltarCommand
        {
            get
            {
                return _voltar ?? (_voltar = new Command(objeto =>
                {


                    NavigationService.GoBackAsync();


                }));
            }
        }

        public void Initialize(INavigationParameters parameters)
        {
           
        }
    }
}
