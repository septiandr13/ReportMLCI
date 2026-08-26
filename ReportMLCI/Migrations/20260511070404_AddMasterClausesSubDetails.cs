using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportMLCI.Migrations
{
    /// <inheritdoc />
    public partial class AddMasterClausesSubDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MasterClauses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ClauseCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ClauseTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ClauseContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterClauses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterClausesDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    MasterClauseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClauseSubCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ClauseSubTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ClauseSubContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MasterClauseId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterClausesDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MasterClausesDetails_MasterClauses_MasterClauseId",
                        column: x => x.MasterClauseId,
                        principalTable: "MasterClauses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MasterClausesDetails_MasterClauses_MasterClauseId1",
                        column: x => x.MasterClauseId1,
                        principalTable: "MasterClauses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MasterClausesSubDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    MasterClauseDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubDetailCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    SubDetailTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SubDetailContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterClausesSubDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MasterClausesSubDetails_MasterClausesDetails_MasterClauseDetailId",
                        column: x => x.MasterClauseDetailId,
                        principalTable: "MasterClausesDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MasterClausesDetails_MasterClauseId",
                table: "MasterClausesDetails",
                column: "MasterClauseId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterClausesDetails_MasterClauseId1",
                table: "MasterClausesDetails",
                column: "MasterClauseId1");

            migrationBuilder.CreateIndex(
                name: "IX_MasterClausesSubDetails_MasterClauseDetailId",
                table: "MasterClausesSubDetails",
                column: "MasterClauseDetailId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MasterClausesSubDetails");

            migrationBuilder.DropTable(
                name: "MasterClausesDetails");

            migrationBuilder.DropTable(
                name: "MasterClauses");
        }
    }
}
