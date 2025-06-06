using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeBlog.API.Migrations
{
    /// <inheritdoc />
    public partial class fixednamingconventions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "URLHandle",
                table: "BlogPosts",
                newName: "UrlHandle");

            migrationBuilder.RenameColumn(
                name: "FeaturedImageURL",
                table: "BlogPosts",
                newName: "FeaturedImageUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UrlHandle",
                table: "BlogPosts",
                newName: "URLHandle");

            migrationBuilder.RenameColumn(
                name: "FeaturedImageUrl",
                table: "BlogPosts",
                newName: "FeaturedImageURL");
        }
    }
}
