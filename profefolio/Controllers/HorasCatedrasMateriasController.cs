using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using profefolio.Helpers;
using profefolio.Repository;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using profefolio.Models.Entities;
using profefolio.Models.DTOs.HorasCatedrasMaterias;

namespace profefolio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HorasCatedrasMateriasController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHorasCatedrasMaterias _horaCatMatService;
        private readonly IProfesor _profesorService;
        private readonly IHoraCatedra _horaCatedraService;
        private static int CantPorPage => Constantes.CANT_ITEMS_POR_PAGE;
        public HorasCatedrasMateriasController(IMapper mapper, IHorasCatedrasMaterias horaCMService, IProfesor profesorService, IHoraCatedra horaCatService)
        {
            _mapper = mapper;
            _horaCatMatService = horaCMService;
            _profesorService = profesorService;
            _horaCatedraService = horaCatService;
        }


        /// <summary>
        /// Obtiene todos los registros de los horarios en los colegios del Profesot
        /// 
        /// </summary>
        /// <returns>Lista de colegios con los horarios de cada materia del profesor</returns>
        /// <remarks>
        /// Ticket <a href="https://nande-y.atlassian.net/browse/PF-277">PF-277</a>
        /// 
        /// </remarks>
        [HttpGet]
        [Authorize(Roles = "Profesor")]
        public async Task<ActionResult<List<HorariosColegiosResultDTO>>> GetAllColegiosHorarios()
        {
            var profeEmail = User.FindFirstValue(ClaimTypes.Name);

            var colegiosProfe = await _horaCatMatService.GetAllHorariosOfColegiosByEmailProfesor(profeEmail);
            var listaResult = new List<HorariosColegiosResultDTO>();

            try
            {
                foreach (var colProf in colegiosProfe)
                {

                    var colegioProfeResult = _mapper.Map<HorariosColegiosResultDTO>(colProf);
                    var horarios = colProf.Persona.ListaMaterias
                                .Select(c => _mapper.Map<HorarioMateriaDTO>(c.Horarios)).ToList();
                    colegioProfeResult.HorariosMaterias = horarios;
                    listaResult.Add(colegioProfeResult);
                }
                return Ok(listaResult);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");
                return BadRequest("Error durante la obtencion del horario");
            }
        }

        /// <summary>
        /// Guarda una relacion entre una hora catedra y una MateriaLista de acuerdo a un dia
        /// 
        /// </summary>
        /// <remarks>
        /// Ticket <a href="https://nande-y.atlassian.net/browse/PF-278">PF-278</a>
        /// 
        /// </remarks>
        [HttpPost]
        [Authorize(Roles = "Profesor")]
        public async Task<ActionResult> Post(HorasCatedrasMateriasDTO dto){
            try
            {
                var profeEmail = User.FindFirstValue(ClaimTypes.Name);
                // se valdia que exista la hora catedra
                if(!(await _horaCatedraService.Exist(dto.HoraCatedraId))){
                    return BadRequest("Hora Catedra no existe");
                }
                // se valida que el profesor ensenhe la materia
                if(!(await _profesorService.IsProfesorInMateria(dto.MateriaListaId, profeEmail))){
                    return BadRequest("El profesor no ensena la materia");
                }

                // validar que no existe una relacion similar previa
                if(await _horaCatMatService.Exist(dto.MateriaListaId, dto.HoraCatedraId, dto.Dia)){
                    return BadRequest("Ya se asigno este horario a la materia");
                }
                var model = _mapper.Map<HorasCatedrasMaterias>(dto);
                model.Created = DateTime.Now;
                model.CreatedBy = profeEmail;
                model.Deleted = false;

                await _horaCatMatService.Add(model);
                await _horaCatMatService.Save();
                return Ok();
            }catch(Exception e){
                Console.WriteLine($"{e}");
                return BadRequest("Error durante el guardado");
            }
        }
    }
}