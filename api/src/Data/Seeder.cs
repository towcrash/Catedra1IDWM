using api.src.Data;
using api.src.Models;

namespace api.src.Data
{
    public static class Seeder
    {
        public static async Task Seed(DataContext context)
        {
            if (context.Users.Any() || context.Genders.Any())
                return;

            var GenderList = new List<Gender>
            {
                new Gender { Id = 1, GenderName = "MASCULINO"},
                new Gender { Id = 2, GenderName = "FEMENINO"},
                new Gender { Id = 3, GenderName = "OTRO"},
                new Gender { Id = 4, GenderName = "PREFIERO NO DECIRLO"}
            };

            await context.Genders.AddRangeAsync(GenderList);
            await context.SaveChangesAsync();
            

            var genders = context.Genders.ToList();
            var random = new Random();

            for (int i = 0; i < 10; i++)
            {
                var randomGender = genders[random.Next(0, genders.Count)];
                var user = new User
                {
                    Rut = $"21.761.312-{i}",
                    Name = $"Name {i}",
                    Email = $"EmailFalso{i}@gmail.com",
                    BirthDate = new DateOnly(2000, i, 10),
                    GenderId = randomGender.Id,
                    Gender = randomGender,
                };

                await context.Users.AddAsync(user);
            }

            await context.SaveChangesAsync();
        }
    }
}