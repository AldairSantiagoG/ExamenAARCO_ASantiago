using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Descripcion
    {
        public string IdDescripcion { get; set; }
        public ML.ModeloSubMarca ModeloSubMarca { get; set; }
        public ML.CatalogoDescripcion CatalogoDescripcion { get; set; }
        public ML.SubMarca SubMarca { get; set; }
        public List<Descripcion> Descripciones { get; set; }
    }
}
