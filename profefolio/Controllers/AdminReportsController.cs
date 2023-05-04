using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using profefolio.Helpers;
using profefolio.Models.DTOs;
using profefolio.Models.DTOs.Persona;

namespace profefolio
{
    [ApiController]
    [Authorize(Roles = "Master")]
    [Route("api/administrador")]
    public class AdminReportsController : ControllerBase
    {
        private readonly IAdmin _adminReportService;
        private readonly IMapper _mapper;
        private static int CantPerPage => Constantes.CANT_ITEMS_POR_PAGE;
        private const string RolAdmin = "Administrador de Colegio";

        public AdminReportsController(IAdmin adminReportService, IMapper mapper)
        {
            _adminReportService = adminReportService;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("admin/assigned/{page:int}")]
        public async Task<ActionResult<DataListDTO<PersonaResultDTO>>> GetAssigned(int page)
        {

            
            var query = await _adminReportService.GetPersonasConColegio(CantPerPage, page);


            var cantPages = (int)Math.Ceiling((double)await _adminReportService.Count(true) / CantPerPage);


            var result = new DataListDTO<PersonaResultDTO>();

            if (page >= cantPages || page < 0)
            {
                return BadRequest($"No existe la pagina: {page} ");
            }

            var enumerable = query.ToArray();
            result.CantItems = enumerable.Length;
            result.CurrentPage = page;
            result.Next = result.CurrentPage + 1 < cantPages;
            result.DataList = _mapper.Map<List<PersonaResultDTO>>(enumerable.ToList());
            result.TotalPage = cantPages;

            return Ok(result);
        }

        [HttpGet]
        [Route("admin/not/assigned/{page:int}")]
        public async Task<ActionResult<DataListDTO<PersonaResultDTO>>> GetNotAssigned(int page)
        {

            
            var query = await _adminReportService.GetPersonasSinColegio(CantPerPage, page);


            var cantPages = (int)Math.Ceiling((double)await _adminReportService.Count(false) / CantPerPage);


            var result = new DataListDTO<PersonaResultDTO>();

            if (page >= cantPages || page < 0)
            {
                return BadRequest($"No existe la pagina: {page} ");
            }

            var enumerable = query.ToArray();
            result.CantItems = enumerable.Length;
            result.CurrentPage = page;
            result.Next = result.CurrentPage + 1 < cantPages;
            result.DataList = _mapper.Map<List<PersonaResultDTO>>(enumerable.ToList());
            result.TotalPage = cantPages;

            return Ok(result);
        }




        


    }
}