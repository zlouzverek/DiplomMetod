using DiplomMetod.Data.Entites;
using DiplomMetod.Models;
using Microsoft.EntityFrameworkCore;

namespace DiplomMetod
{
    public static class ExtensionMethods
    {

        public static IEnumerable<FormExportModel> ToExportModel(this IEnumerable<Form> forms)
        {
            var exportModels = forms.Select(form => new FormExportModel
            {
                InventoryNumber = form.InventoryNumber,
                FormTypeName = form.FormType?.Name,
                RequisiteNumber = form.RequisiteNumber,
                Code = form.Code,
                ReferenceBookName = form.ReferenceBook?.Name,
                KeyWords = string.Join(", ", form.KeyWords.Select(kw => kw.Name)),
                ExplanationNumber = form.Explanation.Number,
                ExplanationDate = form.Explanation.Date,
                OrganizationName = form.Explanation.Organization.Name,
				RegionDivisionName = form.RegionsDivision?.Name,
				Comment = form.Explanation.Comment,
				ApproveLevel = form.Explanation.ApproveLevel.ToString(),
				IsAgreedGenProk = form.Explanation.IsAgreedGenProk ? "да" : "нет",
                IsRevelant = form.Explanation.IsRevelant ? "актуально" : "не актуально",
                IsFavorites = form.Explanation.IsFavorites ? "да" : "нет"
            });

            return exportModels;
        }


        public static ModelBuilder SeedData(this ModelBuilder modelBuilder)
        {
            // Вставка тестовых данных для FormType
            var formType1 = new FormType { Id = 1, Name = "FormType1", FullName = "Full Name 1", FormId = 1 };
            var formType2 = new FormType { Id = 2, Name = "FormType2", FullName = "Full Name 2", FormId = 2 };

            modelBuilder.Entity<FormType>().HasData(formType1, formType2);

            // Вставка тестовых данных для ReferenceBook
            modelBuilder.Entity<ReferenceBook>().HasData(
                new ReferenceBook { Id = 1, Name = "ReferenceBook1", FullName = "Full Name 1" },
                new ReferenceBook { Id = 2, Name = "ReferenceBook2", FullName = "Full Name 2" }
            );

            // Вставка тестовых данных для Organizations
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
                    Description = "ExplanationDescription 1",
                    Comment = "ExplanationComment 1",
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
                    Description = "ExplanationDescription 2",
                    Comment = "ExplanationComment 2",
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
                    InventoryNumber = $"1_{formType1.Name}",
                    RequisiteNumber = 101,
                    Code = 1,
                    ReferenceBooksId = 1,
                    ExplanationId = 1,
                    RegionsDivisionsId = 1,
                    Event = "Event1",
                    Question = "Question1",
                    Answer = "Answer1",
                    FileLink = "http://example.com/file1",
                    IsQuestion = true,
                },
                new Form
                {
                    Id = 2,
                    FormTypeId = 2,
                    InventoryNumber = $"2_{formType1.Name}",
                    RequisiteNumber = 102,
                    Code = 2,
                    ReferenceBooksId = 2,
                    ExplanationId = 2,
                    RegionsDivisionsId = 2,
                    Event = "Event2",
                    Question = "Question2",
                    Answer = "Answer2",
                    FileLink = "http://example.com/file2",
                    IsQuestion = false

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

            return modelBuilder;
        }
    }
}
