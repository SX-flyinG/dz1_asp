using Microsoft.AspNetCore.Mvc;
using homework_project.Models;

namespace homework_project.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            var products = ProductRepository.GetAll();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
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

        public IActionResult Details(int id)
        {
            var product = ProductRepository.GetById(id);
            if (product == null)
            {
                TempData["ErrorMessage"] = "Продукт не знайдено.";
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public IActionResult Edit(int id)
        {
            var product = ProductRepository.GetById(id);
            if (product == null)
            {
                TempData["ErrorMessage"] = "Продукт не знайдено.";
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                bool updated = ProductRepository.Update(product);
                if (updated)
                    TempData["SuccessMessage"] = $"Продукт \"{product.Name}\" успішно оновлено.";
                else
                    TempData["ErrorMessage"] = "Продукт не знайдено.";

                return RedirectToAction("Index");
            }
            return View(product);
        }

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

        [HttpPost]
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