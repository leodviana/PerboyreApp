using System;
using System.Collections.Generic;
using System.Text;

namespace PerboyreApp.Models
{
    public partial class paciente
    {
        public long Id { get; set; }
        public string nome { get; set; }
        public System.DateTime dt_nascimento { get; set; }
        public System.DateTime dt_atendimento { get; set; }
        public long cd_dentista { get; set; }
        public string photo { get; set; }
        public string unidade { get; set; }
    }
}
