
using SuiviCompresseur.GestionResponsable.Domain.Commands;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuiviCompresseur.GestionResponsable.Domain.CommandHandlers
{
    public class TransferCommandHandler : IRequestHandler<CreateTransferCommand, bool>
    {
        private readonly IEventBus _bus;

        public TransferCommandHandler(IEventBus bus)
        {
            _bus = bus;
        }

        public Task<bool> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
        {
            //publish event to RabbitMQ
            _bus.Publish(new SendListeCreateEvent(request.Nom, request.Code));

            return Task.FromResult(true);
        }


    }
}

