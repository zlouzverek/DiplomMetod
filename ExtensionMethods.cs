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
                FormTypeName = form.FormType?.Name,
                RequisiteNumber = form.RequisiteNumber,
                Code = form.Code,
                ReferenceBookName = form.ReferenceBook?.Name,
                KeyWords = string.Join(", ", form.KeyWords.Select(kw => kw.Name)),
                ExplanationNumber = form.Explanation.Number,
                ExplanationDate = form.Explanation.Date,
                OrganizationName = form.Explanation.Organization.Name,
				RegionDivisionName = form.RegionsDivision?.Name,
				Event = form.Event,
				Comment = form.Explanation.Comment,
				Description = form.Explanation.Description,
				ApproveLevel = form.Explanation.ApproveLevel.ToString(),
				IsAgreedGenProk = form.Explanation.IsAgreedGenProk ? "да" : "нет",
                IsRevelant = form.Explanation.IsRevelant ? "актуально" : "не актуально",
                IsFavorites = form.Explanation.IsFavorites ? "да" : "нет"
            });

            return exportModels;
        }


        public static ModelBuilder SeedData(this ModelBuilder modelBuilder)
        {
            // Список типов форм для FormType
            var formType1 = new FormType { Id = 1, Name = "Форма №1", FullName = "Статистическая карточка на выявленнное преступление", FormId = 1 };
            var formType2 = new FormType { Id = 2, Name = "Форма №1.1", FullName = "Статистическая карточка о результатах расследования преступления", FormId = 2 };
            var formType3 = new FormType { Id = 3, Name = "Форма №2", FullName = "Статистическая карточка на лицо, совершившее преступление", FormId = 3 };
            var formType4 = new FormType { Id = 4, Name = "Форма №2.1", FullName = "Статистическая карточка на лицо, подозреваемое (обвиняемое) в совершении преступления", FormId = 4 };
            var formType5 = new FormType { Id = 5, Name = "Форма №3", FullName = "Статистическая карточка о движении уголовного дела", FormId = 5 };
            var formType6 = new FormType { Id = 6, Name = "Форма №4", FullName = "Статистическая карточка об установленной сумме материального ущерба, результатах возмещения и изъятия предметов преступной деятельности", FormId = 6 };
            var formType7 = new FormType { Id = 7, Name = "Форма №5", FullName = "Статистическая карточка о потерпевшем", FormId = 7 };
            var formType8 = new FormType { Id = 8, Name = "Форма №6", FullName = "Статистическая карточка о результатах рассмотрения дела судом первой инстанции", FormId = 8 };
           

            modelBuilder.Entity<FormType>().HasData(formType1, formType2, formType3, formType4, formType5, formType6, formType7, formType8);

            // Список справочников для ReferenceBook
            modelBuilder.Entity<ReferenceBook>().HasData(
                new ReferenceBook { Id = 1, Name = "Справочник №1", FullName = "Классификатор видов экономической деятельности" },
                new ReferenceBook { Id = 2, Name = "Справочник №2", FullName = "Место совершения преступления" },
                new ReferenceBook { Id = 3, Name = "Справочник №3", FullName = "Предмет преступного посягательства или незаконного оборота" },
                new ReferenceBook { Id = 4, Name = "Справочник №4", FullName = "Оружие, боеприпасы и взрывчатые вещества" },
                new ReferenceBook { Id = 5, Name = "Справочник №5", FullName = "Национальности" },
                new ReferenceBook { Id = 6, Name = "Справочник №6", FullName = "Страны (государства)" },
                new ReferenceBook { Id = 7, Name = "Справочник №7", FullName = "Валюта" },
                new ReferenceBook { Id = 8, Name = "Справочник №8", FullName = "Наркотические средства, психотропные, сильнодействующие, одурманивающие вещества, инструменты и оборудование, прекурсоры и ядовитые вещества" },
                new ReferenceBook { Id = 9, Name = "Справочник №9", FullName = "Социальное положение потерпевших и лиц, совершивших преступления" },
                new ReferenceBook { Id = 10, Name = "Справочник №10", FullName = "Должностное положение потерпевших и лиц, совершивших преступления" },
                new ReferenceBook { Id = 11, Name = "Справочник №11", FullName = "Организационно-правовые формы хозяйствующих субъектов" },
                new ReferenceBook { Id = 12, Name = "Справочник №12", FullName = "Способ совершения преступления" },
                new ReferenceBook { Id = 13, Name = "Справочник №14", FullName = "Служба, выявившая преступление или установившая лицо" },
                new ReferenceBook { Id = 14, Name = "Справочник №15", FullName = "Дополнительная характеристика преступления" },
                new ReferenceBook { Id = 15, Name = "Справочник №16", FullName = "Решение, принятое по уголовному делу" },
                new ReferenceBook { Id = 16, Name = "Справочник формы №6", FullName = "Справочник для заполнения статистической карточки №6" }

            );

            // Список организаций для Organizations
            modelBuilder.Entity<Organization>().HasData(
                new Organization { Id = 1, Name = "ГИАЦ МВД России" },
                new Organization { Id = 2, Name = "Генпрокуратура России" },
                new Organization { Id = 3, Name = "ИЦ субъекта" },
                new Organization { Id = 4, Name = "Прокуратура субъекта" }
            );

            // Список регионов (подразделений) для RegionDivision
            modelBuilder.Entity<RegionDivision>().HasData(
                new RegionDivision { Id = 1, Name = "Алтайский край" },
                new RegionDivision { Id = 2, Name = "Амурская область" },
                new RegionDivision { Id = 3, Name = "Архангельская область" },
                new RegionDivision { Id = 4, Name = "Астраханская область" },
                new RegionDivision { Id = 5, Name = "Белгородская область" },
                new RegionDivision { Id = 6, Name = "Брянская область" },
                new RegionDivision { Id = 7, Name = "Владимирская область" },
                new RegionDivision { Id = 8, Name = "Волгоградская область" },
                new RegionDivision { Id = 9, Name = "Вологодская область" },
                new RegionDivision { Id = 10, Name = "Воронежская область" },
                new RegionDivision { Id = 11, Name = "Москва" },
                new RegionDivision { Id = 12, Name = "Санкт-Петербург" },
                new RegionDivision { Id = 13, Name = "Севастополь" },
                new RegionDivision { Id = 14, Name = "Донецкая Народная Республика" },
                new RegionDivision { Id = 15, Name = "Еврейская автономная область" },
                new RegionDivision { Id = 16, Name = "Забайкальский край" },
                new RegionDivision { Id = 17, Name = "Запорожская область" },
                new RegionDivision { Id = 18, Name = "Ивановская область" },
                new RegionDivision { Id = 19, Name = "Иркутская область" },
                new RegionDivision { Id = 20, Name = "Кабардино-Балкарская Республика" },
                new RegionDivision { Id = 21, Name = "Калининградская область" },
                new RegionDivision { Id = 22, Name = "Калужская область" },
                new RegionDivision { Id = 23, Name = "Камчатский край" },
                new RegionDivision { Id = 24, Name = "Карачаево-Черкесская Республика" },
                new RegionDivision { Id = 25, Name = "Кемеровская область — Кузбасс" },
                new RegionDivision { Id = 26, Name = "Кировская область" },
                new RegionDivision { Id = 27, Name = "Костромская область" },
                new RegionDivision { Id = 28, Name = "Краснодарский край" },
                new RegionDivision { Id = 29, Name = "Красноярский край" },
                new RegionDivision { Id = 30, Name = "Курганская область" },
                new RegionDivision { Id = 31, Name = "Курская область" },
                new RegionDivision { Id = 32, Name = "Ленинградская область" },
                new RegionDivision { Id = 33, Name = "Липецкая область" },
                new RegionDivision { Id = 34, Name = "Луганская Народная Республика" },
                new RegionDivision { Id = 35, Name = "Магаданская область" },
                new RegionDivision { Id = 36, Name = "Московская область" },
                new RegionDivision { Id = 37, Name = "Мурманская область" },
                new RegionDivision { Id = 38, Name = "Ненецкий автономный округ" },
                new RegionDivision { Id = 39, Name = "Нижегородская область" },
                new RegionDivision { Id = 40, Name = "Новгородская область" },
                new RegionDivision { Id = 41, Name = "Новосибирская область" },
                new RegionDivision { Id = 42, Name = "Омская область" },
                new RegionDivision { Id = 43, Name = "Оренбургская область" },
                new RegionDivision { Id = 44, Name = "Орловская область" },
                new RegionDivision { Id = 45, Name = "Пензенская область" },
                new RegionDivision { Id = 46, Name = "Пермский край" },
                new RegionDivision { Id = 47, Name = "Приморский край" },
                new RegionDivision { Id = 48, Name = "Псковская область" },
                new RegionDivision { Id = 49, Name = "Республика Адыгея (Адыгея)" },
                new RegionDivision { Id = 50, Name = "Республика Алтай" },
                new RegionDivision { Id = 51, Name = "Республика Башкортостан" },
                new RegionDivision { Id = 52, Name = "Республика Бурятия" },
                new RegionDivision { Id = 53, Name = "Республика Дагестан" },
                new RegionDivision { Id = 54, Name = "Республика Ингушетия" },
                new RegionDivision { Id = 55, Name = "Республика Калмыкия" },
                new RegionDivision { Id = 56, Name = "Республика Карелия" },
                new RegionDivision { Id = 57, Name = "Республика Коми" },
                new RegionDivision { Id = 58, Name = "Республика Крым" },
                new RegionDivision { Id = 59, Name = "Республика Марий Эл" },
                new RegionDivision { Id = 60, Name = "Республика Мордовия" },
                new RegionDivision { Id = 61, Name = "Республика Саха (Якутия)" },
                new RegionDivision { Id = 62, Name = "Республика Северная Осетия — Алания" },
                new RegionDivision { Id = 63, Name = "Республика Татарстан (Татарстан)" },
                new RegionDivision { Id = 64, Name = "Республика Тыва" },
                new RegionDivision { Id = 65, Name = "Республика Хакасия" },
                new RegionDivision { Id = 66, Name = "Ростовская область" },
                new RegionDivision { Id = 67, Name = "Рязанская область" },
                new RegionDivision { Id = 68, Name = "Самарская область" },
                new RegionDivision { Id = 69, Name = "Саратовская область" },
                new RegionDivision { Id = 70, Name = "Сахалинская область" },
                new RegionDivision { Id = 71, Name = "Свердловская область" },
                new RegionDivision { Id = 72, Name = "Смоленская область" },
                new RegionDivision { Id = 73, Name = "Ставропольский край" },
                new RegionDivision { Id = 74, Name = "Тамбовская область" },
                new RegionDivision { Id = 75, Name = "Тверская область" },
                new RegionDivision { Id = 76, Name = "Томская область" },
                new RegionDivision { Id = 77, Name = "Тульская область" },
                new RegionDivision { Id = 78, Name = "Тюменская область" },
                new RegionDivision { Id = 79, Name = "Удмуртская Республика" },
                new RegionDivision { Id = 80, Name = "Ульяновская область" },
                new RegionDivision { Id = 81, Name = "Хабаровский край" },
                new RegionDivision { Id = 82, Name = "Ханты-Мансийский автономный округ — Югра" },
                new RegionDivision { Id = 83, Name = "Херсонская область" },
                new RegionDivision { Id = 84, Name = "Челябинская область" },
                new RegionDivision { Id = 85, Name = "Чеченская Республика" },
                new RegionDivision { Id = 86, Name = "Чувашская Республика — Чувашия" },
                new RegionDivision { Id = 87, Name = "Чукотский автономный округ" },
                new RegionDivision { Id = 88, Name = "Ямало-Ненецкий автономный округ" },
                new RegionDivision { Id = 89, Name = "Ярославская область" }
             
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
