
using Microsoft.AspNetCore.Mvc;
using Product.Data;
using Models;


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

    /// <summary>
    /// Mostrar todos los productos
    /// </summary>
    /// <returns>Todos los productos</returns>
    /// <response code="200">Devuelve el listado de productos</response>
    /// <response code="500">Si hay algún error</response>
    [HttpGet]
    public ActionResult<List<ProductItem>> Get()
    {
        List<ProductItem> products = _context.ProductItems.ToList();
        return   Ok(products);
    }
    [HttpGet]
    [Route("{id:int}")]
    public ActionResult<ProductItem> Get(int id)
    {
    ProductItem ProductItem = _context.ProductItems.Find(id);
        return ProductItem == null? NotFound()
            : Ok(ProductItem);
    }

    /// <summary>
    /// añadir productos
    /// </summary>
    /// <returns>Todos los productos</returns>
    /// <response code="201">Se ha creado correctamente</response>
    /// <response code="500">Si hay algún error</response>
    [HttpPost]
    public ActionResult<ProductItem> Post([FromBody] ProductItem product)
    {
      /*   ProductItems existingProductItem= _context.ProducItem.Find(product.id);
        if (existingProductItem != null)
        {
            return Conflict("Ya existe un elemento ");
        }*/
        product.id=0;
        _context.ProductItems.Add(product);
        _context.SaveChanges();

        string resourceUrl = Request.Path.ToString() + "/" + product.id;
        return Created(resourceUrl, product);
    }
    /// <summary>
    ///Actualizar los productos
    /// </summary>
    /// <returns>Todos los productos</returns>
    /// <response code="201">Devuelve el listado de productos</response>
    /// <response code="500">Si hay algún error</response>
    [HttpPut("{id:int}")]
    public ActionResult<ProductItem> Update([FromBody] ProductItem product, int id)
    {
        ProductItem productItemToUpdate = _context.ProductItems.Find(id);
        if (productItemToUpdate == null)
        {
            return NotFound("producto no encontrado");
        }
        productItemToUpdate.name=product.name;
        productItemToUpdate.category=product.category;
        productItemToUpdate.description=product.description;
        productItemToUpdate.price=product.price;

        _context.SaveChanges();
        string resourceUrl = Request.Path.ToString() + "/" + productItemToUpdate.id;

        return Created(resourceUrl, productItemToUpdate);
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
        ProductItem productItemToDelete = _context.ProductItems.Find(id);
        if (productItemToDelete == null)
        {
            return NotFound("producto no encontrado");
        }
        _context.ProductItems.Remove(productItemToDelete);
        _context.SaveChanges();
        return Ok(productItemToDelete);
    }

}
