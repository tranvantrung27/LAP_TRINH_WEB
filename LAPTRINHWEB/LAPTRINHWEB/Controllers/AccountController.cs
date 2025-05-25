using Microsoft.AspNetCore.Mvc;
using LAPTRINHWEB.Models;

namespace LAPTRINHWEB.Controllers
{
    public class AccountController : Controller
    {
        // Danh sách lưu trữ tạm thời các user đã đăng ký (thay thế database)
        private static List<User> _users = new List<User>();

        // GET: Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra username đã tồn tại chưa
                if (_users.Any(u => u.Username.Equals(model.Username, StringComparison.OrdinalIgnoreCase)))
                {
                    ModelState.AddModelError("Username", "Tên người dùng đã tồn tại");
                    return View(model);
                }

                // Kiểm tra email đã tồn tại chưa
                if (_users.Any(u => u.Email.Equals(model.Email, StringComparison.OrdinalIgnoreCase)))
                {
                    ModelState.AddModelError("Email", "Email đã được sử dụng");
                    return View(model);
                }

                // Thêm user mới vào danh sách
                var newUser = new User
                {
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password, // Trong thực tế nên hash password
                    CreatedAt = DateTime.Now
                };

                _users.Add(newUser);

                // Lưu thông báo thành công vào TempData
                TempData["SuccessMessage"] = "Đăng ký thành công! Bạn có thể đăng nhập ngay bây giờ.";

                return RedirectToAction("Login");
            }

            return View(model);
        }

        // GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // Tìm user trong danh sách
                var user = _users.FirstOrDefault(u =>
                    u.Username.Equals(model.Username, StringComparison.OrdinalIgnoreCase) &&
                    u.Password == model.Password);

                if (user != null)
                {
                    // Đăng nhập thành công - lưu thông tin vào Session
                    HttpContext.Session.SetString("Username", user.Username);
                    HttpContext.Session.SetString("Email", user.Email);
                    TempData["SuccessMessage"] = $"Chào mừng {user.Username}! Đăng nhập thành công.";
                    return RedirectToAction("Index", "Home"); // ←chuyển hướng về trang chủ
                }
                else
                {
                    ModelState.AddModelError("", "Tên người dùng hoặc mật khẩu không đúng");
                }
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

        // GET: Account/UserList (Để kiểm tra danh sách user đã đăng ký)
        public IActionResult UserList()
        {
            return View(_users);
        }

        // Action để kiểm tra trạng thái đăng nhập
        public bool IsLoggedIn()
        {
            return !string.IsNullOrEmpty(HttpContext.Session.GetString("Username"));
        }
    }
}