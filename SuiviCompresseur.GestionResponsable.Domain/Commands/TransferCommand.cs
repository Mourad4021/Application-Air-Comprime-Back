using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.GestionResponsable.Domain.Commands

{
    public abstract class TransferCommand : Command
    {
        public String Nom { get; protected set; }
        public String Code { get; protected set; }

    }
}
