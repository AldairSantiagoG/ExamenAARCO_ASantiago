using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BL
{
    public class Descripcion
    {
        public static ML.Result Add(ML.Descripcion descripcion)
        {
            ML.Result result = new ML.Result();
            DL.Descripcion descripcionMap = new DL.Descripcion();

            descripcionMap.IdDescripcion = descripcion.IdDescripcion;
            descripcionMap.IdModeloSubMarca = Convert.ToByte( descripcion.ModeloSubMarca.IdModeloSubMarca);
            descripcionMap.IdSubMarca = descripcion.SubMarca.IdSubMarca;
            descripcionMap.IdCatalogoDescripcion = descripcion.CatalogoDescripcion.IdCatalogoDescripcion;

            try
            {
                using (DL.AsantiagoExamenAarcoContext context = new DL.AsantiagoExamenAarcoContext())
                {

                    var query = context.Database.ExecuteSqlRaw($"DescripcionAdd '{descripcionMap.IdDescripcion}','{descripcionMap.IdModeloSubMarca}','{descripcionMap.IdCatalogoDescripcion}','{descripcionMap.IdSubMarca}'");

                    if (query > 0)
                    {
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
        public static ML.Result GetById(ML.Descripcion descripcion)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AsantiagoExamenAarcoContext context = new DL.AsantiagoExamenAarcoContext())
                {
                    var getById = context.Descripcions.FromSqlRaw($"DescripcionGetById '{descripcion.SubMarca.Marca.IdMarca}','{descripcion.SubMarca.IdSubMarca}','{descripcion.ModeloSubMarca.IdModeloSubMarca}','{descripcion.CatalogoDescripcion.IdCatalogoDescripcion}'").AsEnumerable().FirstOrDefault();

                    if (getById != null)
                    {
                        ML.Descripcion descripcionResult = new ML.Descripcion();
                        descripcionResult.IdDescripcion = getById.IdDescripcion;
                       

                        result.Object = descripcionResult;
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
        public static ML.Result GetByIdSubMarca(int idSubMarca)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AsantiagoExamenAarcoContext context = new DL.AsantiagoExamenAarcoContext())
                {

                    var getAll = context.Descripcions.FromSqlRaw($"YearGetByIdSubMarca '{idSubMarca}'").ToList();
                    //var gt = context.Descripcions.Join(context.CatalogoDescripcions,i=>i,o=>o,(i,o)=>i).
                    //    Where(x => x.IdSubMarca == idSubMarca).ToList().Distinct();
                    getAll.Distinct();
                    result.Objects = new List<object>();

                    if (getAll != null)
                    {
                        foreach (var obj in getAll)
                        {
                            ML.Descripcion descripcion = new ML.Descripcion();
                            descripcion.IdDescripcion = obj.IdDescripcion;
                            descripcion.ModeloSubMarca = new ML.ModeloSubMarca();
                            descripcion.ModeloSubMarca.IdModeloSubMarca = obj.IdModeloSubMarca.Value;
                            descripcion.ModeloSubMarca.YearModeloSubMarca = obj.YearModeloSubMarca.Value;
                            descripcion.SubMarca = new ML.SubMarca();
                            descripcion.SubMarca.IdSubMarca = obj.IdSubMarca.Value;
                            //descripcion.CatalogoDescripcion = new ML.CatalogoDescripcion();
                            //descripcion.CatalogoDescripcion.IdCatalogoDescripcion = obj.IdCatalogoDescripcion.Value;

                            result.Objects.Add(descripcion);
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
        public static ML.Result GetBySubModeloMarca(int idSubMarca,int idModelo)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AsantiagoExamenAarcoContext context = new DL.AsantiagoExamenAarcoContext())
                {

                    var getAll = context.Descripcions.FromSqlRaw($"GetBySubModeloMarca '{idSubMarca}','{idModelo}'").ToList();

                    result.Objects = new List<object>();

                    if (getAll != null)
                    {
                        foreach (var obj in getAll)
                        {
                            ML.Descripcion descripcion = new ML.Descripcion();
                            descripcion.CatalogoDescripcion = new ML.CatalogoDescripcion();
                            descripcion.CatalogoDescripcion.IdCatalogoDescripcion = obj.IdCatalogoDescripcion.Value;
                            descripcion.CatalogoDescripcion.NombreDescripcion = obj.NombreDescripcion;

                           descripcion.ModeloSubMarca = new ML.ModeloSubMarca();
                            descripcion.ModeloSubMarca.IdModeloSubMarca = obj.IdModeloSubMarca.Value;
                            descripcion.ModeloSubMarca.YearModeloSubMarca = obj.YearModeloSubMarca.Value;

                            result.Objects.Add(descripcion);
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
    }
}
