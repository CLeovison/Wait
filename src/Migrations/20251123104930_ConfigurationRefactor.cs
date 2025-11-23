using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace src.Migrations
{
    /// <inheritdoc />
    public partial class ConfigurationRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploadedAt",
                table: "Image");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Image",
                newName: "StorageUrl");

            migrationBuilder.RenameColumn(
                name: "UploadedBy",
                table: "Image",
                newName: "OriginalFileName");

            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Image",
                newName: "ObjectKey");

            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "Image",
                newName: "MimeType");

            migrationBuilder.AlterColumn<Guid>(
                name: "ImageId",
                table: "Image",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Image",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "current_date");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUploaded",
                table: "Image",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "current_date");

            migrationBuilder.AddColumn<string>(
                name: "FileExtension",
                table: "Image",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Image",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Image",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "DateUploaded",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "FileExtension",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Image");

            migrationBuilder.RenameColumn(
                name: "StorageUrl",
                table: "Image",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "OriginalFileName",
                table: "Image",
                newName: "UploadedBy");

            migrationBuilder.RenameColumn(
                name: "ObjectKey",
                table: "Image",
                newName: "ImagePath");

            migrationBuilder.RenameColumn(
                name: "MimeType",
                table: "Image",
                newName: "ImageName");

            migrationBuilder.AlterColumn<string>(
                name: "ImageId",
                table: "Image",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadedAt",
                table: "Image",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
