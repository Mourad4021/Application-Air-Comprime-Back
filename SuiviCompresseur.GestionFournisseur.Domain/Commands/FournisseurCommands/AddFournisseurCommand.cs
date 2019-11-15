using MediatR;
using SuiviCompresseur.GestionFournisseur.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.GestionFournisseur.Domain.Commands.FournisseurCommands
{
    public class AddFournisseurCommand : IRequest<string>
    {

        public AddFournisseurCommand( Fournisseur fournisseur)
        {
           
            Fournisseur = fournisseur;
        }
  
        public Fournisseur Fournisseur { get; }

    }
}
