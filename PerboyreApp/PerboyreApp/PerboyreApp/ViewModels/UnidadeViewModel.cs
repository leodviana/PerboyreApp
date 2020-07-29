using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using PerboyreApp.Interfaces;
using PerboyreApp.Models;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace PerboyreApp.ViewModels
{
    public class UnidadeViewModel:ViewModelBase
    {
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

        private bool _mostra;

        public bool mostra
        {
            get { return _mostra; }
            set
            {
                SetProperty(ref _mostra, value);

            }
        }


        private Dentista _Selection;
        public Dentista Selection
        {
            get { return _Selection; }
            set
            {
                SetProperty(ref _Selection, value);

                Navega();
            }
        }

        private ICommand _refresh;
        private bool isRunning2;

        //active indicator do rodape 
        public bool IsRunning2
        {
            get { return isRunning2; }
            set
            {
                SetProperty(ref isRunning2, value);

            }
        }
        private bool _isVisible2;
        public bool isVisible2
        {
            get { return _isVisible2; }
            set
            {
                SetProperty(ref _isVisible2, value);

            }
        }

        private ObservableCollection<Unidade> _dentistas;
        public ObservableCollection<Unidade> dentistas
        {
            get { return _dentistas; }
            set
            {
                SetProperty(ref _dentistas, value);

            }
        }


        List<Unidade> Lista;
        ObservableCollection<Unidade> Lista_filtrada;
        private string _DentistaFilter;

        public string DentistaFilter
        {
            get { return _DentistaFilter; }
            set
            {
                SetProperty(ref _DentistaFilter, value);

                GetDentistas();
            }
        }

        /* private void Filtro()
         {
            GetDentistas();
         }*/


        public UnidadeViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IApiService ApiService) : base(navigationService, pageDialogService)
        {

            apiService = ApiService;
            Lista = new List<Unidade>();
            Lista_filtrada = new ObservableCollection<Unidade>();
            dentistas = new ObservableCollection<Unidade>();
            isVisible2 = true;
            mostra = false;
            inicializa = false;

            //PageDialogService.DisplayAlertAsync("app", "Sem conexao!", "Ok");
        }


        private void GetDentistas()
        {
            try
            {

                if (string.IsNullOrEmpty(DentistaFilter))
                {
                    dentistas = new ObservableCollection<Unidade>(Lista);
                }
                else if (DentistaFilter.Trim().Length > 0)
                {

                    Lista_filtrada = new ObservableCollection<Unidade>(Lista.Where(x => x.Descricao.ToUpper().Contains(DentistaFilter.ToUpper())));
                    if (Lista_filtrada.Count == 0)
                        mostra = true;
                    dentistas = Lista_filtrada;

                    //dentistas = new ObservableCollection<Dentista>(Lista.Where(x => x.nome.ToUpper().Contains(DentistaFilter.ToUpper())));
                }
            }
            catch (Exception ex)
            {

            }



        }

        private async Task GetDentistasasync()
        {
            isVisible = true;
            IsRunning = true;


            try
            {
                if (InternetConnectivity())
                {
                    if (Lista != null)
                    {
                        if (Lista.Any())
                            Lista.Clear();
                    }

                    Lista = await apiService.getUnidades();
                    if (Lista != null)
                    {
                        dentistas = new ObservableCollection<Unidade>(Lista);
                    }
                    else
                    {
                        dentistas = null;
                        await exibeErro("Dispositivo não está conectado a internet!");
                    }


                }
                else
                {
                    await exibeErro("Dispositivo não está conectado a internet!");

                    mostra = true;

                    // await PageDialogService.DisplayAlertAsync("app", "Sem conexao!", "Ok");


                }
            }
            catch (Exception ex)
            {
                await exibeErro(ex.Message.ToString());


            }
            IsRunning = false;
            isVisible = false;
            isVisible2 = false;

        }




        /*private async void Initialize(INavigationParameters parameters)
        {
            
                // await PageDialogService.DisplayAlertAsync("app", "Sem conexao!", "Ok");

                await InitializeAsync();
            

        }*/

        private async Task InitializeAsync()
        {
            //await PageDialogService.DisplayAlertAsync("app", "Sem conexao!", "Ok");
            try
            {
                await GetDentistasasync();
                inicializa = true;

            }
            catch (Exception ex)
            {
                await PageDialogService.DisplayAlertAsync("app", ex.ToString(), "Ok");
                return;
            }
        }

        private async void Navega()
        {

            if (Selection != null)
            {
                // await PageDialogService.DisplayAlertAsync("app", Selection.nome, "Ok");
                var navigationParams = new NavigationParameters();
                navigationParams.Add("paciente", Selection);
                await NavigationService.NavigateAsync("PacientesPage", navigationParams);
                Selection = null;
            }

        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            var navigationMode = parameters.GetNavigationMode();

            if (navigationMode != NavigationMode.Back)

                await InitializeAsync();
        }

        public override async void OnNavigatingTo(INavigationParameters parameters)
        {

            // await InitializeAsync();
        }


        public ICommand RefreshCommand
        {
            get
            {
                return _refresh ?? (_refresh = new Command(async objeto =>
                {

                    if (inicializa)
                    {
                        await GetDentistasasync();
                    }
                    inicializa = true;



                }));
            }
        }
    }

}

