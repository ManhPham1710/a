using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlogApi.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogPost",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    ThumbnailUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ViewCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PublishedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogPost_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chuyên mục chia sẻ kiến thức về Công Nghệ", "Công Nghệ", null },
                    { 2, new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chuyên mục chia sẻ kiến thức về Lập Trình", "Lập Trình", null },
                    { 3, new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chuyên mục chia sẻ kiến thức về Web Dev", "Web Dev", null },
                    { 4, new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chuyên mục chia sẻ kiến thức về Mobile", "Mobile", null },
                    { 5, new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chuyên mục chia sẻ kiến thức về DevOps", "DevOps", null },
                    { 6, new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chuyên mục chia sẻ kiến thức về AI", "AI", null },
                    { 7, new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chuyên mục chia sẻ kiến thức về Database", "Database", null },
                    { 8, new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chuyên mục chia sẻ kiến thức về Cloud", "Cloud", null },
                    { 9, new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chuyên mục chia sẻ kiến thức về Bảo Mật", "Bảo Mật", null },
                    { 10, new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chuyên mục chia sẻ kiến thức về Khác", "Khác", null }
                });

            migrationBuilder.InsertData(
                table: "BlogPost",
                columns: new[] { "Id", "Author", "CategoryId", "Content", "CreatedAt", "IsPublished", "PublishedAt", "ThumbnailUrl", "Title", "UpdatedAt", "ViewCount" },
                values: new object[,]
                {
                    { 1, "Admin Hệ Thống", 1, "Đây là nội dung chi tiết của bài viết về Công Nghệ. Nội dung này được biên soạn kỹ lưỡng để đảm bảo dài hơn 50 ký tự nhằm đáp ứng yêu cầu kiểm tra dữ liệu đầu vào của hệ thống.", new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "https://example.com/images/công nghệ-1.jpg", "Hướng dẫn học Công Nghệ cơ bản phần 1", null, 8 },
                    { 2, "Admin Hệ Thống", 1, "Đây là nội dung chi tiết của bài viết về Công Nghệ. Nội dung này được biên soạn kỹ lưỡng để đảm bảo dài hơn 50 ký tự nhằm đáp ứng yêu cầu kiểm tra dữ liệu đầu vào của hệ thống.", new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "https://example.com/images/công nghệ-2.jpg", "Hướng dẫn học Công Nghệ cơ bản phần 2", null, 16 },
                    { 3, "Admin Hệ Thống", 1, "Đây là nội dung chi tiết của bài viết về Công Nghệ. Nội dung này được biên soạn kỹ lưỡng để đảm bảo dài hơn 50 ký tự nhằm đáp ứng yêu cầu kiểm tra dữ liệu đầu vào của hệ thống.", new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "https://example.com/images/công nghệ-3.jpg", "Hướng dẫn học Công Nghệ cơ bản phần 3", null, 24 },
                    { 4, "Admin Hệ Thống", 2, "Đây là nội dung chi tiết của bài viết về Lập Trình. Nội dung này được biên soạn kỹ lưỡng để đảm bảo dài hơn 50 ký tự nhằm đáp ứng yêu cầu kiểm tra dữ liệu đầu vào của hệ thống.", new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "https://example.com/images/lập trình-1.jpg", "Hướng dẫn học Lập Trình cơ bản phần 1", null, 32 },
                    { 5, "Admin Hệ Thống", 2, "Đây là nội dung chi tiết của bài viết về Lập Trình. Nội dung này được biên soạn kỹ lưỡng để đảm bảo dài hơn 50 ký tự nhằm đáp ứng yêu cầu kiểm tra dữ liệu đầu vào của hệ thống.", new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "https://example.com/images/lập trình-2.jpg", "Hướng dẫn học Lập Trình cơ bản phần 2", null, 40 },
                    { 6, "Admin Hệ Thống", 2, "Đây là nội dung chi tiết của bài viết về Lập Trình. Nội dung này được biên soạn kỹ lưỡng để đảm bảo dài hơn 50 ký tự nhằm đáp ứng yêu cầu kiểm tra dữ liệu đầu vào của hệ thống.", new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "https://example.com/images/lập trình-3.jpg", "Hướng dẫn học Lập Trình cơ bản phần 3", null, 48 },
                    { 7, "Admin Hệ Thống", 3, "Đây là nội dung chi tiết của bài viết về Web Dev. Nội dung này được biên soạn kỹ lưỡng để đảm bảo dài hơn 50 ký tự nhằm đáp ứng yêu cầu kiểm tra dữ liệu đầu vào của hệ thống.", new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "https://example.com/images/web dev-1.jpg", "Hướng dẫn học Web Dev cơ bản phần 1", null, 56 },
                    { 8, "Admin Hệ Thống", 3, "Đây là nội dung chi tiết của bài viết về Web Dev. Nội dung này được biên soạn kỹ lưỡng để đảm bảo dài hơn 50 ký tự nhằm đáp ứng yêu cầu kiểm tra dữ liệu đầu vào của hệ thống.", new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "https://example.com/images/web dev-2.jpg", "Hướng dẫn học Web Dev cơ bản phần 2", null, 64 },
                    { 9, "Admin Hệ Thống", 3, "Đây là nội dung chi tiết của bài viết về Web Dev. Nội dung này được biên soạn kỹ lưỡng để đảm bảo dài hơn 50 ký tự nhằm đáp ứng yêu cầu kiểm tra dữ liệu đầu vào của hệ thống.", new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "https://example.com/images/web dev-3.jpg", "Hướng dẫn học Web Dev cơ bản phần 3", null, 72 },
                    { 10, "Admin Hệ Thống", 4, "Đây là nội dung chi tiết của bài viết về Mobile. Nội dung này được biên soạn kỹ lưỡng để đảm bảo dài hơn 50 ký tự nhằm đáp ứng yêu cầu kiểm tra dữ liệu đầu vào của hệ thống.", new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "https://example.com/images/mobile-1.jpg", "Hướng dẫn học Mobile cơ bản phần 1", null, 80 },
                    { 11, "Admin Hệ Thống", 4, "Đây là nội dung chi tiết của bài viết về Mobile. Nội dung này được biên soạn kỹ lưỡng để đảm bảo dài hơn 50 ký tự nhằm đáp ứng yêu cầu kiểm tra dữ liệu đầu vào của hệ thống.", new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "https://example.com/images/mobile-2.jpg", "Hướng dẫn học Mobile cơ bản phần 2", null, 88 },
                    { 12, "Admin Hệ Thống", 4, "Đây là nội dung chi tiết của bài viết về Mobile. Nội dung này được biên soạn kỹ lưỡng để đảm bảo dài hơn 50 ký tự nhằm đáp ứng yêu cầu kiểm tra dữ liệu đầu vào của hệ thống.", new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "https://example.com/images/mobile-3.jpg", "Hướng dẫn học Mobile cơ bản phần 3", null, 96 },
                    { 13, "Admin Hệ Thống", 5, "Đây là nội dung chi tiết của bài viết về DevOps. Nội dung này được biên soạn kỹ lưỡng để đảm bảo dài hơn 50 ký tự nhằm đáp ứng yêu cầu kiểm tra dữ liệu đầu vào của hệ thống.", new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "https://example.com/images/devops-1.jpg", "Hướng dẫn học DevOps cơ bản phần 1", null, 104 },
                    { 14, "Admin Hệ Thống", 5, "Đây là nội dung chi tiết của bài viết về DevOps. Nội dung này được biên soạn kỹ lưỡng để đảm bảo dài hơn 50 ký tự nhằm đáp ứng yêu cầu kiểm tra dữ liệu đầu vào của hệ thống.", new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "https://example.com/images/devops-2.jpg", "Hướng dẫn học DevOps cơ bản phần 2", null, 112 },
                    { 15, "Admin Hệ Thống", 5, "Đây là nội dung chi tiết của bài viết về DevOps. Nội dung này được biên soạn kỹ lưỡng để đảm bảo dài hơn 50 ký tự nhằm đáp ứng yêu cầu kiểm tra dữ liệu đầu vào của hệ thống.", new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "https://example.com/images/devops-3.jpg", "Hướng dẫn học DevOps cơ bản phần 3", null, 120 },
                    { 16, "Admin Hệ Thống", 6, "Đây là nội dung chi tiết của bài viết về AI. Nội dung này được biên soạn kỹ lưỡng để đảm bảo dài hơn 50 ký tự nhằm đáp ứng yêu cầu kiểm tra dữ liệu đầu vào của hệ thống.", new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "https://example.com/images/ai-1.jpg", "Hướng dẫn học AI cơ bản phần 1", null, 128 },
                    { 17, "Admin Hệ Thống", 6, "Đây là nội dung chi tiết của bài viết về AI. Nội dung này được biên soạn kỹ lưỡng để đảm bảo dài hơn 50 ký tự nhằm đáp ứng yêu cầu kiểm tra dữ liệu đầu vào của hệ thống.", new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "https://example.com/images/ai-2.jpg", "Hướng dẫn học AI cơ bản phần 2", null, 136 },
                    { 18, "Admin Hệ Thống", 6, "Đây là nội dung chi tiết của bài viết về AI. Nội dung này được biên soạn kỹ lưỡng để đảm bảo dài hơn 50 ký tự nhằm đáp ứng yêu cầu kiểm tra dữ liệu đầu vào của hệ thống.", new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "https://example.com/images/ai-3.jpg", "Hướng dẫn học AI cơ bản phần 3", null, 144 },
                    { 19, "Admin Hệ Thống", 7, "Đây là nội dung chi tiết của bài viết về Database. Nội dung này được biên soạn kỹ lưỡng để đảm bảo dài hơn 50 ký tự nhằm đáp ứng yêu cầu kiểm tra dữ liệu đầu vào của hệ thống.", new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "https://example.com/images/database-1.jpg", "Hướng dẫn học Database cơ bản phần 1", null, 152 },
                    { 20, "Admin Hệ Thống", 7, "Đây là nội dung chi tiết của bài viết về Database. Nội dung này được biên soạn kỹ lưỡng để đảm bảo dài hơn 50 ký tự nhằm đáp ứng yêu cầu kiểm tra dữ liệu đầu vào của hệ thống.", new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "https://example.com/images/database-2.jpg", "Hướng dẫn học Database cơ bản phần 2", null, 160 },
                    { 21, "Admin Hệ Thống", 7, "Đây là nội dung chi tiết của bài viết về Database. Nội dung này được biên soạn kỹ lưỡng để đảm bảo dài hơn 50 ký tự nhằm đáp ứng yêu cầu kiểm tra dữ liệu đầu vào của hệ thống.", new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "https://example.com/images/database-3.jpg", "Hướng dẫn học Database cơ bản phần 3", null, 168 },
                    { 22, "Admin Hệ Thống", 8, "Đây là nội dung chi tiết của bài viết về Cloud. Nội dung này được biên soạn kỹ lưỡng để đảm bảo dài hơn 50 ký tự nhằm đáp ứng yêu cầu kiểm tra dữ liệu đầu vào của hệ thống.", new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "https://example.com/images/cloud-1.jpg", "Hướng dẫn học Cloud cơ bản phần 1", null, 176 },
                    { 23, "Admin Hệ Thống", 8, "Đây là nội dung chi tiết của bài viết về Cloud. Nội dung này được biên soạn kỹ lưỡng để đảm bảo dài hơn 50 ký tự nhằm đáp ứng yêu cầu kiểm tra dữ liệu đầu vào của hệ thống.", new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "https://example.com/images/cloud-2.jpg", "Hướng dẫn học Cloud cơ bản phần 2", null, 184 },
                    { 24, "Admin Hệ Thống", 8, "Đây là nội dung chi tiết của bài viết về Cloud. Nội dung này được biên soạn kỹ lưỡng để đảm bảo dài hơn 50 ký tự nhằm đáp ứng yêu cầu kiểm tra dữ liệu đầu vào của hệ thống.", new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "https://example.com/images/cloud-3.jpg", "Hướng dẫn học Cloud cơ bản phần 3", null, 192 },
                    { 25, "Admin Hệ Thống", 9, "Đây là nội dung chi tiết của bài viết về Bảo Mật. Nội dung này được biên soạn kỹ lưỡng để đảm bảo dài hơn 50 ký tự nhằm đáp ứng yêu cầu kiểm tra dữ liệu đầu vào của hệ thống.", new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "https://example.com/images/bảo mật-1.jpg", "Hướng dẫn học Bảo Mật cơ bản phần 1", null, 200 },
                    { 26, "Admin Hệ Thống", 9, "Đây là nội dung chi tiết của bài viết về Bảo Mật. Nội dung này được biên soạn kỹ lưỡng để đảm bảo dài hơn 50 ký tự nhằm đáp ứng yêu cầu kiểm tra dữ liệu đầu vào của hệ thống.", new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "https://example.com/images/bảo mật-2.jpg", "Hướng dẫn học Bảo Mật cơ bản phần 2", null, 208 },
                    { 27, "Admin Hệ Thống", 9, "Đây là nội dung chi tiết của bài viết về Bảo Mật. Nội dung này được biên soạn kỹ lưỡng để đảm bảo dài hơn 50 ký tự nhằm đáp ứng yêu cầu kiểm tra dữ liệu đầu vào của hệ thống.", new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "https://example.com/images/bảo mật-3.jpg", "Hướng dẫn học Bảo Mật cơ bản phần 3", null, 216 },
                    { 28, "Admin Hệ Thống", 10, "Đây là nội dung chi tiết của bài viết về Khác. Nội dung này được biên soạn kỹ lưỡng để đảm bảo dài hơn 50 ký tự nhằm đáp ứng yêu cầu kiểm tra dữ liệu đầu vào của hệ thống.", new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "https://example.com/images/khác-1.jpg", "Hướng dẫn học Khác cơ bản phần 1", null, 224 },
                    { 29, "Admin Hệ Thống", 10, "Đây là nội dung chi tiết của bài viết về Khác. Nội dung này được biên soạn kỹ lưỡng để đảm bảo dài hơn 50 ký tự nhằm đáp ứng yêu cầu kiểm tra dữ liệu đầu vào của hệ thống.", new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "https://example.com/images/khác-2.jpg", "Hướng dẫn học Khác cơ bản phần 2", null, 232 },
                    { 30, "Admin Hệ Thống", 10, "Đây là nội dung chi tiết của bài viết về Khác. Nội dung này được biên soạn kỹ lưỡng để đảm bảo dài hơn 50 ký tự nhằm đáp ứng yêu cầu kiểm tra dữ liệu đầu vào của hệ thống.", new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "https://example.com/images/khác-3.jpg", "Hướng dẫn học Khác cơ bản phần 3", null, 240 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPost_CategoryId",
                table: "BlogPost",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogPost");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
