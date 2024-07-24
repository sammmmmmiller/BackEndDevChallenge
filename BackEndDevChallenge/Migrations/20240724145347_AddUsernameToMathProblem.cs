using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndDevChallenge.Migrations
{
    /// <inheritdoc />
    public partial class AddUsernameToMathProblem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "MathProblems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Legacy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "MathProblems");
        }
    }
}
