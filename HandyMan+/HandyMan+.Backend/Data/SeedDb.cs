using HandyMan_.Shared.Entities;
using HandyMan_.Shered.Entities;
using Orders.Shared.Enums;

namespace HandyMan_.Backend.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
            await CheckCategoriesAsync();
            await CheckSubscriptionTypesAsync();
            await CheckSurveyDataAsync();
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country { Name = "Colombia" });
                _context.Countries.Add(new Country { Name = "Estados Unidos" });
            }

            await _context.SaveChangesAsync();
        }

        private async Task CheckCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { Name = "Calzado" });
                _context.Categories.Add(new Category { Name = "Tecnología" });
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
                    UserType = UserType.Usuario.ToString()
                });
                _context.SubscriptionTypes.Add(new SubscriptionType
                {
                    Name = "Premium Level 1",
                    Price = 19.99,
                    Description = "Acceso a recursos avanzados y soporte técnico estándar.",
                    UserType = UserType.Usuario.ToString()
                });
                _context.SubscriptionTypes.Add(new SubscriptionType
                {
                    Name = "Premium Level 2",
                    Price = 39.99,
                    Description = "Acceso completo a todos los recursos y soporte prioritario.",
                    UserType = UserType.Usuario.ToString()
                });
                _context.SubscriptionTypes.Add(new SubscriptionType
                {
                    Name = "Associated",
                    Price = 99.99,
                    Description = "Para entidades asociadas con beneficios extendidos y colaboraciones.",
                    UserType = UserType.Proveedor.ToString()
                });
                _context.SubscriptionTypes.Add(new SubscriptionType
                {
                    Name = "Expert",
                    Price = 199.99,
                    Description = "Para expertos y consultores con acceso a análisis de datos y herramientas avanzadas.",
                    UserType = UserType.Proveedor.ToString()
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
                    UserType = UserType.Usuario.ToString()
                });

                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Información por parte del usuario",
                    Description = "¿El usuario entregó la información necesaria para realizar el servicio?",
                    QuestionType = QuestionTypeEnum.SingleResponse.ToString(),
                    UserType = UserType.Proveedor.ToString()
                });

                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Satisfacción general",
                    Description = "¿Qué tan satisfecho estás con nuestro servicio en general?",
                    QuestionType = QuestionTypeEnum.StarRange.ToString(),
                    UserType = UserType.Usuario.ToString()
                });

                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Calidad del producto",
                    Description = "Califica la calidad del producto recibido",
                    QuestionType = QuestionTypeEnum.StarRange.ToString(),
                    UserType = UserType.Usuario.ToString()
                });

                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Tiempo de respuesta",
                    Description = "¿El tiempo de respuesta fue adecuado?",
                    QuestionType = QuestionTypeEnum.TrueFalse.ToString(),
                    UserType = UserType.Proveedor.ToString()
                });

                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Facilidad de uso",
                    Description = "¿Qué tan fácil es utilizar nuestro servicio?",
                    QuestionType = QuestionTypeEnum.SingleResponse.ToString(),
                    UserType = UserType.Usuario.ToString()
                });

                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Recomendaciones",
                    Description = "¿Recomendarías nuestro servicio a otras personas?",
                    QuestionType = QuestionTypeEnum.SingleResponse.ToString(),
                    UserType = UserType.Usuario.ToString()
                });

                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Eficiencia del proceso",
                    Description = "¿El proceso de servicio fue eficiente?",
                    QuestionType = QuestionTypeEnum.TrueFalse.ToString(),
                    UserType = UserType.Proveedor.ToString()
                });

                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Sugerencias de mejora",
                    Description = "¿Tienes alguna sugerencia para mejorar nuestro servicio?",
                    QuestionType = QuestionTypeEnum.Comment.ToString(),
                    UserType = UserType.Proveedor.ToString()
                });

                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Comentarios generales",
                    Description = "Por favor, proporciona cualquier otro comentario que tengas.",
                    QuestionType = QuestionTypeEnum.Comment.ToString(),
                    UserType = UserType.Usuario.ToString()
                });

                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Disponibilidad del servicio",
                    Description = "¿El servicio estuvo disponible cuando lo necesitaste?",
                    QuestionType = QuestionTypeEnum.TrueFalse.ToString(),
                    UserType = UserType.Usuario.ToString()
                });

                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Atención al cliente",
                    Description = "Califica la atención recibida por parte de nuestro equipo de soporte",
                    QuestionType = QuestionTypeEnum.StarRange.ToString(),
                    UserType = UserType.Usuario.ToString()
                });

                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Claridad de la información",
                    Description = "¿La información proporcionada fue clara y comprensible?",
                    QuestionType = QuestionTypeEnum.SingleResponse.ToString(),
                    UserType = UserType.Usuario.ToString()
                });

                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Interacción con el proveedor",
                    Description = "Describe cómo fue tu interacción con el proveedor del servicio.",
                    QuestionType = QuestionTypeEnum.Comment.ToString(),
                    UserType = UserType.Proveedor.ToString()
                });

                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Compatibilidad del servicio",
                    Description = "¿El servicio es compatible con tus necesidades?",
                    QuestionType = QuestionTypeEnum.MultipleChoice.ToString(),
                    UserType = UserType.Proveedor.ToString()
                });

                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Compatibilidad del servicio",
                    Description = "¿El servicio es compatible con tus necesidades?",
                    QuestionType = QuestionTypeEnum.MultipleChoice.ToString(),
                    UserType = UserType.Proveedor.ToString()
                });

                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Compatibilidad del servicio",
                    Description = "¿El servicio es compatible con tus necesidades?",
                    QuestionType = QuestionTypeEnum.MultipleChoice.ToString(),
                    UserType = UserType.Proveedor.ToString()
                });
                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Compatibilidad del servicio",
                    Description = "¿El servicio es compatible con tus necesidades?",
                    QuestionType = QuestionTypeEnum.MultipleChoice.ToString(),
                    UserType = UserType.Proveedor.ToString()
                });

                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Compatibilidad del servicio",
                    Description = "¿El servicio es compatible con tus necesidades?",
                    QuestionType = QuestionTypeEnum.MultipleChoice.ToString(),
                    UserType = UserType.Proveedor.ToString()
                });

                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Compatibilidad del servicio",
                    Description = "¿El servicio es compatible con tus necesidades?",
                    QuestionType = QuestionTypeEnum.MultipleChoice.ToString(),
                    UserType = UserType.Proveedor.ToString()
                });
                _context.SurveyDefinitions.Add(new SurveyDefinitionEntity
                {
                    Title = "Compatibilidad del servicio",
                    Description = "¿El servicio es compatible con tus necesidades?",
                    QuestionType = QuestionTypeEnum.MultipleChoice.ToString(),
                    UserType = UserType.Proveedor.ToString()
                });



                await _context.SaveChangesAsync();
            }
        }
    }
}
