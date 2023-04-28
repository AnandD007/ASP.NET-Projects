using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatterManagementWebApp.Services.Migrations
{
    /// <inheritdoc />
    public partial class MigrationV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Age = table.Column<int>(type: "int", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_ClientId", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "Jurisdiction",
                columns: table => new
                {
                    JurisdictionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Area = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EmailId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_JurisdictionId", x => x.JurisdictionId);
                });

            migrationBuilder.CreateTable(
                name: "Attorneys",
                columns: table => new
                {
                    AttorneyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HourlyRate = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JurisdictionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_AttorneyId", x => x.AttorneyId);
                    table.ForeignKey(
                        name: "FK_Attorneys_Jurisdiction_JurisdictionId",
                        column: x => x.JurisdictionId,
                        principalTable: "Jurisdiction",
                        principalColumn: "JurisdictionId");
                });

            migrationBuilder.CreateTable(
                name: "Matters",
                columns: table => new
                {
                    MatterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    OpenDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CloseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JurisdictionId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    BillingAttorneyId = table.Column<int>(type: "int", nullable: false),
                    ResponsibleAttorneyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_MatterId", x => x.MatterId);
                    table.ForeignKey(
                        name: "FK_Matters_Attorneys_BillingAttorneyId",
                        column: x => x.BillingAttorneyId,
                        principalTable: "Attorneys",
                        principalColumn: "AttorneyId");
                    table.ForeignKey(
                        name: "FK_Matters_Attorneys_ResponsibleAttorneyId",
                        column: x => x.ResponsibleAttorneyId,
                        principalTable: "Attorneys",
                        principalColumn: "AttorneyId");
                    table.ForeignKey(
                        name: "FK_Matters_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "ClientId");
                    table.ForeignKey(
                        name: "FK_Matters_Jurisdiction_JurisdictionId",
                        column: x => x.JurisdictionId,
                        principalTable: "Jurisdiction",
                        principalColumn: "JurisdictionId");
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoursWorked = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MatterId = table.Column<int>(type: "int", nullable: false),
                    AttorneyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_InvoiceId", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoice_Attorneys_AttorneyId",
                        column: x => x.AttorneyId,
                        principalTable: "Attorneys",
                        principalColumn: "AttorneyId");
                    table.ForeignKey(
                        name: "FK_Invoice_Matters_MatterId",
                        column: x => x.MatterId,
                        principalTable: "Matters",
                        principalColumn: "MatterId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attorneys_JurisdictionId",
                table: "Attorneys",
                column: "JurisdictionId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_AttorneyId",
                table: "Invoice",
                column: "AttorneyId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_MatterId",
                table: "Invoice",
                column: "MatterId");

            migrationBuilder.CreateIndex(
                name: "IX_Matters_BillingAttorneyId",
                table: "Matters",
                column: "BillingAttorneyId");

            migrationBuilder.CreateIndex(
                name: "IX_Matters_ClientId",
                table: "Matters",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Matters_JurisdictionId",
                table: "Matters",
                column: "JurisdictionId");

            migrationBuilder.CreateIndex(
                name: "IX_Matters_ResponsibleAttorneyId",
                table: "Matters",
                column: "ResponsibleAttorneyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "Matters");

            migrationBuilder.DropTable(
                name: "Attorneys");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Jurisdiction");
        }
    }
}
