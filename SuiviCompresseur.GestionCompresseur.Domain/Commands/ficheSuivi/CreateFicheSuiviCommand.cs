using MediatR;
using SuiviCompresseur.GestionCompresseur.Domain.DTOs;
using SuiviCompresseur.GestionCompresseur.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.GestionCompresseur.Domain.Commands.ficheSuivi
{
    public class CreateFicheSuiviCommand:IRequest<Guid>
    {
        public Fiche_Suivi FicheSuivi { get; }
        public CreateFicheSuiviCommand(Fiche_Suivi ficheSuivi)
        {
            FicheSuivi = ficheSuivi;
        }
        
    }
}
