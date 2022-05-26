
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
        List<ProductItems> product = _context.ProductI.ToList();
        return product == null? NoContent()
            : Ok(product);
    }



    [HttpPost]
    public ActionResult<ProductItems> Post([FromBody] ProductItems product)
    {
         ProductItems existingProductItems= _context.ProductI.Find(product.id);
        if (existingProductItems != null)
        {
            return Conflict("Ya existe un elemento ");
        }
        _context.ProductI.Add(product);
        _context.SaveChanges();

        string resourceUrl = Request.Path.ToString() + "/" + product.id;
        return Created(resourceUrl, product);
    }

    [HttpPut("{id:int}")]
    public ActionResult<ProductItems> Update([FromBody] ProductItems product, int id)
    {
        ProductItems productItemToUpdate = _context.ProductI.Find(id);
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
        ProductItems productItemToDelete = _context.ProductI.Find(id);
        if (productItemToDelete == null)
        {
            return NotFound("Elemento del producto no encontrado");
        }
        _context.ProductI.Remove(productItemToDelete);
        _context.SaveChanges();
        return Ok(productItemToDelete);
    }

}
