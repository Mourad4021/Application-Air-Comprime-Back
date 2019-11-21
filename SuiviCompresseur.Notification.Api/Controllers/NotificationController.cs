using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SuiviCompresseur.Notification.Data.Context;
using SuiviCompresseur.Notification.Domain.Commands;
using SuiviCompresseur.Notification.Domain.Models;
using SuiviCompresseur.Notification.Domain.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace SuiviCompresseur.Notification.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class NotificationController : ControllerBase
    {

        private readonly Notification_context _context;
        private readonly IMediator mediator;
        public NotificationController(Notification_context context, IMediator mediator)
        {
            this.mediator = mediator;
            _context = context;
        }


        // GET api/values
        //[HttpGet("{Address}")]
        //[AllowAnonymous]
        [Authorize(Roles = "Editors , TotalControl , LimitedAccess")]
        [HttpGet]

        public async Task<IEnumerable<EmailFrom>> GetNotification(string address)
        {
            string s1 = "Rached Trabelsi/SIEGE";
            return await mediator.Send(new GetNotificationsQuery(address));
        }

        // POST api/values
         [Authorize(Roles = "Editors , TotalControl , LimitedAccess")]
        [HttpPost]
        public async Task<string> SendEmail([FromBody] EmailMessage emailMessage)
        {


            emailMessage.FromAddresses = "\"" + emailMessage.FromAddresses + "\"";
            emailMessage.ToAddresses = "\"" + emailMessage.ToAddresses + "\"";
            //emailMessage.CcAddresses = "\"" + emailMessage.CcAddresses + "\"";
            //emailMessage.CccAddresses = "\"" + emailMessage.CccAddresses + "\"";
            return await mediator.Send(new SendEmailCommand(emailMessage));
        }

        //[AllowAnonymous]
        // PUT api/values/5
        [Authorize(Roles = "Editors , TotalControl , LimitedAccess")]
        [HttpGet("{id}")]
        public async Task<string> Seen(Guid id, string address)
        {
            return await mediator.Send(new NotificationSeenCommand(id, address));
        }


    }
}
