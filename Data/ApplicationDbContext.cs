using DiplomMetod.Data.Configurations;
using DiplomMetod.Data.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DiplomMetod.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<ReferenceBook> ReferenceBooks { get; set; }
        public DbSet<FormType> Forms { get; set; }
        public DbSet<RegionDivision> RegionsDivisions { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Explanation> Explanations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new FormConfiguration());
            modelBuilder.ApplyConfiguration(new ExplanationConfiguration());
            modelBuilder.ApplyConfiguration(new KeyWordConfiguration());

            Seed(modelBuilder);
        }


        private void Seed(ModelBuilder modelBuilder)
        {
            // Вставка тестовых данных для FormType
            modelBuilder.Entity<FormType>().HasData(
                new FormType { Id = 1, Name = "FormType1", FullName = "Full Name 1", FormId = 1 },
                new FormType { Id = 2, Name = "FormType2", FullName = "Full Name 2", FormId = 2 }
            );

            // Вставка тестовых данных для ReferenceBook
            modelBuilder.Entity<ReferenceBook>().HasData(
                new ReferenceBook { Id = 1, Name = "ReferenceBook1", FullName = "Full Name 1" },
                new ReferenceBook { Id = 2, Name = "ReferenceBook2", FullName = "Full Name 2" }
            );

            // Вставка тестовых данных для Organization
            modelBuilder.Entity<Organization>().HasData(
                new Organization { Id = 1, Name = "Organization1" },
                new Organization { Id = 2, Name = "Organization2" }
            );

            // Вставка тестовых данных для RegionDivision
            modelBuilder.Entity<RegionDivision>().HasData(
                new RegionDivision { Id = 1, Name = "RegionDivision1" },
                new RegionDivision { Id = 2, Name = "RegionDivision2" }
            );

            // Вставка тестовых данных для Explanation
            modelBuilder.Entity<Explanation>().HasData(
                new Explanation
                {
                    Id = 1,
                    Name = "Explanation1",
                    FullName = "Full Name 1",
                    Number = "123",
                    Date = DateTime.Now,
                    OrganizationId = 1,
                    IsAgreedGenProk = true,
                    IsRevelant = true,
                    Description = "Description 1",
                    Comment = "Comment 1",
                    IsFavorites = true,
                    ApproveLevel = ApproveLevel.Local,
                    FormId = 1
                },
                new Explanation
                {
                    Id = 2,
                    Name = "Explanation2",
                    FullName = "Full Name 2",
                    Number = "456",
                    Date = DateTime.Now,
                    OrganizationId = 2,
                    IsAgreedGenProk = false,
                    IsRevelant = false,
                    Description = "Description 2",
                    Comment = "Comment 2",
                    IsFavorites = false,
                    ApproveLevel = ApproveLevel.Federal,
                    FormId = 2
                }
            );

            // Вставка тестовых данных для Form
            modelBuilder.Entity<Form>().HasData(
                new Form
                {
                    Id = 1,
                    FormTypeId = 1,
                    InventoryNumber = "Inventory1",
                    RequisiteNumber = 101,
                    Code = 1,
                    ReferenceBooksId = 1,
                    ExplanationId = 1,
                    RegionsDivisionsId = 1,
                    Event = "Event1",
                    Question = "Question1",
                    Answer = "Answer1",
                    FileLink = "http://example.com/file1"
                },
                new Form
                {
                    Id = 2,
                    FormTypeId = 2,
                    InventoryNumber = "Inventory2",
                    RequisiteNumber = 102,
                    Code = 2,
                    ReferenceBooksId = 2,
                    ExplanationId = 2,
                    RegionsDivisionsId = 2,
                    Event = "Event2",
                    Question = "Question2",
                    Answer = "Answer2",
                    FileLink = "http://example.com/file2"
                }
            );

            // Вставка тестовых данных для KeyWords
            modelBuilder.Entity<KeyWord>().HasData(
                new KeyWord
                {
                    Id = 1,
                    Name = "KeyWordName1",
                    FormId = 1

                },
                new KeyWord
                {
                    Id = 2,
                    Name = "KeyWordName2",
                    FormId = 2
                },
                new KeyWord
                {
                    Id = 3,
                    Name = "KeyWordName3",
                    FormId = 1

                },
                new KeyWord
                {
                    Id = 4,
                    Name = "KeyWordName4",
                    FormId = 1
                },
                new KeyWord
                {
                    Id = 5,
                    Name = "KeyWordName5",
                    FormId = 1
                },
                new KeyWord
                {
                    Id = 6,
                    Name = "KeyWordName6",
                    FormId = 2
                }
            );

        }

    }
}
