using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using PerboyreApp.Interfaces;
using PerboyreApp.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Stormlion.PhotoBrowser;
using Xamarin.Forms;

namespace PerboyreApp.ViewModels
{
    public class TesteImagemViewModel : ViewModelBase, IInitialize
    {

        

        private string _url;

        public string Url
        {
            get { return _url; }
            set
            {
                SetProperty(ref _url, value);

            }
        }

        public TesteImagemViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IApiService ApiService) : base(navigationService, pageDialogService)
        {
            Url = "";
        }
        public void Initialize(INavigationParameters parameters)
        {
             var nomeSelecionado = (ArqImagens)parameters["imagem"];
            Url = nomeSelecionado.nome_arquivo_completo;


            
        }

       
        
        
        public  void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public  void OnNavigatedTo(NavigationParameters parameters)
        {

        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            //if (CrossConnectivity.Current.IsConnected)
            // {

            /*}
            else
            {
                _dialogService.DisplayAlertAsync("app", "Sem conexao!", "Ok");
                IsRunning = false;
                isVisible = false;
                _navigationService.GoBackAsync();
                return;

            }*/

        }
    }
}
