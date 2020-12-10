using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PerboyreApp.Interfaces;
using PerboyreApp.Models;

namespace PerboyreApp.Services
{
    public class ApiService : IApiService
    {
        public async Task<List<paciente>> getPacientes(long Id_dentista)
        {
            try
            {
                var paramrequest = new ParamRequest
                {
                    id_dentista = Id_dentista.ToString()

                };

                var jsonRequest = JsonConvert.SerializeObject(paramrequest);
                var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://www.painelstudio.com/perboyre/");
                var url = "api/Paciente/getPacientes";
                var response = await client.PostAsync(url, httpContent);

                if (!response.IsSuccessStatusCode)
                {
                    return null;

                }

                var result = await response.Content.ReadAsStringAsync();
                List<paciente> exames = JsonConvert.DeserializeObject<List<paciente>>(result);
                return exames.OrderBy(x => x.nome).ToList();



            }
            catch (Exception ex)
            {
                return null;
                /*return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
                throw;*/
            }
        }


        public async Task<List<Dentista>> getDentistas()
        {
            try
            {

                /*var jsonRequest = JsonConvert.SerializeObject(paramrequest);
                var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");*/
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://www.painelstudio.com/perboyre/");
                // client.BaseAddress = new Uri("http://192.168.0.15:3000/");
                //var url = "api/Dentista/getDentistas";
                var url = "api/Dentista/getDentistas";
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return null;

                }

                var result = await response.Content.ReadAsStringAsync();
                List<Dentista> exames = JsonConvert.DeserializeObject<List<Dentista>>(result);
                if (App.usuariologado.Id == 999999999)
                {
                    return exames;
                }
                else
                {
                    return exames.Where(x => x.Id == App.usuariologado.Id).ToList();
                }
                // return exames.Skip(pageIndex * pageSize).Take(pageSize).ToList();     //_pessoas.Skip(pageIndex * pageSize).Take(pageSize).ToList();




            }
            catch (Exception ex)
            {
                return null;
                /*return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
                throw;*/
            }
        }


        public async Task<List<Unidade>> getUnidades()
        {
            try
            {

                /*var jsonRequest = JsonConvert.SerializeObject(paramrequest);
                var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");*/
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://www.painelstudio.com/perboyre/");
                // client.BaseAddress = new Uri("http://192.168.0.15:3000/");
                //var url = "api/Dentista/getDentistas";
                var url = "api/Unidade/Getunidades";
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return null;

                }

                var result = await response.Content.ReadAsStringAsync();
                List<Unidade> exames = JsonConvert.DeserializeObject<List<Unidade>>(result);
                
                
                return exames;
                
                // return exames.Skip(pageIndex * pageSize).Take(pageSize).ToList();     //_pessoas.Skip(pageIndex * pageSize).Take(pageSize).ToList();




            }
            catch (Exception ex)
            {
                return null;
                /*return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
                throw;*/
            }
        }
        public async Task<List<ArqImagens>> getExames(paciente pac)
        {
            try
            {

                var jsonRequest = JsonConvert.SerializeObject(pac);
                var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://www.painelstudio.com/perboyre/");
                // client.BaseAddress = new Uri("http://192.168.0.12s:3000/");
                var url = "api/Paciente/getExames";
                var response = await client.PostAsync(url, httpContent);

                if (!response.IsSuccessStatusCode)
                {
                    return null;

                }

                var result = await response.Content.ReadAsStringAsync();
                List<ArqImagens> exames = JsonConvert.DeserializeObject<List<ArqImagens>>(result);
                return exames;



            }
            catch (Exception ex)
            {
                return null;
                /*return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
                throw;*/
            }
        }


        public async Task<List<ArqImagens>> getExamespdf(paciente pac)
        {
            try
            {

                var jsonRequest = JsonConvert.SerializeObject(pac);
                var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://www.painelstudio.com/perboyre/");
                var url = "api/Paciente/getExamespdf";
                var response = await client.PostAsync(url, httpContent);

                if (!response.IsSuccessStatusCode)
                {
                    return null;

                }

                var result = await response.Content.ReadAsStringAsync();
                List<ArqImagens> exames = JsonConvert.DeserializeObject<List<ArqImagens>>(result);
                return exames;



            }
            catch (Exception ex)
            {
                return null;
                /*return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
                throw;*/
            }
        }
        public async Task<Response> Login(string email, string password)
        {
            try
            {
                var loginRequest = new LoginRequest
                {
                    Email = email,
                    Password = password
                };

                var jsonRequest = JsonConvert.SerializeObject(loginRequest);
                var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://www.painelstudio.com/perboyre/");
                //client.BaseAddress = new Uri("http://192.168.0.7:30000/");
                var url = "api/Dentista/Login";

                var response = await client.PostAsync(url, httpContent);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Usuário ou Senha Incorretos",

                    };

                }

                var result = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<Dentista>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "login Ok",
                    Result = user
                };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
                throw;
            }
        }


        public async Task<Response> PutDentista(Dentista dentista)
        {
            try
            {

                var jsonRequest = JsonConvert.SerializeObject(dentista);
                var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://www.painelstudio.com/perboyre/");
                // client.BaseAddress = new Uri("http://192.168.0.7:30000/");
                //var url = "api/Dentista/getDentistas";
                var url = "api/Dentista/PutDentista";
                var response = await client.PutAsync(url, httpContent);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    //string teste  =   response.RequestMessage.ToString();
                    // string teste2 = response.Headers.ToString();
                    return new Response
                    {
                        IsSuccess = false,
                        Message = error,

                    };


                }

                var result = await response.Content.ReadAsStringAsync();
                Dentista dent = JsonConvert.DeserializeObject<Dentista>(result);
                // return exames.Skip(pageIndex * pageSize).Take(pageSize).ToList();     //_pessoas.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                return new Response
                {
                    IsSuccess = true,
                    Message = "Dentista Atualizado com Sucesso!",
                    Result = dent
                };


            }
            catch (Exception ex)
            {

                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
                throw;


            }
        }

        public async Task<byte[]> getExame(paciente pac)
        {

            try
            {

                var jsonRequest = JsonConvert.SerializeObject(pac);
                var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://www.painelstudio.com/perboyre/");
                var url = "api/Paciente/getImagem";
                var response = await client.PostAsync(url, httpContent);

                if (!response.IsSuccessStatusCode)
                {
                    return null;

                }

                var result = await response.Content.ReadAsStringAsync();
                byte[] exames = JsonConvert.DeserializeObject<byte[]>(result);
                return exames;



            }
            catch (Exception ex)
            {
                return null;
                /*return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
                throw;*/
            }
        }


        public  async Task<MemoryStream> DownloadFileAsync(string url)
        {
            
            try
            {
                var stream = new MemoryStream();
                using (var httpClient = new HttpClient())
                {
                    var downloadStream = await httpClient.GetStreamAsync(new Uri(url));
                    if (downloadStream != null)
                    {
                        await downloadStream.CopyToAsync(stream);
                    }
                }

                return stream;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                return null;
            }
        }
    }
}
