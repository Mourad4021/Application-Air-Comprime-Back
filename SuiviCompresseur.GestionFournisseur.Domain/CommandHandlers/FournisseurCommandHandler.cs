//using MediatR;
//using SuiviCompresseur.GestionFournisseur.Domain.Commands;
//using SuiviCompresseur.GestionFournisseur.Domain.Events;
//using SuiviCompresseur.Domain.Core.Bus;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using SuiviCompresseur.GestionFournisseur.Domain.Interfaces;
//using SuiviCompresseur.GestionFournisseur.Domain.Models;

//namespace SuiviCompresseur.GestionFournisseur.Domain.CommandHandlers
//{
//    public class FournisseurCommandHandler : IRequestHandler<CreateFournisseurCommand, bool>
//    {
//        private readonly IEventBus _bus;
//        //private readonly IFournisseurRepository _db;

//        public FournisseurCommandHandler(IEventBus bus)
//        {
//            _bus = bus;
           
//        }

//        public Task<bool> Handle(CreateFournisseurCommand request, CancellationToken cancellationToken)
//        {


//            //_db.AddF(new Fournisseur()
//            //{
//            //    FournisseurID = request.FournisseurID,
//            //    Nom = request.Nom,
//            //    Adresse = request.Adresse,
//            //    Email = request.Email
//            //});

//            //publish event to RabbitMQ
//            _bus.Publish(new CreationDoneEvent(request.FournisseurID, request.Nom, request.Adresse, request.Email));
           

//            return Task.FromResult(true);
//        }
//    }
//}
