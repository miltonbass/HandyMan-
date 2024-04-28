using HandyMan_.Shered.Entities;

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
            await CheckoutPeopleTypeAsync();
            await CheckoutPeopleAsync();
            await CheckoutServiceAsync();
            await CheckoutServiceOrderAsync();
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
                _context.Categories.Add(new Category { Name = "Hogar" });
                _context.Categories.Add(new Category { Name = "Construcción" });
                _context.Categories.Add(new Category { Name = "Reparaciones Locativas" });
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
    }
}