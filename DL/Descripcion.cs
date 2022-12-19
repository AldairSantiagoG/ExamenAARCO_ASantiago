using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DL;

public partial class Descripcion
{
   
    public string IdDescripcion { get; set; } = null!;

    public byte? IdModeloSubMarca { get; set; }

    public int? IdCatalogoDescripcion { get; set; }

    public int? IdSubMarca { get; set; }
    public string? NombreDescripcion { get; set; }
    public int? YearModeloSubMarca { get; set; }


    public virtual CatalogoDescripcion? IdCatalogoDescripcionNavigation { get; set; }

    public virtual ModeloSubMarca? IdModeloSubMarcaNavigation { get; set; }

    public virtual SubMarca? IdSubMarcaNavigation { get; set; }
}
