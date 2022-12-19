using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Marca
    {
        public static ML.Result Add(ML.Marca marca)
        {
            ML.Result result = new ML.Result();
            DL.Marca marcaMap = new DL.Marca();
            marcaMap.NombreMarca = marca.NombreMarca;
             
            try
            {
                using (DL.AsantiagoExamenAarcoContext context = new DL.AsantiagoExamenAarcoContext())
                {
                    
                   context.Marcas.Add(marcaMap);

                   context.SaveChanges();
                    

                    if (marcaMap.IdMarca > 0)
                    {
                        result.Object = marcaMap.IdMarca;
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
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AsantiagoExamenAarcoContext context = new DL.AsantiagoExamenAarcoContext())
                {
                    
                    var getAll = context.Marcas.FromSqlRaw("MarcaGetAll").ToList();

                    result.Objects = new List<object>();

                    if (getAll != null)
                    {
                        foreach (var obj in getAll)
                        {
                            ML.Marca marca = new ML.Marca();

                            marca.IdMarca = obj.IdMarca;
                            marca.NombreMarca = obj.NombreMarca;
                            
                            result.Objects.Add(marca);
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
        public static ML.Result MarcaGetNombre(string nombreMarca)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AsantiagoExamenAarcoContext context = new DL.AsantiagoExamenAarcoContext())
                {
                    var getNombre = context.Marcas.FromSqlRaw($"MarcaGetNombre '{nombreMarca}'").AsEnumerable().FirstOrDefault();
                    
                    if (getNombre != null)
                    {
                        ML.Marca marca = new ML.Marca();
                        marca.IdMarca = getNombre.IdMarca;
                        marca.NombreMarca = getNombre.NombreMarca;

                        result.Object = marca;
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
