using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Auth.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                    table.UniqueConstraint("AK_roles_code", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                    table.UniqueConstraint("AK_users_username", x => x.username);
                    table.ForeignKey(
                        name: "FK_users_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_role_id",
                table: "users",
                column: "role_id");

            /* add roles */
            
            migrationBuilder.Sql(
                "insert into roles (id, code, created_at, updated_at)"+ 
                "values (1, 'User', now(), now())"
            );
            
            migrationBuilder.Sql(
                "insert into roles (id, code, created_at, updated_at)"+ 
                "values (2, 'Moderator', now(), now())"
            );
            
            migrationBuilder.Sql(
                "insert into roles (id, code, created_at, updated_at)"+ 
                "values (3, 'Admin', now(), now())"
            );
            
            /* add users */
            
            migrationBuilder.Sql(
                "INSERT INTO \"users\" (username, password, role_id, created_at, updated_at) VALUES(" +
                "'user'," +
                "'QMlcrdFiffW+C3Mq6J7MxQ=='," +
                "1," +
                "now()," + 
                "now()" +
                ") ON CONFLICT DO NOTHING;"
            );
            
            migrationBuilder.Sql(
                "INSERT INTO \"users\" (username, password, role_id, created_at, updated_at) VALUES(" +
                "'moderator'," +
                "'V9OXwKERi8ST2aES3iATvw=='," +
                "2," +
                "now()," + 
                "now()" +
                ") ON CONFLICT DO NOTHING;"
            );
            
            migrationBuilder.Sql(
                "INSERT INTO \"users\" (username, password, role_id, created_at, updated_at) VALUES(" +
                "'admin'," +
                "'kKxEtQgZ20gp8EJPWuHQ9w=='," +
                "3," +
                "now()," + 
                "now()" +
                ") ON CONFLICT DO NOTHING;"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
