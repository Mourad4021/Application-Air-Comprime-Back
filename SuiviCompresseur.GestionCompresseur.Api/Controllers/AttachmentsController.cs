using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuiviCompresseur.GestionCompresseur.Data.Context;
using SuiviCompresseur.GestionCompresseur.Domain.Models;
using SuiviCompresseur.GestionCompresseur.Domain.Queries.AttachementQuery;

namespace SuiviCompresseur.GestionCompresseur.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AttachmentsController : ControllerBase
    {

        private readonly IMediator mediator;


        public AttachmentsController( IMediator mediator)
        {
            this.mediator = mediator;

        }

        // GET: api/Attachments
        [AllowAnonymous]
        [HttpGet("getAttachementsByFicheSuiviId")]
        public async Task<IEnumerable<Attachment>> GetAttachementsByFicheSuiviId(Guid ficheSuiviId) =>
            await mediator.Send(new GetAttachmentsByFicheSuiviIdQuery(ficheSuiviId));


        // GET: api/Attachments/5
        [AllowAnonymous]
        [HttpGet("getAttachementFileById")]
        public async Task<byte[]> GetAttachementFileById(Guid attachementId) =>
            await mediator.Send(new GetAttachmentFileByIdQuery(attachementId));


        [AllowAnonymous]
        [HttpGet("getAttachementsByEntretienRervoirID")]
        public async Task<IEnumerable<Attachment>> GetAttachementsByEntretienRervoirID(Guid entretienReservoirID) =>
            await mediator.Send(new GetAttachmentsByEntretienReservoirIdQuery(entretienReservoirID));


    }
}
