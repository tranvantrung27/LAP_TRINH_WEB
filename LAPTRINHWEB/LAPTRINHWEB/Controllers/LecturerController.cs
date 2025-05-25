using LAPTRINHWEB.Models;
using Microsoft.AspNetCore.Mvc;

namespace LAPTRINHWEB.Controllers
{
    public class LecturerController : Controller
    {
        // Action để hiển thị danh sách giảng viên ưu tú
        public IActionResult OutstandingLecturers()
        {
            // Dữ liệu mẫu - trong thực tế sẽ lấy từ database
            var lecturers = GetSampleLecturers();
            return View(lecturers);
        }

        // Method để tạo dữ liệu mẫu
        private List<OutstandingLecturer> GetSampleLecturers()
        {
            return new List<OutstandingLecturer>
            {
                new OutstandingLecturer
                {
                    Id = 1,
                    FullName = "TS. Nguyễn Văn An",
                    Position = "Phó Hiệu trưởng, Giáo sư",
                    Degree = "Tiến sĩ Khoa học Máy tính - MIT (Mỹ)",
                    Department = "Khoa Công nghệ Thông tin",
                    Email = "nva@dhcongnghe.edu.vn",
                    Phone = "0901234567",
                    YearsOfExperience = 25,
                    Biography = "Chuyên gia hàng đầu trong lĩnh vực Trí tuệ nhân tạo và Machine Learning với hơn 25 năm kinh nghiệm giảng dạy và nghiên cứu.",
                    Specialization = "Trí tuệ nhân tạo, Machine Learning, Deep Learning",
                    Achievements = new List<string>
                    {
                        "Giải thưởng Giảng viên xuất sắc năm 2019",
                        "Chủ nhiệm 8 đề tài nghiên cứu khoa học",
                        "Tác giả 28 bài báo khoa học quốc tế",
                        "Chứng chỉ PMP (Project Management Professional)"
                    },
                    StartDate = new DateTime(2005, 3, 15),
                    IsActive = true
                },
                new OutstandingLecturer
                {
                    Id = 3,
                    FullName = "PGS.TS. Lê Minh Cường",
                    Position = "Phó Trưởng khoa Điện tử - Viễn thông",
                    Degree = "Phó Giáo sư, Tiến sĩ Kỹ thuật Điện tử - ĐHBK HCM",
                    Department = "Khoa Điện tử - Viễn thông",
                    Email = "lmc@dhcongnghe.edu.vn",
                    Phone = "0903456789",
                    YearsOfExperience = 22,
                    Biography = "Chuyên gia trong lĩnh vực Điện tử viễn thông, IoT và Hệ thống nhúng với nhiều năm kinh nghiệm trong nghiên cứu và ứng dụng.",
                    Specialization = "IoT, Embedded Systems, Wireless Communication",
                    Achievements = new List<string>
                    {
                        "Danh hiệu Phó Giáo sư năm 2018",
                        "Giải Ba Giải thưởng Hồ Chí Minh về KH&CN",
                        "12 bằng sáng chế đã được cấp",
                        "Chủ nhiệm 20 dự án hợp tác doanh nghiệp"
                    },
                    StartDate = new DateTime(2001, 8, 20),
                    IsActive = true
                },
                new OutstandingLecturer
                {
                    Id = 4,
                    FullName = "TS. Phạm Thị Diệu",
                    Position = "Giảng viên chính, Trưởng Bộ môn Toán ứng dụng",
                    Degree = "Tiến sĩ Toán học - Université Paris-Sud (Pháp)",
                    Department = "Khoa Khoa học Cơ bản",
                    Email = "ptd@dhcongnghe.edu.vn",
                    Phone = "0904567890",
                    YearsOfExperience = 16,
                    Biography = "Chuyên gia về Toán ứng dụng, Thống kê và Khoa học dữ liệu với kinh nghiệm nghiên cứu tại các trường đại học hàng đầu châu Âu.",
                    Specialization = "Applied Mathematics, Statistics, Data Science",
                    Achievements = new List<string>
                    {
                        "Học bổng Marie Curie Fellowship (EU)",
                        "25 công trình nghiên cứu trên tạp chí quốc tế",
                        "Giải thưởng Nghiên cứu khoa học trẻ 2020",
                        "Đồng tác giả 3 cuốn sách chuyên ngành"
                    },
                    StartDate = new DateTime(2007, 1, 10),
                    IsActive = true
                },
                new OutstandingLecturer
                {
                    Id = 5,
                    FullName = "TS. Võ Hoàng Gia",
                    Position = "Trưởng phòng Nghiên cứu & Phát triển",
                    Degree = "Tiến sĩ Khoa học Máy tính - Stanford University (Mỹ)",
                    Department = "Khoa Công nghệ Thông tin",
                    Email = "vhg@dhcongnghe.edu.vn",
                    Phone = "0905678901",
                    YearsOfExperience = 20,
                    Biography = "Chuyên gia hàng đầu về Blockchain, Cryptocurrency và Fintech với kinh nghiệm làm việc tại Silicon Valley.",
                    Specialization = "Blockchain, Cryptocurrency, Fintech, Cybersecurity",
                    Achievements = new List<string>
                    {
                        "Cựu kỹ sư tại Google và Facebook",
                        "Founder của 2 startup công nghệ thành công",
                        "18 bằng sáng chế về blockchain",
                        "Diễn giả chính tại 50+ hội thảo quốc tế"
                    },
                    StartDate = new DateTime(2010, 9, 1),
                    IsActive = true
                },
                new OutstandingLecturer
                {
                    Id = 6,
                    FullName = "TS. Nguyễn Thị Kim Hoa",
                    Position = "Phó Trưởng khoa Kinh tế - Quản trị",
                    Degree = "Tiến sĩ Quản trị Kinh doanh - Harvard Business School (Mỹ)",
                    Department = "Khoa Kinh tế - Quản trị",
                    Email = "ntkh@dhcongnghe.edu.vn",
                    Phone = "0906789012",
                    YearsOfExperience = 19,
                    Biography = "Chuyên gia về Quản trị doanh nghiệp, Digital Marketing và E-commerce với kinh nghiệm tư vấn cho nhiều tập đoàn lớn.",
                    Specialization = "Business Administration, Digital Marketing, E-commerce",
                    Achievements = new List<string>
                    {
                        "Top 40 under 40 Business Leaders in Vietnam",
                        "Tác giả 5 cuốn sách về quản trị",
                        "Tư vấn cho 100+ doanh nghiệp lớn",
                        "Giải thưởng Nhà quản lý xuất sắc 2021"
                    },
                    StartDate = new DateTime(2004, 6, 15),
                    IsActive = true
                }
            };
        }
    }
}

