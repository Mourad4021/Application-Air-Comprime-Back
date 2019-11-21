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
using SuiviCompresseur.GestionFournisseur.Data.Context;
using SuiviCompresseur.GestionFournisseur.Domain.Models;
using SuiviCompresseur.GestionFournisseur.Domain.Queries.AttachementQuery;

namespace SuiviCompresseur.GestionFournisseur.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AttachementFournisseursController : ControllerBase
    {

        // GET: api/AttachementFournisseurs
        private readonly IMediator mediator;


        public AttachementFournisseursController(IMediator mediator)
        {
            this.mediator = mediator;

        }

        // GET: api/Attachments
        [AllowAnonymous]
        [HttpGet("getAttachementsByFournisseurId")]
        public async Task<IEnumerable<AttachementFournisseur>> GetAttachementsByFournisseurId(Guid fournisseuId) =>
            await mediator.Send(new GetAttachmentsByFournisseurIdQuery(fournisseuId));


        // GET: api/Attachments/5
        [AllowAnonymous]
        [HttpGet("getAttachementFournisseurFileById")]
        public async Task<byte[]> GetAttachementFileById(Guid attachementId) =>
            await mediator.Send(new GetAttachmentFournisseurFileByIdQuery(attachementId));




    }
}
