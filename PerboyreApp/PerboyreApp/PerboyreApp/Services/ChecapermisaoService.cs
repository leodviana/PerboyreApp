
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using static Xamarin.Essentials.Permissions;

namespace PerboyreApp.Services
{
    public static class ChecapermisaoService
    {
        public static async Task<PermissionStatus> checa_permissao<T>(T permission) where T:BasePermission
        {

            var status = await permission.CheckStatusAsync();
            if (status!=PermissionStatus.Granted)
            {
                status = await permission.RequestAsync();
            }
            return status;
        }
    }
}
