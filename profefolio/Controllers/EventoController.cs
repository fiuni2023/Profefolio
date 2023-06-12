using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using profefolio.Models.DTOs.Evento;
using profefolio.Models.Entities;
using profefolio.Repository;
using System.Security.Claims;

namespace profefolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private IEvento _eventoService;
        private IMateriaLista _materiaListaService;
        private IColegioProfesor _colegioProfesorService;

        public EventoController(IEvento eventoService, IMateriaLista materiaListaService,
            IColegioProfesor colegioProfesorService)
        {
            _eventoService = eventoService;
            _materiaListaService = materiaListaService;
            _colegioProfesorService = colegioProfesorService;
        }

        [HttpPost]
        [Authorize(Roles = "Profesor")]
        public async Task<ActionResult> PostEvento([FromBody] EventoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Objeto No valido");
            }

            try
            {
                var mlMateriaLista = await _materiaListaService.FindById(dto.IdMateriaLista);

                var evalue = dto.Tipo != null && !(dto.Tipo.Equals("Examen")
                                                   || dto.Tipo.Equals("Parcial")
                                                   || dto.Tipo.Equals("Prueba sumatoria")
                                                   || dto.Tipo.Equals("Evento"));

                var evalueEtapa = dto.Etapa is not ("Primera" or "Segunda");

                if (evalue || evalueEtapa)
                {

                    return BadRequest("Tipo de evento invalido");
                }

                var user = User.FindFirstValue(ClaimTypes.Name);

                //verificar que el prf pertenezca al colegio al cual quiere agregar un evento
                var exist = _colegioProfesorService.Count(mlMateriaLista.Clase.ColegioId, user);
                var resultado = exist.Result;
                if (resultado == 0)
                {
                    return BadRequest("No puede agregar eventos en otros colegios.");
                }


                var p = new Evaluacion
                {
                    Nombre = dto.Nombre,
                    Tipo = dto.Tipo,
                    Etapa = dto.Etapa,
                    CreatedBy = user,
                    Deleted = false,
                    MateriaListaId = mlMateriaLista.Id,
                    PuntajeTotal = dto.Puntaje,
                    Created = DateTime.Now,
                    Fecha = dto.Fecha,
                    Nombre = dto.Nombre
                };
                await _eventoService.Add(p, user);


                await _eventoService.Save();

                return Ok("Guardado");
                
            }
            catch (BadHttpRequestException e)
            {
                Console.WriteLine(e);
                return BadRequest($"Error al crear el evento");
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e);
                return Unauthorized();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e);
                return NotFound();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("Error al realizar la consulta");
            }
            
        }
    }
}