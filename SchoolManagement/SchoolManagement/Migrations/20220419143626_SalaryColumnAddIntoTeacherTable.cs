using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolManagement.Migrations
{
    public partial class SalaryColumnAddIntoTeacherTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Teachers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2022, 4, 19, 18, 36, 26, 552, DateTimeKind.Local).AddTicks(1537),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 4, 18, 17, 30, 55, 804, DateTimeKind.Local).AddTicks(4727));

            migrationBuilder.AddColumn<double>(
                name: "Salary",
                table: "Teachers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "Students",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2022, 4, 19, 18, 36, 26, 551, DateTimeKind.Local).AddTicks(9859),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 4, 18, 17, 30, 55, 804, DateTimeKind.Local).AddTicks(3536));

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Students",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2022, 4, 19, 18, 36, 26, 548, DateTimeKind.Local).AddTicks(9556),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 4, 18, 17, 30, 55, 802, DateTimeKind.Local).AddTicks(592));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Teachers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Teachers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2022, 4, 18, 17, 30, 55, 804, DateTimeKind.Local).AddTicks(4727),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 4, 19, 18, 36, 26, 552, DateTimeKind.Local).AddTicks(1537));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "Students",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2022, 4, 18, 17, 30, 55, 804, DateTimeKind.Local).AddTicks(3536),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 4, 19, 18, 36, 26, 551, DateTimeKind.Local).AddTicks(9859));

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Students",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2022, 4, 18, 17, 30, 55, 802, DateTimeKind.Local).AddTicks(592),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 4, 19, 18, 36, 26, 548, DateTimeKind.Local).AddTicks(9556));
        }
    }
}
