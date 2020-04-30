using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PerboyreApp.Interfaces;
using PerboyreApp.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Essentials;

namespace PerboyreApp.ViewModels
{
    public class PacientesViewModel : ViewModelBase
    {

        private Dentista _dentista;
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
        private bool _isVisible;

        public bool isVisible
        {
            get { return _isVisible; }
            set
            {
                SetProperty(ref _isVisible, value);

            }
        }


        private bool mostra;

        public bool Mostra
        {
            get { return mostra; }
            set
            {
                SetProperty(ref mostra, value);

            }
        }

        private string mensagem;

        public string Mensagem
        {
            get { return mensagem; }
            set
            {
                SetProperty(ref mensagem, value);

            }
        }
        public ObservableCollection<paciente> _pacs;
        public ObservableCollection<paciente> pacs
        {
            get { return _pacs; }
            set
            {
                SetProperty(ref _pacs, value);

            }
        }

        public List<paciente> Lista;

        private string _DentistaFilter = "";

        public string DentistaFilter
        {
            get { return _DentistaFilter; }
            set
            {
                SetProperty(ref _DentistaFilter, value);
                Filtro();
            }
        }

        private void Filtro()
        {
            if (DentistaFilter.Trim().Length > 0)
            {
                pacs.Clear();
                foreach (var item in Lista.Where(x => x.nome.ToUpper().Contains(DentistaFilter.ToUpper())))
                {
                    pacs.Add(new paciente()
                    {
                        Id = item.Id,
                        nome = item.nome,
                        dt_atendimento = item.dt_atendimento,
                        dt_nascimento = item.dt_nascimento,
                        photo = item.photo


                    });
                }
            }
        }


        public PacientesViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IApiService ApiService) : base(navigationService, pageDialogService)
        {

            apiService = ApiService;
            pacs = new ObservableCollection<paciente>();
            Lista = new List<paciente>();
            //GetPacientes(1204);

        }

        private async void GetPacientes(long id)
        {

            isVisible = true;
            IsRunning = true;
            //testa a conexao 
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)

            {

            }
            else
            {
                await PageDialogService.DisplayAlertAsync("app", "Sem conexao!", "Ok");
                IsRunning = false;
                isVisible = false;
                await NavigationService.GoBackAsync();
                return;
            }

            //var apiSecurity = App.Current.Resources["URLAPI"].ToString();
            // if (CrossConnectivity.Current.IsConnected)
            // {

            Lista = await apiService.getPacientes(id);


            if (Lista == null)
            {
                await PageDialogService.DisplayAlertAsync("app", "lista nula", "OK");
                return;
            }

            if (Lista.Count == 0)
            {
                await PageDialogService.DisplayAlertAsync("app", "Dentista sem pacientes para o periodo", "OK");
                IsRunning = false;
                isVisible = false;
                await NavigationService.GoBackAsync();

                return;
            }
            pacs = new ObservableCollection<paciente>(Lista);
            /* foreach (var item in Lista)
             {
                 pacs.Add(new paciente()
                 {
                     Id = item.Id,
                     nome = item.nome,
                     dt_atendimento = item.dt_atendimento,
                     dt_nascimento = item.dt_nascimento,
                     photo = item.photo

                 });
             }*/

            IsRunning = false;
            isVisible = false;
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            //  throw new NotImplementedException();
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            //throw new NotImplementedException();
            if (_dentista == null)
            {
                _dentista = (Dentista)parameters["paciente"];

                GetPacientes(_dentista.Id);
            }
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            //if (CrossConnectivity.Current.IsConnected)
            // {

            /* }
            / else
             {
                 _dialogService.DisplayAlertAsync("app", "Sem conexao!", "Ok");
                 IsRunning = false;
                 isVisible = false;
                 _navigationService.GoBackAsync();
                 return;
             }*/

        }

        public DelegateCommand<paciente> NavigateCommand
        {
            get
            {
                return new DelegateCommand<paciente>((item) =>
                {

                    var navigationParams = new NavigationParameters();
                    navigationParams.Add("paciente", item);
                    // _navigationService.NavigateAsync("Imagens", navigationParams);
                    NavigationService.NavigateAsync("ExamesTab", navigationParams);
                });

            }
        }
    }
}
