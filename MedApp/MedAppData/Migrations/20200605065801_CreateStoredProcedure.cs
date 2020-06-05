using Microsoft.EntityFrameworkCore.Migrations;

namespace MedAppData.Migrations
{
    public partial class CreateStoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE UpdateAppointmentStoredProcedure
                        @DoctrorId uniqueidentifier,
		                @PatientId uniqueidentifier,
		                @Date DATETIME
                    AS
                    BEGIN
	                SET NOCOUNT ON;
	                UPDATE Appointments SET [DateTime] = @Date
                    WHERE DoctorId = @DoctrorId AND PatientId = @PatientId   
                    END
                    GO";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
