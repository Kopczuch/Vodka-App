using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Konefeld.Kopiec.VodkaApp.DaoSqlite.Migrations
{
    /// <inheritdoc />
    public partial class RemoveProducerAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Producers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Producers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
