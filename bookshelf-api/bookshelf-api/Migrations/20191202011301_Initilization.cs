using Microsoft.EntityFrameworkCore.Migrations;

namespace bookshelf_api.Migrations
{
    public partial class Initilization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ISBN = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Author = table.Column<string>(nullable: true),
                    LoanedToId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Books_Users_LoanedToId",
                        column: x => x.LoanedToId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Author", "ISBN", "LoanedToId", "Title" },
                values: new object[] { 1, "Thomas H. Cormen, Charles E. Leiserson, Ronald L. Rivest, Clifford Stein", "978-0262033848", null, "Introduction to Algorithms" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Author", "ISBN", "LoanedToId", "Title" },
                values: new object[] { 2, "Jane Austen", "9780141439518", null, "Pride and Prejudice" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Author", "ISBN", "LoanedToId", "Title" },
                values: new object[] { 3, "Maxim Gorky", "978-1598955094", null, "The Mother v2" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Author", "ISBN", "LoanedToId", "Title" },
                values: new object[] { 4, "Maxim Gorky", "978-1544955094", null, "The Mother" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Name", "Password", "UserName" },
                values: new object[] { 1, "Channa", null, "channa@gmail.com" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Name", "Password", "UserName" },
                values: new object[] { 2, "Lasith", null, "lasitha@gmail.com" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Name", "Password", "UserName" },
                values: new object[] { 3, "Dilshan", null, "dilshan@gmail.com" });

            migrationBuilder.CreateIndex(
                name: "IX_Books_LoanedToId",
                table: "Books",
                column: "LoanedToId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
