using MediatR;
//using SuiviCompresseur.Domain.Core.Bus;
using SuiviCompresseur.Gestion.Responsable.Aplication.Interfaces;
using SuiviCompresseur.Gestion.Responsable.Aplication.Models;
using SuiviCompresseur.Gestion.Responsable.Domain.Commands;
using SuiviCompresseur.Gestion.Responsable.Domain.Interfaces;
using SuiviCompresseur.Gestion.Responsable.Domain.Models;
using SuiviCompresseur.Gestion.Responsable.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuiviCompresseur.Gestion.Responsable.Aplication.Services
{
    public class FilialeService : IFilialeService
    {
        private readonly IGenericRepositoryResponsable<Filiale> _filialeIRepository;
        //private readonly IEventBus _bus;
        private readonly IMediator _mediator;
        public FilialeService(IGenericRepositoryResponsable<Filiale> filialeIRepository,/* IEventBus bus,*/ IMediator mediator)
        {
            _filialeIRepository = filialeIRepository;
            //_bus = bus;
            _mediator = mediator;
        }

        public Task<string> DeleteFiliale(Guid id)
        {
            return _mediator.Send(new RemoveGenericCommandGR<Filiale>(id));
        }

        public Task<Filiale> GetFiliale(Guid id)
        {
            return _mediator.Send(new GetGenericQueryGR<Filiale>(id));
        }

        public Task<IEnumerable<Filiale>> GetFiliales()
        {
            return _mediator.Send(new GetAllGenericQueryGR<Filiale>());
        }

        //public string PostFiliale(Filiale filiale) => _filialeIRepository.PostFiliale(filiale);


        public Task<string> PutFiliale(Guid id, Filiale filiale)
        {
            return _mediator.Send(new PutGenericCommandGR<Filiale>(filiale, id));
        }

        //rabbit
        public Task<string> Transfer(Filiale filiale)
        {
            //var createFilialeCommand = new /*CreateTransferCommand*/(
            // filialeEnv.FilialeID,
            // filialeEnv.Nom,
            // filialeEnv.Code
            // );
            //_bus.SendCommand(createFilialeCommand);

            //Filiale filiale = new Filiale()
            //{
            //    FilialeID = filialeEnv.FilialeID,
            //    Nom = filialeEnv.Nom,
            //    Code = filialeEnv.Code
            //};
            return _mediator.Send(new CreateGenericCommandGR<Filiale>(filiale));

        }

    }
}