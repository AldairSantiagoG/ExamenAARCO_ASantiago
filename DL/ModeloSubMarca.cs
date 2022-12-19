using System;
using System.Collections.Generic;

namespace DL;

public partial class ModeloSubMarca
{
    public byte IdModeloSubMarca { get; set; }

    public int? YearModeloSubMarca { get; set; }

    public virtual ICollection<Descripcion> Descripcions { get; } = new List<Descripcion>();
}
