string ubicacionArchivo = "C:\\Users\\digis\\OneDrive\\Documents\\Aldair Santiago Garcia\\ExamenAarco\\PL_CONSOLE\\ArchivoTXT\\ListaDeAutos.txt";

System.IO.StreamReader archivo = new System.IO.StreamReader(ubicacionArchivo);
int bandera = 0,contador=0;
string linea;

while ((linea = archivo.ReadLine()) != null)
{
    if (bandera == 0)
    {
        bandera = 1;
    }
    else
    {
        string[] words = linea.Split('\t');

        ML.Marca marca = new ML.Marca();
        ML.SubMarca subMarca = new ML.SubMarca();
        ML.ModeloSubMarca modeloSubMarca = new ML.ModeloSubMarca();
        ML.CatalogoDescripcion catalogoDescripcion = new ML.CatalogoDescripcion();
        ML.Descripcion descripcion = new ML.Descripcion();

        marca.NombreMarca = words[0];
        subMarca.NombreSubMarca = words[1];
        modeloSubMarca.YearModeloSubMarca = Convert.ToInt16(words[2]);
        catalogoDescripcion.NombreDescripcion = words[3];
        descripcion.IdDescripcion = words[4];

        ML.Result result = BL.Marca.MarcaGetNombre(marca.NombreMarca);
        if (result.Correct)
        {
            subMarca.Marca = (ML.Marca)result.Object;
        }
        else
        {
            result = BL.Marca.Add(marca);
            subMarca.Marca= new ML.Marca();
            subMarca.Marca.IdMarca = ((int)result.Object);
                 
        }

        result = BL.SubMarca.GetNombre(subMarca.NombreSubMarca);

        if (result.Correct)
        {
            descripcion.SubMarca = (ML.SubMarca)result.Object;
        }
        else
        {
            result = BL.SubMarca.Add(subMarca);
            descripcion.SubMarca = new ML.SubMarca();
            descripcion.SubMarca.IdSubMarca = ((ML.SubMarca)result.Object).IdSubMarca;
        }

        result = BL.ModeloSubMarca.GetByYear(modeloSubMarca.YearModeloSubMarca);
        if (result.Correct)
        {
            descripcion.ModeloSubMarca = (ML.ModeloSubMarca)result.Object;
        }
        else
        {
            result = BL.ModeloSubMarca.Add(modeloSubMarca);
            descripcion.ModeloSubMarca = new ML.ModeloSubMarca();
            descripcion.ModeloSubMarca.IdModeloSubMarca = ((byte)result.Object);
        }

        result = BL.CatalogoDescripcion.GetByNombre(catalogoDescripcion.NombreDescripcion);
        if (result.Correct)
        {
            descripcion.CatalogoDescripcion = (ML.CatalogoDescripcion)result.Object;
        }
        else
        {
            result = BL.CatalogoDescripcion.Add(catalogoDescripcion);
            descripcion.CatalogoDescripcion = new ML.CatalogoDescripcion();
            descripcion.CatalogoDescripcion.IdCatalogoDescripcion = ((int)result.Object);
        }

        result = BL.Descripcion.Add(descripcion);
        contador++;
        if (result.Correct)
        {
           
        }
        else
        {
            
            Console.WriteLine("Error en linea " + contador+"\n");
            Console.WriteLine(descripcion.IdDescripcion + "\n");
            Console.WriteLine(descripcion.SubMarca.IdSubMarca + "\n");
            Console.WriteLine(descripcion.ModeloSubMarca.IdModeloSubMarca + "\n");
            Console.WriteLine(descripcion.CatalogoDescripcion.IdCatalogoDescripcion + "\n");
        }


    }

}

Console.WriteLine("FIN DE LA CARGA MASIVA");





