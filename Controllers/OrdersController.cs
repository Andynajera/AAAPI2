
using Microsoft.AspNetCore.Mvc;
using Product.Data;
using Product;
using OrderItem;

namespace ProductsController;


[ApiController]
[Route("[Controller]")]

public class OrdersController : ControllerBase
{

    private readonly DataContext _context;

    public OrdersController(DataContext dataContext)
    {
        _context = dataContext;
    }


    [HttpGet]
    public ActionResult<List<OrderItems>> Get()
    {
        List<OrderItems> product = _context.OrdeItem.ToList();
        return product == null? NoContent()
            : Ok(product);
    }


    [HttpPost]
    public ActionResult<OrderItems> Post([FromBody] OrderItems product)
    {
         OrderItems existingOrderItems= _context.OrdeItem.Find(product.id);
        if (existingOrderItems != null)
        {
            return Conflict("Ya existe un elemento ");
        }
        _context.OrdeItem.Add(product);
        _context.SaveChanges();

        string resourceUrl = Request.Path.ToString() + "/" + product.id;
        return Created(resourceUrl, product);
    }

    [HttpPut("{id:int}")]
    public ActionResult<OrderItems> Update([FromBody] OrderItems product, int id)
    {
        OrderItems orderItemToUpdate = _context.OrdeItem.Find(id);
        if (orderItemToUpdate == null)
        {
            return NotFound("Elemento del order no encontrado");
        }
        orderItemToUpdate.IdOrder = product.IdOrder;
        orderItemToUpdate.cantidad = product.cantidad;
        _context.SaveChanges();
        string resourceUrl = Request.Path.ToString() + "/" + product.id;

        return Created(resourceUrl, product);
    }
        [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        OrderItems orderItemToDelete = _context.OrdeItem.Find(id);
        if (orderItemToDelete == null)
        {
            return NotFound("Elemento del order no encontrado");
        }
        _context.OrdeItem.Remove(orderItemToDelete);
        _context.SaveChanges();
        return Ok(orderItemToDelete);
    }

}
