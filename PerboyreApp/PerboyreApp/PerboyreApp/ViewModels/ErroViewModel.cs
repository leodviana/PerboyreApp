using System;
using System.Threading.Tasks;
using System.Windows.Input;
using PerboyreApp.Interfaces;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace PerboyreApp.ViewModels
{
    public class ErroViewModel : ViewModelBase 
    {
        private ICommand _refresh;


       


        public ErroViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IApiService ApiService) : base(navigationService, pageDialogService)
        {
            
        }

        

        public ICommand RefreshCommand
        {
            get
            {
                return _refresh ?? (_refresh = new Command(async objeto =>
                {



                    await NavigationService.GoBackAsync();


                }));
            }
        }
    }
}
