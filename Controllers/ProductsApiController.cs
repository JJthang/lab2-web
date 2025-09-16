using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;
using MyMvcApp.Models;

[ApiController]
[Route("api/[controller]")] // => /api/products
public class ProductsController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public ProductsController(ApplicationDbContext db) => _db = db;

    // GET /api/products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAll()
    {
        var items = await _db.Products.ToListAsync();
        var fakeProducts = new List<Product>
        {
            new Product { Id = 1, Name = "iPhone 15 Pro", Price = 1200 },
            new Product { Id = 2, Name = "Samsung Galaxy S24", Price = 1000 },
            new Product { Id = 3, Name = "Xiaomi 14 Ultra", Price = 800 },
        };

        return Ok(fakeProducts);
    }

    // GET /api/products/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> Get(int id)
    {
        var item = await _db.Products.FindAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    // POST /api/products
    [HttpPost]
    public async Task<ActionResult<Product>> Create([FromBody] Product product)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _db.Products.Add(product);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
    }

    // PUT /api/products/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] Product product)
    {
        if (id != product.Id) return BadRequest();
        _db.Entry(product).State = EntityState.Modified;
        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _db.Products.AnyAsync(p => p.Id == id)) return NotFound();
            throw;
        }
        return NoContent();
    }

    // DELETE /api/products/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _db.Products.FindAsync(id);
        if (item == null) return NotFound();
        _db.Products.Remove(item);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
