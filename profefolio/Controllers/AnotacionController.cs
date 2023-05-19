using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Mvc;
using profefolio.Helpers;
using profefolio.Repository;
using profefolio.Models.DTOs.Anotacion;
using Microsoft.AspNetCore.Authorization;
using profefolio.Models.Entities;
using System.Security.Claims;

namespace profefolio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnotacionController : ControllerBase
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(MateriaController));
        private readonly IAnotacion _anotacionService;
        private readonly IMateriaLista _materiaListaService;
        private static int _cantPorPag => Constantes.CANT_ITEMS_POR_PAGE;
        private readonly IMapper _mapper;
        public AnotacionController(IAnotacion anotacionService, IMapper mapper, IMateriaLista materiaListaService)
        {
            _materiaListaService = materiaListaService;
            _anotacionService = anotacionService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = "Profesor")]
        public async Task<ActionResult<AnotacionResultDTO>> Post([FromBody] AnotacionCreateDTO dTO)
        {

            var model = _mapper.Map<Anotacion>(dTO);
            var name = User.FindFirstValue(ClaimTypes.Name);
            
            model.CreatedBy = name;
            model.Created = DateTime.Now;
            model.Deleted = false;

            var materia = await _materiaListaService.FindById(dTO.MateriaId);
            model.MateriaListaId = materia.Id;
            if(!name.Equals(materia.Profesor.Email)){
                return BadRequest();
            }

            await _anotacionService.Add(model);
            await _anotacionService.Save();
            return Ok();
        }
    }
}