using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace monqTest.Data.Migrations
{
    public partial class firstNigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    mailSubject = table.Column<string>(nullable: true),
                    mailBody = table.Column<string>(nullable: true),
                    mailRecipients = table.Column<string>(nullable: true),
                    mailCreationDate = table.Column<DateTime>(nullable: false),
                    mailResult = table.Column<int>(nullable: false),
                    failedMessage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mails");
        }
    }
}
