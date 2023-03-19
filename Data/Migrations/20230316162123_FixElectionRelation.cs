using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class FixElectionRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Election_ElectionId",
                table: "Vote");

            migrationBuilder.DropIndex(
                name: "IX_Vote_ElectionId",
                table: "Vote");

            migrationBuilder.DropColumn(
                name: "ElectionId",
                table: "Vote");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ElectionId",
                table: "Vote",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vote_ElectionId",
                table: "Vote",
                column: "ElectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Election_ElectionId",
                table: "Vote",
                column: "ElectionId",
                principalTable: "Election",
                principalColumn: "Id");
        }
    }
}
