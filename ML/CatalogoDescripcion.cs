using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class CatalogoDescripcion
    {
        public int IdCatalogoDescripcion { get; set; }
        public string NombreDescripcion { get; set; }
        public List<object> Catalogos { get; set; }
    }
}
