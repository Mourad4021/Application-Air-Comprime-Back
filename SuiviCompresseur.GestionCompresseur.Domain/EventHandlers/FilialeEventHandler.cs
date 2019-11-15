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
//    public class FilialeEventHandler : IEventHandler<SendListeCreateEvent>
//    {
//        private readonly IFilialeDupRepository _filialeDupRepository;
//        public FilialeEventHandler(IFilialeDupRepository filialeDupRepository )
//        {
//            _filialeDupRepository = filialeDupRepository;
//        }
//        public Task Handle(SendListeCreateEvent @event)
//        {
//            _filialeDupRepository.Add(new FilialeDup()
//            {

//                FilialeID = @event.FilialeID,
//                Nom = "rached",
//                Code = @event.Code
//            });
//            return Task.CompletedTask;
//        }
//    }
//}
