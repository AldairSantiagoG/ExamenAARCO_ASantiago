using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class ModeloSubMarca
    {
        public static ML.Result Add(ML.ModeloSubMarca modeloSubMarca)
        {
            ML.Result result = new ML.Result();
            DL.ModeloSubMarca modeloSubMarcaMap = new DL.ModeloSubMarca();

            modeloSubMarcaMap.YearModeloSubMarca = modeloSubMarca.YearModeloSubMarca;
            
            try
            {
                using (DL.AsantiagoExamenAarcoContext context = new DL.AsantiagoExamenAarcoContext())
                {

                    context.ModeloSubMarcas.Add(modeloSubMarcaMap);

                    context.SaveChanges();


                    if (modeloSubMarcaMap.IdModeloSubMarca > 0)
                    {
                        result.Object = modeloSubMarcaMap.IdModeloSubMarca;
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
        public static ML.Result GetById(int idModeloSubMarca)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AsantiagoExamenAarcoContext context = new DL.AsantiagoExamenAarcoContext())
                {
                    var getById = context.ModeloSubMarcas.FromSqlRaw($"ModeloSubMarcaGetById '{idModeloSubMarca}'").AsEnumerable().FirstOrDefault();

                    if (getById != null)
                    {
                        ML.ModeloSubMarca modeloSubMarca = new ML.ModeloSubMarca();
                        modeloSubMarca.IdModeloSubMarca = getById.IdModeloSubMarca;
                        modeloSubMarca.YearModeloSubMarca = getById.YearModeloSubMarca.Value;

                        result.Object = modeloSubMarca;
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

        public static ML.Result GetByYear(int year)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AsantiagoExamenAarcoContext context = new DL.AsantiagoExamenAarcoContext())
                {
                    var getById = context.ModeloSubMarcas.FromSqlRaw($"ModeloSubMarcaGetYear '{year}'").AsEnumerable().FirstOrDefault();

                    if (getById != null)
                    {
                        ML.ModeloSubMarca modeloSubMarca = new ML.ModeloSubMarca();
                        modeloSubMarca.IdModeloSubMarca = getById.IdModeloSubMarca;
                        modeloSubMarca.YearModeloSubMarca = year;

                        result.Object = modeloSubMarca;
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
