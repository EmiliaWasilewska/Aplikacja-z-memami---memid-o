﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Memidło.Web.Migrations
{
    /// <inheritdoc />
    public partial class addinguseridtocommentmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Comments");
        }
    }
}
