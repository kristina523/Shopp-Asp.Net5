using Shopp.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Shopp.Controllers
{
    public class HomeController : Controller
    {
        private readonly CosmeticcShopContext _context;

        public HomeController(CosmeticcShopContext context)
        {
            _context = context;
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult FAQ()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Products()
        {
            var products = _context.Products.ToList();
            return View(products);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product products)
        {
            _context.Products.Add(products);
            await _context.SaveChangesAsync();
            return RedirectToAction("Products");
        }

        [HttpGet]
        public async Task<IActionResult> DetailsProduct(int? id)
        {
            if (id != null)
            {
                Product product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
                if (product != null)
                    return View(product);
            }
            return NotFound();
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Product product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
                if (product != null)
                    return View(product);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Products");
        }

        [HttpGet]
        [ActionName("ConfirmDeleteProducts")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ConfirmDeleteProducts(int? id)
        {
            if (id != null)
            {
                Product product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
                if (product != null)
                    return View(product);

            }
            return NotFound();
        }

        public async Task<IActionResult> DeleteProducts(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("Products");
        }



        public async Task<IActionResult> Brands()
        {
            var brands = _context.Brands.ToList();
            return View(brands);
        }
        public IActionResult CreateBrands()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateBrands(Brand brands)
        {
            _context.Brands.Add(brands);
            await _context.SaveChangesAsync();
            return RedirectToAction("Brands");
        }

        [HttpGet]
        public async Task<IActionResult> DetailsBrands(int? id)
        {
            if (id != null)
            {
                Brand brands = await _context.Brands.FirstOrDefaultAsync(p => p.BrandsId == id);
                if (brands != null)
                    return View(brands);
            }
            return NotFound();
        }
        public async Task<IActionResult> EditBrands(int? id)
        {
            if (id != null)
            {
                Brand brands = await _context.Brands.FirstOrDefaultAsync(p => p.BrandsId == id);
                if (brands != null)
                    return View(brands);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditBrands(Brand brands)
        {
            _context.Brands.Update(brands);
            await _context.SaveChangesAsync();
            return RedirectToAction("Brands");
        }


        [HttpGet]
        [ActionName("ConfirmDeleteBrand")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ConfirmDeleteBrand(int? id)
        {
            if (id != null)
            {
                Brand brands = await _context.Brands.FirstOrDefaultAsync(p => p.BrandsId == id);
                if (brands != null)
                    return View(brands);

                if (brands.Products.Any())
                {
                    // Сообщение об ошибке
                    TempData["ErrorMessage"] = "Невозможно удалить роль, так как она связана с пользователями.";
                    return RedirectToAction("Brands", new { id });
                }
            }
            return NotFound();
        }
        public async Task<IActionResult> DeleteBrands(int id)
        {
            var brands = await _context.Brands.FindAsync(id);
            if (brands == null)
            {
                return NotFound();
            }

            _context.Brands.Remove(brands);
            await _context.SaveChangesAsync();

            return RedirectToAction("Brands");
        }


        [HttpGet]
        public async Task<IActionResult> Categories()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("Categories");
        }

        [HttpGet]
        public async Task<IActionResult> DetailsCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FirstOrDefaultAsync(m => m.CategoriesId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        public async Task<IActionResult> EditCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(Category categories)
        {
            _context.Categories.Update(categories);
            await _context.SaveChangesAsync();
            return RedirectToAction("Categories");
        }


        [HttpGet]
        [ActionName("ConfirmDeleteCategory")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ConfirmDeleteCategory(int? id)

        {
            if (id != null)
            {
                Category categories = await _context.Categories.FirstOrDefaultAsync(p => p.CategoriesId == id);
                if (categories != null)
                    return View(categories);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FirstOrDefaultAsync(m => m.CategoriesId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        [HttpGet]
        public async Task<IActionResult> Customers()
        {
            var customers = await _context.Customers.ToListAsync();
            return View(customers);
        }
        public IActionResult CreateCustomer()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Customers");
        }

        [HttpGet]
        public async Task<IActionResult> DetailsCustomer(int? id)
        {
            if (id != null)
            {
                Customer customer = await _context.Customers.FirstOrDefaultAsync(p => p.CustomerId == id);
                if (customer != null)
                    return View(customer);
            }
            return NotFound();
        }
        public async Task<IActionResult> EditCustomer(int? id)
        {
            if (id != null)
            {
                Customer customer = await _context.Customers.FirstOrDefaultAsync(p => p.CustomerId == id);
                if (customer != null)
                    return View(customer);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Customers");
        }


        [HttpGet]
        [ActionName("ConfirmDeleteCustomer")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ConfirmDeleteCustomer(int? id)
        {
            if (id != null)
            {
                Customer customer = await _context.Customers.FirstOrDefaultAsync(p => p.CustomerId == id);
                if (customer != null)
                    return View(customer);
            }
            return NotFound();
        }
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return RedirectToAction("Customers");
        }
        public IActionResult Orders()
        {
            var orders = _context.Orders.ToList();
            return View(orders);
        }
        public IActionResult CreateOrders()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrders(Order orders)
        {
            _context.Orders.Add(orders);
            await _context.SaveChangesAsync();
            return RedirectToAction("Orders");
        }
        public async Task<IActionResult> DetailsOrders(int? id)
        {
            if (id != null)
            {
                Order orders = await _context.Orders.FirstOrDefaultAsync(p => p.OrderId == id);
                if (orders != null)
                    return View(orders);
            }
            return NotFound();
        }
        public async Task<IActionResult> EditOrders(int? id)
        {
            if (id != null)
            {
                Order orders = await _context.Orders.FirstOrDefaultAsync(p => p.OrderId == id);
                if (orders != null)
                    return View(orders);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditOrder(Order orders)
        {
            _context.Orders.Update(orders);
            await _context.SaveChangesAsync();
            return RedirectToAction("Orders");
        }


        [HttpGet]
        [ActionName("ConfirmDeleteOders")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ConfirmDeleteOders(int? id)
        {
            if (id != null)
            {
                Order orders = await _context.Orders.FirstOrDefaultAsync(p => p.OrderId == id);
                if (orders != null)
                    return View(orders);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteOrders(int id)
        {
            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(orders);
            await _context.SaveChangesAsync();

            return RedirectToAction("Orders");
        }
        public IActionResult CartItem()
        {
            var cartItems = _context.CartItems.Include(u=>u.Product).ToList();
            return View(cartItems);
        }
    }
}

