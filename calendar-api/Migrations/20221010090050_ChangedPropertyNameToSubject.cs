using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace signalr.Migrations
{
    public partial class ChangedPropertyNameToSubject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "CalendarEvents",
                newName: "Subject");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Subject",
                table: "CalendarEvents",
                newName: "Name");
        }
    }
}
