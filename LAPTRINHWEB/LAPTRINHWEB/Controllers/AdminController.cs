using LAPTRINHWEB.Data;
using LAPTRINHWEB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AdminController(ApplicationDbContext context, IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    // GET: Hiển thị trang quản lý
    [HttpGet]
    public async Task<IActionResult> UserManagement()
    {
        var rolesToManage = new[] { "admin", "quanly", "nhanvien" };
        var allUsers = await _context.Users.ToListAsync();
        var users = allUsers
            .Where(u => !string.IsNullOrEmpty(u.Role) && rolesToManage.Contains(u.Role.ToLower()))
            .ToList();
        return View(users);
    }

    // POST: Thêm user mới
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateUser(UserCreateViewModel model) // ← Change ici
    {
        if (!ModelState.IsValid)
        {
            var rolesToManage = new[] { "admin", "quanly", "nhanvien" };
            var allUsers = await _context.Users.ToListAsync();
            var users = allUsers
                .Where(u => !string.IsNullOrEmpty(u.Role) && rolesToManage.Contains(u.Role.ToLower()))
                .ToList();
            TempData["Error"] = "Vui lòng kiểm tra lại thông tin!";
            return View("UserManagement", users);
        }

        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
        if (existingUser != null)
        {
            TempData["Error"] = "Tài khoản đã tồn tại!";
            return RedirectToAction("UserManagement");
        }

        var user = new User
        {
            Username = model.Username,
            FullName = model.FullName,
            Email = model.Email,        // ← Maintenant ça va marcher
            Phone = model.Phone,        // ← Et ça aussi
            Role = model.Role,
            CreatedAt = DateTime.Now,
            PasswordHash = _passwordHasher.HashPassword(null, model.Password)
        };

        try
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Thêm tài khoản thành công!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Có lỗi xảy ra: " + ex.Message;
        }

        return RedirectToAction("UserManagement");
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteUser(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null)
        {
            TempData["Error"] = "Tài khoản không tồn tại.";
            return RedirectToAction("UserManagement", "Admin");
        }

        try
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
            TempData["Success"] = "Xóa tài khoản thành công.";
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Có lỗi xảy ra khi xóa tài khoản.";
        }

        return RedirectToAction("UserManagement", "Admin");
    }
    [HttpGet]
    public async Task<IActionResult> EditUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            TempData["Error"] = "Tài khoản không tồn tại.";
            return RedirectToAction("UserManagement");
        }

        var model = new UserEditViewModel
        {
            Id = user.Id,
            Username = user.Username,
            FullName = user.FullName,
            Email = user.Email,
            Phone = user.Phone,
            Role = user.Role
        };

        return View(model);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditUser(UserEditViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _context.Users.FindAsync(model.Id);
        if (user == null)
        {
            TempData["Error"] = "Tài khoản không tồn tại.";
            return RedirectToAction("UserManagement");
        }

        user.FullName = model.FullName;
        user.Email = model.Email;
        user.Phone = model.Phone;
        user.Role = model.Role;

        try
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Cập nhật tài khoản thành công.";
        }
        catch (Exception)
        {
            TempData["Error"] = "Có lỗi xảy ra khi cập nhật tài khoản.";
            return View(model);
        }

        return RedirectToAction("UserManagement");
    }

}