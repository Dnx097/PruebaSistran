using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaSistran.Models;
using PruebaSistran.Servicios.Services;
using SISTRAN.CAD.Models;
using System.Diagnostics;
using System.Globalization;

namespace PruebaSistran.Controllers
{
    public class HomeController : Controller
    {

        private readonly IPeopleRegister _register;

        public HomeController(IPeopleRegister register)
        {
            _register = register;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            try
            {
                var lista = await _register.GetAll();

        

                return StatusCode(StatusCodes.Status200OK, lista);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
          
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PersonsVM obj)
        {
            Persona persona = new Persona()
            {
                Identificacion = obj.Identificacion,
                Nombres = obj.Nombres,
                Apellidos = obj.Apellidos,
                FechaNacimiento = obj.FechaNacimiento,
                Celular = !string.IsNullOrEmpty(obj.Celular) ? long.Parse(obj.Celular) : (long?)null,
                TelAlternativo = !string.IsNullOrEmpty(obj.TelAlternativo) ? long.Parse(obj.TelAlternativo) : (long?)null,
                Correo = obj.Correo,
                CorreoAlt = obj.CorreoAlt,
                Direccion = obj.Direccion,
                DireccionAlt = obj.DireccionAlt
            };
            bool res = await _register.Insert(persona);
            return StatusCode(StatusCodes.Status200OK, new { valor = res });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PersonsVM obj)
        {
            Persona persona = new Persona()
            {
                Identificacion = obj.Identificacion,
                Nombres = obj.Nombres,
                Apellidos = obj.Apellidos,
                FechaNacimiento = obj.FechaNacimiento,
                Celular = !string.IsNullOrEmpty(obj.Celular) ? long.Parse(obj.Celular) : (long?)null,
                TelAlternativo = !string.IsNullOrEmpty(obj.TelAlternativo) ? long.Parse(obj.TelAlternativo) : (long?)null,
                Correo = obj.Correo,
                CorreoAlt = obj.CorreoAlt,
                Direccion = obj.Direccion,
                DireccionAlt = obj.DireccionAlt
            };
            bool res = await _register.Update(persona);
            return StatusCode(StatusCodes.Status200OK, new { valor = res });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            bool res = await _register.Delete(id);
            return StatusCode(StatusCodes.Status200OK, new { valor = res });
        }

    
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            Persona res = await _register.GetById(id);
            return StatusCode(StatusCodes.Status200OK, new { valor = res });
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
