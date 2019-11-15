//using SuiviCompresseur.Domain.Core.Bus;
//using SuiviCompresseur.GestionCompresseur.Domain.Events;
//using SuiviCompresseur.GestionCompresseur.Domain.Interfaces;
//using SuiviCompresseur.GestionCompresseur.Domain.Models;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;

//namespace SuiviCompresseur.GestionCompresseur.Domain.EventHandlers
//{
//    public class CreationEventHandler : IEventHandler<CreationDoneEvent>
//    {
//        private readonly ICompresseurRepository _compresseurRepository;

//        public CreationEventHandler(ICompresseurRepository compresseurRepository)
//        {
//            _compresseurRepository = compresseurRepository;
//        }

//        public Task Handle(CreationDoneEvent @event)
//        {
//            _compresseurRepository.Add(new FournisseurDup()
//            {
//                FournisseurID = @event.FournisseurID,
//                Nom = @event.Nom,
//                Adresse = @event.Adresse,
//                Email=@event.Email
//            });

//            return Task.CompletedTask;
//        }
//    }
//}
