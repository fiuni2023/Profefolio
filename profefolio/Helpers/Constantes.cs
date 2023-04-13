using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Helpers
{
    public static class Constantes
    {
        public static IConfiguration JsonConfig { get; private set; }
        public static int CANT_ITEMS_POR_PAGE { get; private set; }

        static Constantes()
        {

            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json");

            var json = builder.Build();
            
            // se obtienen los valores del json
            CANT_ITEMS_POR_PAGE = Int16.Parse(json["CantidadElementoPorPagina"]);
            

        }
    }
}

