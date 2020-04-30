using System;
using System.Windows.Input;
using PerboyreApp.Interfaces;
using PerboyreApp.Utils;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PerboyreApp.ViewModels
{
    public class PerfilViewModel :ViewModelBase
    {

        private ICommand _logout;

        public PerfilViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IApiService ApiService) : base(navigationService, pageDialogService)
        {
        }

        public ICommand LogoutCommand
        {
            get
            {
                return _logout ?? (_logout = new Command(objeto =>
                {


                    Preferences.Clear();
                    App.usuariologado = null;
                    Page nova = navegacaoAux.GetMainPage();
                    App.Current.MainPage =nova ;

                }));
            }
        }
    }
}
