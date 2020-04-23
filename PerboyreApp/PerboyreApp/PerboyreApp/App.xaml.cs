
using PerboyreApp.Interfaces;
using PerboyreApp.Services;
using PerboyreApp.ViewModels;
using PerboyreApp.Views;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PerboyreApp
{
    public partial class App : PrismApplication
    {
        public App() : base(null) { }
        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            var mainPage = $"{nameof(NavigationPage)}/{nameof(MainPage2)}";
            await NavigationService.NavigateAsync(mainPage);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage2,MainPage2ViewModel>();
            containerRegistry.RegisterForNavigation<Login,LoginViewModel>();
            containerRegistry.RegisterForNavigation<Dentista,DentistaViewModel>();
            containerRegistry.RegisterForNavigation<Localizacao,LocalizacaoViewModel>();
            containerRegistry.RegisterForNavigation<Perfil, PerfilViewModel>();
            containerRegistry.RegisterForNavigation<Exames, ExamesViewModel>();


            containerRegistry.RegisterSingleton<IApiService, ApiService>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
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
