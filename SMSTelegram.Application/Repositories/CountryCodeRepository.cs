using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SMSTelegram.Domain;
using SMSTelegram.Domain.Entities;

namespace SMSTelegram.Application.Repositories;

public class CountryCodeRepository(SmsDbContext context) : ICountryCodeRepository
{
    public async Task<List<Country>> GetAll(CancellationToken cancellationToken)
    {
        var query = context.Countries.AsQueryable();

        var country = await query.AsNoTracking().ToListAsync(cancellationToken);
        return country;
    }

    public async Task<Country?> GetCountry(Expression<Func<Country, bool>>? filter = null,CancellationToken cancellationToken = default)
    {
        var query = context.Countries.AsQueryable();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<int> CreateCountry(Country request,CancellationToken cancellationToken = default)
    {
        await context.Countries.AddAsync(request, cancellationToken);
        return await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> UpdateCountry(Country request,CancellationToken cancellationToken = default)
    {
        context.Countries.Update(request);
        return await context.SaveChangesAsync(cancellationToken);
    }
}