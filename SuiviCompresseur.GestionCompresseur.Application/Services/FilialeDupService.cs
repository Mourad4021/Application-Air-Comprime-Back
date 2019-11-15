//using SuiviCompresseur.Domain.Core.Bus;
//using SuiviCompresseur.GestionCompresseur.Application.Interfaces;
//using SuiviCompresseur.GestionCompresseur.Domain.Interfaces;
//using SuiviCompresseur.GestionCompresseur.Domain.Models;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace SuiviCompresseur.GestionCompresseur.Application.Services
//{
//    public class FilialeDupService : IFilialeDupService
//    {
//        private readonly IFilialeDupRepository _filialeDupRepository;
//        private readonly IEventBus _bus;
//        public FilialeDupService(IFilialeDupRepository filialeDupRepository, IEventBus bus)
//        {
//            _filialeDupRepository = filialeDupRepository;
//            _bus = bus;
//        }


//        public IEnumerable<FilialeDup> GetFilialesDup()
//        {
//            return _filialeDupRepository.GetFilialesDup();
//        }
//    }
//}