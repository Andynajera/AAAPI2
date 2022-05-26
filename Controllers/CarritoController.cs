
using Microsoft.AspNetCore.Mvc;
using CarritoItem;
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

   /* [HttpGet]
    public ActionResult<List<CarritoItems>> Get(){
    return Ok (_context.CarriItem);
    }   

    [HttpGet]
    [Route("{id}")]
    public ActionResult<CarritoItems> Get(int id)
    {
    var CarritoItems = _context.CarriItem.Find(id);
        return CarritoItems == null? NotFound()
            : Ok(CarritoItems);
    }*/
    [HttpGet]
    public ActionResult<List<CarritoItems>> Get()
    {
        List<CarritoItems> carrito = _context.CarritoItem.ToList();
        return carrito == null? NoContent()
            : Ok(carrito);
    }


    [HttpPost]
    public ActionResult<CarritoItems> Post([FromBody] CarritoItems carrito)
    {
         CarritoItems existingCarritoItems= _context.CarritoItem.Find(carrito.id);
        if (existingCarritoItems != null)
        {
            return Conflict("Ya existe un elemento ");
        }
        _context.CarritoItem.Add(carrito);
        _context.SaveChanges();

        string resourceUrl = Request.Path.ToString() + "/" + carrito.id;
        return Created(resourceUrl, carrito);
    }

    [HttpPut("{id:int}")]
    public ActionResult<CarritoItems> Update([FromBody] CarritoItems carrito, int id)
    {
        CarritoItems carritotItemToUpdate = _context.CarritoItem.Find(id);
        if (carritotItemToUpdate == null)
        {
            return NotFound("Elemento del carrito no encontrado");
        }
        carritotItemToUpdate.cantidad = carrito.cantidad;
        carritotItemToUpdate.IdCarrito = carrito.IdCarrito;
        _context.SaveChanges();
        string resourceUrl = Request.Path.ToString() + "/" + carrito.id;

        return Created(resourceUrl, carrito);
    }
        [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        CarritoItems carritotItemToDelete = _context.CarritoItem.Find(id);
        if (carritotItemToDelete == null)
        {
            return NotFound("Elemento del carrito no encontrado");
        }
        _context.CarritoItem.Remove(carritotItemToDelete);
        _context.SaveChanges();
        return Ok(carritotItemToDelete);
    }

}
