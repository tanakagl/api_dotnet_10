using System.Linq.Expressions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories;

public class RepositoryBase<TEntity>(ApplicationDbContext context, ILogger logger) : IRepositoryBase<TEntity>
    where TEntity : EntityBase
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly DbSet<TEntity> Set = context.Set<TEntity>();
    private readonly ILogger _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public virtual async Task<TEntity?> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Trying create register in entity: {Entity}", entity);
        await Set.AddAsync(entity, cancellationToken).ConfigureAwait(false);
        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        _logger.LogInformation("Successful create register in entity: {Entity}", entity);
        return entity;
    }

    public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Trying update register in entity: {Entity}", entity);
        Set.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Successful update register in entity: {Entity}", entity);
    }

    public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Trying delete register in entity: {Entity}", entity);
        Set.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        _logger.LogInformation("Successful delete register in entity: {Entity}", entity);
    }

    public virtual async Task<TEntity?> GetAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Searching register by predicate");
        return await Set.Where(e => !e.Deleted.HasValue || !e.Deleted.Value)
                      .FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Searching register by simple id");
        return await Set.Where(e => !e.Deleted.HasValue || !e.Deleted.Value)
                      .FirstOrDefaultAsync(e => id == e.Id, cancellationToken).ConfigureAwait(false);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Searching all registers of entity");

        var baseQuery = Set.Where(e => !e.Deleted.HasValue || !e.Deleted.Value);

        if (filter != null)
        {
            baseQuery = baseQuery.Where(filter);
        }

        return await baseQuery.ToListAsync(cancellationToken).ConfigureAwait(false);
    }
}