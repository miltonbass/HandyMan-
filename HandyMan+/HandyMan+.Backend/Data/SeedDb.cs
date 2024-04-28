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
    }
}
