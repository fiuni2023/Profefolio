using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using profefolio.Models.DTOs.ClasesAlumnosColegio;
using profefolio.Models.Entities;
using profefolio.Repository;

namespace profefolio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClasesAlumnosColegioController : ControllerBase
    {
        private IClasesAlumnosColegio _clasesAlumnosColegioService;
        private IClase _claseService;
        private IColegiosAlumnos _colegiosAlumnosService;
        private IMapper _mapper;
        public ClasesAlumnosColegioController(IMapper mapper, IClasesAlumnosColegio clasesAlumnosColegio, IClase claseService, IColegiosAlumnos colegiosAlumnos)
        {
            _mapper = mapper;
            _clasesAlumnosColegioService = clasesAlumnosColegio;
            _claseService = claseService;
            _colegiosAlumnosService = colegiosAlumnos;
        }

        [HttpPost]
        public async Task<ActionResult<ClasesAlumnosColegioDTOResult>> PostWithIdAlumnoInColegio([FromBody] ClasesAlumnosColegioDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Peticion invalida");
            }

            var adminEmail = User.FindFirstValue(ClaimTypes.Name);

            try
            {
                // verificar si existe la clase con el Id de dto
                var clase = await _claseService.FindById(dto.ClaseId);
                if (clase == null)
                {
                    return BadRequest("La Clase no existe.");
                }

                var colegioAlumno = await _colegiosAlumnosService.FindById(dto.ColegioAlumnoId);
                if (colegioAlumno == null)
                {
                    return BadRequest("El Identificador del Alumno dentro del Colegio no fue encontrado.");
                }


                // se valida que tanto el alumno en el colegio y la clase sean del colegio donde se encuentra el administrador
                if (clase.Colegio != null
                    && (!adminEmail.Equals(colegioAlumno.Colegio.personas.Email)
                    || !adminEmail.Equals(clase.Colegio.personas.Email)))
                {
                    return BadRequest("No pude matipular datos ajenos a su institucion, la Clase o el Alumno no es de su propiedad.");
                }

                // se valida que ya no se haya agregado el alumno a la clase
                if (await _clasesAlumnosColegioService.Exist(dto.ClaseId, dto.ColegioAlumnoId))
                {
                    return BadRequest("Ya se tiene registrado el alumno en la clase");
                }

                var model = _mapper.Map<ClasesAlumnosColegio>(dto);
                model.Deleted = false;
                model.Created = DateTime.Now;
                model.CreatedBy = adminEmail;

                var relacion = await _clasesAlumnosColegioService.Add(model);
                await _clasesAlumnosColegioService.Save();

                var result = await _clasesAlumnosColegioService.FindById(relacion.Id);

                var resultMapped = _mapper.Map<ClasesAlumnosColegioDTOResult>(result);
                return Ok(resultMapped);

            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");

                return BadRequest("Error inesperado durante el guardado");
            }
        }

        [HttpPut]
        public async Task<ActionResult> PutList([FromBody] ClaseAlumnosColegioLitPutDTO dtoList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Peticion Invalida");
            }
            if (dtoList.ListaAlumnos.Count() == 0)
            {
                return BadRequest("No se puede editar valores si es que no mando niguno");
            }

            var adminEmail = User.FindFirstValue(ClaimTypes.Name);

            try
            {
                // verificar si existe la clase con el Id de dto
                var clase = await _claseService.FindById(dtoList.ClaseId);
                if (clase == null)
                {
                    return BadRequest("La Clase no existe.");
                }
                if (clase.Colegio == null || !adminEmail.Equals(clase.Colegio.personas.Email))
                {
                    return BadRequest("No puede editar Clases de otro Colegio");
                }

                var claseId = dtoList.ClaseId;

                foreach (var element in dtoList.ListaAlumnos)
                {
                    if (element.Estado == 'D')
                    {
                        var relacion = await _clasesAlumnosColegioService.FindByClaseIdAndColegioAlumnoId(claseId, element.ColegioAlumnoId);
                        if (relacion == null)
                        {
                            _clasesAlumnosColegioService.Dispose();
                            return BadRequest("El elemento que quiere eliminar no existe");
                        }

                        if (relacion.Clase == null || relacion.Clase.Colegio == null || $"{adminEmail + 6}".Equals(relacion.Clase.Colegio.personas.Email))
                        {
                            _clasesAlumnosColegioService.Dispose();
                            return BadRequest("El elemento que quiere eliminar no pertenece al Colegio");
                        }

                        if (true)
                        {
                            Console.WriteLine($"\n\n\nCuando tenga una relacion validar que no tenga niguna para marcar como eliminado\n\n\n");
                        }


                        relacion.CreatedBy = adminEmail;
                        relacion.Modified = DateTime.Now;
                        relacion.Deleted = true;

                        _clasesAlumnosColegioService.Edit(relacion);
                    }
                    else if (element.Estado == 'N')
                    {

                        var colegioAlumno = await _colegiosAlumnosService.FindById(element.ColegioAlumnoId);
                        if (colegioAlumno == null)
                        {
                            _clasesAlumnosColegioService.Dispose();

                            return BadRequest("El Identificador del Alumno dentro del Colegio no fue encontrado.");
                        }


                        // se valida que tanto el alumno en el colegio y la clase sean del colegio donde se encuentra el administrador
                        if (!adminEmail.Equals(colegioAlumno.Colegio.personas.Email))
                        {
                            _clasesAlumnosColegioService.Dispose();
                            return BadRequest("No pude matipular datos ajenos a su institucion, la Clase o el Alumno no es de su propiedad.");
                        }

                        // se valida que ya no se haya agregado el alumno a la clase
                        if (await _clasesAlumnosColegioService.Exist(claseId, element.ColegioAlumnoId))
                        {
                            _clasesAlumnosColegioService.Dispose();
                            return BadRequest("Ya se tiene registrado el alumno en la clase");
                        }
                        var dto = new ClasesAlumnosColegioDTO()
                        {
                            ClaseId = claseId,
                            ColegioAlumnoId = element.ColegioAlumnoId
                        };

                        var model = _mapper.Map<ClasesAlumnosColegio>(dto);
                        model.Deleted = false;
                        model.Created = DateTime.Now;
                        model.CreatedBy = adminEmail;

                        await _clasesAlumnosColegioService.Add(model);
                    }
                };
                await _clasesAlumnosColegioService.Save();

                return NoContent();
            }
            catch (Exception e)
            {
                _clasesAlumnosColegioService.Dispose();
                Console.WriteLine($"{e}");
                return BadRequest("Error inesperado durante la actualizacion");
            }
        }
    }
}