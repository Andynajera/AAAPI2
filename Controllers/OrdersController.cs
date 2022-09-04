
using Microsoft.AspNetCore.Mvc;
using Product.Data;
using Models;
using Microsoft.EntityFrameworkCore;
namespace OrdersController;


[ApiController]
[Route("[Controller]")]

public class OrdersController : ControllerBase
{

    private readonly DataContext _context;

    public OrdersController(DataContext dataContext)
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
    public ActionResult<List<Order>> Get()
    {
        List<Order> orders = _context.Orders.Include(order=>order.orderItems).ToList();
        return Ok(orders);
    }

    /// <summary>
    /// añadir productos
    /// </summary>
    /// <returns>Todos los productos</returns>
    /// <response code="201">Se ha creado correctamente</response>
    /// <response code="500">Si hay algún error</response>
    [HttpPost]
    public ActionResult<Order> Post([FromBody] Order order)
    {
        order.id=0;
        _context.Orders.Add(order);
        _context.SaveChanges();

        string resourceUrl = Request.Path.ToString() + "/" + order.id;
        return Created(resourceUrl, order);
    }

    /// <summary>
    ///Actualizar los productos
    /// </summary>
    /// <returns>Todos los productos</returns>
    /// <response code="201">Devuelve el listado de productos</response>
    /// <response code="500">Si hay algún error</response>
    
    [HttpPut("{id:int}")]
    public ActionResult<Order> Update([FromBody] Order order, int id)
    {
        
        Order orderToUpdate = _context.Orders.Find(id);
        if (orderToUpdate == null)
        {
            return NotFound("La orden no ha sido encontrada");
        }
       orderToUpdate.id=id;
       orderToUpdate.orderItems=order.orderItems;
        _context.SaveChanges();
        string resourceUrl = Request.Path.ToString();

        return Created(resourceUrl, orderToUpdate);
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
        Order orderToDelete = _context.Orders.Where(order=>order.id==id).Include(order=>order.orderItems).FirstOrDefault();
        if (orderToDelete == null)
        {
            return NotFound("order no encontrada");
        }
        _context.Orders.Remove(orderToDelete);
        _context.SaveChanges();
        return Ok(orderToDelete);
    }

}
