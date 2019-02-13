using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JWT.Persistence.Migrations.Identity
{
    public partial class SetDefaultDateJoined : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateJoined",
                table: "AspNetUsers",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTimeOffset));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateJoined",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "now()");
        }
    }
}
