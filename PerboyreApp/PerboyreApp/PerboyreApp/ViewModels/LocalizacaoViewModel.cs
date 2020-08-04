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
    public class LocalizacaoViewModel : ViewModelBase

    {
        Unidade unidade;
        public LocalizacaoViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IApiService ApiService) : base(navigationService, pageDialogService)
        {

        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {

            unidade = (Unidade)parameters["unidade"];
           

            await navigatetoBuilding(unidade);

             
        }

        private async Task navigatetoBuilding(Unidade unidade)
        {
           // var location = new Location(-3.748845, -38.505996);
           
        }
    }
       
}

