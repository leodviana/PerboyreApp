using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using PerboyreApp.Helpers;
using PerboyreApp.Interfaces;
using PerboyreApp.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Stormlion.PhotoBrowser;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PerboyreApp.ViewModels
{
    public class ImagensViewModel : ViewModelBase, IInitialize
    {
        public paciente _paciente;

        private bool isRunning;

        public DelegateCommand zoomCommand { get; set; }

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

        private Int32 _imagemcont = 0;

        public Int32 imagemcont
        {
            get { return _imagemcont; }
            set
            {
                SetProperty(ref _imagemcont, value);

            }
        }
        private ICommand _selecionarItem;
        private ICommand _selecionarItem2;
        private ICommand _selecionarItem3;
        private ICommand _compartilhar;
        //public List<ArqImagens> Lista;

        public string _titulo;
        public string titulo

        {
            get { return _titulo; }
            set
            {
                SetProperty(ref _titulo, value);

            }
        }

        private bool mostra_label;

        public bool Mostra_label
        {
            get { return mostra_label; }
            set
            {
                SetProperty(ref mostra_label, value);

            }
        }
        private bool mostra_listview;

        public bool Mostra_listview
        {
            get { return mostra_listview; }
            set
            {
                SetProperty(ref mostra_listview, value);

            }
        }
        IApiService apiService;

        private string mensagem;

        public string Mensagem
        {
            get { return mensagem; }
            set
            {
                SetProperty(ref mensagem, value);

            }
        }



        public ObservableCollection<ArqImagens> _imgs;
        public ObservableCollection<ArqImagens> imgs

        {
            get { return _imgs; }
            set
            {
                SetProperty(ref _imgs, value);

            }
        }
        public List<ArqImagens> Lista;


        public ImagensViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IApiService ApiService) : base(navigationService, pageDialogService)
        {

            apiService = ApiService;

            Lista = new List<ArqImagens>();
            imgs = new ObservableCollection<ArqImagens>();
            mostra_label = false;
            mostra_listview = true;
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        private async Task GetExames()
        {

            isVisible = true;
            IsRunning = true;
            Mostra_label = false;

            try
            {
                if (InternetConnectivity())
                {

                    if (Lista != null)
                    {
                        if (Lista.Any())
                        {
                            Lista.Clear();
                        }
                        /*IsRunning = false;
                        isVisible = false;
                        // await _navigationService.GoBackAsync();
                        Mostra_label = true;
                        Mostra_listview = false;
                        Mensagem = "Sem Imagens!";
                        return;*/
                    }
                    Lista = await apiService.getExames(_paciente);
                    if (Lista != null)
                    {
                        if (Lista.Count > 0)
                        {
                            imgs = new ObservableCollection<ArqImagens>(Lista);
                            imagemcont = Lista.Count;
                        }

                        else
                        {
                            Mostra_label = true;
                            Mostra_listview = false;
                            Mensagem = "Sem Imagens!";
                            imagemcont = 0;

                        }

                    }
                    else
                    {
                        // await _dialogService.DisplayAlertAsync("app", "Paciente sem exames", "OK");

                        IsRunning = false;
                        isVisible = false;
                        // await _navigationService.GoBackAsync();
                        Mostra_label = true;
                        Mostra_listview = false;
                        Mensagem = "Sem Imagens!";
                        imagemcont = 0;
                        return;
                    }

                    //pacientecont = imgs.Count;

                }
                else
                {
                    await exibeErro("Dispositivo não está conectado a internet");
                    Mensagem = "Sem Imagens!";
                    mostra_label = true;
                }



            }
            catch (Exception ex)
            {
                await exibeErro(ex.Message.ToString());
                return;
            }

            IsRunning = false;
            isVisible = false;
        }


        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            //if (CrossConnectivity.Current.IsConnected)
            // {

            /*}
            else
            {
                _dialogService.DisplayAlertAsync("app", "Sem conexao!", "Ok");
                IsRunning = false;
                isVisible = false;
                _navigationService.GoBackAsync();
                return;

            }*/

        }

        public ICommand Compartilhar
        {
            get
            {
                return _compartilhar ?? (_compartilhar = new Command<ArqImagens>(objeto =>
                {
                    ArqImagens teste = objeto;


                    string NomeSelecionado = teste.nome_arquivo_completo;

                }));
            }
        }

        public ICommand SelecionarItem3
        {
            get
            {
                return _selecionarItem3 ?? (_selecionarItem3 = new Command<ArqImagens>(objeto =>
                {
                    ArqImagens teste = objeto;

                    compartilhaImagemAsync(teste.nome_arquivo_completo);
                    // string NomeSelecionado = teste.nome_arquivo_completo;

                }));
            }
        }

        private async void compartilhaImagemAsync(string nome_arquivo_completo)
        {
            var permission = await PermissionHelper.CheckAndRequestPermission(new Permissions.StorageWrite());
            string caminho = Path.Combine(FileSystem.CacheDirectory, "teste.jpg");
            if (InternetConnectivity())
            {
                using (var Dialog = UserDialogs.Instance.Loading("Compartilhando...", null, null, true, MaskType.Clear))
                {
                    try
                    {
                        var retorno = await apiService.DownloadFileAsync(nome_arquivo_completo);

                        File.WriteAllBytes(caminho, retorno.ToArray());
                    }
                    catch (Exception ex)
                    {
                        UserDialogs.Instance.Toast("Erro ao baixar imagem para compartilhamento");
                    }
                }

                compartilhaImagem(caminho);
            }
        }


        /* private async void compartilhaImagemAsync(string nome_arquivo_completo)
        {

         if (conectionHelper.testaConexao())
         {

             paciente pac = new paciente();
             pac.photo = nome_arquivo_completo;
             byte[] imagem = null;
             using (var Dialog = UserDialogs.Instance.Loading("Compartilhando...", null, null, true, MaskType.Clear))
             {
                 imagem = await apiService.getExame(pac);
             }


             try
             {
                 if (imagem == null)
                 {
                     await PageDialogService.DisplayAlertAsync("app", "Imagem nao encontrada!", "Ok");
                 }
                 else
                 {

                     MemoryStream ms = new MemoryStream(imagem);
                     Xamarin.Forms.DependencyService.Get<IFileService>().SavePicture("ImageName.jpg", ms, "Download");
                     var filePath = Xamarin.Forms.DependencyService.Get<IFileStore>().GetFilePath();

                     compartilhaImagem(filePath);

                 }
             }
             catch (Exception ex)
             {

             }
         }
         else
         {
             await PageDialogService.DisplayAlertAsync("app", "Por favor Verifique sua conexao!", "Ok");
             IsRunning = false;
             isVisible = false;

             return;
         }*/
        //}

        private void compartilhaImagem(string file)
        {

            Share.RequestAsync(new ShareFileRequest()
            {
                Title = "Exames do Paciente",

                File = new ShareFile(file)
            });
        }

        public ICommand SelecionarItem2
        {
            get
            {
                return _selecionarItem2 ?? (_selecionarItem2 = new Command<ArqImagens>(objeto =>
               {
                   ArqImagens teste = objeto;

                   showZoom(teste);

               }));
            }
        }


        public ICommand SelecionarItem
        {
            get
            {
                return _selecionarItem ?? (_selecionarItem = new Command<ArqImagens>(objeto =>
                {
                    ArqImagens teste = objeto;

                    string NomeSelecionado = teste.nome_arquivo_completo;
                    showZoom(teste);

                }));
            }
        }

        private void showZoom(ArqImagens nomeSelecionado)
        {
            try
            {


                new PhotoBrowser
                {

                    Photos = new List<Photo>
                {
                    new Photo

                    {
                        URL = nomeSelecionado.nome_arquivo_completo,


                    },


                },
                    ActionButtonPressed = (index) =>
                    {
                        Debug.WriteLine($"Clicked {index}");
                        PhotoBrowser.Close();
                        //  _navigationService.GoBackAsync();
                    }

                }.Show();
            }
            catch (Exception ex)
            {

            }
        }



        public async void Initialize(INavigationParameters parameters)
        {

            _paciente = (paciente)parameters["paciente"];
            titulo = _paciente.nome;

            await GetExames();

        }
    }
}

