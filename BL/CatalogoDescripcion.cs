using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class CatalogoDescripcion
    {
        public static ML.Result Add(ML.CatalogoDescripcion catalogoDescripcion)
        {
            ML.Result result = new ML.Result();
            DL.CatalogoDescripcion ctDescripcion = new DL.CatalogoDescripcion();

            ctDescripcion.NombreDescripcion = catalogoDescripcion.NombreDescripcion;

            try
            {
                using (DL.AsantiagoExamenAarcoContext context = new DL.AsantiagoExamenAarcoContext())
                {
                    context.CatalogoDescripcions.Add(ctDescripcion);

                    context.SaveChanges();

                    if (ctDescripcion.IdCatalogoDescripcion > 0)
                    {
                        result.Object = ctDescripcion.IdCatalogoDescripcion;
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
        public static ML.Result GetById(int idCatalogoDescripcion)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AsantiagoExamenAarcoContext context = new DL.AsantiagoExamenAarcoContext())
                {
                    var getCatalogoDescripcion = context.CatalogoDescripcions.FromSqlRaw($"CatalogoDescripcionGetById '{idCatalogoDescripcion}'").AsEnumerable().FirstOrDefault();

                    if (getCatalogoDescripcion != null)
                    {
                        ML.CatalogoDescripcion catalogoDescripcion = new ML.CatalogoDescripcion();
                        catalogoDescripcion.IdCatalogoDescripcion = getCatalogoDescripcion.IdCatalogoDescripcion;
                        catalogoDescripcion.NombreDescripcion = getCatalogoDescripcion.NombreDescripcion;

                        result.Object = catalogoDescripcion;
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
        public static ML.Result GetByNombre(string nombreCatalogoDescripcion)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AsantiagoExamenAarcoContext context = new DL.AsantiagoExamenAarcoContext())
                {
                    var getCatalogoDescripcion = context.CatalogoDescripcions.FromSqlRaw($"CatalogoDescripcionGetNombre '{nombreCatalogoDescripcion}'").AsEnumerable().FirstOrDefault();
                    

                    if (getCatalogoDescripcion != null)
                    {
                        ML.CatalogoDescripcion catalogoDescripcion = new ML.CatalogoDescripcion();
                        catalogoDescripcion.IdCatalogoDescripcion = getCatalogoDescripcion.IdCatalogoDescripcion;
                        catalogoDescripcion.NombreDescripcion = getCatalogoDescripcion.NombreDescripcion;

                        result.Object = catalogoDescripcion;
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
