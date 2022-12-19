using System;
using System.Collections.Generic;

namespace DL;

public partial class Marca
{
    public int IdMarca { get; set; }

    public string? NombreMarca { get; set; }

    public virtual ICollection<SubMarca> SubMarcas { get; } = new List<SubMarca>();
}
