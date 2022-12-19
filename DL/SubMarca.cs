using System;
using System.Collections.Generic;

namespace DL;

public partial class SubMarca
{
    public int IdSubMarca { get; set; }

    public string? NombreSubMarca { get; set; }

    public int? IdMarca { get; set; }
    public string? NombreMarca { get; set; }

    public virtual ICollection<Descripcion> Descripcions { get; } = new List<Descripcion>();

    public virtual Marca? IdMarcaNavigation { get; set; }
}
