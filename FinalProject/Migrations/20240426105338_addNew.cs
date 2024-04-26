using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject.Migrations
{
    public partial class addNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "Blogs",
                newName: "UpdatedDate");

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Blogs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Blogs");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Blogs",
                newName: "UpdateDate");
        }
    }
}
