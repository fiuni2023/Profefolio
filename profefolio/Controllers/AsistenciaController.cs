using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using profefolio.Helpers;
using profefolio.Models.DTOs.Asistencia;
using profefolio.Models.Entities;
using profefolio.Repository;

namespace profefolio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AsistenciaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAsistencia _asistenciaService;
        private readonly IMateriaLista _materiaListaService;
        private readonly IClasesAlumnosColegio _claseAlumColgService;
        private static int CantPorPage => Constantes.CANT_ITEMS_POR_PAGE;

        public AsistenciaController(IMapper mapper, IAsistencia asistencia, IMateriaLista materiaLista, IClasesAlumnosColegio clasesAlumnosColegio)
        {
            _mapper = mapper;
            _asistenciaService = asistencia;
            _materiaListaService = materiaLista;
            _claseAlumColgService = clasesAlumnosColegio;
        }

        [HttpGet("{idMateriaLista:int}")]
        [Authorize(Roles = "Profesor")]
        public async Task<ActionResult<List<AsistenciaResultDTO>>> GetAll(int idMateriaLista)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Name);
            try
            {
                var alumnosColegios = await _asistenciaService.FindAll(idMateriaLista, userEmail);

                var results = new List<AsistenciaResultDTO>();

                foreach (var alumnoColegio in alumnosColegios.GroupBy(a => a.ColegiosAlumnosId).ToList())
                {


                    foreach (var item in alumnoColegio)
                    {
                        var resultDto = _mapper.Map<AsistenciaResultDTO>(item);
                        resultDto.Asistencias = item.Asistencias.OrderBy(a => a.Fecha)
                                .TakeWhile(a => a.Fecha > a.Fecha.AddDays(-5))
                                .Select(b => new AssitenciasFechaResult()
                                {
                                    Fecha = b.Fecha,
                                    Id = b.Id,
                                    Estado = b.Estado,
                                    Observacion = b.Observacion
                                })
                                .ToList();
                        results.Add(resultDto);
                    }

                }

                return Ok(results);

            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"{e}");
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");
                return BadRequest("Error durante la obtencion de asistencias");
            }
        }


        /// <summary>
        ///     Actualiza, Agrega y Elimina una lista de asistecias
        /// </summary>
        /// <remarks>
        /// <a href="https://nande-y.atlassian.net/browse/PF-316">Ticket PF-316</a>
        /// 
        /// La utilidad del ID recibido en los objetos de la lista va a variar de acuerdo a la accion
        /// 
        ///     * si la accion es N, el ID tiene que ser del alumno al que se le quiere dar la asistencia
        /// 
        ///     * si la accion es U, el ID tiene que ser el de la asistencia a editar (solo se editan los estados y las observaciones)
        /// 
        ///     * si la opcion es D, el ID tiene que ser el de la asistencia a eliminar 
        /// 
        /// Body:
        /// 
        ///     [
        ///         {
        ///             id = 1,                 // id del alumno o de la asistencia dependiendo de la accion
        ///             "fecha": "2023-05-19T08:41:00.979Z",
        ///             "estado": "string",
        ///             "accion": "string",
        ///             "observacion": "string"
        ///         }
        ///     ]       
        ///     
        /// 
        /// </remarks>
        /// 
        [HttpPut("{idMateriaLista:int}")]
        [Authorize(Roles = "Profesor")]
        public async Task<ActionResult> PutAll(int idMateriaLista, [FromBody] List<AsistenciaPutDTO> dto)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Name);
            try
            {
                var profesor = await _materiaListaService.GetProfesorOfMateria(idMateriaLista, userEmail);

                foreach (var item in dto)
                {
                    if (item.Accion == 'N')
                    {
                        // validar que el id recibido sea de un alumno de la materia
                        if (!(await _claseAlumColgService.IsAlumnoOfClaseAndMateria(item.Id, idMateriaLista)))
                        {
                            _asistenciaService.Dispose();
                            return BadRequest("El id no es de un alumno de la materia");
                        }
                        var asistencia = new Asistencia()
                        {
                            ClasesAlumnosColegioId = item.Id,
                            Fecha = item.Fecha,
                            Estado = item.Estado,
                            Observacion = item.Observacion,
                            MateriaListaId = idMateriaLista,
                            Created = DateTime.Now,
                            CreatedBy = userEmail
                        };
                        await _asistenciaService.Add(asistencia);

                    }
                    else if (item.Accion == 'U')
                    {
                        //validar que el id sea de una asistencia
                        var asistencia = await _asistenciaService.FindByIdAndProfesorId(item.Id, profesor.Id);
                        if (asistencia == null)
                        {
                            _asistenciaService.Dispose();
                            return BadRequest("El id no es de una asistencia");
                        }

                        asistencia.Modified = DateTime.Now;
                        asistencia.ModifiedBy = userEmail;
                        asistencia.Estado = item.Estado;
                        asistencia.Observacion = item.Observacion;

                        _asistenciaService.Edit(asistencia);

                    }
                    else if (item.Accion == 'D')
                    {
                        var asistencia = await _asistenciaService.FindByIdAndProfesorId(item.Id, profesor.Id);
                        if (asistencia == null)
                        {
                            _asistenciaService.Dispose();
                            return BadRequest("El id no es de una asistencia");
                        }
                        await _asistenciaService.Delete(asistencia);
                    }
                }
                await _asistenciaService.Save();
                return Ok();

            }
            catch (FileNotFoundException e)
            {
                _asistenciaService.Dispose();
                Console.WriteLine($"{e}");
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _asistenciaService.Dispose();
                Console.WriteLine($"{e}");
                return BadRequest("Error durante el guardado");
            }
        }
    }
}