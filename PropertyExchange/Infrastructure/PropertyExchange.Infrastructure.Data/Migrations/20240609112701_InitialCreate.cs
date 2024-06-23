using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyExchange.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tbl_pe_Property",
                columns: table => new
                {
                    PropertyID = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Location = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PropertyType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HoldingType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OverallAreaInSft = table.Column<decimal>(type: "decimal(65,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsAvailableForInvesting = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    InitialTotalValuation = table.Column<decimal>(type: "decimal(65,2)", nullable: false),
                    InitialTokenPrice = table.Column<decimal>(type: "decimal(65,2)", nullable: false),
                    InitialTotalNumberOfTokens = table.Column<int>(type: "int", nullable: false),
                    InitialAvailableNumberOfTokens = table.Column<int>(type: "int", nullable: false),
                    InitialTotalTokensValuation = table.Column<int>(type: "int", nullable: false),
                    CurrentTotalValuation = table.Column<decimal>(type: "decimal(65,2)", nullable: false),
                    CurrentTokenPrice = table.Column<decimal>(type: "decimal(65,2)", nullable: false),
                    CurrentTotalNumberOfTokens = table.Column<int>(type: "int", nullable: false),
                    CurrentAvailableNumberOfTokens = table.Column<int>(type: "int", nullable: false),
                    CurrentTotalTokensValuation = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastUpdatedDate = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_pe_Property", x => x.PropertyID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tbl_pe_UserLogin",
                columns: table => new
                {
                    UserEmail = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserPhoneNumber = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LoginDateTime = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_pe_UserLogin", x => new { x.UserEmail, x.UserPhoneNumber, x.LoginDateTime });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tbl_pe_UserRegistration",
                columns: table => new
                {
                    UserEmail = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserPhoneNumber = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id = table.Column<int>(type: "int", nullable: false),
                    UserPassword = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Role = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastUpdatedDate = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Salt = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_pe_UserRegistration", x => new { x.UserEmail, x.UserPhoneNumber });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tbl_pe_PropertyAddressDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PropertyID = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressLine1 = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressLine2 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressLine3 = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Landmark = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    City = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    State = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Country = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PINCode = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastUpdatedDate = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_pe_PropertyAddressDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_pe_PropertyAddressDetails_tbl_pe_Property_PropertyID",
                        column: x => x.PropertyID,
                        principalTable: "tbl_pe_Property",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tbl_pe_PropertyExpensesBreakOut",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PropertyID = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExpenseType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExpenseAmount = table.Column<decimal>(type: "decimal(65,2)", nullable: false),
                    ExpenseIncurredOnDate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExpenseMonth = table.Column<int>(type: "int", nullable: false),
                    ExpenseYear = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastUpdatedDate = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_pe_PropertyExpensesBreakOut", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_pe_PropertyExpensesBreakOut_tbl_pe_Property_PropertyID",
                        column: x => x.PropertyID,
                        principalTable: "tbl_pe_Property",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tbl_pe_PropertyIncomeBreakOut",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PropertyID = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IncomeType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IncomeAmount = table.Column<decimal>(type: "decimal(65,2)", nullable: false),
                    IncomeGeneratedOnDate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IncomeMonth = table.Column<int>(type: "int", nullable: false),
                    IncomeYear = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastUpdatedDate = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_pe_PropertyIncomeBreakOut", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_pe_PropertyIncomeBreakOut_tbl_pe_Property_PropertyID",
                        column: x => x.PropertyID,
                        principalTable: "tbl_pe_Property",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tbl_pe_PropertyPassbook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PropertyID = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TxnType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TxnNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(65,2)", nullable: false),
                    TotalTxnValue = table.Column<decimal>(type: "decimal(65,2)", nullable: false),
                    OrderDateTime = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastUpdatedDate = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_pe_PropertyPassbook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_pe_PropertyPassbook_tbl_pe_Property_PropertyID",
                        column: x => x.PropertyID,
                        principalTable: "tbl_pe_Property",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tbl_pe_PropertyProjectedValuationMetrics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PropertyID = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PricePerSft = table.Column<decimal>(type: "decimal(65,2)", nullable: false),
                    TotalSft = table.Column<decimal>(type: "decimal(65,2)", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastUpdatedDate = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_pe_PropertyProjectedValuationMetrics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_pe_PropertyProjectedValuationMetrics_tbl_pe_Property_Pro~",
                        column: x => x.PropertyID,
                        principalTable: "tbl_pe_Property",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tbl_pe_PropertyPurchaseOrSaleExpensesBreakOut",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PropertyID = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PurchaseOrSale = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExpenseType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExpenseAmount = table.Column<decimal>(type: "decimal(65,2)", nullable: false),
                    ExpenseIncurredOnDate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExpenseMonth = table.Column<int>(type: "int", nullable: false),
                    ExpenseYear = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastUpdatedDate = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_pe_PropertyPurchaseOrSaleExpensesBreakOut", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_pe_PropertyPurchaseOrSaleExpensesBreakOut_tbl_pe_Propert~",
                        column: x => x.PropertyID,
                        principalTable: "tbl_pe_Property",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tbl_pe_PropertyTradeRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PropertyID = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CurrentTokenPrice = table.Column<decimal>(type: "decimal(65,2)", nullable: false),
                    CurrentTotalNumberOfTokens = table.Column<int>(type: "int", nullable: false),
                    CurrentAvailableNumberOfTokens = table.Column<int>(type: "int", nullable: false),
                    CurrentTotalTokensValuation = table.Column<decimal>(type: "decimal(65,2)", nullable: false),
                    CreatedDate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastUpdatedDate = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_pe_PropertyTradeRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_pe_PropertyTradeRecords_tbl_pe_Property_PropertyID",
                        column: x => x.PropertyID,
                        principalTable: "tbl_pe_Property",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tbl_pe_PropertyValuationMetrics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PropertyID = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PricePerSft = table.Column<decimal>(type: "decimal(65,2)", nullable: false),
                    TotalSft = table.Column<decimal>(type: "decimal(65,2)", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastUpdatedDate = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_pe_PropertyValuationMetrics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_pe_PropertyValuationMetrics_tbl_pe_Property_PropertyID",
                        column: x => x.PropertyID,
                        principalTable: "tbl_pe_Property",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tbl_pe_Tenant",
                columns: table => new
                {
                    TenantID = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id = table.Column<int>(type: "int", nullable: false),
                    PropertyID = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Country = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TenancyType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastUpdatedDate = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_pe_Tenant", x => x.TenantID);
                    table.ForeignKey(
                        name: "FK_tbl_pe_Tenant_tbl_pe_Property_PropertyID",
                        column: x => x.PropertyID,
                        principalTable: "tbl_pe_Property",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tbl_pe_UserDetails",
                columns: table => new
                {
                    UserEmail = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserPhoneNumber = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DOB = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Gender = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecondaryEmail = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecondaryPhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Occupation = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IncomeRange = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MaritalStatus = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FatherName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AvailableMoneyForInvesting = table.Column<decimal>(type: "decimal(65,2)", nullable: true),
                    CreatedDate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastUpdatedDate = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_pe_UserDetails", x => new { x.UserEmail, x.UserPhoneNumber });
                    table.ForeignKey(
                        name: "FK_tbl_pe_UserDetails_tbl_pe_UserRegistration_UserEmail_UserPho~",
                        columns: x => new { x.UserEmail, x.UserPhoneNumber },
                        principalTable: "tbl_pe_UserRegistration",
                        principalColumns: new[] { "UserEmail", "UserPhoneNumber" },
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tbl_pe_TenantLeaseDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TenantID = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PropertyID = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LeaseAdvanceAmount = table.Column<decimal>(type: "decimal(65,2)", nullable: false),
                    LeaseStartDate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LeaseEndDate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LeaseTenureInMonths = table.Column<int>(type: "int", nullable: false),
                    RentPerSft = table.Column<decimal>(type: "decimal(65,2)", nullable: false),
                    TotalAreaInSft = table.Column<decimal>(type: "decimal(65,2)", nullable: false),
                    EscalationInPercentage = table.Column<decimal>(type: "decimal(65,2)", nullable: false),
                    EscalationTenure = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastUpdatedDate = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_pe_TenantLeaseDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_pe_TenantLeaseDetails_tbl_pe_Tenant_TenantID",
                        column: x => x.TenantID,
                        principalTable: "tbl_pe_Tenant",
                        principalColumn: "TenantID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tbl_pe_TenantRentPaymentDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TenantID = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PropertyID = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExpenseType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExpenseAmount = table.Column<decimal>(type: "decimal(65,2)", nullable: false),
                    ExpenseIncurredOnDate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExpenseMonth = table.Column<int>(type: "int", nullable: false),
                    ExpenseYear = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastUpdatedDate = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_pe_TenantRentPaymentDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_pe_TenantRentPaymentDetails_tbl_pe_Tenant_TenantID",
                        column: x => x.TenantID,
                        principalTable: "tbl_pe_Tenant",
                        principalColumn: "TenantID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tbl_pe_UserFundDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserEmail = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserPhoneNumber = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TxnNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddOrWithdraw = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TxnAmount = table.Column<decimal>(type: "decimal(65,2)", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ModeOfTxn = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TxnDateTime = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BankName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastUpdatedDate = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_pe_UserFundDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_pe_UserFundDetails_tbl_pe_UserDetails_UserEmail_UserPhon~",
                        columns: x => new { x.UserEmail, x.UserPhoneNumber },
                        principalTable: "tbl_pe_UserDetails",
                        principalColumns: new[] { "UserEmail", "UserPhoneNumber" },
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tbl_pe_UserPassbook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserEmail = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserPhoneNumber = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PropertyID = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TxnNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TxnType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(65,2)", nullable: false),
                    TotalTxnValue = table.Column<decimal>(type: "decimal(65,2)", nullable: false),
                    OrderDateTime = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsCurrentHolding = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastUpdatedDate = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_pe_UserPassbook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_pe_UserPassbook_tbl_pe_Property_PropertyID",
                        column: x => x.PropertyID,
                        principalTable: "tbl_pe_Property",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_pe_UserPassbook_tbl_pe_UserDetails_UserEmail_UserPhoneNu~",
                        columns: x => new { x.UserEmail, x.UserPhoneNumber },
                        principalTable: "tbl_pe_UserDetails",
                        principalColumns: new[] { "UserEmail", "UserPhoneNumber" },
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tbl_pe_UserPortfolio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserEmail = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserPhoneNumber = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PropertyID = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PropertyName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(65,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastUpdatedDate = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_pe_UserPortfolio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_pe_UserPortfolio_tbl_pe_Property_PropertyID",
                        column: x => x.PropertyID,
                        principalTable: "tbl_pe_Property",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_pe_UserPortfolio_tbl_pe_UserDetails_UserEmail_UserPhoneN~",
                        columns: x => new { x.UserEmail, x.UserPhoneNumber },
                        principalTable: "tbl_pe_UserDetails",
                        principalColumns: new[] { "UserEmail", "UserPhoneNumber" },
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_pe_PropertyAddressDetails_PropertyID",
                table: "tbl_pe_PropertyAddressDetails",
                column: "PropertyID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_pe_PropertyExpensesBreakOut_PropertyID",
                table: "tbl_pe_PropertyExpensesBreakOut",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_pe_PropertyIncomeBreakOut_PropertyID",
                table: "tbl_pe_PropertyIncomeBreakOut",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_pe_PropertyPassbook_PropertyID",
                table: "tbl_pe_PropertyPassbook",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_pe_PropertyProjectedValuationMetrics_PropertyID",
                table: "tbl_pe_PropertyProjectedValuationMetrics",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_pe_PropertyPurchaseOrSaleExpensesBreakOut_PropertyID",
                table: "tbl_pe_PropertyPurchaseOrSaleExpensesBreakOut",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_pe_PropertyTradeRecords_PropertyID",
                table: "tbl_pe_PropertyTradeRecords",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_pe_PropertyValuationMetrics_PropertyID",
                table: "tbl_pe_PropertyValuationMetrics",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_pe_Tenant_PropertyID",
                table: "tbl_pe_Tenant",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_pe_TenantLeaseDetails_TenantID",
                table: "tbl_pe_TenantLeaseDetails",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_pe_TenantRentPaymentDetails_TenantID",
                table: "tbl_pe_TenantRentPaymentDetails",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_pe_UserFundDetails_UserEmail_UserPhoneNumber",
                table: "tbl_pe_UserFundDetails",
                columns: new[] { "UserEmail", "UserPhoneNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_pe_UserPassbook_PropertyID",
                table: "tbl_pe_UserPassbook",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_pe_UserPassbook_UserEmail_UserPhoneNumber",
                table: "tbl_pe_UserPassbook",
                columns: new[] { "UserEmail", "UserPhoneNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_pe_UserPortfolio_PropertyID",
                table: "tbl_pe_UserPortfolio",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_pe_UserPortfolio_UserEmail_UserPhoneNumber",
                table: "tbl_pe_UserPortfolio",
                columns: new[] { "UserEmail", "UserPhoneNumber" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_pe_PropertyAddressDetails");

            migrationBuilder.DropTable(
                name: "tbl_pe_PropertyExpensesBreakOut");

            migrationBuilder.DropTable(
                name: "tbl_pe_PropertyIncomeBreakOut");

            migrationBuilder.DropTable(
                name: "tbl_pe_PropertyPassbook");

            migrationBuilder.DropTable(
                name: "tbl_pe_PropertyProjectedValuationMetrics");

            migrationBuilder.DropTable(
                name: "tbl_pe_PropertyPurchaseOrSaleExpensesBreakOut");

            migrationBuilder.DropTable(
                name: "tbl_pe_PropertyTradeRecords");

            migrationBuilder.DropTable(
                name: "tbl_pe_PropertyValuationMetrics");

            migrationBuilder.DropTable(
                name: "tbl_pe_TenantLeaseDetails");

            migrationBuilder.DropTable(
                name: "tbl_pe_TenantRentPaymentDetails");

            migrationBuilder.DropTable(
                name: "tbl_pe_UserFundDetails");

            migrationBuilder.DropTable(
                name: "tbl_pe_UserLogin");

            migrationBuilder.DropTable(
                name: "tbl_pe_UserPassbook");

            migrationBuilder.DropTable(
                name: "tbl_pe_UserPortfolio");

            migrationBuilder.DropTable(
                name: "tbl_pe_Tenant");

            migrationBuilder.DropTable(
                name: "tbl_pe_UserDetails");

            migrationBuilder.DropTable(
                name: "tbl_pe_Property");

            migrationBuilder.DropTable(
                name: "tbl_pe_UserRegistration");
        }
    }
}
