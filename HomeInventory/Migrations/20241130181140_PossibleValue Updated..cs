using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HomeInventory.Migrations
{
    /// <inheritdoc />
    public partial class PossibleValueUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Possible",
                table: "Inventories");

            migrationBuilder.AddColumn<int>(
                name: "PossibleValueId",
                table: "Inventories",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PossibleValue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PossibleValueType = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: true),
                    Currency = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Description = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PossibleValue", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_PossibleValueId",
                table: "Inventories",
                column: "PossibleValueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_PossibleValue_PossibleValueId",
                table: "Inventories",
                column: "PossibleValueId",
                principalTable: "PossibleValue",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_PossibleValue_PossibleValueId",
                table: "Inventories");

            migrationBuilder.DropTable(
                name: "PossibleValue");

            migrationBuilder.DropIndex(
                name: "IX_Inventories_PossibleValueId",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "PossibleValueId",
                table: "Inventories");

            migrationBuilder.AddColumn<double>(
                name: "Possible",
                table: "Inventories",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
