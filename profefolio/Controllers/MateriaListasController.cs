using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using profefolio.Models.DTOs.ClaseMateria;
using profefolio.Models.Entities;
using profefolio.Repository;
using System.Runtime.CompilerServices;
using System.Transactions;

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
        public MateriaListasController(IMateriaLista materiaListaService, IPersona profesorService, IMateria materiaService)
        {
            _materiaListaService = materiaListaService;
            _profesorService = profesorService;
            _materiaService = materiaService;
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

                if(p == null) { continue; }
                await _materiaListaService.Add(new MateriaLista
                {
                    ClaseId = dto.IdClase,
                    ProfesorId = profes,
                    MateriaId = dto.IdMateria,
                    Created  = DateTime.Now,
                    CreatedBy = username
                });
            }

            await _materiaListaService.Save();

            return Ok();
        }


        //Este metodo GET NO SE DEBE IMPLEMENTAR EN EL FRONT-ENT, es con fines de Testing
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



            return Ok();
        }

        [HttpDelete]
        [Route("{idmateria:int}/{idclase:int}")]
        public async Task<ActionResult> Delete(int idmateria, int idclase)
        {
            var username = User.Identity.Name;
            var listas = _materiaListaService.FilterByIdMateriaAndUserAndClass(idmateria, username, idclase);

            foreach(var item in listas)
            {
                item.Deleted = true;
                item.ModifiedBy = username;
                item.Modified = DateTime.Now;
                _materiaListaService.Edit(item);
            }

            await _materiaListaService.Save();
            return Ok();
        }

    }
}
