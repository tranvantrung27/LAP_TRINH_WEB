using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.IO;
using LAPTRINHWEB.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LAPTRINHWEB.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hành động Index để hiển thị danh sách sinh viên
        public IActionResult Index()
        {
            var students = _context.SinhViens.ToList();
            return View(students);
        }

        public IActionResult Create()
        {
            ViewBag.MaNganh = new SelectList(_context.NganhHocs, "MaNganh", "TenNganh");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SinhVien student, IFormFile Hinh)
        {
            try
            {
                // Debug: In ra thông tin sinh viên nhận được
                System.Diagnostics.Debug.WriteLine($"=== DEBUG INFO ===");
                System.Diagnostics.Debug.WriteLine($"MaSV: '{student.MaSV}'");
                System.Diagnostics.Debug.WriteLine($"HoTen: '{student.HoTen}'");
                System.Diagnostics.Debug.WriteLine($"GioiTinh: '{student.GioiTinh}'");
                System.Diagnostics.Debug.WriteLine($"NgaySinh: {student.NgaySinh}");
                System.Diagnostics.Debug.WriteLine($"MaNganh: '{student.MaNganh}'");

                // Loại bỏ validation error cho các trường không cần thiết
                if (ModelState.ContainsKey("Hinh"))
                {
                    ModelState.Remove("Hinh");
                }

                // Loại bỏ validation error cho NganhHoc navigation property
                if (ModelState.ContainsKey("NganhHoc"))
                {
                    ModelState.Remove("NganhHoc");
                }

                // Debug: Kiểm tra ModelState
                if (!ModelState.IsValid)
                {
                    System.Diagnostics.Debug.WriteLine("=== MODELSTATE ERRORS ===");
                    foreach (var error in ModelState)
                    {
                        System.Diagnostics.Debug.WriteLine($"Key: {error.Key}");
                        foreach (var err in error.Value.Errors)
                        {
                            System.Diagnostics.Debug.WriteLine($"Error: {err.ErrorMessage}");
                        }
                    }

                    ViewBag.MaNganh = new SelectList(_context.NganhHocs, "MaNganh", "TenNganh");
                    return View(student);
                }

                // Kiểm tra kết nối database và dữ liệu trước khi lưu
                System.Diagnostics.Debug.WriteLine("=== DATABASE CHECK ===");
                try
                {
                    var nganhCount = await _context.NganhHocs.CountAsync();
                    System.Diagnostics.Debug.WriteLine($"Số ngành học: {nganhCount}");

                    var existingStudentCount = await _context.SinhViens.CountAsync();
                    System.Diagnostics.Debug.WriteLine($"Số sinh viên hiện tại: {existingStudentCount}");

                    // Kiểm tra ngành học có tồn tại không
                    var nganhExists = await _context.NganhHocs
                        .AnyAsync(n => n.MaNganh == student.MaNganh);
                    System.Diagnostics.Debug.WriteLine($"Ngành {student.MaNganh} có tồn tại: {nganhExists}");

                    if (!nganhExists)
                    {
                        ModelState.AddModelError("MaNganh", "Ngành học không tồn tại");
                        ViewBag.MaNganh = new SelectList(_context.NganhHocs, "MaNganh", "TenNganh");
                        return View(student);
                    }
                }
                catch (Exception dbCheckEx)
                {
                    System.Diagnostics.Debug.WriteLine($"Lỗi kiểm tra database: {dbCheckEx.Message}");
                }

                // Kiểm tra trùng mã sinh viên
                var existingStudent = await _context.SinhViens
                    .FirstOrDefaultAsync(s => s.MaSV == student.MaSV);

                if (existingStudent != null)
                {
                    System.Diagnostics.Debug.WriteLine($"DUPLICATE: Sinh viên {student.MaSV} đã tồn tại");
                    ModelState.AddModelError("MaSV", "Mã sinh viên đã tồn tại");
                    ViewBag.MaNganh = new SelectList(_context.NganhHocs, "MaNganh", "TenNganh");
                    return View(student);
                }

                // Xử lý hình ảnh
                if (Hinh != null && Hinh.Length > 0)
                {
                    System.Diagnostics.Debug.WriteLine($"File upload: {Hinh.FileName}, Size: {Hinh.Length}");

                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var fileExtension = Path.GetExtension(Hinh.FileName).ToLower();

                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        ModelState.AddModelError("Hinh", "Chỉ chấp nhận file ảnh có định dạng: jpg, jpeg, png, gif");
                        ViewBag.MaNganh = new SelectList(_context.NganhHocs, "MaNganh", "TenNganh");
                        return View(student);
                    }

                    var fileName = Guid.NewGuid().ToString() + fileExtension;
                    var imagesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

                    if (!Directory.Exists(imagesDirectory))
                    {
                        Directory.CreateDirectory(imagesDirectory);
                        System.Diagnostics.Debug.WriteLine($"Đã tạo thư mục: {imagesDirectory}");
                    }

                    var filePath = Path.Combine(imagesDirectory, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Hinh.CopyToAsync(stream);
                    }

                    student.Hinh = $"/images/{fileName}";
                    System.Diagnostics.Debug.WriteLine($"Đã lưu file: {filePath}");
                }
                else
                {
                    student.Hinh = null; // Đặt null thay vì string.Empty
                    System.Diagnostics.Debug.WriteLine("Không có file hình ảnh");
                }

                // Tạo một instance mới để tránh tracking issues
                var newStudent = new SinhVien
                {
                    MaSV = student.MaSV,
                    HoTen = student.HoTen,
                    GioiTinh = student.GioiTinh,
                    NgaySinh = student.NgaySinh,
                    MaNganh = student.MaNganh,
                    Hinh = student.Hinh
                };

                // Kiểm tra kết nối database
                System.Diagnostics.Debug.WriteLine("=== SAVING TO DATABASE ===");

                // In ra thông tin sinh viên trước khi lưu
                System.Diagnostics.Debug.WriteLine($"Final Student Data:");
                System.Diagnostics.Debug.WriteLine($"- MaSV: '{newStudent.MaSV}'");
                System.Diagnostics.Debug.WriteLine($"- HoTen: '{newStudent.HoTen}'");
                System.Diagnostics.Debug.WriteLine($"- GioiTinh: '{newStudent.GioiTinh}'");
                System.Diagnostics.Debug.WriteLine($"- NgaySinh: {newStudent.NgaySinh}");
                System.Diagnostics.Debug.WriteLine($"- MaNganh: '{newStudent.MaNganh}'");
                System.Diagnostics.Debug.WriteLine($"- Hinh: '{newStudent.Hinh}'");

                // Thêm sinh viên vào context
                _context.SinhViens.Add(newStudent);
                System.Diagnostics.Debug.WriteLine("Đã add student vào context");

                // Kiểm tra trạng thái của entity
                var entry = _context.Entry(newStudent);
                System.Diagnostics.Debug.WriteLine($"Entity State: {entry.State}");

                // Lưu thay đổi
                var result = await _context.SaveChangesAsync();
                System.Diagnostics.Debug.WriteLine($"SaveChangesAsync result: {result}");

                if (result > 0)
                {
                    System.Diagnostics.Debug.WriteLine("✓ LƯU THÀNH CÔNG!");

                    // Kiểm tra lại trong database
                    var savedStudent = await _context.SinhViens
                        .FirstOrDefaultAsync(s => s.MaSV == newStudent.MaSV);
                    if (savedStudent != null)
                    {
                        System.Diagnostics.Debug.WriteLine($"✓ Đã tìm thấy sinh viên vừa lưu: {savedStudent.HoTen}");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("✗ KHÔNG tìm thấy sinh viên vừa lưu!");
                    }

                    TempData["SuccessMessage"] = "Thêm sinh viên thành công!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("✗ SaveChanges trả về 0 - không có bản ghi nào được lưu!");
                    ModelState.AddModelError("", "Có lỗi xảy ra khi lưu dữ liệu - không có thay đổi nào được lưu");
                }
            }
            catch (DbUpdateException dbEx)
            {
                System.Diagnostics.Debug.WriteLine($"Database Error: {dbEx.Message}");
                System.Diagnostics.Debug.WriteLine($"Inner Exception: {dbEx.InnerException?.Message}");
                ModelState.AddModelError("", "Lỗi cơ sở dữ liệu: " + (dbEx.InnerException?.Message ?? dbEx.Message));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"General Error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
                ModelState.AddModelError("", "Có lỗi xảy ra: " + ex.Message);
            }

            // Nếu có lỗi, trả về view với dữ liệu
            ViewBag.MaNganh = new SelectList(_context.NganhHocs, "MaNganh", "TenNganh");
            return View(student);
        }

        // Chi tiết sinh viên
        public IActionResult Details(string id)
        {
            var student = _context.SinhViens.FirstOrDefault(s => s.MaSV == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // Phương thức GET Edit
        public IActionResult Edit(string id)
        {
            var student = _context.SinhViens.FirstOrDefault(s => s.MaSV == id);
            if (student == null)
            {
                return NotFound();
            }

            ViewBag.MaNganh = new SelectList(_context.NganhHocs, "MaNganh", "TenNganh", student.MaNganh);
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, SinhVien student, IFormFile HinhMoi)
        {
            if (id != student.MaSV)
            {
                return NotFound();
            }

            // Loại bỏ validation cho Hinh và NganhHoc
            if (ModelState.ContainsKey("Hinh"))
            {
                ModelState.Remove("Hinh");
            }
            if (ModelState.ContainsKey("NganhHoc"))
            {
                ModelState.Remove("NganhHoc");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var sv = await _context.SinhViens.FirstOrDefaultAsync(s => s.MaSV == id);
                    if (sv == null) return NotFound();

                    // Cập nhật thông tin cơ bản
                    sv.HoTen = student.HoTen;
                    sv.GioiTinh = student.GioiTinh;
                    sv.NgaySinh = student.NgaySinh;
                    sv.MaNganh = student.MaNganh;

                    // Xử lý hình ảnh mới nếu có
                    if (HinhMoi != null && HinhMoi.Length > 0)
                    {
                        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                        var fileExtension = Path.GetExtension(HinhMoi.FileName).ToLower();

                        if (!allowedExtensions.Contains(fileExtension))
                        {
                            ModelState.AddModelError("HinhMoi", "Chỉ chấp nhận file ảnh: jpg, jpeg, png, gif");
                            ViewBag.MaNganh = new SelectList(_context.NganhHocs, "MaNganh", "TenNganh", student.MaNganh);
                            return View(student);
                        }

                        // Xóa file cũ
                        if (!string.IsNullOrEmpty(sv.Hinh))
                        {
                            var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", sv.Hinh.TrimStart('/'));
                            if (System.IO.File.Exists(oldFilePath))
                            {
                                System.IO.File.Delete(oldFilePath);
                            }
                        }

                        // Lưu file mới
                        var fileName = Guid.NewGuid().ToString() + fileExtension;
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await HinhMoi.CopyToAsync(stream);
                        }

                        sv.Hinh = $"/images/{fileName}";
                    }

                    _context.Update(sv);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Cập nhật sinh viên thành công!";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException dbEx)
            {
                ModelState.AddModelError("", "Lỗi cập nhật: " + dbEx.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi: " + ex.Message);
            }

            ViewBag.MaNganh = new SelectList(_context.NganhHocs, "MaNganh", "TenNganh", student.MaNganh);
            return View(student);
        }



        // Phương thức GET Delete
        public IActionResult Delete(string id)
        {
            var student = _context.SinhViens.FirstOrDefault(s => s.MaSV == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                var student = await _context.SinhViens.FirstOrDefaultAsync(s => s.MaSV == id);

                if (student != null)
                {
                    // Xóa file hình ảnh
                    if (!string.IsNullOrEmpty(student.Hinh))
                    {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", student.Hinh.TrimStart('/'));
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                    }

                    var dangKys = _context.DangKys.Where(d => d.MaSV == id).ToList();
                    var maDKs = dangKys.Select(dk => dk.MaDK).ToList();

                    if (maDKs.Count > 0)
                    {
                        var chiTietDangKys = _context.ChiTietDangKys.Where(cd => maDKs.Contains(cd.MaDK)).ToList();
                        _context.ChiTietDangKys.RemoveRange(chiTietDangKys);
                    }

                    _context.DangKys.RemoveRange(dangKys);
                    _context.SinhViens.Remove(student);

                    var result = await _context.SaveChangesAsync();

                    if (result > 0)
                    {
                        TempData["SuccessMessage"] = "Xóa sinh viên thành công!";
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi xóa: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // Action để test kết nối database
      
    }
}