using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unit)
        {
            _unitOfWork = unit;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.ProductRepo.GetAll().ToList();



            return View(objProductList);
        }

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.CategoryRepo.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            //ViewBag.CategoryList = CategoryList;
            ViewData["CategoryList"] = CategoryList;

            return View();
        }

        [HttpPost]
        public IActionResult Create(Product obj)
        {
            //if (obj.Name == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("Name", "Display Order cannot match the Category Name");
            //}

            if (obj.Title.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is an invalid value");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.ProductRepo.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index", "Product");
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
            }

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();   // Or return error page
            }

            Product? product= _unitOfWork.ProductRepo.Get(u => u.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.ProductRepo.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product changed successfully";
                return RedirectToAction("Index", "Product");
            }
            return View(obj);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();   // Or return error page
            }

            Product? product = _unitOfWork.ProductRepo.Get(u => u.Id == id);
            //Category? category1 = _db.Categories.FirstOrDefault(category => category.Id == id);
            //Category? category2 = _db.Categories.Where(category => category.Id == id).FirstOrDefault();

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product? product= _unitOfWork.ProductRepo.Get(u => u.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            _unitOfWork.ProductRepo.Remove(product);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index", "Product");

        }

    }

}

