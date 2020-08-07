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

        private string _photo;

        public string Photo
        {
            get { return _photo; }
            set
            {
                SetProperty(ref _photo, value);
            }
        }
        public PerfilViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IApiService ApiService) : base(navigationService, pageDialogService)
        {
            Photo = App.usuariologado.ImagePath;
        }

        public ICommand LogoutCommand
        {
            get
            {
                return _logout ?? (_logout = new Command(async  objeto =>
                {
                    

                    //await PageDialogService.DisplayAlertAsync("app", "Sem conexao!", "Ok");
                    Preferences.Clear();
                    App.usuariologado = null;
                    Page nova = navegacaoAux.GetMainPage();
                    App.Current.MainPage =nova ;

                }));
            }
        }
    }
}
