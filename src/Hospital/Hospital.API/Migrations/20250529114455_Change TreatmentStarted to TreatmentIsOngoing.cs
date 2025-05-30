using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.API.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTreatmentStartedtoTreatmentIsOngoing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TreatmentStarted",
                table: "MedicalRecords",
                newName: "TreatmentIsOngoing");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TreatmentIsOngoing",
                table: "MedicalRecords",
                newName: "TreatmentStarted");
        }
    }
}
