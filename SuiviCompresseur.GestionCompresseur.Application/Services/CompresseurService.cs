//using SuiviCompresseur.GestionCompresseur.Application.Interfaces;
//using SuiviCompresseur.GestionCompresseur.Domain.Interfaces;
//using SuiviCompresseur.GestionCompresseur.Domain.Models;
//using SuiviCompresseur.Domain.Core.Bus;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace SuiviCompresseur.GestionCompresseur.Application.Services
//{
//    public class CompresseurService : ICompresseurService
//    {
//        private readonly ICompresseurRepository _compresseurRepository;
//        private readonly IEventBus _bus;

//        public CompresseurService(ICompresseurRepository compresseurRepository, IEventBus bus)
//        {
//            _compresseurRepository = compresseurRepository;
//            _bus = bus;
//        }

//        public IEnumerable<FournisseurDup> GetFournisseursDup()
//        {
//            return _compresseurRepository.GetFournisseur();
//        }
//    }
//}
