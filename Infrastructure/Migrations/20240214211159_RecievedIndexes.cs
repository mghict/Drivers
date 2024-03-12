using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Driver.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RecievedIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_Recieved_StartedMission_AutoId",
                table: "Recieved_StartedMission",
                newName: "IX_RecievedWeight_AutoId");

            migrationBuilder.RenameIndex(
                name: "IX_Recieved_SpeedAndTemprature_RecievedWeightId",
                table: "Recieved_SpeedAndTemprature",
                newName: "IX_RecievedSpeedAndTemprature_RecievedWeightId");

            migrationBuilder.RenameIndex(
                name: "IX_Recieved_SpeedAndTemprature_AutoId",
                table: "Recieved_SpeedAndTemprature",
                newName: "IX_RecievedSpeedAndTempraturer_AutoId");

            migrationBuilder.RenameIndex(
                name: "IX_Recieved_Number_AutoId",
                table: "Recieved_Number",
                newName: "IX_RecievedNumberr_AutoId");

            migrationBuilder.RenameIndex(
                name: "IX_Recieved_FinishedMission_AutoId",
                table: "Recieved_FinishedMission",
                newName: "IX_RecievedMission_AutoId");

            migrationBuilder.RenameIndex(
                name: "IX_Recieved_Error_AutoId",
                table: "Recieved_Error",
                newName: "IX_RecievedError_AutoId");

            migrationBuilder.CreateIndex(
                name: "IX_RecievedWeight_DeviceCode",
                table: "Recieved_StartedMission",
                column: "DeviceCode");

            migrationBuilder.CreateIndex(
                name: "IX_RecievedWeight_MissionCode",
                table: "Recieved_StartedMission",
                column: "MissionCode");

            migrationBuilder.CreateIndex(
                name: "IX_RecievedWeight_RowNumber",
                table: "Recieved_StartedMission",
                column: "RowNumber");

            migrationBuilder.CreateIndex(
                name: "IX_RecievedWeight_SendDate",
                table: "Recieved_StartedMission",
                column: "SendDate");

            migrationBuilder.CreateIndex(
                name: "IX_RecievedSpeedAndTemprature_DeviceCode",
                table: "Recieved_SpeedAndTemprature",
                column: "DeviceCode");

            migrationBuilder.CreateIndex(
                name: "IX_RecievedSpeedAndTemprature_MissionCode",
                table: "Recieved_SpeedAndTemprature",
                column: "MissionCode");

            migrationBuilder.CreateIndex(
                name: "IX_RecievedSpeedAndTemprature_RowNumber",
                table: "Recieved_SpeedAndTemprature",
                column: "RowNumber");

            migrationBuilder.CreateIndex(
                name: "IX_RecievedSpeedAndTemprature_SendDate",
                table: "Recieved_SpeedAndTemprature",
                column: "SendDate");

            migrationBuilder.CreateIndex(
                name: "IX_RecievedNumber_DeviceCode",
                table: "Recieved_Number",
                column: "DeviceCode");

            migrationBuilder.CreateIndex(
                name: "IX_RecievedNumber_RowNumber",
                table: "Recieved_Number",
                column: "RowNumber");

            migrationBuilder.CreateIndex(
                name: "IX_RecievedNumber_SendDate",
                table: "Recieved_Number",
                column: "SendDate");

            migrationBuilder.CreateIndex(
                name: "IX_RecievedMission_DeviceCode",
                table: "Recieved_FinishedMission",
                column: "DeviceCode");

            migrationBuilder.CreateIndex(
                name: "IX_RecievedMission_MissionCode",
                table: "Recieved_FinishedMission",
                column: "MissionCode");

            migrationBuilder.CreateIndex(
                name: "IX_RecievedMission_RecievedWeightId",
                table: "Recieved_FinishedMission",
                column: "RecievedWeightId");

            migrationBuilder.CreateIndex(
                name: "IX_RecievedMission_RowNumber",
                table: "Recieved_FinishedMission",
                column: "RowNumber");

            migrationBuilder.CreateIndex(
                name: "IX_RecievedMission_SendDate",
                table: "Recieved_FinishedMission",
                column: "SendDate");

            migrationBuilder.CreateIndex(
                name: "IX_RecievedError_DeviceCode",
                table: "Recieved_Error",
                column: "DeviceCode");

            migrationBuilder.CreateIndex(
                name: "IX_RecievedError_RowNumber",
                table: "Recieved_Error",
                column: "RowNumber");

            migrationBuilder.CreateIndex(
                name: "IX_RecievedError_SendDate",
                table: "Recieved_Error",
                column: "SendDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RecievedWeight_DeviceCode",
                table: "Recieved_StartedMission");

            migrationBuilder.DropIndex(
                name: "IX_RecievedWeight_MissionCode",
                table: "Recieved_StartedMission");

            migrationBuilder.DropIndex(
                name: "IX_RecievedWeight_RowNumber",
                table: "Recieved_StartedMission");

            migrationBuilder.DropIndex(
                name: "IX_RecievedWeight_SendDate",
                table: "Recieved_StartedMission");

            migrationBuilder.DropIndex(
                name: "IX_RecievedSpeedAndTemprature_DeviceCode",
                table: "Recieved_SpeedAndTemprature");

            migrationBuilder.DropIndex(
                name: "IX_RecievedSpeedAndTemprature_MissionCode",
                table: "Recieved_SpeedAndTemprature");

            migrationBuilder.DropIndex(
                name: "IX_RecievedSpeedAndTemprature_RowNumber",
                table: "Recieved_SpeedAndTemprature");

            migrationBuilder.DropIndex(
                name: "IX_RecievedSpeedAndTemprature_SendDate",
                table: "Recieved_SpeedAndTemprature");

            migrationBuilder.DropIndex(
                name: "IX_RecievedNumber_DeviceCode",
                table: "Recieved_Number");

            migrationBuilder.DropIndex(
                name: "IX_RecievedNumber_RowNumber",
                table: "Recieved_Number");

            migrationBuilder.DropIndex(
                name: "IX_RecievedNumber_SendDate",
                table: "Recieved_Number");

            migrationBuilder.DropIndex(
                name: "IX_RecievedMission_DeviceCode",
                table: "Recieved_FinishedMission");

            migrationBuilder.DropIndex(
                name: "IX_RecievedMission_MissionCode",
                table: "Recieved_FinishedMission");

            migrationBuilder.DropIndex(
                name: "IX_RecievedMission_RecievedWeightId",
                table: "Recieved_FinishedMission");

            migrationBuilder.DropIndex(
                name: "IX_RecievedMission_RowNumber",
                table: "Recieved_FinishedMission");

            migrationBuilder.DropIndex(
                name: "IX_RecievedMission_SendDate",
                table: "Recieved_FinishedMission");

            migrationBuilder.DropIndex(
                name: "IX_RecievedError_DeviceCode",
                table: "Recieved_Error");

            migrationBuilder.DropIndex(
                name: "IX_RecievedError_RowNumber",
                table: "Recieved_Error");

            migrationBuilder.DropIndex(
                name: "IX_RecievedError_SendDate",
                table: "Recieved_Error");

            migrationBuilder.RenameIndex(
                name: "IX_RecievedWeight_AutoId",
                table: "Recieved_StartedMission",
                newName: "IX_Recieved_StartedMission_AutoId");

            migrationBuilder.RenameIndex(
                name: "IX_RecievedSpeedAndTempraturer_AutoId",
                table: "Recieved_SpeedAndTemprature",
                newName: "IX_Recieved_SpeedAndTemprature_AutoId");

            migrationBuilder.RenameIndex(
                name: "IX_RecievedSpeedAndTemprature_RecievedWeightId",
                table: "Recieved_SpeedAndTemprature",
                newName: "IX_Recieved_SpeedAndTemprature_RecievedWeightId");

            migrationBuilder.RenameIndex(
                name: "IX_RecievedNumberr_AutoId",
                table: "Recieved_Number",
                newName: "IX_Recieved_Number_AutoId");

            migrationBuilder.RenameIndex(
                name: "IX_RecievedMission_AutoId",
                table: "Recieved_FinishedMission",
                newName: "IX_Recieved_FinishedMission_AutoId");

            migrationBuilder.RenameIndex(
                name: "IX_RecievedError_AutoId",
                table: "Recieved_Error",
                newName: "IX_Recieved_Error_AutoId");
        }
    }
}
