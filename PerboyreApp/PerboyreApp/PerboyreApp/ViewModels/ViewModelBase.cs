using System;
using System.Threading.Tasks;
using Prism;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PerboyreApp.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible, IActiveAware

    {
        
        protected INavigationService NavigationService { get; set; }
        protected IPageDialogService PageDialogService { get; set; }

        protected bool HasInitialized { get; set; }

        public event EventHandler IsActiveChanged;

        string title = string.Empty;

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        string subtitle = string.Empty;

        public string Subtitle
        {
            get => subtitle;
            set => SetProperty(ref subtitle, value);
        }

        string icon = string.Empty;

        public string Icon
        {
            get => icon;
            set => SetProperty(ref icon, value);
        }

        bool isBusy;

        public bool IsBusy
        {
            get => isBusy;
            set
            {
                if (SetProperty(ref isBusy, value))
                    IsNotBusy = !isBusy;
            }
        }

        bool isNotBusy = true;

        public bool IsNotBusy
        {
            get => isNotBusy;
            set
            {
                if (SetProperty(ref isNotBusy, value))
                    IsBusy = !isNotBusy;
            }
        }

        bool canLoadMore = true;

        public bool CanLoadMore
        {
            get => canLoadMore;
            set => SetProperty(ref canLoadMore, value);
        }


        string header = string.Empty;

        public string Header
        {
            get => header;
            set => SetProperty(ref header, value);
        }

        string footer = string.Empty;

        public string Footer
        {
            get => footer;
            set => SetProperty(ref footer, value);
        }

        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value, RaiseIsActiveChanged);
        }
        public static bool InternetConnectivity()
        {
            
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                return true;
            }
            
            return false;
        }


        protected ViewModelBase(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            NavigationService = navigationService;
            PageDialogService = pageDialogService;

            this.Title = $"Default";
        }

        public  async Task exibeErro(string _mensagem)
        {
            var navigationParams = new NavigationParameters();
            navigationParams.Add("mensagem", _mensagem);
            await NavigationService.NavigateAsync("PopupMensagemPage", navigationParams, useModalNavigation: true);
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {

        }


        public virtual void Destroy()
        {

        }

        protected virtual void RaiseIsActiveChanged()
        {
            IsActiveChanged?.Invoke(this, EventArgs.Empty);
        }

        
    }
}
