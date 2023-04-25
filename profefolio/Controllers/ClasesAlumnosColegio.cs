using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using profefolio.Repository;

namespace profefolio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClasesAlumnosColegio : ControllerBase
    {
        private IClasesAlumnosColegio _clasesAlumnosColegioService;
        private IMapper _mapper;
        public ClasesAlumnosColegio(IMapper mapper, IClasesAlumnosColegio clasesAlumnosColegio)
        {
            _mapper = mapper;
            _clasesAlumnosColegioService = clasesAlumnosColegio;
        }
    }
}