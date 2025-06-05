using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LAPTRINHWEB.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSinhVienTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.CreateTable(
                name: "HocPhans",
                columns: table => new
                {
                    MaHP = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenHP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoTinChi = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocPhans", x => x.MaHP);
                });

            migrationBuilder.CreateTable(
                name: "NganhHocs",
                columns: table => new
                {
                    MaNganh = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenNganh = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NganhHocs", x => x.MaNganh);
                });

            migrationBuilder.CreateTable(
                name: "SinhVien",
                columns: table => new
                {
                    MaSV = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GioiTinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaNganh = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinhVien", x => x.MaSV);
                    table.ForeignKey(
                        name: "FK_SinhVien_NganhHocs_MaNganh",
                        column: x => x.MaNganh,
                        principalTable: "NganhHocs",
                        principalColumn: "MaNganh",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DangKys",
                columns: table => new
                {
                    MaDK = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayDK = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaSV = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DangKys", x => x.MaDK);
                    table.ForeignKey(
                        name: "FK_DangKys_SinhVien_MaSV",
                        column: x => x.MaSV,
                        principalTable: "SinhVien",
                        principalColumn: "MaSV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDangKys",
                columns: table => new
                {
                    MaDK = table.Column<int>(type: "int", nullable: false),
                    MaHP = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DangKyMaDK = table.Column<int>(type: "int", nullable: true),
                    HocPhanMaHP = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDangKys", x => new { x.MaDK, x.MaHP });
                    table.ForeignKey(
                        name: "FK_ChiTietDangKys_DangKys_DangKyMaDK",
                        column: x => x.DangKyMaDK,
                        principalTable: "DangKys",
                        principalColumn: "MaDK");
                    table.ForeignKey(
                        name: "FK_ChiTietDangKys_DangKys_MaDK",
                        column: x => x.MaDK,
                        principalTable: "DangKys",
                        principalColumn: "MaDK",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietDangKys_HocPhans_HocPhanMaHP",
                        column: x => x.HocPhanMaHP,
                        principalTable: "HocPhans",
                        principalColumn: "MaHP");
                    table.ForeignKey(
                        name: "FK_ChiTietDangKys_HocPhans_MaHP",
                        column: x => x.MaHP,
                        principalTable: "HocPhans",
                        principalColumn: "MaHP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDangKys_DangKyMaDK",
                table: "ChiTietDangKys",
                column: "DangKyMaDK");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDangKys_HocPhanMaHP",
                table: "ChiTietDangKys",
                column: "HocPhanMaHP");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDangKys_MaHP",
                table: "ChiTietDangKys",
                column: "MaHP");

            migrationBuilder.CreateIndex(
                name: "IX_DangKys_MaSV",
                table: "DangKys",
                column: "MaSV");

            migrationBuilder.CreateIndex(
                name: "IX_SinhVien_MaNganh",
                table: "SinhVien",
                column: "MaNganh");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietDangKys");

            migrationBuilder.DropTable(
                name: "DangKys");

            migrationBuilder.DropTable(
                name: "HocPhans");

            migrationBuilder.DropTable(
                name: "SinhVien");

            migrationBuilder.DropTable(
                name: "NganhHocs");

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "CategoryId", "Description", "Image", "Price", "Title" },
                values: new object[,]
                {
                    { 3, "Cay Horstmann", 2, "Sách lập trình Java cơ bản và nâng cao", null, 800000m, "Core Java: Fundamentals, Volume 1" },
                    { 4, "Hải Dỗ", 1, "Đàn ông tuổi 15 mơ ước thành đàn ông tuổi 20, đàn ông tuổi 20 mơ ước thành đàn ông tuổi 30, đàn ông tuổi 30 mơ ước được trở thành đàn ông tuổi 40 và đàn ông tuổi 40 lại mơ ước đặt chân lên cỗ máy thời gian để quay lại tuổi 30 với toàn bộ tài sản của mình! Vậy đây!", null, 61000m, "Cuộc Sống Rất Giống Cuộc Đời" }
                });
        }
    }
}
