using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
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
    public class pdfViewModel : ViewModelBase, IInitialize
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



        public ObservableCollection<ArqImagens> _pdf;
        public ObservableCollection<ArqImagens> pdf

        {
            get { return _pdf; }
            set
            {
                SetProperty(ref _pdf, value);

            }
        }
        public List<ArqImagens> Lista;


        public pdfViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IApiService ApiService) : base(navigationService, pageDialogService)
        {

            apiService = ApiService;

            Lista = new List<ArqImagens>();
            pdf = new ObservableCollection<ArqImagens>();
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
                    Lista = await apiService.getExamespdf(_paciente);
                    if (Lista != null)
                    {
                        if (Lista.Count > 0)
                        {
                            pdf = new ObservableCollection<ArqImagens>(Lista);
                            imagemcont = Lista.Count;
                        }

                        else
                        {
                            Mostra_label = true;
                            Mostra_listview = false;
                            Mensagem = "Sem Arquivos PDF!";
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
                        Mensagem = "Sem Arquivos PDF!";
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
                    //apaguei para saber se era utilizado 
                    /*   var ShareMessage = new Plugin.Share.Abstractions.ShareMessage
                       {

                           Text = "Exemplo de como compartilhar textos ou links em Aplicações Xamarin.Forms. / Example of how to share texts or links in Xamarin.Forms Applications.",
                           Title = "Share",
                           Url = "https://www.julianocustodio.com"

                       };

                       CrossShare.Current.Share(ShareMessage);
                      /* Share.RequestAsync(new ShareFileRequest
                       {
                           Title = Title,
                           File = new ShareFile(file)
                       });*/
                    //showZoom(NomeSelecionado);
                    /*var navigationParams = new NavigationParameters();
                    navigationParams.Add("imagem", NomeSelecionado);
                     _navigationService.NavigateAsync("ImagensDetalhes2", navigationParams);*/
                    //EscolaSelecionado = teste.Escola;*/
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
            var nome_arquivo = Path.GetFileName(nome_arquivo_completo);

            string caminho = Path.Combine(FileSystem.CacheDirectory, nome_arquivo);
            if (InternetConnectivity())
            {

                using (var Dialog = UserDialogs.Instance.Loading("Compartilhando...", null, null, true, MaskType.Clear))
                {

                    var retorno = await apiService.DownloadFileAsync(nome_arquivo_completo);


                    File.WriteAllBytes(caminho, retorno.ToArray());
                }


                compartilhaImagem(caminho);


            }
            else
            {
                await exibeErro("Dispositivo não está conectado a internet!");
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

                    string NomeSelecionado = teste.nome_arquivo_completo;
                    showZoom(teste);
                    /*var navigationParams = new NavigationParameters();
                    navigationParams.Add("imagem", NomeSelecionado);
                     _navigationService.NavigateAsync("ImagensDetalhes2", navigationParams);*/
                    //EscolaSelecionado = teste.Escola;
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
                    /*var navigationParams = new NavigationParameters();
                    navigationParams.Add("imagem", NomeSelecionado);
                     _navigationService.NavigateAsync("ImagensDetalhes2", navigationParams);*/
                    //EscolaSelecionado = teste.Escola;
                }));
            }
        }

        private void showZoom(ArqImagens nomeSelecionado)
        {
            
            try
            {

                List<Photo> _photo = new List<Photo>();

                // NavigationService.NavigateAsync("LoginPacientePage");
                // teste(nomeSelecionado);
                for (int i = 0; i < nomeSelecionado.lista_jpeg.Count(); i++)
                {
                    var foto = new Photo();

                    foto.URL = nomeSelecionado.lista_jpeg[i].ToString().Replace(" ", "%20");
                    //foto.URL = nomeSelecionado.lista_jpeg[i];
                    _photo.Add(foto);

                }
                // var retorno = await apiService.DownloadFileAsync(nome_arquivo_completo);
                new PhotoBrowser
                {
                    Photos = _photo.ToList(),



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


      /*  private async void showZoom(ArqImagens nomeSelecionado)
        {
            List<Photo> _photo = new List<Photo>();
            string caminho = Path.Combine(FileSystem.CacheDirectory, "teste.jpg");
            // NavigationService.NavigateAsync("LoginPacientePage");
            // teste(nomeSelecionado);
            _photo = await grava_local(nomeSelecionado);
            try
            {

            
            new PhotoBrowser
            {
                Photos = _photo.ToList(),



                ActionButtonPressed = (index) =>
                {
                    Debug.WriteLine($"Clicked {index}");
                    PhotoBrowser.Close();

                    //  _navigationService.GoBackAsync();
                }

            }.Show();
            }
            catch(Exception ex)
            {

            }

        }


        private async Task<List<Photo>> grava_local(ArqImagens nomeSelecionado)
        {
            string caminho = "";
            List<Photo> _photo = new List<Photo>();
            for (int i = 0; i < nomeSelecionado.lista_jpeg.Count(); i++)
            {
                caminho = Path.Combine(FileSystem.CacheDirectory, "teste" + i + ".jpg");
                var foto = new Photo();

                foto.URL = nomeSelecionado.lista_jpeg[i].ToString().Replace(" ", "%20");
                //foto.URL = nomeSelecionado.lista_jpeg[i];
                var retorno = await apiService.DownloadFileAsync(foto.URL);
                File.WriteAllBytes(caminho, retorno.ToArray());
                foto.Title = "teste" + i + ".jpg";
                foto.URL = caminho;
                _photo.Add(foto);

            }
            return _photo;
        }*/
      /*private void showZoom(ArqImagens nomeSelecionado)
        {
                    try
                    {
                        new PhotoBrowser
                        {
                            Photos = new List<Photo>
                {
                    new Photo
                    {
                        URL = "https://www.painelstudio.com/perboyre/exames/687014-ERICA_GONCALVES_GOMES/687014pa.jpg",
                        Title = "Vincent"
                    },
                    new Photo
                    {
                        URL = "https://www.painelstudio.com/perboyre/exames/685593-ISADORA_REIS_FRANCO/pdf/685593L/teste.jpg",
                        Title = "Vincent"
                    },
                    new Photo
                    {
                        URL = nomeSelecionado.nome_arquivo_completo,
                        Title = "Jules"
                    },
                    new Photo
                    {
                        URL = "https://raw.githubusercontent.com/stfalcon-studio/FrescoImageViewer/v.0.5.0/images/posters/Korben.jpg",
                        Title = "Korben"
                    },
                    new Photo
                    {
                        URL = "https://raw.githubusercontent.com/stfalcon-studio/FrescoImageViewer/v.0.5.0/images/posters/Toretto.jpg",
                        Title = "Toretto"
                    },
                    new Photo
                    {
                        URL = "https://raw.githubusercontent.com/stfalcon-studio/FrescoImageViewer/v.0.5.0/images/posters/Marty.jpg",
                        Title = "Marty"
                    },
                    new Photo
                    {
                        URL = "https://raw.githubusercontent.com/stfalcon-studio/FrescoImageViewer/v.0.5.0/images/posters/Driver.jpg",
                        Title = "Driver"
                    },
                    new Photo
                    {
                        URL = "https://raw.githubusercontent.com/stfalcon-studio/FrescoImageViewer/v.0.5.0/images/posters/Frank.jpg",
                        Title = "Frank"
                    },
                    new Photo
                    {
                        URL = "https://raw.githubusercontent.com/stfalcon-studio/FrescoImageViewer/v.0.5.0/images/posters/Max.jpg",
                        Title = "Max"
                    },
                    new Photo
                    {
                        URL = "https://raw.githubusercontent.com/stfalcon-studio/FrescoImageViewer/v.0.5.0/images/posters/Daniel.jpg",
                        Title = "Daniel"
                    }
                },
                            ActionButtonPressed = (index) =>
                            {
                                Debug.WriteLine($"Clicked {index}");
                                PhotoBrowser.Close();
                            },




                        }.Show();
                    }
                    catch(Exception e)
                    {

                    }



        }*/
        private async void teste(ArqImagens nomeSelecionado)
        {
          var navigationParams = new NavigationParameters();
          navigationParams.Add("imagem", nomeSelecionado);
          await NavigationService.NavigateAsync("TesteImagem",navigationParams);
           
            // await Navigation.PushAsync(new TesteImagem());
        }

        public async void Initialize(INavigationParameters parameters)
        {

          _paciente = (paciente)parameters["paciente"];
          titulo = _paciente.nome;

          await GetExames();

        }
    }
}

