using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class LinkVoteToToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PartyId",
                table: "Vote",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TokenId",
                table: "Vote",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vote_PartyId",
                table: "Vote",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_TokenId",
                table: "Vote",
                column: "TokenId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Party_PartyId",
                table: "Vote",
                column: "PartyId",
                principalTable: "Party",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Token_TokenId",
                table: "Vote",
                column: "TokenId",
                principalTable: "Token",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Party_PartyId",
                table: "Vote");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Token_TokenId",
                table: "Vote");

            migrationBuilder.DropIndex(
                name: "IX_Vote_PartyId",
                table: "Vote");

            migrationBuilder.DropIndex(
                name: "IX_Vote_TokenId",
                table: "Vote");

            migrationBuilder.DropColumn(
                name: "PartyId",
                table: "Vote");

            migrationBuilder.DropColumn(
                name: "TokenId",
                table: "Vote");
        }
    }
}
