using MediatR;
using SuiviCompresseur.GestionFournisseur.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.GestionFournisseur.Domain.Commands.FournisseurCommands
{
    public class PutFournisseurCommand: IRequest<string>
    {
        public PutFournisseurCommand(Guid id,Fournisseur fournisseur)
        {
            Id = id;
            Fournisseur = fournisseur;
        }
        public Guid Id { get; }
        public Fournisseur Fournisseur { get; }
    }
}
