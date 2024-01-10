using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPADATOS.Entidades
{
    public class clsCredencialesCorreo
    {
        public string EmailBase { get; set; }
        public string Hostname { get; set; }        
        public int Puerto { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
    }
}
