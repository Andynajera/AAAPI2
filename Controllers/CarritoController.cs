
using Microsoft.AspNetCore.Mvc;
using Models;
using Product.Data;

namespace CarritoController;





[ApiController]
[Route("[Controller]")]

public class CarritoController : ControllerBase
{

    private readonly DataContext _context;

    public CarritoController(DataContext dataContext)
    {
        _context = dataContext;
    }

    /// <summary>
    /// Mostrar todos los productos
    /// </summary>
    /// <returns>Todos los productos</returns>
    /// <response code="200">Devuelve el listado de productos</response>
    /// <response code="500">Si hay algún error</response>
    [HttpGet]
   public ActionResult<List<CarritoItem>> Get()
    {
        List<CarritoItem> carritos = _context.CarritoItems.ToList();
        return   Ok(carritos);
    }

    [HttpGet]
    [Route("{id:int}")]
    public ActionResult<CarritoItem> Get(int id)
    {
    CarritoItem CarritoItem = _context.CarritoItems.Find(id);
        return CarritoItem == null? NotFound()
            : Ok(CarritoItem);
    }
    /*
    [HttpGet]
    public ActionResult<List<CarritoItems>> Get()
    {
        List<CarritoItems> carrito = _context.CarritoItem.ToList();
        return carrito == null? NoContent()
            : Ok(carrito);
    }
*/

    /// <summary>
    /// añadir productos
    /// </summary>
    /// <returns>Todos los productos</returns>
    /// <response code="201">Se ha creado correctamente</response>
    /// <response code="500">Si hay algún error</response>
    [HttpPost]
    public ActionResult<CarritoItem> Post([FromBody] CarritoItem carrito)
    {
     /*    CarritoItems existingCarritoItems= _context.CarritItem.Find(carrito.id);
        if (existingCarritoItems != null)
        {
            return Conflict("Ya existe un elemento ");
        }*/
        carrito.id=0;
        _context.CarritoItems.Add(carrito);
        _context.SaveChanges();

        string resourceUrl = Request.Path.ToString() + "/" + carrito.id;
        return Created(resourceUrl, carrito);
    }
    /// <summary>
    ///Actualizar los productos
    /// </summary>
    /// <returns>Todos los productos</returns>
    /// <response code="201">Devuelve el objeto modificado</response>
    /// <response code="404">No encontrado</response>
    /// <response code="500">Si hay algún error</response>
    [HttpPut("{id:int}")]
    public ActionResult<CarritoItem> Update([FromBody] CarritoItem carrito, int id)
    {
        CarritoItem carritoItemToUpdate = _context.CarritoItems.Find(id);
        if (carritoItemToUpdate == null)
        {
            return NotFound("Producto del carrito no encontrado");
        }
        carritoItemToUpdate.precioTotal = carrito.precioTotal;
        carritoItemToUpdate.productId = carrito.productId;

        _context.SaveChanges();
        string resourceUrl = Request.Path.ToString();

        return Created(resourceUrl, carritoItemToUpdate);
    }
        /// <summary>
    /// Eliminar productos seleccionados
    /// </summary>
    /// <returns>Todos los productos</returns>
    /// <response code="200">Se ha eliminado</response>
    /// <response code="500">Si hay algún error</response>
        [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        CarritoItem carritoItemToDelete = _context.CarritoItems.Find(id);
        if (carritoItemToDelete == null)
        {
            return NotFound("Producto del carrito no encontrado");
        }
        _context.CarritoItems.Remove(carritoItemToDelete);
        _context.SaveChanges();
        return Ok();
    }

}
