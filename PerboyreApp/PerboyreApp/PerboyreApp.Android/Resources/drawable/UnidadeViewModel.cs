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
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PerboyreApp.ViewModels
{
    public class UnidadeViewModel:ViewModelBase
    {
        IApiService apiService;

        private bool isRunning;

        //private ICommand _navegar;

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


        private Unidade _Selection;
        public Unidade Selection
        {
            get { return _Selection; }
            set
            {
                SetProperty(ref _Selection, value);

                Navega();
            }
        }

        private ICommand _refresh;
        private ICommand _navega;
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

            IsActiveChanged += HandleIsActiveTrue;
            IsActiveChanged += HandleIsActiveFalse;

            //PageDialogService.DisplayAlertAsync("app", "Sem conexao!", "Ok");
        }

        private void HandleIsActiveFalse(object sender, EventArgs e)
        {
            if (IsActive == true) return;

        }

        private async void HandleIsActiveTrue(object sender, EventArgs e)
        {
            if (IsActive == false) return;
            if (!inicializa)
                await GetDentistasasync();

        }

        public override void Destroy()
        {
            IsActiveChanged -= HandleIsActiveTrue;
            IsActiveChanged -= HandleIsActiveFalse;
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


       
        private async Task Navega()
        {

            if (Selection != null)
            {
                await pegaDirecao(Selection);
                Selection = null;
            }

        }


        private async Task pegaDirecao(Unidade unidade)
        {
            // await PageDialogService.DisplayAlertAsync("app", Selection.nome, "Ok");
            // var navigationParams = new NavigationParameters();
            // navigationParams.Add("unidade", Selection);
            //await NavigationService.NavigateAsync("Localizacao", navigationParams);
            var options = new MapLaunchOptions { NavigationMode = Xamarin.Essentials.NavigationMode.Driving };


            var adress = unidade.Endereco;
            var locations = await Geocoding.GetLocationsAsync(adress);
            var location = locations?.FirstOrDefault();
            await Map.OpenAsync(location, options);
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            var navigationMode = parameters.GetNavigationMode();

            if (navigationMode != Prism.Navigation.NavigationMode.Back)

                await InitializeAsync();
        }

        public override async void OnNavigatingTo(INavigationParameters parameters)
        {

            // await InitializeAsync();
        }


        public ICommand navegaCommand
        {
            get
            {
                return _navega ?? (_navega = new Command<Unidade>(async objeto =>
                {

                    
                    await pegaDirecao(objeto);

                }));
            }
        }
        private ICommand _selecionarItem3;
        public ICommand SelecionarItem3
        {
            get
            {
                return _selecionarItem3 ?? (_selecionarItem3 = new Command<Unidade>(objeto =>
                {


                    Share.RequestAsync(new ShareTextRequest()
                    {
                        Title = objeto.Descricao,

                        Text = objeto.Endereco + " Cep " + objeto.Cep + " Telefone " + objeto.Telefone + " - " + objeto.Telefone02
                    }); 
                    // string NomeSelecionado = teste.nome_arquivo_completo;

                }));
            }
        }

        private ICommand _whatsapp;
        public ICommand Comunicador
        {
            get
            {
                return _whatsapp ?? (_whatsapp = new Command<Unidade>( async objeto =>
                {


                    await abre_zap(objeto);
                    

                }));
            }
        }

        private async  Task abre_zap(Unidade objeto)
        {
            try

            {
                string  fone = "+55" + objeto.Telefone02.Replace("(","");
                 fone = fone.Replace(")","");
                var uriString = "whatsapp://send?phone=" + fone + " teste de mensagen";
                await Launcher.OpenAsync(uriString);
            }
            catch(Exception ex)
            {

            }
            
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

