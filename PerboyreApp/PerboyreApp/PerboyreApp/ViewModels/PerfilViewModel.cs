using System;
using System.Windows.Input;
using FFImageLoading.Cache;
using FFImageLoading.Forms;
using PerboyreApp.Helpers;
using PerboyreApp.Interfaces;
using PerboyreApp.Utils;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PerboyreApp.ViewModels
{
    public class PerfilViewModel :ViewModelBase
    {

        private ICommand _logout;

        public byte[] imageArray;
        private ICommand _abrircameraCommand;

        /*private string _photo;

        public string Photo
        {
            get { return _photo; }
            set
            {
                SetProperty(ref _photo, value);
            }
        }
        */
        private ImageSource _photo;

        public ImageSource Photo
        {
            get { return _photo; }
            set
            {
                SetProperty(ref _photo, value);
                CachedImage.InvalidateCache(_photo, CacheType.All, true);
            }
        }
        public PerfilViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IApiService ApiService) : base(navigationService, pageDialogService)
        {
            Photo = App.usuariologado.ImagePath;
        }

        public ICommand LogoutCommand
        {
            get
            {
                return _logout ?? (_logout = new Command(async  objeto =>
                {
                    

                    //await PageDialogService.DisplayAlertAsync("app", "Sem conexao!", "Ok");
                    Preferences.Clear();
                    App.usuariologado = null;
                    Page nova = navegacaoAux.GetMainPage();
                    App.Current.MainPage =nova ;

                }));
            }
        }


        public ICommand abrircameraCommand
        {
            get
            {
                return _abrircameraCommand ?? (_abrircameraCommand = new Command(objeto =>
                {

                    selecionartipodeImagem();
                }));
            }
        }

        private async void selecionartipodeImagem()
        {
            var action = await PageDialogService.DisplayActionSheetAsync("Selecione Imagem:", "Cancel", null, "Camera", "Galeria");
            if (action == "Camera")
            {
                abrircamera();
            }
            else if (action == "Galeria")
            {
                selecionargaleria();
            }
            else
            {
                Photo = null;
                return;

            }
        }
        private async void abrircamera()
        {

            // var action = await _dialogService.DisplayActionSheetAsync("" , "Cancel", null, "Tirar nova foto", "Galeria");

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await exibeErro("Sua Camera não esta Disponivel");
               // await _dialogService.DisplayAlertAsync("Painel Studio - Perboyre", "Sua Camera não esta Disponivel", "Ok");
                //DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
               // Name = _dentista.nome,
                AllowCropping = true,
                Directory = "Photos",
                SaveToAlbum = true,
                CompressionQuality = 75,
                CustomPhotoSize = 50,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 2000,
                DefaultCamera = CameraDevice.Front
            });
            // Plugin.Media.Abstractions.StoreCameraMediaOptions teste = new Plugin.Media.Abstractions.StoreCameraMediaOptions();

            if (file == null)
                return;

            // DisplayAlert("File Location", file.Path, "OK");


            Photo = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();

                if (file != null)
                {
                    imageArray = FilesHelper.ReadFully(file.GetStream());


                }
                file.Dispose();
                return stream;
            });

        }

        private async void selecionargaleria()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await exibeErro("Arquivo não Suportado"); //_dialogService.DisplayAlertAsync("Painel Studio - Perboyre", "Arquivo não Suportado", "Ok");
                //DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return;
            }
            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,

            });


            if (file == null)
                return;

            Photo = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                if (file != null)
                {
                    imageArray = FilesHelper.ReadFully(file.GetStream());

                }
                file.Dispose();
                return stream;
            });
        }

    }
}
