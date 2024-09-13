using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAV_Site.Models
{
    public class Log_UtilisateurModels
    {
        [Key]
        public Guid Id_log {get; set; }
        private string? email;
        private string? password;

        public Log_UtilisateurModels()
        {
            email = "inconnue";
            password = "inconnue";
        }

        public Log_UtilisateurModels(string? email, string? password)
        {
            this.email = email;
            this.password = password;
        }   

        public string? Email { get { return email; } set { email = value; } }
        public string? Password { get { return password; } set {  password = value; } }
    }
}
