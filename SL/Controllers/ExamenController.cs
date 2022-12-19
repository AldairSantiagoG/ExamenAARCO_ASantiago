using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamenController : ControllerBase
    {
        // GET: api/<ExamenController>
        [HttpGet]
        [Route("/GetMarcas")]
        public IActionResult GetAllMarcas()
        {
            ML.Result result = BL.Marca.GetAll();
            if(result.Correct)
            {
               return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet]
        [Route("/GetSubMarcas/")]
        public IActionResult GetAllSubMarca(int idMarca)
        {
            ML.Result result = BL.SubMarca.GetByIdMarca(idMarca);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpGet]
        [Route("/GetModelo/")]
        public IActionResult GetAllModelo(int idSubMarca)
        {
            ML.Result result = BL.Descripcion.GetByIdSubMarca(idSubMarca);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpPost]
        [Route("/GetDescripcion/")]
        public IActionResult GetAllDescripcion([FromBody] ML.Descripcion descripcion)
        {
            ML.Result result = BL.Descripcion.GetBySubModeloMarca(descripcion.SubMarca.IdSubMarca,descripcion.ModeloSubMarca.IdModeloSubMarca);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
       
    }
}
