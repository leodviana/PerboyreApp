using System;
using System.Collections.Generic;
using System.Text;

namespace PerboyreApp.Models
{
   
        public partial class Dentista
        {
            public long Id { get; set; }
            public string nome { get; set; }
            public string Email { get; set; }
            public string logon { get; set; }
            public string senha { get; set; }
            public string status { get; set; }
            public string Email2 { get; set; }
            public string email3 { get; set; }
            public Nullable<int> cd_empresa { get; set; }
            public string tipo { get; set; }
            public byte[] ImageArray { get; set; }
            public string ImagePath { get; set; }
    }

}
