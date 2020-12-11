using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using PerboyreApp.Interfaces;
using PerboyreApp.Models;
using PerboyreApp.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Navigation.Xaml;
using Prism.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Dentista = PerboyreApp.Models.Dentista;

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


        private bool _inicializa;

        public bool inicializa
        {
            get { return _inicializa; }
            set
            {
                SetProperty(ref _inicializa, value);

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

        private bool mostra_mensage;

        public bool Mostramensagem
        {
            get { return mostra_mensage; }
            set
            {
                SetProperty(ref mostra_mensage, value);

            }
        }


        private string _nomeProfissional;

        public string nomeProfissional
        {
            get { return _nomeProfissional; }
            set
            {
                SetProperty(ref _nomeProfissional, value);

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

        private ICommand _voltar;
        public List<paciente> Lista;
        public ObservableCollection<paciente> Lista_filtrada;

        private string _DentistaFilter = "";

        public string DentistaFilter
        {
            get { return _DentistaFilter; }
            set
            {
                SetProperty(ref _DentistaFilter, value);
                //Filtro();
                SearchPacientes();
            }
        }

        Task taskThro;
        private async void SearchPacientes()
        {
            if (taskThro is null || taskThro.IsCompleted)
            {
                taskThro = Task.Run(async () =>
                {
                    await Task.Delay(500);
                    GetPacientes();
                });
            }
        }

        private Int32 _pacientecont = 0;

        public Int32 pacientecont
        {
            get { return _pacientecont; }
            set
            {
                SetProperty(ref _pacientecont, value);
                
            }
        }
        private paciente _Selection;
        public paciente Selection
        {
            get { return _Selection; }
            set
            {
                SetProperty(ref _Selection, value);

                Navega();
            }
        }

       /* private void Filtro()
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
        }*/


        public PacientesViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IApiService ApiService) : base(navigationService, pageDialogService)
        {

            apiService = ApiService;
            pacs = new ObservableCollection<paciente>();
            Lista = new List<paciente>();
            Lista_filtrada = new ObservableCollection<paciente>();
            inicializa = false;
            mostra = false;
            //GetPacientes(1204);

        }
        private async void Navega()
        {

            if (Selection != null)
            {
                // await PageDialogService.DisplayAlertAsync("app", Selection.nome, "Ok");
                var navigationParams = new Prism.Navigation.NavigationParameters();
                navigationParams.Add("paciente", Selection);
                //var mainPage = $"{nameof(NavigationPage)}/{nameof(Exames)}";

                await NavigationService.NavigateAsync("Exames", navigationParams);
                Selection = null;
            }

        }

        private void GetPacientes()
        {
            try
            {
                if (string.IsNullOrEmpty(DentistaFilter))
                {
                    pacs = new ObservableCollection<paciente>(Lista);
                    pacientecont = pacs.Count;
                }
                else if (DentistaFilter.Trim().Length > 0)
                {
                    Lista_filtrada = new ObservableCollection<paciente>(Lista.Where(x => x.nome.ToUpper().Contains(DentistaFilter.ToUpper())));
                    if (Lista_filtrada.Count == 0)
                    {
                        mostra = true;
                    }
                    pacs = Lista_filtrada;
                    pacientecont = Lista_filtrada.Count;
                }

            }
            catch (Exception ex)
            {
                exibeErro(ex.Message.ToString());
            }finally
            {
                taskThro?.Dispose();
                taskThro = null;
            }
        }

        private async Task GetPacientesAsync(long id)
        {


            isVisible = true;
            IsRunning = true;
            Mostra = true;
            Mostramensagem = false;
            //testa a conexao 

            try
            {
                if(InternetConnectivity())
                {
                    if (Lista!=null)
                    {
                        if (Lista.Any())
                            Lista.Clear();
                    }
                    Lista = await apiService.getPacientes(id);
                    if (Lista != null)
                    {
                        if (Lista.Count == 0)
                        {
                            pacientecont = 0;
                            Mostra = false;
                            Mostramensagem = true;
                            Mensagem = "Não há pacientes para o periodo";
                            //await PageDialogService.DisplayAlertAsync("app", "Dentista sem pacientes para o periodo", "OK");
                            IsRunning = false;
                            isVisible = false;
                            // await NavigationService.GoBackAsync();

                            //return;
                        }
                        else
                        {
                            pacs = new ObservableCollection<paciente>(Lista);
                            pacientecont = pacs.Count;
                        }
                        
                    }
                    else
                    {
                        await exibeErro("Dispositivo não está conectado com a internet");
                    }
                }
            }
            catch(Exception ex)
            {
                await exibeErro(ex.Message.ToString());
            }
            
            IsRunning = false;
            isVisible = false;
        }

       /* private async Task GetPacientesAsync(long id)
        {
            

            isVisible = true;
            IsRunning = true;
            Mostra = true;
            Mostramensagem = false;
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
                pacientecont = 0;
                Mostra = false;
                Mostramensagem = true;
                Mensagem = "Não há pacientes para o periodo";
                //await PageDialogService.DisplayAlertAsync("app", "Dentista sem pacientes para o periodo", "OK");
                IsRunning = false;
                isVisible = false;
               // await NavigationService.GoBackAsync();

                //return;
            }
            pacs = new ObservableCollection<paciente>(Lista);
            pacientecont = pacs.Count;
           

            IsRunning = false;
            isVisible = false;
        }*/

        private async Task InitializeAsync(long id)
        {
            try
            {
                                 // GetPacientes(_dentista.Id);
                    await GetPacientesAsync(id);
                    inicializa = true;
                                
            }
            catch(Exception ex)
            {
                await exibeErro(ex.Message.ToString());
            }
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            //  throw new NotImplementedException();
        }

        public override  void OnNavigatedTo(INavigationParameters parameters)
        {
            //throw new NotImplementedException();
            if (_dentista == null)
            {
                _dentista = (Dentista)parameters["paciente"];
                nomeProfissional = _dentista.nome;
                Task.Run(() => InitializeAsync(_dentista.Id)).ConfigureAwait(false);
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

                   /* var navigationParams = new NavigationParameters();
                    navigationParams.Add("paciente", item);
                    // _navigationService.NavigateAsync("Imagens", navigationParams);
                    NavigationService.NavigateAsync("ExamesTab", navigationParams);*/
                });

            }
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
    }
}
