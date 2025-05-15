using Microsoft.AspNetCore.Mvc;
using LAPTRINHWEB.Models;
using System.Collections.Generic;
using System.Linq;

namespace LAPTRINHWEB.Controllers
{
    public class StudentController : Controller
    {
        // Static list để lưu trữ thông tin đăng ký (trong thực tế nên dùng database)
        private static List<Student> registeredStudents = new List<Student>();

        // GET: Student/Index
        public IActionResult Index()
        {
            // Clear error state to show clean form
            ModelState.Clear();
            return View();
        }

        // POST: Student/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Student student)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem sinh viên đã đăng ký chưa
                var existingStudent = registeredStudents.FirstOrDefault(s => s.MSSV == student.MSSV);
                if (existingStudent != null)
                {
                    ModelState.AddModelError("MSSV", "Sinh viên với MSSV này đã đăng ký!");
                    return View(student);
                }

                // Lưu thông tin sinh viên vào danh sách
                registeredStudents.Add(student);

                // Chuyển hướng đến trang hiển thị kết quả
                return RedirectToAction("ShowKQ", new { mssv = student.MSSV });
            }

            return View(student);
        }

        // GET: Student/ShowKQ
        public IActionResult ShowKQ(string mssv)
        {
            var student = registeredStudents.FirstOrDefault(s => s.MSSV == mssv);

            if (student == null)
            {
                TempData["Error"] = "Không tìm thấy thông tin đăng ký!";
                return RedirectToAction("Index");
            }

            // Đếm số lượng sinh viên cùng chuyên ngành (không tính sinh viên hiện tại)
            int sameSpecialtyCount = registeredStudents
                .Where(s => s.ChuyenNganh == student.ChuyenNganh && s.MSSV != student.MSSV)
                .Count();

            ViewBag.SameSpecialtyCount = sameSpecialtyCount;
            ViewBag.TotalStudents = registeredStudents.Count;

            return View(student);
        }

        // GET: Student/List - Xem danh sách sinh viên theo chuyên ngành
        public IActionResult List(string chuyenNganh = null)
        {
            var studentsList = registeredStudents.AsQueryable();

            // Lọc theo chuyên ngành nếu có
            if (!string.IsNullOrEmpty(chuyenNganh))
            {
                studentsList = studentsList.Where(s => s.ChuyenNganh == chuyenNganh);
                ViewBag.SelectedChuyenNganh = chuyenNganh;
            }

            // Thống kê số lượng theo từng chuyên ngành
            var stats = registeredStudents
                .GroupBy(s => s.ChuyenNganh)
                .Select(g => new
                {
                    ChuyenNganh = g.Key,
                    Count = g.Count(),
                    AvgScore = g.Average(s => s.DiemTB)
                })
                .ToList();

            ViewBag.Stats = stats;
            ViewBag.TotalStudents = registeredStudents.Count;

            return View(studentsList.OrderBy(s => s.ChuyenNganh).ThenBy(s => s.MSSV).ToList());
        }
    }
}