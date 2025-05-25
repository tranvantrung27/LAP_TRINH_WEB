using Microsoft.AspNetCore.Mvc;
using LAPTRINHWEB.Models;
using LAPTRINHWEB.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore; // Thêm dòng này để dùng FirstOrDefaultAsync, AnyAsync

namespace LAPTRINHWEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountController(ApplicationDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        // GET: Account/Register
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (await _context.Users.AnyAsync(u => u.Username == model.Username))
                    {
                        ModelState.AddModelError("Username", "Tên người dùng đã tồn tại");
                        return View(model);
                    }

                    var user = new User
                    {
                        Username = model.Username,
                        Email = model.Email,
                        CreatedAt = DateTime.Now
                    };

                    user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);

                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Đăng ký thành công!";
                    return RedirectToAction("Login");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra trong quá trình đăng ký.");
                }
            }

            return View(model);
        }



        // GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _context.Users
                        .FirstOrDefaultAsync(u => u.Username == model.Username);

                    if (user != null)
                    {
                        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);

                        if (result == PasswordVerificationResult.Success)
                        {
                            HttpContext.Session.SetString("Username", user.Username);
                            HttpContext.Session.SetString("Role", user.Username.ToLower() == "admin" ? "Admin" : "User");
                            TempData["SuccessMessage"] = $"Chào mừng {user.Username}!";
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra trong quá trình đăng nhập.");
            }
            return View(model);
        }




        // GET: Account/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["SuccessMessage"] = "Đăng xuất thành công!";
            return RedirectToAction("Index", "Home");
        }

        // GET: Account/UserList
        public async Task<IActionResult> UserList()
        {
            return View(await _context.Users.ToListAsync());
        }

        // Action để kiểm tra trạng thái đăng nhập
        public bool IsLoggedIn()
        {
            return !string.IsNullOrEmpty(HttpContext.Session.GetString("Username"));
        }
    }
}
