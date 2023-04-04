using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using profefolio.Models.DTOs;
using profefolio.Models.DTOs.Persona;
using profefolio.Models.DTOs.Colegio;
using profefolio.Models.Entities;
using profefolio.Repository;
/**
* Controlador que devuelve los datos del colegio y los datos del administrador en la misma petición.
* 
**/

namespace profefolio.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Master")]
    [ApiController]
    public class ColegiosFullController : ControllerBase
    {
        private readonly IFullColegio _colegioService;
        private readonly int _cantPorPag = 10;
        private readonly IMapper _mapper;
        public ColegiosFullController(IFullColegio colegioService, IMapper mapper)
        {
            _colegioService = colegioService;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("page/{page}")]
        public ActionResult<DataListDTO<Colegio>> GetColegios(int page)
        {
            var query = _colegioService.GetAll(page, _cantPorPag);
            int totalPage = (int)Math.Ceiling((double)_colegioService.Count() / _cantPorPag);

            var result = new DataListDTO<ColegioFullDTO>();

            var enumerable = query as Colegio[] ?? query.ToArray();
            result.CantItems = enumerable.Length;
            result.CurrentPage = page > totalPage ? totalPage : page;
            result.Next = result.CurrentPage + 1 < totalPage;
            //result.DataList = _mapper.Map<List<ColegioDTO>>(enumerable.ToList());
            result.DataList = _mapper.Map<List<ColegioFullDTO>>(enumerable.ToList());
            result.TotalPage = totalPage;

            return Ok(result);
        }

        // GET: api/Colegios/1
        /**
        * Devuelve los datos del colegio y los datos de persona{nombre,apellido,edad,id}
        **/
        [HttpGet("{id}")]
        public async Task<ActionResult<Colegio>> GetColegio(int id)
        {
            //var colegio = await _colegioService.FindById(id);
            var colegio = await _colegioService.FindById(id);
            
            if (colegio == null)
            {
               
                return NotFound();
            }
            var dtos = _mapper.Map<ColegioFullDTO>(colegio);
            //var response = _mapper.Map<ColegioFullDTO>(colegio);
            return Ok(dtos);
        }
    }
}