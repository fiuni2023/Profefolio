using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using profefolio.Models.DTOs.ClaseMateria;
using profefolio.Models.Entities;
using profefolio.Repository;

namespace profefolio.Controllers
{
    [Route("api/administrador/materia/profesores")]
    [ApiController]
    [Authorize(Roles = "Administrador de Colegio")]
    public class MateriaListasController : ControllerBase
    {
        private readonly IMateriaLista _materiaListaService;
        private readonly IPersona _profesorService;
        private readonly IMateria _materiaService;
        private readonly IClase _claseService;
        public MateriaListasController(IMateriaLista materiaListaService, IPersona profesorService, IMateria materiaService, IClase claseService)
        {
            _materiaListaService = materiaListaService;
            _profesorService = profesorService;
            _materiaService = materiaService;
            _claseService = claseService;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ClaseMateriaCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return Ok(dto);

            var username = User.Identity.Name;


            foreach (var profes in dto.IdProfesores.Distinct())
            {

                var p = await _profesorService.FindById(profes);

                if (p == null) { continue; }
                await _materiaListaService.Add(new MateriaLista
                {
                    ClaseId = dto.IdClase,
                    ProfesorId = profes,
                    MateriaId = dto.IdMateria,
                    Created = DateTime.Now,
                    CreatedBy = username
                });
            }

            await _materiaListaService.Save();

            return Ok();
        }


        //Este metodo GET NO SE DEBE IMPLEMENTAR EN EL FRONT-END, es con fines de Testing
        [HttpGet]
        public ActionResult GetAllTemp()
        {

            var query = _materiaListaService
                .GetAll(0, 0)
                .ToList()
                .ConvertAll(p => new
                {
                    Id = p.Id,
                    Profes = new
                    {
                        IdProfesor = p.ProfesorId,
                        ProfesorMail = p.Profesor.Email,
                    },
                    Clase = new
                    {
                        ClaseName = p.Clase.Nombre,
                        Id = p.ClaseId
                    },
                    Materia = new
                    {
                        Id = p.MateriaId,
                        Materia = p.Materia.Nombre_Materia
                    }






                });


            return Ok(query);

        }

        [HttpGet]
        [Route("{idMateria:int}")]
        public async Task<ActionResult> GetByIdMateria(int idMateria)
        {
            var username = User.Identity.Name;
            try
            {
                List<MateriaLista> query = (List<MateriaLista>)await _materiaListaService.GetDetalleClaseByIdMateriaAndUsername(username, idMateria);



                var result = new ClaseDetallesDTO();

                result.MateriaId = query[0].MateriaId;
                result.ClaseId = query[0].ClaseId;
                result.IdProfesores = query.ConvertAll(q => q.ProfesorId);

                return Ok(result);
            }
            catch (FileNotFoundException)
            {

            }
            return NotFound();


        }

        [HttpDelete]
        [Route("{idmateria:int}/{idclase:int}")]
        public async Task<ActionResult> Delete(int idmateria, int idclase)
        {
            var username = User.Identity.Name;
            var listas = _materiaListaService.FilterByIdMateriaAndUserAndClass(idmateria, username, idclase);

            foreach (var item in listas)
            {
                item.Deleted = true;
                item.ModifiedBy = username;
                item.Modified = DateTime.Now;
                _materiaListaService.Edit(item);
            }

            await _materiaListaService.Save();
            return Ok();
        }

        [HttpPut]
        [Route("{idClase:int}")]
        public async Task<ActionResult> Put(int idClase, [FromBody] ClaseMateriaEditDTO dto)
        {

            if (idClase != dto.IdClase)
            {
                return BadRequest();
            }
            try
            {
                var clase = await _claseService.FindById(idClase);

                var detalles = new Collection<MateriaLista>();

                foreach (var item in dto.IdProfesores.Distinct())
                {
                    var p = await _profesorService.FindById(item);

                    if (p == null) { continue; }

                    var detalle = _materiaListaService.Find
                        (p =>
                            p.MateriaId == dto.IdMateria &&
                            item == p.ProfesorId &&
                            dto.IdClase == p.ClaseId
                        );

                    if (detalle == null)
                    {
                        detalle = new MateriaLista
                        {
                            ClaseId = dto.IdClase,
                            MateriaId = dto.IdMateria,
                            ProfesorId = item
                        };
                    }
                    else
                    {
                        detalle.ClaseId = dto.IdClase;
                        detalle.MateriaId = dto.IdMateria;
                        detalle.ProfesorId = item;
                    }

                    detalles.Add(detalle);

                }


                clase.MateriaListas = detalles;



                _claseService.Edit(clase);
                await _claseService.Save();
                return Ok();
            }
            catch (FileNotFoundException e)
            {
                return NotFound();
            }


        }

    }




}
