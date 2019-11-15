using SuiviCompresseur.GestionFournisseur.Application.Interfaces;
using SuiviCompresseur.GestionFournisseur.Application.Models;
using SuiviCompresseur.GestionFournisseur.Domain.Commands;
using SuiviCompresseur.GestionFournisseur.Domain.Interfaces;
using SuiviCompresseur.GestionFournisseur.Domain.Models;
//using SuiviCompresseur.Domain.Core.Bus;
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using SuiviCompresseur.GestionFournisseur.Domain.Queries;
using System.Threading.Tasks;
using SuiviCompresseur.GestionFournisseur.Domain.Commands.FournisseurCommands;

namespace SuiviCompresseur.GestionFournisseur.Application.Services
{
    public class FournisseurService : IFournisseurService
    {
        private readonly IFournisseurRepository _fournisseurRepository;
        //private readonly IEventBus _bus;
        private readonly IMediator meditor;

        public FournisseurService(IFournisseurRepository fournisseurRepository/*, IEventBus bus*/, IMediator meditor)
        {
            _fournisseurRepository = fournisseurRepository;
            //_bus = bus;
            this.meditor = meditor;
        }

        public Task<IEnumerable<Fournisseur>> GetFournisseurs()
        {
            return meditor.Send(new GetFournisseursQuery());
        }

        public Task<string> Creation(Fournisseur fournisseur)
        {
            //var createTransferCommand = new CreateFournisseurCommand(
            //        fournisseurCreation.FournisseurID,
            //        fournisseurCreation.Nom,
            //        fournisseurCreation.Adresse,
            //        fournisseurCreation.Email
            //    );

            //_bus.SendCommand(createTransferCommand);
            //var fournisseur = new Fournisseur()
            //{
            //    Nom = fournisseurCreation.Nom,
            //    Adresse = fournisseurCreation.Adresse,
            //    Email = fournisseurCreation.Email,
            //    FournisseurID = fournisseurCreation.FournisseurID
            //};
            return meditor.Send(new AddFournisseurCommand(fournisseur));
                
               
        }

        public Task<Fournisseur> GetFournisseur(Guid id)
        {
            return meditor.Send(new GetFournisseurQuery(id)); ;
        }

        public Task<string> DeleteFournisseurs(Guid id)
        {
            return meditor.Send(new RemoveFournisseurCommand(id));
        }

        public Task<string> PutFournisseurs(Guid id, Fournisseur fournisseur)
        {
            return meditor.Send(new PutFournisseurCommand(id, fournisseur));
        }

      
    }
}
