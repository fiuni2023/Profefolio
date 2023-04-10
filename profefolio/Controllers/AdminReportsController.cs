using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using profefolio.Models.DTOs;
using profefolio.Models.DTOs.Colegio;
using profefolio.Repository;

namespace profefolio.Controllers
{
    [Route("api/administrador")]
    [ApiController]
    [Authorize(Roles ="Master")]
    public class AdminReportsController : ControllerBase
    {
        private readonly IAdmin _adminReportService;
        private readonly IMapper _mapper;

        public AdminReportsController(IAdmin adminReportService, IMapper mapper)
        {
            _adminReportService = adminReportService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("page/{page}/{band}")]
        public async Task<ActionResult<DataListDTO<ColegioFullDTO>>> Get(int page, bool band)
        {
            return Ok();
        }
    }
}
