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
    public class LocalizacaoViewModel : ViewModelBase

    {
        public LocalizacaoViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IApiService ApiService) : base(navigationService, pageDialogService)
        {

        }
    }
       
}

