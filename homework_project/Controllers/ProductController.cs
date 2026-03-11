using Microsoft.AspNetCore.Mvc;
using homework_project.Models;

namespace homework_project.Controllers
{
    public class ProductController : Controller
    {
        // GET: /Product/Index
        public IActionResult Index()
        {
            var products = ProductRepository.GetAll();
            return View(products);
        }

        // GET: /Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                ProductRepository.Add(product);
                TempData["SuccessMessage"] = $"Продукт \"{product.Name}\" успішно додано!";
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: /Product/Delete/5
        public IActionResult Delete(int id)
        {
            var product = ProductRepository.GetById(id);
            if (product == null)
            {
                TempData["ErrorMessage"] = "Продукт не знайдено.";
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // POST: /Product/DeleteConfirmed/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = ProductRepository.GetById(id);
            if (product != null)
            {
                string name = product.Name;
                ProductRepository.Delete(id);
                TempData["SuccessMessage"] = $"Продукт \"{name}\" успішно видалено.";
            }
            else
            {
                TempData["ErrorMessage"] = "Продукт не знайдено.";
            }
            return RedirectToAction("Index");
        }

        // GET: /Product/Search
        public IActionResult Search(string query)
        {
            var model = new SearchViewModel
            {
                Query = query,
                Results = string.IsNullOrWhiteSpace(query)
                    ? new List<Product>()
                    : ProductRepository.SearchByName(query)
            };
            return View(model);
        }
    }
}
