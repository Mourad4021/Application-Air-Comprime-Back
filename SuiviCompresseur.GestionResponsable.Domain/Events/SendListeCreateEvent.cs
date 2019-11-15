using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.GestionResponsable.Domain.Events
{
    public class SendListeCreateEvent : Event
    {


        public string Nom{ get; set; }
        public string Code { get; set; }
        public SendListeCreateEvent(string nom_Filiale, string code_Filiale)
        {

            Nom = nom_Filiale;
            Code = code_Filiale;
        }
    }
}
