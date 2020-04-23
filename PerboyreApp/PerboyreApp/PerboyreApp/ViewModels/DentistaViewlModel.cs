using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using PerboyreApp.Interfaces;
using PerboyreApp.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PerboyreApp.ViewModels
{
    public class DentistaViewModel : ViewModelBase ,IInitialize
    {
        IApiService apiService;

        private bool isRunning;

        private bool _primeiro = true;

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

        private ObservableCollection<Dentista> _dentistas;
        public ObservableCollection<Dentista> dentistas
        {
            get { return _dentistas; }
            set
            {
                SetProperty(ref _dentistas, value);

            }
        }


        List<Dentista> Lista;
        private string _DentistaFilter = "";

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
        public DentistaViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IApiService ApiService) : base(navigationService, pageDialogService)
        {

            apiService = ApiService;
            Lista = new List<Dentista>();

            
            dentistas = new ObservableCollection<Dentista>();
            isVisible2 = true;
        }


        private async void GetDentistas()
        {
           
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                // GetDentistas();
            }
            else
            {
                await PageDialogService.DisplayAlertAsync("app", "Por favor Verifique sua conexao!", "Ok");
                IsRunning = false;
                isVisible = false;
                await NavigationService.GoBackAsync();
                return;

            }

            if (_primeiro)
            {

                await GetDentistasasync();
               
            }
            if (Lista == null)
            {
                await PageDialogService.DisplayAlertAsync("app", "lista nula", "Ok");
                return;
            }
            else if (Lista.Any())
            {
                if (string.IsNullOrEmpty(DentistaFilter))
                {
                    dentistas = new ObservableCollection<Dentista>(Lista);
                }
                else if (DentistaFilter.Trim().Length > 0)
                {
                    // dentistas.Clear();
                    dentistas = new ObservableCollection<Dentista>(Lista.Where(x => x.nome.ToUpper().Contains(DentistaFilter.ToUpper())));
                    // _dentistas.Where(x => x.nome.ToUpper().Contains(DentistaFilter.ToUpper()));
                    /* foreach (var item in Lista.Where(x => x.nome.ToUpper().Contains(DentistaFilter.ToUpper())))
                     {
                         dentistas.Add(new Dentista()
                         {
                             Id = item.Id,
                             nome = item.nome,
                             Email = item.Email


                         });
                     }*/
                }
                
            }
            else
            {
                //era assim 
                // antigo sem assincrono Lista = await apiService.getDentistas();
                await GetDentistasasync();
            }
           

        }

        private async Task GetDentistasasync()
        {
            isVisible = true;
            IsRunning = true;
            try
            {

                if (Lista != null)
                {
                    if (Lista.Any())
                        Lista.Clear();
                }
                Lista = await apiService.getDentistas();
                _primeiro = false;
            }
            catch (Exception ex)
            {
                await PageDialogService.DisplayAlertAsync("app", ex.ToString(), "Ok");
                return;
            }
            IsRunning = false;
            isVisible = false;
            isVisible2 = false;

        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            // throw new NotImplementedException();
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            //  throw new NotImplementedException();
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            //  throw new NotImplementedException();
        }

        public void Initialize(INavigationParameters parameters)
        {
            
        }

        /* public DelegateCommand<Dentista> NavigateCommand
         {
             get
             {
                 return new DelegateCommand<Dentista>((item) =>
                 {

                     var navigationParams = new NavigationParameters();
                     navigationParams.Add("paciente", item);
                   //  NavigationService.NavigateAsync("ExamesPage", navigationParams);
                 });

             }
         }*/



     private async void Navega()
     {
            if (Selection != null)
            {
                //await PageDialogService.DisplayAlertAsync("app", Selection.nome, "Ok");
                var navigationParams = new NavigationParameters();
                navigationParams.Add("paciente", Selection);
                await  NavigationService.NavigateAsync("Exames", navigationParams);
            }

        }


        
        public ICommand RefreshCommand
        {
            get
            {
                return _refresh ?? (_refresh = new Command(objeto =>
                {

                   
                    _primeiro = true;
                    GetDentistas();
                    

                }));
            }
        }
    }
}

