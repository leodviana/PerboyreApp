
using PerboyreApp.Interfaces;
using PerboyreApp.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace PerboyreApp.ViewModels
{
	public class LoginPacientePageViewModel : ViewModelBase
    {
        private ICommand _navegar;
        private ICommand _ForgotPasswordCommand;
       
        IApiService apiService;

        private bool isRunning;

        public bool IsRunning
        {
            get { return isRunning; }
            set
            {
                SetProperty(ref isRunning, value);

            }
        }

        private string _usuarioid;
        public string Usuarioid
        {
            get { return _usuarioid; }
            set
            {
                SetProperty(ref _usuarioid, value);

            }
        }
   
        private string _senha;
        public string Senha
        {
            get { return _senha; }
            set
            {
                SetProperty(ref _senha, value);

            }
        }
        public LoginPacientePageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IApiService ApiService) : base(navigationService, pageDialogService)
        {
            apiService = ApiService;
        }

        public ICommand ForgotPasswordCommand
        {
            get
            {
                return _ForgotPasswordCommand ?? (_ForgotPasswordCommand = new Command(objeto =>
                {
                    //_navigationService.NavigateAsync("/MasterPage/NavigationPage/DentistaPage");

                    NavigationService.GoBackAsync();
                }));
            }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
           
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
           
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
           
        }
    }
}
