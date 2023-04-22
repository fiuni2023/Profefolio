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
                .ConvertAll(p => new MateriaListDTO
                {
                    Id = p.Id,
                    IdProfesor = p.Profesor.Email,
                    Clase = p.Clase.Nombre,
                    Materia = p.Materia.Nombre_Materia,
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
                result.Profes = query.ConvertAll(q => q.ProfesorId);

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
            try
            {
                var clase = await _claseService.FindById(idClase);

                var detalles = new Collection<MateriaLista>();         
                
                foreach(var item in dto.IdListas.DistinctBy(m => m.IdProfesor))
                {
                    var detalle = new MateriaLista
                    {
                        Id = item.IdDetalle ,
                        ClaseId = dto.IdClase,
                        MateriaId = dto.IdMateria,
                        ProfesorId = item.IdProfesor
                    };

                    detalles.Add(detalle);
                }

                clase.MateriaListas = Merge(clase.MateriaListas, detalles);

                
                
                 _claseService.Edit(clase);
                return Ok();
            } catch(FileNotFoundException e)
            {
                return NotFound();
            }


        }

        private ICollection<MateriaLista> Merge(ICollection<MateriaLista> old, ICollection<MateriaLista> nuevo)
        {
            var union = old.Concat(nuevo);

            // Agrupación por Id y selección del elemento más reciente
            var merge = union.GroupBy(p => p.Id)
                             .Select(g => g.OrderByDescending(p => p.Deleted)
                                          .ThenByDescending(p => p == nuevo.FirstOrDefault(e => e.Id == p.Id))
                                          .First());

            // Actualización del arreglo1 con los valores de merge
            var result = old.Select(p =>
            {
                var m = merge.FirstOrDefault(e => e.Id == p.Id);
                return m != null ? m : new MateriaLista
                {
                    Id = p.Id,
                    ClaseId = p.ClaseId,
                    MateriaId = p.MateriaId,
                    ProfesorId = p.ProfesorId
                };
            }).ToArray();



            return (ICollection<MateriaLista>)result;
        }

    }




}
