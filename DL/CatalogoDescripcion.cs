using System;
using System.Collections.Generic;

namespace DL;

public partial class CatalogoDescripcion
{
    public int IdCatalogoDescripcion { get; set; }

    public string? NombreDescripcion { get; set; }

    public virtual ICollection<Descripcion> Descripcions { get; } = new List<Descripcion>();
}
