
using Microsoft.AspNetCore.Mvc;
using Product.Data;
using Product;


namespace ProductsController;


[ApiController]
[Route("[Controller]")]

public class ProductsController : ControllerBase
{

    private readonly DataContext _context;

    public ProductsController(DataContext dataContext)
    {
        _context = dataContext;
    }


    [HttpGet]
    public ActionResult<List<ProductItems>> Get()
    {
        List<ProductItems> product = _context.ProducItem.ToList();
        return product == null? NoContent()
            : Ok(product);
    }



    [HttpPost]
    public ActionResult<ProductItems> Post([FromBody] ProductItems product)
    {
         ProductItems existingProductItems= _context.ProducItem.Find(product.id);
        if (existingProductItems != null)
        {
            return Conflict("Ya existe un elemento ");
        }
        _context.ProducItem.Add(product);
        _context.SaveChanges();

        string resourceUrl = Request.Path.ToString() + "/" + product.id;
        return Created(resourceUrl, product);
    }

    [HttpPut("{id:int}")]
    public ActionResult<ProductItems> Update([FromBody] ProductItems product, int id)
    {
        ProductItems productItemToUpdate = _context.ProducItem.Find(id);
        if (productItemToUpdate == null)
        {
            return NotFound("Elemento del producto no encontrado");
        }
        productItemToUpdate.name = product.name;
        productItemToUpdate.id = product.id;
        _context.SaveChanges();
        string resourceUrl = Request.Path.ToString() + "/" + product.id;

        return Created(resourceUrl, product);
    }
        [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        ProductItems productItemToDelete = _context.ProducItem.Find(id);
        if (productItemToDelete == null)
        {
            return NotFound("Elemento del producto no encontrado");
        }
        _context.ProducItem.Remove(productItemToDelete);
        _context.SaveChanges();
        return Ok(productItemToDelete);
    }

}
