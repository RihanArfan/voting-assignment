using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ManyDataImprovements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Party_Election_ElectionId",
                table: "Party");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Election_ElectionId",
                table: "Vote");

            migrationBuilder.DropIndex(
                name: "IX_Party_ElectionId",
                table: "Party");

            migrationBuilder.DropIndex(
                name: "IX_NationalInsurance_UserId",
                table: "NationalInsurance");

            migrationBuilder.DropColumn(
                name: "ElectionId",
                table: "Party");

            migrationBuilder.RenameColumn(
                name: "IsOnline",
                table: "Token",
                newName: "IsOnlineVote");

            migrationBuilder.AlterColumn<int>(
                name: "ElectionId",
                table: "Vote",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateTable(
                name: "ElectionParty",
                columns: table => new
                {
                    ElectionId = table.Column<int>(type: "INTEGER", nullable: false),
                    PartiesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectionParty", x => new { x.ElectionId, x.PartiesId });
                    table.ForeignKey(
                        name: "FK_ElectionParty_Election_ElectionId",
                        column: x => x.ElectionId,
                        principalTable: "Election",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElectionParty_Party_PartiesId",
                        column: x => x.PartiesId,
                        principalTable: "Party",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NationalInsurance_UserId",
                table: "NationalInsurance",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ElectionParty_PartiesId",
                table: "ElectionParty",
                column: "PartiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Election_ElectionId",
                table: "Vote",
                column: "ElectionId",
                principalTable: "Election",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Election_ElectionId",
                table: "Vote");

            migrationBuilder.DropTable(
                name: "ElectionParty");

            migrationBuilder.DropIndex(
                name: "IX_NationalInsurance_UserId",
                table: "NationalInsurance");

            migrationBuilder.RenameColumn(
                name: "IsOnlineVote",
                table: "Token",
                newName: "IsOnline");

            migrationBuilder.AlterColumn<int>(
                name: "ElectionId",
                table: "Vote",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ElectionId",
                table: "Party",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Party_ElectionId",
                table: "Party",
                column: "ElectionId");

            migrationBuilder.CreateIndex(
                name: "IX_NationalInsurance_UserId",
                table: "NationalInsurance",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Party_Election_ElectionId",
                table: "Party",
                column: "ElectionId",
                principalTable: "Election",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Election_ElectionId",
                table: "Vote",
                column: "ElectionId",
                principalTable: "Election",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
