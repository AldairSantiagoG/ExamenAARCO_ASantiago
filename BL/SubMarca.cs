using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class SubMarca
    {
        public static ML.Result Add(ML.SubMarca subMarca)
        {
            ML.Result result = new ML.Result();
            //DL.SubMarca subMarcaMap = new DL.SubMarca();
            
            //subMarcaMap.NombreSubMarca = subMarca.NombreSubMarca;
            //subMarcaMap.IdMarca = subMarca.Marca.IdMarca;
            //subMarcaMap.NombreMarca = subMarca.Marca.NombreMarca;
            try
            {
                using (DL.AsantiagoExamenAarcoContext context = new DL.AsantiagoExamenAarcoContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"SubMarcaAdd '{subMarca.NombreSubMarca}','{subMarca.Marca.IdMarca}'");
                   
                    if (query > 0)
                    {
                        result = BL.SubMarca.GetNombre(subMarca.NombreSubMarca);
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se inserto el registro";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        public static ML.Result GetByIdMarca(int idMarca)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AsantiagoExamenAarcoContext context = new DL.AsantiagoExamenAarcoContext())
                {

                    var getByMarca = context.SubMarcas.FromSqlRaw($"SubMarcaGetByIdMarca '{idMarca}'").ToList();

                    result.Objects = new List<object>();

                    if (getByMarca != null)
                    {
                        foreach (var obj in getByMarca)
                        {
                            ML.SubMarca subMarca = new ML.SubMarca();

                            subMarca.IdSubMarca = obj.IdSubMarca;
                            subMarca.NombreSubMarca = obj.NombreSubMarca;
                            subMarca.Marca = new ML.Marca();
                            subMarca.Marca.IdMarca = obj.IdMarca.Value;
                            subMarca.Marca.NombreMarca = obj.NombreMarca;

                            result.Objects.Add(subMarca);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
            }

            return result;
        }
        public static ML.Result GetNombre(string nombreSubMarca)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AsantiagoExamenAarcoContext context = new DL.AsantiagoExamenAarcoContext())
                {
                    var getNombre = context.SubMarcas.FromSqlRaw($"SubMarcaGetNombre '{nombreSubMarca}'").AsEnumerable().FirstOrDefault();

                    if (getNombre != null)
                    {
                        ML.SubMarca submarca = new ML.SubMarca();
                        submarca.IdSubMarca = getNombre.IdSubMarca;
                        submarca.NombreSubMarca = getNombre.NombreSubMarca;
                        submarca.Marca = new ML.Marca();
                        submarca.Marca.IdMarca = getNombre.IdMarca.Value;

                        result.Object = submarca;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
            }

            return result;
        }
    }
}
