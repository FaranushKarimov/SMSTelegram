using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SMSTelegram.Domain;
using SMSTelegram.Domain.Entities;

namespace SMSTelegram.Application.Seeder;

public static class CountrySeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<SmsDbContext>();

        if (await context.Countries.AnyAsync())
            return;

        var countries = new List<Country>
        {
            new() { Code = "355", Name = "Albania", IsActive = true },
            new() { Code = "376", Name = "Andorra", IsActive = true },
            new() { Code = "374", Name = "Armenia", IsActive = true },
            new() { Code = "43", Name = "Austria", IsActive = true },
            new() { Code = "375", Name = "Belarus", IsActive = true },
            new() { Code = "32", Name = "Belgium", IsActive = true },
            new() { Code = "387", Name = "BosniaAndHerzegovina", IsActive = true }, 
            new() { Code = "359", Name = "Bulgaria", IsActive = true },
            new() { Code = "86", Name = "China", IsActive = true },
            new() { Code = "385", Name = "Croatia", IsActive = true },
            new() { Code = "357", Name = "Cyprus", IsActive = true },
            new() { Code = "420", Name = "Czech Republic", IsActive = true },
            new() { Code = "45", Name = "Denmark", IsActive = true },
            new() { Code = "372", Name = "Estonia", IsActive = true },
            new() { Code = "298", Name = "Faroe Islands", IsActive = true },
            new() { Code = "358", Name = "Finland", IsActive = true },
            new() { Code = "33", Name = "France", IsActive = true },
            new() { Code = "49", Name = "Germany", IsActive = true },
            new() { Code = "350", Name = "Gibraltar", IsActive = true },
            new() { Code = "30", Name = "Greece", IsActive = true },
            new() { Code = "299", Name = "Greenland", IsActive = true },
            new() { Code = "36", Name = "Hungary", IsActive = true },
            new() { Code = "354", Name = "Iceland", IsActive = true },
            new() { Code = "98", Name = "Iran", IsActive = true },
            new() { Code = "353", Name = "Ireland", IsActive = true },
            new() { Code = "972", Name = "Israel", IsActive = true },
            new() { Code = "39", Name = "Italy", IsActive = true },
            new() { Code = "7", Name = "Kazakhstan", IsActive = true },
            new() { Code = "996", Name = "Kyrgyzstan", IsActive = true },
            new() { Code = "371", Name = "Latvia", IsActive = true },
            new() { Code = "423", Name = "Liechtenstein", IsActive = true },
            new() { Code = "370", Name = "Lithuania", IsActive = true },
            new() { Code = "352", Name = "Luxembourg", IsActive = true },
            new() { Code = "356", Name = "Malta", IsActive = true },
            new() { Code = "373", Name = "Moldova", IsActive = true },
            new() { Code = "377", Name = "Monaco", IsActive = true },
            new() { Code = "382", Name = "Montenegro", IsActive = true },
            new() { Code = "212", Name = "Morocco", IsActive = true }, 
            new() { Code = "31", Name = "Netherlands", IsActive = true },
            new() { Code = "389", Name = "North Macedonia", IsActive = true },
            new() { Code = "47", Name = "Norway", IsActive = true },
            new() { Code = "48", Name = "Poland", IsActive = true },
            new() { Code = "351", Name = "Portugal", IsActive = true },
            new() { Code = "40", Name = "Romania", IsActive = true },
            new() { Code = "7", Name = "Russia", IsActive = true },
            new() { Code = "378", Name = "San Marino", IsActive = true },
            new() { Code = "966", Name = "Saudi Arabia", IsActive = true },
            new() { Code = "381", Name = "Serbia", IsActive = true },
            new() { Code = "421", Name = "Slovakia", IsActive = true },
            new() { Code = "386", Name = "Slovenia", IsActive = true },
            new() { Code = "27", Name = "South Africa", IsActive = true },
            new() { Code = "34", Name = "Spain", IsActive = true },
            new() { Code = "46", Name = "Sweden", IsActive = true },
            new() { Code = "41", Name = "Switzerland", IsActive = true },
            new() { Code = "992", Name = "Tajikistan", IsActive = true },
            new() { Code = "90", Name = "Turkey", IsActive = true },
            new() { Code = "993", Name = "Turkmenistan", IsActive = true },
            new() { Code = "971", Name = "UAE", IsActive = true },
            new() { Code = "380", Name = "Ukraine", IsActive = true },
            new() { Code = "44", Name = "United Kingdom", IsActive = true },
            new() { Code = "1", Name = "USA", IsActive = true },
            new() { Code = "998", Name = "Uzbekistan", IsActive = true },
            new() { Code = "379", Name = "Vatican City", IsActive = true }
        };

        context.Countries.AddRange(countries);
        await context.SaveChangesAsync();
    }
}