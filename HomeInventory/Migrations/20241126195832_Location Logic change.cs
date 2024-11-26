using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HomeInventory.Migrations
{
    /// <inheritdoc />
    public partial class LocationLogicchange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Inventories");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Inventories",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    LocationType = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    Adress = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    X = table.Column<double>(type: "double precision", nullable: true),
                    Y = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_LocationId",
                table: "Inventories",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_Location_LocationId",
                table: "Inventories",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Location_LocationId",
                table: "Inventories");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropIndex(
                name: "IX_Inventories_LocationId",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Inventories");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Inventories",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
