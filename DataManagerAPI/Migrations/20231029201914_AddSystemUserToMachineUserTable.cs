using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataManagerAPI.Migrations
{
    public partial class AddSystemUserToMachineUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommandsHistory_Machines_MachineId",
                table: "CommandsHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Users_UserId",
                table: "Machines");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Machines_UserId",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "MachineUser",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Machines");

            migrationBuilder.RenameColumn(
                name: "MachineId",
                table: "CommandsHistory",
                newName: "MachineUserId");

            migrationBuilder.RenameIndex(
                name: "IX_CommandsHistory_MachineId",
                table: "CommandsHistory",
                newName: "IX_CommandsHistory_MachineUserId");

            migrationBuilder.CreateTable(
                name: "MachineUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MachineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MachineUsers_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemUserToMachineUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("7e056d96-fd04-4d0f-8b39-20e3f3ec9d51")),
                    SystemUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MachineUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemUserToMachineUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemUserToMachineUser_MachineUsers_MachineUserId",
                        column: x => x.MachineUserId,
                        principalTable: "MachineUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MachineUsers_MachineId",
                table: "MachineUsers",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemUserToMachineUser_MachineUserId",
                table: "SystemUserToMachineUser",
                column: "MachineUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommandsHistory_MachineUsers_MachineUserId",
                table: "CommandsHistory",
                column: "MachineUserId",
                principalTable: "MachineUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommandsHistory_MachineUsers_MachineUserId",
                table: "CommandsHistory");

            migrationBuilder.DropTable(
                name: "SystemUserToMachineUser");

            migrationBuilder.DropTable(
                name: "MachineUsers");

            migrationBuilder.RenameColumn(
                name: "MachineUserId",
                table: "CommandsHistory",
                newName: "MachineId");

            migrationBuilder.RenameIndex(
                name: "IX_CommandsHistory_MachineUserId",
                table: "CommandsHistory",
                newName: "IX_CommandsHistory_MachineId");

            migrationBuilder.AddColumn<string>(
                name: "MachineUser",
                table: "Machines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Machines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Machines",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Machines_UserId",
                table: "Machines",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommandsHistory_Machines_MachineId",
                table: "CommandsHistory",
                column: "MachineId",
                principalTable: "Machines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Users_UserId",
                table: "Machines",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
