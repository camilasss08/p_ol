using Backend.Data; //Avisa que utiliza el espacio de nombres Backend.Data, que contiene la clase AppDbContext, que se utiliza para interactuar con la base de datos.
using Backend.Modelos;
using Microsoft.AspNetCore.Mvc; //Avisa que utiliza el framework de ASP.NET Core para crear controladores y manejar solicitudes HTTP.
using Microsoft.EntityFrameworkCore;
namespace Backend.Controllers
{
    [ApiController] //Avisa que la clase es un controlador de API y que maneja solicitudes HTTP y devuelve respuestas HTTP.
    [Route("api/[controller]")]//Define la ruta base para las solicitudes HTTP que se envían a este controlador. En este caso, la ruta base es "api/categoria", donde "categoria" es el nombre del controlador.
    public class CategoriaController : ControllerBase //define la clase del controlador, que hereda de ControllerBase, lo que le permite manejar solicitudes HTTP y devolver respuestas HTTP.
    {
        private readonly AppDbContext _context;//Representa el acceso a la base de datos y se utiliza para realizar operaciones CRUD en la tabla de categorías.
        public CategoriaController(AppDbContext context) //Constructor de la clase CategoriaController, que recibe una instancia de AppDbContext como parámetro y la asigna a la variable _context. Esto permite que el controlador acceda a la base de datos.
        {
            _context = context;//guarda la instancia de AppDbContext en la variable _context para que pueda ser utilizada en los métodos del controlador.
            
        }
        [HttpGet]//nos especifica que este método maneja solicitudes HTTP GET, lo que significa que se utiliza para obtener datos del servidor.
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
        {
            return await _context.Categoria.ToListAsync();//Devuelve una lista de todas las categorías en la base de datos. Utiliza el método ToListAsync() para obtener los datos de forma asincrónica.

        }

        //Ahora va a devolver una lista con todas las categorías que hay en la base de datos, para eso se utiliza el método ToListAsync() 

    }
}