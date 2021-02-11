
using Newtonsoft.Json;
using PerboyreApp.Interfaces;
using PerboyreApp.Services;
using PerboyreApp.ViewModels;
using PerboyreApp.Views;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PerboyreApp.Models;

using PerboyreApp.Navegacao;
using Prism.Plugin.Popups;
using static Xamarin.Essentials.Permissions;
using Acr.UserDialogs;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace PerboyreApp
{
    public partial class App : PrismApplication
    {
        public static Models.Dentista usuariologado { get; set; }
        public App() : base(null) { }
        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            string usuario_logado = Preferences.Get("dentistaserializado", "");
            App.usuariologado = JsonConvert.DeserializeObject<Models.Dentista>(usuario_logado);


            if (App.usuariologado == null)
            {

                await this.NavigationService.NavigateAsync("LoginPage");
            }
            else

            {
                if (usuariologado.ImagePath.Equals(""))
                {
                    usuariologado.ImagePath = "perfil";
                }
               //  var status  = await ChecapermisaoService.checa_permissao( new Permissions.StorageWrite());
               // var testa2 = await ChecapermisaoService.checa_permissao(new Permissions.LocationWhenInUse());
                if (usuariologado.Id == 999999999)
                {
                   
                    usuariologado.tipo = "Administrador";
                    // $"{nameof(NavigationPage)}/{nameof(MainPage)}"
                    var mainPage = $"{nameof(NavigationPage)}/{nameof(MainPage2)}";
                    await NavigationService.NavigateAsync(mainPage);
                   // await this.NavigationService.NavigateAsync("/MasterPage/NavigationPage/DentistaPage");
                }
                else
                {
                    App.usuariologado.tipo = "Dentista";
                    var navigationParams = new NavigationParameters();
                    navigationParams.Add("paciente", App.usuariologado);


                    var mainPage = $"{nameof(NavigationPage)}/{nameof(MainPage2)}";
                    await NavigationService.NavigateAsync(mainPage);
                    
                }
            }
           // var mainPage = $"{nameof(NavigationPage)}/{nameof(MainPage2)}";
           // await NavigationService.NavigateAsync(mainPage);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
           // containerRegistry.RegisterPopupNavigationService();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterInstance(UserDialogs.Instance);
            //containerRegistry.RegisterForNavigation<CustomNavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage2,MainPage2ViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage,LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<DentistaPage, Dentista2ViewModel>();
            containerRegistry.RegisterForNavigation<Localizacao,LocalizacaoViewModel>();
            containerRegistry.RegisterForNavigation<Perfil, PerfilViewModel>();
            containerRegistry.RegisterForNavigation<Exames, ExamesViewModel>();
            containerRegistry.RegisterForNavigation<PacientesPage, PacientesViewModel>();
            containerRegistry.RegisterForNavigation<PopupMensagemPage,PopMensagemViewModel>();
            containerRegistry.RegisterForNavigation<imagensPage, ImagensViewModel>();
            containerRegistry.RegisterForNavigation<PdfPage, pdfViewModel>();
            containerRegistry.RegisterForNavigation<Unidades, UnidadeViewModel>();

            containerRegistry.RegisterForNavigation<TesteImagem, TesteImagemViewModel>();
            containerRegistry.RegisterSingleton<IApiService, ApiService>();
        }

        protected override void OnStart()
        {
            AppCenter.Start("ios=e86fd80a-d526-4fa1-9a87-d299aa1c8318;" +

                  "android={Your Android App secret here}",
                  typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
