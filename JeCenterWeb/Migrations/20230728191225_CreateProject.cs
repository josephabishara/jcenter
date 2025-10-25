using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JeCenterWeb.Migrations
{
    /// <inheritdoc />
    public partial class CreateProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FristName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FamilyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    School = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentJob = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    UserTypeId = table.Column<int>(type: "int", nullable: false),
                    Pwd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imgurl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    online = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cardwrite",
                columns: table => new
                {
                    Cardwriteid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardCount = table.Column<int>(type: "int", nullable: false),
                    Cardvalue = table.Column<int>(type: "int", nullable: false),
                    Printed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cardwrite", x => x.Cardwriteid);
                });

            migrationBuilder.CreateTable(
                name: "CBranch",
                columns: table => new
                {
                    BranchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomsCount = table.Column<int>(type: "int", nullable: false),
                    Writed = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteId = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CBranch", x => x.BranchId);
                });

            migrationBuilder.CreateTable(
                name: "CDepartment",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Writed = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteId = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CDepartment", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "ChargingCard",
                columns: table => new
                {
                    ChargingCardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChargingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    Cardwriteid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargingCard", x => x.ChargingCardId);
                });

            migrationBuilder.CreateTable(
                name: "CheckCouponViewModel",
                columns: table => new
                {
                    MyProperty = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    scheduleId = table.Column<int>(type: "int", nullable: false),
                    LectureType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LectureName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Studentmobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeacherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentIndex = table.Column<int>(type: "int", nullable: false),
                    Printed = table.Column<int>(type: "int", nullable: false),
                    TeacherFee = table.Column<int>(type: "int", nullable: false),
                    CenterFee = table.Column<int>(type: "int", nullable: false),
                    ServiceFee = table.Column<int>(type: "int", nullable: false),
                    LecturePrice = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CenterDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    teacherDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TeacherValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CenterValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Paided = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckCouponViewModel", x => x.MyProperty);
                });

            migrationBuilder.CreateTable(
                name: "CloseStudentLectureViewModel",
                columns: table => new
                {
                    ItemNo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    scheduleId = table.Column<int>(type: "int", nullable: false),
                    LectureType = table.Column<int>(type: "int", nullable: false),
                    TeacherFee = table.Column<int>(type: "int", nullable: false),
                    CenterFee = table.Column<int>(type: "int", nullable: false),
                    ServiceFee = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CenterDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    teacherDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TeacherValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CenterValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Paided = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CloseStudentLectureViewModel", x => x.ItemNo);
                });

            migrationBuilder.CreateTable(
                name: "CPhases",
                columns: table => new
                {
                    PhaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhaseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imgurl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationValue = table.Column<int>(type: "int", nullable: false),
                    Parent = table.Column<int>(type: "int", nullable: false),
                    Writed = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteId = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPhases", x => x.PhaseId);
                });

            migrationBuilder.CreateTable(
                name: "ElRa3yNews",
                columns: table => new
                {
                    ElRa3yNewsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitelNews = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParagraphNews = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElRa3yNews", x => x.ElRa3yNewsId);
                });

            migrationBuilder.CreateTable(
                name: "FinancialAccount",
                columns: table => new
                {
                    FinancialAccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountTypeId = table.Column<int>(type: "int", nullable: false),
                    Writed = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteId = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialAccount", x => x.FinancialAccountId);
                });

            migrationBuilder.CreateTable(
                name: "LectureAbsentViewModel",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LectureAbsentViewModel", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "MovementType",
                columns: table => new
                {
                    MovementTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovementTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Writed = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteId = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovementType", x => x.MovementTypeId);
                });

            migrationBuilder.CreateTable(
                name: "PhysicalYear",
                columns: table => new
                {
                    PhysicalyearId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhysicalyearName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Writed = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteId = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalYear", x => x.PhysicalyearId);
                });

            migrationBuilder.CreateTable(
                name: "ResourceCity",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceCity", x => x.CityId);
                });

            migrationBuilder.CreateTable(
                name: "ResourceDays",
                columns: table => new
                {
                    DayOfWeekId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOfWeekName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceDays", x => x.DayOfWeekId);
                });

            migrationBuilder.CreateTable(
                name: "ResourceGender",
                columns: table => new
                {
                    GenderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lecture = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceGender", x => x.GenderId);
                });

            migrationBuilder.CreateTable(
                name: "ResourceMailbox",
                columns: table => new
                {
                    MailboxId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MailFrom = table.Column<int>(type: "int", nullable: false),
                    MailTo = table.Column<int>(type: "int", nullable: false),
                    MailSubject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MailBody = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MailDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AttachUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sent = table.Column<bool>(type: "bit", nullable: false),
                    Recived = table.Column<bool>(type: "bit", nullable: false),
                    SentDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RecivedDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Writed = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteId = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceMailbox", x => x.MailboxId);
                });

            migrationBuilder.CreateTable(
                name: "RoomGroupReviewsExamsScheduleViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    Titel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SessionStart = table.Column<TimeSpan>(type: "time", nullable: false),
                    SessionEnd = table.Column<TimeSpan>(type: "time", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomGroupReviewsExamsScheduleViewModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentBalancePendingViewModel",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    Debit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Credit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentBalancePendingViewModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeacherAssistant",
                columns: table => new
                {
                    TeacherAssistantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    AssistantId = table.Column<int>(type: "int", nullable: false),
                    Writed = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteId = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherAssistant", x => x.TeacherAssistantId);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    TeacherID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TeacherEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phase = table.Column<int>(type: "int", nullable: false),
                    Schools = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<int>(type: "int", nullable: false),
                    EducaAdmin = table.Column<int>(type: "int", nullable: false),
                    TeacherPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhasesID = table.Column<int>(type: "int", nullable: false),
                    curriculumID = table.Column<int>(type: "int", nullable: false),
                    img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    create_uid = table.Column<int>(type: "int", nullable: false),
                    create_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    write_uid = table.Column<int>(type: "int", nullable: false),
                    write_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Account = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.TeacherID);
                });

            migrationBuilder.CreateTable(
                name: "TeacherSyllabus",
                columns: table => new
                {
                    TeacherSyllabusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    SyllabusID = table.Column<int>(type: "int", nullable: false),
                    PhaseId = table.Column<int>(type: "int", nullable: false),
                    SyllabusName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Writed = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteId = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherSyllabus", x => x.TeacherSyllabusId);
                });

            migrationBuilder.CreateTable(
                name: "UsersBalance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SumDebit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SumCredit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersBalance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebAboutUS",
                columns: table => new
                {
                    AboutId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Paragraph_ar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imgurl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Writed = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteId = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebAboutUS", x => x.AboutId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentLecture",
                columns: table => new
                {
                    StudentLectureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    scheduleId = table.Column<int>(type: "int", nullable: false),
                    LectureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LectureType = table.Column<int>(type: "int", nullable: false),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    StudentIndex = table.Column<int>(type: "int", nullable: false),
                    Printed = table.Column<int>(type: "int", nullable: false),
                    StudentType = table.Column<int>(type: "int", nullable: false),
                    TeacherFee = table.Column<int>(type: "int", nullable: false),
                    CenterFee = table.Column<int>(type: "int", nullable: false),
                    ServiceFee = table.Column<int>(type: "int", nullable: false),
                    LecturePrice = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CenterDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    teacherDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TeacherValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CenterValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Paided = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Writed = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteId = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentLecture", x => x.StudentLectureId);
                    table.ForeignKey(
                        name: "FK_StudentLecture_AspNetUsers_StudentID",
                        column: x => x.StudentID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CRooms",
                columns: table => new
                {
                    RoomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Floor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    Writed = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteId = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CRooms", x => x.RoomId);
                    table.ForeignKey(
                        name: "FK_CRooms_CBranch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "CBranch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CSyllabus",
                columns: table => new
                {
                    SyllabusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SyllabusName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhaseID = table.Column<int>(type: "int", nullable: false),
                    imgurl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Writed = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteId = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CSyllabus", x => x.SyllabusID);
                    table.ForeignKey(
                        name: "FK_CSyllabus_CPhases_PhaseID",
                        column: x => x.PhaseID,
                        principalTable: "CPhases",
                        principalColumn: "PhaseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentBalancePending",
                columns: table => new
                {
                    MyProperty = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    Debit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Credit = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentBalancePending", x => x.MyProperty);
                    table.ForeignKey(
                        name: "FK_StudentBalancePending_AspNetUsers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentBalancePending_FinancialAccount_AccountID",
                        column: x => x.AccountID,
                        principalTable: "FinancialAccount",
                        principalColumn: "FinancialAccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentDiscount",
                columns: table => new
                {
                    StudentDiscountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LectureType = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CenterDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    teacherDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    physicalyearId = table.Column<int>(type: "int", nullable: false),
                    Writed = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteId = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentDiscount", x => x.StudentDiscountId);
                    table.ForeignKey(
                        name: "FK_StudentDiscount_AspNetUsers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentDiscount_FinancialAccount_StudentId",
                        column: x => x.StudentId,
                        principalTable: "FinancialAccount",
                        principalColumn: "FinancialAccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FinancialDocuments",
                columns: table => new
                {
                    FinancialDocumentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JournalEntryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TreasuryID = table.Column<int>(type: "int", nullable: false),
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MovementTypeId = table.Column<int>(type: "int", nullable: false),
                    physicalyearId = table.Column<int>(type: "int", nullable: false),
                    Receipted = table.Column<bool>(type: "bit", nullable: false),
                    ReceiptDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Writed = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteId = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialDocuments", x => x.FinancialDocumentId);
                    table.ForeignKey(
                        name: "FK_FinancialDocuments_MovementType_MovementTypeId",
                        column: x => x.MovementTypeId,
                        principalTable: "MovementType",
                        principalColumn: "MovementTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CApplicationsValue",
                columns: table => new
                {
                    ApplicationValueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhaseId = table.Column<int>(type: "int", nullable: false),
                    PhysicalyearId = table.Column<int>(type: "int", nullable: false),
                    ApplicationValue = table.Column<int>(type: "int", nullable: false),
                    Writed = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteId = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CApplicationsValue", x => x.ApplicationValueId);
                    table.ForeignKey(
                        name: "FK_CApplicationsValue_CPhases_PhaseId",
                        column: x => x.PhaseId,
                        principalTable: "CPhases",
                        principalColumn: "PhaseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CApplicationsValue_PhysicalYear_PhysicalyearId",
                        column: x => x.PhysicalyearId,
                        principalTable: "PhysicalYear",
                        principalColumn: "PhysicalyearId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentApplications",
                columns: table => new
                {
                    ApplicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    PhysicalyearId = table.Column<int>(type: "int", nullable: false),
                    PhaseId = table.Column<int>(type: "int", nullable: false),
                    Paided = table.Column<bool>(type: "bit", nullable: false),
                    DocNo = table.Column<int>(type: "int", nullable: false),
                    Writed = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteId = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentApplications", x => x.ApplicationId);
                    table.ForeignKey(
                        name: "FK_StudentApplications_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentApplications_CPhases_PhaseId",
                        column: x => x.PhaseId,
                        principalTable: "CPhases",
                        principalColumn: "PhaseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentApplications_PhysicalYear_PhysicalyearId",
                        column: x => x.PhysicalyearId,
                        principalTable: "PhysicalYear",
                        principalColumn: "PhysicalyearId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CGroups",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    TeacherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SyllabusID = table.Column<int>(type: "int", nullable: false),
                    GroupNo = table.Column<int>(type: "int", nullable: false),
                    physicalyearId = table.Column<int>(type: "int", nullable: false),
                    DayOfWeekId = table.Column<int>(type: "int", nullable: false),
                    FirstDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SessionStart = table.Column<TimeSpan>(type: "time", nullable: false),
                    SessionEnd = table.Column<TimeSpan>(type: "time", nullable: false),
                    TeacherFee = table.Column<int>(type: "int", nullable: false),
                    CenterFee = table.Column<int>(type: "int", nullable: false),
                    ServiceFee = table.Column<int>(type: "int", nullable: false),
                    LecturePrice = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    ResourceGenderGenderId = table.Column<int>(type: "int", nullable: true),
                    Writed = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteId = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CGroups", x => x.GroupId);
                    table.ForeignKey(
                        name: "FK_CGroups_AspNetUsers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CGroups_CRooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "CRooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CGroups_CSyllabus_SyllabusID",
                        column: x => x.SyllabusID,
                        principalTable: "CSyllabus",
                        principalColumn: "SyllabusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CGroups_PhysicalYear_physicalyearId",
                        column: x => x.physicalyearId,
                        principalTable: "PhysicalYear",
                        principalColumn: "PhysicalyearId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CGroups_ResourceDays_DayOfWeekId",
                        column: x => x.DayOfWeekId,
                        principalTable: "ResourceDays",
                        principalColumn: "DayOfWeekId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CGroups_ResourceGender_ResourceGenderGenderId",
                        column: x => x.ResourceGenderGenderId,
                        principalTable: "ResourceGender",
                        principalColumn: "GenderId");
                });

            migrationBuilder.CreateTable(
                name: "ReviewsSchedule",
                columns: table => new
                {
                    ReviewsScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    ReviewTypeId = table.Column<int>(type: "int", nullable: false),
                    ReviewsScheduleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MonthCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    physicalyearId = table.Column<int>(type: "int", nullable: false),
                    SyllabusID = table.Column<int>(type: "int", nullable: false),
                    TeacherFee = table.Column<int>(type: "int", nullable: false),
                    CenterFee = table.Column<int>(type: "int", nullable: false),
                    ServiceFee = table.Column<int>(type: "int", nullable: false),
                    LecturePrice = table.Column<int>(type: "int", nullable: false),
                    SessionStart = table.Column<TimeSpan>(type: "time", nullable: false),
                    SessionEnd = table.Column<TimeSpan>(type: "time", nullable: false),
                    Paided = table.Column<bool>(type: "bit", nullable: false),
                    PaidedValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Closed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewsSchedule", x => x.ReviewsScheduleId);
                    table.ForeignKey(
                        name: "FK_ReviewsSchedule_CRooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "CRooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReviewsSchedule_CSyllabus_SyllabusID",
                        column: x => x.SyllabusID,
                        principalTable: "CSyllabus",
                        principalColumn: "SyllabusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReviewsSchedule_PhysicalYear_physicalyearId",
                        column: x => x.physicalyearId,
                        principalTable: "PhysicalYear",
                        principalColumn: "PhysicalyearId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeachesVideos",
                columns: table => new
                {
                    TeacheVideoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    VideoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SyllabusID = table.Column<int>(type: "int", nullable: false),
                    TeacherFee = table.Column<int>(type: "int", nullable: false),
                    CenterFee = table.Column<int>(type: "int", nullable: false),
                    LecturePrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachesVideos", x => x.TeacheVideoId);
                    table.ForeignKey(
                        name: "FK_TeachesVideos_CSyllabus_SyllabusID",
                        column: x => x.SyllabusID,
                        principalTable: "CSyllabus",
                        principalColumn: "SyllabusID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FinancialJournalEntryLine",
                columns: table => new
                {
                    JournalEntryDetilsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FinancialDocumentId = table.Column<int>(type: "int", nullable: false),
                    JournalEntryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    Debit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Credit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    physicalyearId = table.Column<int>(type: "int", nullable: false),
                    Writed = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteId = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialJournalEntryLine", x => x.JournalEntryDetilsID);
                    table.ForeignKey(
                        name: "FK_FinancialJournalEntryLine_FinancialAccount_AccountID",
                        column: x => x.AccountID,
                        principalTable: "FinancialAccount",
                        principalColumn: "FinancialAccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinancialJournalEntryLine_FinancialDocuments_FinancialDocumentId",
                        column: x => x.FinancialDocumentId,
                        principalTable: "FinancialDocuments",
                        principalColumn: "FinancialDocumentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinancialJournalEntryLine_PhysicalYear_physicalyearId",
                        column: x => x.physicalyearId,
                        principalTable: "PhysicalYear",
                        principalColumn: "PhysicalyearId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CGroupSchedule",
                columns: table => new
                {
                    GroupscheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    LectureNo = table.Column<int>(type: "int", nullable: false),
                    LectureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MonthCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Closed = table.Column<bool>(type: "bit", nullable: false),
                    Paided = table.Column<bool>(type: "bit", nullable: false),
                    PaidedValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CGroupSchedule", x => x.GroupscheduleId);
                    table.ForeignKey(
                        name: "FK_CGroupSchedule_CGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "CGroups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentGroup",
                columns: table => new
                {
                    StudentGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Writed = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteId = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentGroup", x => x.StudentGroupId);
                    table.ForeignKey(
                        name: "FK_StudentGroup_CGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "CGroups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherGroups",
                columns: table => new
                {
                    TeacherGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    Writed = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteId = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherGroups", x => x.TeacherGroupId);
                    table.ForeignKey(
                        name: "FK_TeacherGroups_CGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "CGroups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CApplicationsValue_PhaseId",
                table: "CApplicationsValue",
                column: "PhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_CApplicationsValue_PhysicalyearId",
                table: "CApplicationsValue",
                column: "PhysicalyearId");

            migrationBuilder.CreateIndex(
                name: "IX_CGroups_DayOfWeekId",
                table: "CGroups",
                column: "DayOfWeekId");

            migrationBuilder.CreateIndex(
                name: "IX_CGroups_physicalyearId",
                table: "CGroups",
                column: "physicalyearId");

            migrationBuilder.CreateIndex(
                name: "IX_CGroups_ResourceGenderGenderId",
                table: "CGroups",
                column: "ResourceGenderGenderId");

            migrationBuilder.CreateIndex(
                name: "IX_CGroups_RoomId",
                table: "CGroups",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_CGroups_SyllabusID",
                table: "CGroups",
                column: "SyllabusID");

            migrationBuilder.CreateIndex(
                name: "IX_CGroups_TeacherId",
                table: "CGroups",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_CGroupSchedule_GroupId",
                table: "CGroupSchedule",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CRooms_BranchId",
                table: "CRooms",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_CSyllabus_PhaseID",
                table: "CSyllabus",
                column: "PhaseID");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialDocuments_MovementTypeId",
                table: "FinancialDocuments",
                column: "MovementTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialJournalEntryLine_AccountID",
                table: "FinancialJournalEntryLine",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialJournalEntryLine_FinancialDocumentId",
                table: "FinancialJournalEntryLine",
                column: "FinancialDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialJournalEntryLine_physicalyearId",
                table: "FinancialJournalEntryLine",
                column: "physicalyearId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewsSchedule_physicalyearId",
                table: "ReviewsSchedule",
                column: "physicalyearId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewsSchedule_RoomId",
                table: "ReviewsSchedule",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewsSchedule_SyllabusID",
                table: "ReviewsSchedule",
                column: "SyllabusID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentApplications_PhaseId",
                table: "StudentApplications",
                column: "PhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentApplications_PhysicalyearId",
                table: "StudentApplications",
                column: "PhysicalyearId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentApplications_StudentId",
                table: "StudentApplications",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentBalancePending_AccountID",
                table: "StudentBalancePending",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentBalancePending_TeacherId",
                table: "StudentBalancePending",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentDiscount_StudentId",
                table: "StudentDiscount",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentDiscount_TeacherId",
                table: "StudentDiscount",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroup_GroupId",
                table: "StudentGroup",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentLecture_StudentID",
                table: "StudentLecture",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherGroups_GroupId",
                table: "TeacherGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachesVideos_SyllabusID",
                table: "TeachesVideos",
                column: "SyllabusID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CApplicationsValue");

            migrationBuilder.DropTable(
                name: "Cardwrite");

            migrationBuilder.DropTable(
                name: "CDepartment");

            migrationBuilder.DropTable(
                name: "CGroupSchedule");

            migrationBuilder.DropTable(
                name: "ChargingCard");

            migrationBuilder.DropTable(
                name: "CheckCouponViewModel");

            migrationBuilder.DropTable(
                name: "CloseStudentLectureViewModel");

            migrationBuilder.DropTable(
                name: "ElRa3yNews");

            migrationBuilder.DropTable(
                name: "FinancialJournalEntryLine");

            migrationBuilder.DropTable(
                name: "LectureAbsentViewModel");

            migrationBuilder.DropTable(
                name: "ResourceCity");

            migrationBuilder.DropTable(
                name: "ResourceMailbox");

            migrationBuilder.DropTable(
                name: "ReviewsSchedule");

            migrationBuilder.DropTable(
                name: "RoomGroupReviewsExamsScheduleViewModel");

            migrationBuilder.DropTable(
                name: "StudentApplications");

            migrationBuilder.DropTable(
                name: "StudentBalancePending");

            migrationBuilder.DropTable(
                name: "StudentBalancePendingViewModel");

            migrationBuilder.DropTable(
                name: "StudentDiscount");

            migrationBuilder.DropTable(
                name: "StudentGroup");

            migrationBuilder.DropTable(
                name: "StudentLecture");

            migrationBuilder.DropTable(
                name: "TeacherAssistant");

            migrationBuilder.DropTable(
                name: "TeacherGroups");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "TeacherSyllabus");

            migrationBuilder.DropTable(
                name: "TeachesVideos");

            migrationBuilder.DropTable(
                name: "UsersBalance");

            migrationBuilder.DropTable(
                name: "WebAboutUS");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "FinancialDocuments");

            migrationBuilder.DropTable(
                name: "FinancialAccount");

            migrationBuilder.DropTable(
                name: "CGroups");

            migrationBuilder.DropTable(
                name: "MovementType");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CRooms");

            migrationBuilder.DropTable(
                name: "CSyllabus");

            migrationBuilder.DropTable(
                name: "PhysicalYear");

            migrationBuilder.DropTable(
                name: "ResourceDays");

            migrationBuilder.DropTable(
                name: "ResourceGender");

            migrationBuilder.DropTable(
                name: "CBranch");

            migrationBuilder.DropTable(
                name: "CPhases");
        }
    }
}
