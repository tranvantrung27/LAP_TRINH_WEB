using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using LAPTRINHWEB.Models;

namespace LAPTRINHWEB.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Book
        public async Task<IActionResult> Index(int? categoryId)
        {
            var categories = await _context.Categories.ToListAsync();
            ViewBag.Categories = categories;
            ViewBag.SelectedCategoryId = categoryId;

            var books = categoryId.HasValue
                ? await _context.Books.Include(b => b.Category)
                    .Where(b => b.CategoryId == categoryId.Value).ToListAsync()
                : await _context.Books.Include(b => b.Category).ToListAsync();

            return View(books);
        }

        // GET: Book/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Book/Create
        // GET: Book/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await _context.Categories.ToListAsync(), "CategoryId", "CategoryName");
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                // Xử lý upload ảnh
                if (imageFile != null && imageFile.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "books");

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }

                    book.Image = uniqueFileName;
                }

                _context.Add(book);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Thêm sách thành công!";
                return RedirectToAction("Admin");
            }

            // Load lại categories nếu có lỗi
            var categories = await _context.Categories.ToListAsync();
            ViewBag.Categories = categories;
            return View(book);
        }
        // GET: Book/Edit/5

        // GET: Book/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            // Load categories và set selected value
            var categories = await _context.Categories.ToListAsync();
            ViewBag.CategoryId = new SelectList(categories, "CategoryId", "CategoryName", book.CategoryId);

            return View(book);
        }

        // POST: Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book, IFormFile? imageFile)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            // Debug
            Console.WriteLine($"Received CategoryId: {book.CategoryId}");

            if (ModelState.IsValid)
            {
                try
                {
                    var existingBook = await _context.Books.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
                    if (existingBook == null) return NotFound();

                    // Xử lý ảnh
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "books");

                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        // Xóa ảnh cũ
                        if (!string.IsNullOrEmpty(existingBook.Image))
                        {
                            string oldImagePath = Path.Combine(uploadsFolder, existingBook.Image);
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }

                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(fileStream);
                        }

                        book.Image = uniqueFileName;
                    }
                    else
                    {
                        book.Image = existingBook.Image;
                    }

                    _context.Update(book);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Cập nhật sách thành công!";
                    return RedirectToAction("Admin");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật sách.");
                }
            }

            // Load lại categories khi có lỗi
            var categories = await _context.Categories.ToListAsync();
            ViewBag.CategoryId = new SelectList(categories, "CategoryId", "CategoryName", book.CategoryId);
            return View(book);
        }

        // GET: Book/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                // Xóa ảnh nếu có
                if (!string.IsNullOrEmpty(book.Image))
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "books");
                    string imagePath = Path.Combine(uploadsFolder, book.Image);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Xóa sách thành công!";
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Book/ByCategory/2
        public async Task<IActionResult> ByCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            var books = await _context.Books
                .Include(b => b.Category)
                .Where(b => b.CategoryId == id)
                .ToListAsync();

            ViewBag.CategoryName = category.CategoryName;
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.SelectedCategoryId = id;

            return View("Index", books);
        }

        // GET: Book/Admin - Trang quản lý dành cho admin
        public async Task<IActionResult> Admin(int? categoryId)
        {
            var categories = await _context.Categories.ToListAsync();
            ViewBag.Categories = categories;
            ViewBag.SelectedCategoryId = categoryId;

            var books = categoryId.HasValue
                ? await _context.Books.Include(b => b.Category)
                    .Where(b => b.CategoryId == categoryId.Value).ToListAsync()
                : await _context.Books.Include(b => b.Category).ToListAsync();

            return View(books);
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}