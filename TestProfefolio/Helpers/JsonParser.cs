using Newtonsoft.Json;
using profefolio.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace TestProfefolio.Helpers
{
    public class JsonParser<T> where T : class
    {
        private string _path;
        public JsonParser( string path) 
        {
            _path = path;
        }

        public IEnumerable<T> ToIEnumerable()
        {
            var text = File.ReadAllText(_path);

            return JsonConvert.DeserializeObject<IEnumerable<T>>(text);
        }
    }
}
