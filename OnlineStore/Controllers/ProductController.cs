using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Data;
using OnlineStore.Data.Models.Entities;
using OnlineStore.Data.Models.Product;
using OnlineStore.Services.Images;
using OnlineStore.Services.Product;

namespace OnlineStore.Controllers;
public class ProductController : Controller
{
    private readonly ApplicationDbContext dbContext;
    private readonly IProductService productService;
    private readonly IImageService imageService;

    public ProductController(ApplicationDbContext dbContext, IProductService productService, IImageService imageService)
    {
        this.dbContext = dbContext;
        this.productService = productService;
        this.imageService = imageService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet]
    public IActionResult All()
    {
        List<GetAllProductsViewModel> allProducts = productService.GetAll();
        return View(allProducts);
    }
   
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
   
    [HttpPost]
    public IActionResult Create(CreateProductDto productDto)
    {
        this.productService.Create(productDto);

        return RedirectToAction("All", "Product");
        
    }
   
   [HttpGet]
    public IActionResult Edit(long id)
    {
        GetProductViewModel product = this.productService.Get(id);
        return View(product);
    }
   
    [HttpPost]
    public IActionResult Edit(long id, UpdateProductDto product)
    {
        this.productService.Update(id, product);
        return RedirectToAction("All", "Product");
    }
    
    [HttpGet]
    public IActionResult Delete(long id)
    {
        GetProductViewModel product = productService.Get(id);
        return View(product);
    }
    
    [HttpPost]
    [ActionName("Delete")]
    public IActionResult DeletePost(long id)
    {
        this.productService.Delete(id);
        return RedirectToAction("All", "Product");
    }
    
    public IActionResult ProductDetails(long id)
    {
        GetProductViewModel product = this.productService.Get(id);
        return View(product);
    }

    //Image control

    [HttpPost]
    public IActionResult AddImage(long productId, string url)
    {
        var image = new ImageModel { ProductId = (int)productId, Url = url };
        imageService.Create(productId, url);
        return Json(new { id = image.Id, url = image.Url });
    }

    [HttpPost]
    public IActionResult UpdateImage(long id, string url)
    {
        imageService.Update(id, url);
        return Ok();
    }

    [HttpPost]
    public IActionResult DeleteImage(long id)
    {
        imageService.Delete(id);
        return Ok();
    }



}