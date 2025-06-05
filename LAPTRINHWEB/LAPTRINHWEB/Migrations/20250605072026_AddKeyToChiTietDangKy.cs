using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LAPTRINHWEB.Migrations
{
    /// <inheritdoc />
    public partial class AddKeyToChiTietDangKy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietDangKys_DangKys_DangKyMaDK",
                table: "ChiTietDangKys");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietDangKys_DangKys_MaDK",
                table: "ChiTietDangKys");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietDangKys_HocPhans_HocPhanMaHP",
                table: "ChiTietDangKys");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietDangKys_HocPhans_MaHP",
                table: "ChiTietDangKys");

            migrationBuilder.DropForeignKey(
                name: "FK_DangKys_SinhVien_MaSV",
                table: "DangKys");

            migrationBuilder.DropForeignKey(
                name: "FK_SinhVien_NganhHocs_MaNganh",
                table: "SinhVien");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NganhHocs",
                table: "NganhHocs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DangKys",
                table: "DangKys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChiTietDangKys",
                table: "ChiTietDangKys");

            migrationBuilder.RenameTable(
                name: "NganhHocs",
                newName: "NganhHoc");

            migrationBuilder.RenameTable(
                name: "DangKys",
                newName: "DangKy");

            migrationBuilder.RenameTable(
                name: "ChiTietDangKys",
                newName: "ChiTietDangKy");

            migrationBuilder.RenameIndex(
                name: "IX_DangKys_MaSV",
                table: "DangKy",
                newName: "IX_DangKy_MaSV");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietDangKys_MaHP",
                table: "ChiTietDangKy",
                newName: "IX_ChiTietDangKy_MaHP");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietDangKys_HocPhanMaHP",
                table: "ChiTietDangKy",
                newName: "IX_ChiTietDangKy_HocPhanMaHP");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietDangKys_DangKyMaDK",
                table: "ChiTietDangKy",
                newName: "IX_ChiTietDangKy_DangKyMaDK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NganhHoc",
                table: "NganhHoc",
                column: "MaNganh");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DangKy",
                table: "DangKy",
                column: "MaDK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChiTietDangKy",
                table: "ChiTietDangKy",
                columns: new[] { "MaDK", "MaHP" });

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietDangKy_DangKy_DangKyMaDK",
                table: "ChiTietDangKy",
                column: "DangKyMaDK",
                principalTable: "DangKy",
                principalColumn: "MaDK");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietDangKy_DangKy_MaDK",
                table: "ChiTietDangKy",
                column: "MaDK",
                principalTable: "DangKy",
                principalColumn: "MaDK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietDangKy_HocPhans_HocPhanMaHP",
                table: "ChiTietDangKy",
                column: "HocPhanMaHP",
                principalTable: "HocPhans",
                principalColumn: "MaHP");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietDangKy_HocPhans_MaHP",
                table: "ChiTietDangKy",
                column: "MaHP",
                principalTable: "HocPhans",
                principalColumn: "MaHP",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DangKy_SinhVien_MaSV",
                table: "DangKy",
                column: "MaSV",
                principalTable: "SinhVien",
                principalColumn: "MaSV",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SinhVien_NganhHoc_MaNganh",
                table: "SinhVien",
                column: "MaNganh",
                principalTable: "NganhHoc",
                principalColumn: "MaNganh",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietDangKy_DangKy_DangKyMaDK",
                table: "ChiTietDangKy");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietDangKy_DangKy_MaDK",
                table: "ChiTietDangKy");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietDangKy_HocPhans_HocPhanMaHP",
                table: "ChiTietDangKy");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietDangKy_HocPhans_MaHP",
                table: "ChiTietDangKy");

            migrationBuilder.DropForeignKey(
                name: "FK_DangKy_SinhVien_MaSV",
                table: "DangKy");

            migrationBuilder.DropForeignKey(
                name: "FK_SinhVien_NganhHoc_MaNganh",
                table: "SinhVien");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NganhHoc",
                table: "NganhHoc");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DangKy",
                table: "DangKy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChiTietDangKy",
                table: "ChiTietDangKy");

            migrationBuilder.RenameTable(
                name: "NganhHoc",
                newName: "NganhHocs");

            migrationBuilder.RenameTable(
                name: "DangKy",
                newName: "DangKys");

            migrationBuilder.RenameTable(
                name: "ChiTietDangKy",
                newName: "ChiTietDangKys");

            migrationBuilder.RenameIndex(
                name: "IX_DangKy_MaSV",
                table: "DangKys",
                newName: "IX_DangKys_MaSV");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietDangKy_MaHP",
                table: "ChiTietDangKys",
                newName: "IX_ChiTietDangKys_MaHP");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietDangKy_HocPhanMaHP",
                table: "ChiTietDangKys",
                newName: "IX_ChiTietDangKys_HocPhanMaHP");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietDangKy_DangKyMaDK",
                table: "ChiTietDangKys",
                newName: "IX_ChiTietDangKys_DangKyMaDK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NganhHocs",
                table: "NganhHocs",
                column: "MaNganh");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DangKys",
                table: "DangKys",
                column: "MaDK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChiTietDangKys",
                table: "ChiTietDangKys",
                columns: new[] { "MaDK", "MaHP" });

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietDangKys_DangKys_DangKyMaDK",
                table: "ChiTietDangKys",
                column: "DangKyMaDK",
                principalTable: "DangKys",
                principalColumn: "MaDK");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietDangKys_DangKys_MaDK",
                table: "ChiTietDangKys",
                column: "MaDK",
                principalTable: "DangKys",
                principalColumn: "MaDK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietDangKys_HocPhans_HocPhanMaHP",
                table: "ChiTietDangKys",
                column: "HocPhanMaHP",
                principalTable: "HocPhans",
                principalColumn: "MaHP");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietDangKys_HocPhans_MaHP",
                table: "ChiTietDangKys",
                column: "MaHP",
                principalTable: "HocPhans",
                principalColumn: "MaHP",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DangKys_SinhVien_MaSV",
                table: "DangKys",
                column: "MaSV",
                principalTable: "SinhVien",
                principalColumn: "MaSV",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SinhVien_NganhHocs_MaNganh",
                table: "SinhVien",
                column: "MaNganh",
                principalTable: "NganhHocs",
                principalColumn: "MaNganh",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
