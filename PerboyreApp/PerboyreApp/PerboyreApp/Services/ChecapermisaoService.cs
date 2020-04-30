using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PerboyreApp.Services
{
    public static class ChecapermisaoService
    {
        public static async Task<bool> checa_permissao()
        {
            bool permissao = false;
            try
            {
               
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                if (status != PermissionStatus.Granted)
                {
                   
                    var rationale = await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Storage);
                    if (rationale)
                    {
                        // await DisplayAlert("Erro", "gimme permissions", "ok");

                    }
                    /* if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                     {
                         await _dialogService.DisplayAlertAsync("Need location", "Gunna need that location", "OK");
                        // status = await Util.CheckPermissions(Permission.Location);
                     }*/

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Storage))
                        status = results[Permission.Storage];
                    permissao = true;
                }

                if (status == PermissionStatus.Granted)
                {

                    permissao = true;
                }
                else if (status != PermissionStatus.Unknown)
                {
                    permissao = false;
                    //string teste = "";
                    // await App.Current.MainPage.DisplayAlert("Alert", "Localizacao negada,verifique e tente novamente!", "OK");
                    //await _dialogService.DisplayAlertAsync("Erro", "Location Denied,Can not continue, try again.", "OK");

                }

                return permissao;


            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Erro!", ex.Message.ToString(), "OK");
                return permissao;
            }

        }
    }
}
