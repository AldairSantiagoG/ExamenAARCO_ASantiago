using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class SubMarca
    {
        public int IdSubMarca { get; set; }
        public string NombreSubMarca { get; set; }
        
        public ML.Marca Marca { get; set; }
        public List<object> SubMarcas { get; set; }


    }
}
