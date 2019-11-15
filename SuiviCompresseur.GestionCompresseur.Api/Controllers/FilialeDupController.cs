//using SuiviCompresseur.GestionCompresseur.Application.Interfaces;
//using SuiviCompresseur.GestionCompresseur.Domain.Models;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace SuiviCompresseur.GestionCompresseur.Api.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class FilialeDupController: ControllerBase
//    {
//        private readonly IFilialeDupService _FilialeDupService;
//        public FilialeDupController(IFilialeDupService FilialeDupService)
//        {
//            _FilialeDupService = FilialeDupService;
//        }
//        // GET api/transfer
//        [HttpGet]
//        public ActionResult<IEnumerable<FilialeDup>> Get()
//        {
//            return Ok(_FilialeDupService.GetFilialesDup());
//        }
//    }
//}

