using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.GestionResponsable.Domain.Commands

{
    public class CreateTransferCommand : TransferCommand
    {
        public CreateTransferCommand(string nom, string code)
        {
            Nom = nom;
            Code = code;
        }
    }


}

