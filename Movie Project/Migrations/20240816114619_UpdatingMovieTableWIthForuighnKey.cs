using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movie_Project.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingMovieTableWIthForuighnKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CatId",
                table: "M_Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RatId",
                table: "M_Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_M_Movies_CatId",
                table: "M_Movies",
                column: "CatId");

            migrationBuilder.CreateIndex(
                name: "IX_M_Movies_RatId",
                table: "M_Movies",
                column: "RatId");

            migrationBuilder.AddForeignKey(
                name: "FK_M_Movies_C_Category_CatId",
                table: "M_Movies",
                column: "CatId",
                principalTable: "C_Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_M_Movies_R_Ratings_RatId",
                table: "M_Movies",
                column: "RatId",
                principalTable: "R_Ratings",
                principalColumn: "RatingsId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_M_Movies_C_Category_CatId",
                table: "M_Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_M_Movies_R_Ratings_RatId",
                table: "M_Movies");

            migrationBuilder.DropIndex(
                name: "IX_M_Movies_CatId",
                table: "M_Movies");

            migrationBuilder.DropIndex(
                name: "IX_M_Movies_RatId",
                table: "M_Movies");

            migrationBuilder.DropColumn(
                name: "CatId",
                table: "M_Movies");

            migrationBuilder.DropColumn(
                name: "RatId",
                table: "M_Movies");
        }
    }
}
