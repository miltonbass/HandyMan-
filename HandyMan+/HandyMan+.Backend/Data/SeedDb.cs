using HandyMan_.Backend.Helpers;
using HandyMan_.Backend.UnitsOfWork.Interfaces;
using HandyMan_.Shared.Entities;
using HandyMan_.Shered.Entities;
using Microsoft.EntityFrameworkCore;
using HandyMan_.Shared.Enums;

namespace HandyMan_.Backend.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUsersUnitOfWork _usersUnitOfWork;
        private readonly IFileStorage _fileStorage;

        public SeedDb(DataContext context, IUsersUnitOfWork usersUnitOfWork, IFileStorage fileStorage)
        {
            _context = context;
            _usersUnitOfWork = usersUnitOfWork;
            _fileStorage = fileStorage;
        }


        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesFullAsync();
            await CheckCountriesAsync();
            await CheckCategoriesAsync();
            await CheckSubscriptionTypesAsync();
            await CheckSurveyDataAsync();
            await CheckoutPeopleTypeAsync();
            await CheckRolesAsync();
            await CheckUserAsync("0001", "user", "admin", "admin@yopmail.com", "318 4756753", "Avenida siempre viva 123", "admin.jpg", UserType.Admin);
            await CheckUserAsync("0002", "User", "asistant", "asistant@yopmail.com", "111 111 111", "Avenida 2", "specialist.jpg", UserType.Specialist);
            await CheckUserAsync("0003", "User", "provider", "provider@yopmail.com", "111 111 111", "Avenida 3", "provider.jpg", UserType.Provider);
            await CheckUserAsync("0003", "User", "costumer", "costumer@yopmail.com", "111 111 111", "Avenida 3", "costumer.jpg", UserType.Costumer);

            //await CheckoutPeopleAsync();
            //await CheckoutServiceAsync();
            //await CheckoutServiceOrderAsync();
        }

        private async Task CheckRolesAsync()
        {
            await _usersUnitOfWork.CheckRoleAsync(UserType.Admin.ToString());
            await _usersUnitOfWork.CheckRoleAsync(UserType.Costumer.ToString());
            await _usersUnitOfWork.CheckRoleAsync(UserType.CostumerPremium.ToString());
            await _usersUnitOfWork.CheckRoleAsync(UserType.Provider.ToString());
            await _usersUnitOfWork.CheckRoleAsync(UserType.ProviderPremium.ToString());
            await _usersUnitOfWork.CheckRoleAsync(UserType.Specialist.ToString());
        }

        private async Task<User> CheckUserAsync(string document, string firstName, string lastName, string email, string phone, string address, string image, UserType userType)
        {
            var user = await _usersUnitOfWork.GetUserAsync(email);
            if (user == null)
            {
                var city = await _context.Cities.FirstOrDefaultAsync(x => x.Name == "Medellín");
                city ??= await _context.Cities.FirstOrDefaultAsync();

                var filePath = $"{Environment.CurrentDirectory}\\Images\\users\\{image}";
                var fileBytes = File.ReadAllBytes(filePath);
                var imagePath = await _fileStorage.SaveFileAsync(fileBytes, "jpg", "users");


                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    City = city,
                    UserType = userType,
                    Photo = imagePath,
                };

                await _usersUnitOfWork.AddUserAsync(user, "123456");
                await _usersUnitOfWork.AddUserToRoleAsync(user, userType.ToString());

                var token = await _usersUnitOfWork.GenerateEmailConfirmationTokenAsync(user);
                await _usersUnitOfWork.ConfirmEmailAsync(user, token);
            }

            return user;
        }



        private async Task CheckCountriesFullAsync()
        {
            if (!_context.Countries.Any())
            {
                var countriesStatesCitiesSQLScript = File.ReadAllText("Data\\CountriesStatesCities.sql");
                await _context.Database.ExecuteSqlRawAsync(countriesStatesCitiesSQLScript);
            }
        }

        private async Task CheckCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { Name = "Apple" });
                _context.Categories.Add(new Category { Name = "Autos" });
                _context.Categories.Add(new Category { Name = "Belleza" });
                _context.Categories.Add(new Category { Name = "Calzado" });
                _context.Categories.Add(new Category { Name = "Comida" });
                _context.Categories.Add(new Category { Name = "Cosmeticos" });
                _context.Categories.Add(new Category { Name = "Deportes" });
                _context.Categories.Add(new Category { Name = "Erótica" });
                _context.Categories.Add(new Category { Name = "Ferreteria" });
                _context.Categories.Add(new Category { Name = "Gamer" });
                _context.Categories.Add(new Category { Name = "Hogar" });
                _context.Categories.Add(new Category { Name = "Jardín" });
                _context.Categories.Add(new Category { Name = "Jugetes" });
                _context.Categories.Add(new Category { Name = "Lenceria" });
                _context.Categories.Add(new Category { Name = "Mascotas" });
                _context.Categories.Add(new Category { Name = "Nutrición" });
                _context.Categories.Add(new Category { Name = "Ropa" });
                _context.Categories.Add(new Category { Name = "Tecnología" });
            }

            await _context.SaveChangesAsync();
        }
        private async Task CheckoutPeopleTypeAsync()
        {
            if (!_context.PeopleTypes.Any())
            {
                _context.PeopleTypes.Add(new PeopleType { Name = "Proveedor" });
            }
            await _context.SaveChangesAsync();
        }

        private async Task CheckoutPeopleAsync()
        {
            if (!_context.Peoples.Any())
            {
                _context.Peoples.Add(new People
                {
                    Identification = "56232222",
                    Name = "Tecnico aldo",
                    Surname = "The best",
                    Email = "tec_aldo@yopmail.com",
                    PeopleTypeId = 2,
                    CityId = 1,
                    PeopleType = null,
                    City = null,
                    Service = null
                });
            }
            await _context.SaveChangesAsync();
        }

        private async Task CheckoutServiceAsync()
        {
            if (!_context.Services.Any())
            {
                _context.Services.Add(new Service
                {
                    CategoryId = 1,
                    Category = null,
                    PeopleId = 2,
                    People = null,
                    Name = "Repacion de Jardiner",
                    Detail = "Repacion de 2mts de ceped mas 400gm de abono",
                    Price = "300.000"
                });
            }
            await _context.SaveChangesAsync();
        }

        private async Task CheckoutServiceOrderAsync()
        {
            if (!_context.ServiceOrders.Any())
            {
                _ = _context.ServiceOrders.Add(new ServiceOrder
                {
                    State = "Creado",
                    CreationDate = null,
                    ExecutionDate = null,
                    Detail = "pintar 40 mts cuadrados",
                });
            }
            await _context.SaveChangesAsync();
        }


        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country
                {
                    Name = "Colombia",
                    States =
                    [
                        new State()
                        {
                            Name = "Antioquia",
                            Cities = [
                                new () { Name = "Medellín" },
                                new () { Name = "Itagüí" },
                                new () { Name = "Envigado" },
                                new () { Name = "Bello" },
                                new () { Name = "Rionegro" },
                            ]
                        },
                        new State()
                        {
                            Name = "Bogotá",
                            Cities = [
                                new () { Name = "Usaquen" },
                                new () { Name = "Champinero" },
                                new () { Name = "Santa fe" },
                                new () { Name = "Useme" },
                                new () { Name = "Bosa" },
                            ]
                        },
            ]
                });
                _context.Countries.Add(new Country
                {
                    Name = "Estados Unidos",
                    States =
                    [
                        new State()
                        {
                            Name = "Florida",
                            Cities = [
                                new () { Name = "Orlando" },
                                new () { Name = "Miami" },
                                new () { Name = "Tampa" },
                                new () { Name = "Fort Lauderdale" },
                                new () { Name = "Key West" },
                            ]
                        },
                        new State()
                        {
                            Name = "Texas",
                            Cities = [
                                new () { Name = "Houston" },
                                new () { Name = "San Antonio" },
                                new () { Name = "Dallas" },
                                new () { Name = "Austin" },
                                new () { Name = "El Paso" },
                            ]
                        },
                    ]
                });
            }

            await _context.SaveChangesAsync();
        }

        private async Task CheckSubscriptionTypesAsync()
        {
            if (!_context.SubscriptionTypes.Any())
            {
                _context.SubscriptionTypes.Add(new SubscriptionType
                {
                    Name = "Free",
                    Price = 0.00,
                    Description = "Acceso básico sin costo.",
                    UserType = UserType.Costumer.ToString()
                });
                _context.SubscriptionTypes.Add(new SubscriptionType
                {
                    Name = "Premium Level 1",
                    Price = 19.99,
                    Description = "Acceso a recursos avanzados y soporte técnico estándar.",
                    UserType = UserType.Costumer.ToString()
                });
                _context.SubscriptionTypes.Add(new SubscriptionType
                {
                    Name = "Premium Level 2",
                    Price = 39.99,
                    Description = "Acceso completo a todos los recursos y soporte prioritario.",
                    UserType = UserType.Costumer.ToString()
                });
                _context.SubscriptionTypes.Add(new SubscriptionType
                {
                    Name = "Associated",
                    Price = 99.99,
                    Description = "Para entidades asociadas con beneficios extendidos y colaboraciones.",
                    UserType = UserType.Costumer.ToString()
                });
                _context.SubscriptionTypes.Add(new SubscriptionType
                {
                    Name = "Expert",
                    Price = 199.99,
                    Description = "Para expertos y consultores con acceso a análisis de datos y herramientas avanzadas.",
                    UserType = UserType.Costumer.ToString()
                });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckSurveyDataAsync()
        {
            if (!_context.SurveyDefinitions.Any())
            {
                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Experiencia del servicio",
                    Description = "¿Cómo fue tu experiencia con el servicio prestado?",
                    QuestionType = QuestionTypeEnum.MultipleChoice.ToString(),
                    UserType = UserType.Costumer.ToString()
                });

                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Información por parte del usuario",
                    Description = "¿El usuario entregó la información necesaria para realizar el servicio?",
                    QuestionType = QuestionTypeEnum.SingleResponse.ToString(),
                    UserType = UserType.Costumer.ToString()
                });

                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Satisfacción general",
                    Description = "¿Qué tan satisfecho estás con nuestro servicio en general?",
                    QuestionType = QuestionTypeEnum.StarRange.ToString(),
                    UserType = UserType.Costumer.ToString()
                });

                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Calidad del producto",
                    Description = "Califica la calidad del producto recibido",
                    QuestionType = QuestionTypeEnum.StarRange.ToString(),
                    UserType = UserType.Costumer.ToString()
                });

                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Tiempo de respuesta",
                    Description = "¿El tiempo de respuesta fue adecuado?",
                    QuestionType = QuestionTypeEnum.TrueFalse.ToString(),
                    UserType = UserType.Costumer.ToString()
                });

                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Facilidad de uso",
                    Description = "¿Qué tan fácil es utilizar nuestro servicio?",
                    QuestionType = QuestionTypeEnum.SingleResponse.ToString(),
                    UserType = UserType.Costumer.ToString()
                });

                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Recomendaciones",
                    Description = "¿Recomendarías nuestro servicio a otras personas?",
                    QuestionType = QuestionTypeEnum.SingleResponse.ToString(),
                    UserType = UserType.Costumer.ToString()
                });

                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Eficiencia del proceso",
                    Description = "¿El proceso de servicio fue eficiente?",
                    QuestionType = QuestionTypeEnum.TrueFalse.ToString(),
                    UserType = UserType.Costumer.ToString()
                });

                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Sugerencias de mejora",
                    Description = "¿Tienes alguna sugerencia para mejorar nuestro servicio?",
                    QuestionType = QuestionTypeEnum.Comment.ToString(),
                    UserType = UserType.Costumer.ToString()
                });

                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Comentarios generales",
                    Description = "Por favor, proporciona cualquier otro comentario que tengas.",
                    QuestionType = QuestionTypeEnum.Comment.ToString(),
                    UserType = UserType.Costumer.ToString()
                });

                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Disponibilidad del servicio",
                    Description = "¿El servicio estuvo disponible cuando lo necesitaste?",
                    QuestionType = QuestionTypeEnum.TrueFalse.ToString(),
                    UserType = UserType.Costumer.ToString()
                });

                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Atención al cliente",
                    Description = "Califica la atención recibida por parte de nuestro equipo de soporte",
                    QuestionType = QuestionTypeEnum.StarRange.ToString(),
                    UserType = UserType.Costumer.ToString()
                });

                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Claridad de la información",
                    Description = "¿La información proporcionada fue clara y comprensible?",
                    QuestionType = QuestionTypeEnum.SingleResponse.ToString(),
                    UserType = UserType.Costumer.ToString()
                });

                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Interacción con el proveedor",
                    Description = "Describe cómo fue tu interacción con el proveedor del servicio.",
                    QuestionType = QuestionTypeEnum.Comment.ToString(),
                    UserType = UserType.Costumer.ToString()
                });

                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Compatibilidad del servicio",
                    Description = "¿El servicio es compatible con tus necesidades?",
                    QuestionType = QuestionTypeEnum.MultipleChoice.ToString(),
                    UserType = UserType.Costumer.ToString()
                });

                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Compatibilidad del servicio",
                    Description = "¿El servicio es compatible con tus necesidades?",
                    QuestionType = QuestionTypeEnum.MultipleChoice.ToString(),
                    UserType = UserType.Costumer.ToString()
                });

                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Compatibilidad del servicio",
                    Description = "¿El servicio es compatible con tus necesidades?",
                    QuestionType = QuestionTypeEnum.MultipleChoice.ToString(),
                    UserType = UserType.Costumer.ToString()
                });
                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Compatibilidad del servicio",
                    Description = "¿El servicio es compatible con tus necesidades?",
                    QuestionType = QuestionTypeEnum.MultipleChoice.ToString(),
                    UserType = UserType.Costumer.ToString()
                });

                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Compatibilidad del servicio",
                    Description = "¿El servicio es compatible con tus necesidades?",
                    QuestionType = QuestionTypeEnum.MultipleChoice.ToString(),
                    UserType = UserType.Costumer.ToString()
                });

                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Compatibilidad del servicio",
                    Description = "¿El servicio es compatible con tus necesidades?",
                    QuestionType = QuestionTypeEnum.MultipleChoice.ToString(),
                    UserType = UserType.Costumer.ToString()
                });
                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Compatibilidad del servicio",
                    Description = "¿El servicio es compatible con tus necesidades?",
                    QuestionType = QuestionTypeEnum.MultipleChoice.ToString(),
                    UserType = UserType.Costumer.ToString()
                });



                await _context.SaveChangesAsync();
            }
        }
    }
}