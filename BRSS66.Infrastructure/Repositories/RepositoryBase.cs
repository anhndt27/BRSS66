using System.Linq.Expressions;
using BRSS66.ApplicationCore.Entities;
using BRSS66.ApplicationCore.Interfaces.IRepositorys;
using BRSS66.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace BRSS66.Infrastructure.Repositories;

public class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
{
    private readonly AppDbContext _context;

    public RepositoryBase(AppDbContext context)
    {
        _context = context;
    }

  

    public async Task Add(T obj, bool commit = true)
    {
        _context.Set<T>().Add(obj);
        if (commit)
            await _context.SaveChangesAsync();
    }

    public async Task<T?> Get(int id, string includeProperties = "")
    {
        IQueryable<T> query = _context.Set<T>();
        foreach (var includeProperty in includeProperties.Split
                     (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        return await query.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<T?> Get(Expression<Func<T, bool>> filter, string includeProperties = "")
    {
        IQueryable<T> query = _context.Set<T>();
        foreach (var includeProperty in includeProperties.Split
                     (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        return await query.FirstOrDefaultAsync(filter);
    }

    public async Task<List<T>> Get(string? includeProperties = "")
    {
        if (string.IsNullOrEmpty(includeProperties))
            return await _context.Set<T>().ToListAsync();
        IQueryable<T> query = _context.Set<T>();
        foreach (var includeProperty in includeProperties.Split
                     (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        return await query.ToListAsync();
    }

    public async Task Delete(T obj, bool commit = true)
    {
        _context.Set<T>().Remove(obj);
        if (commit)
            await _context.SaveChangesAsync();
    }

    public async Task Update(T obj, bool commit = true)
    {
        _context.Set<T>().Update(obj);
        if (commit)
            await _context.SaveChangesAsync();
    }

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }

    public IEnumerable<T> Find(Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "")
    {
        IQueryable<T> query = _context.Set<T>();
        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split
                     (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            return orderBy(query);
        }

        return query.ToList();
    }

    public IQueryable<T> Query(Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "")
    {
        IQueryable<T> query = _context.Set<T>().AsNoTracking();
        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split
                     (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            return orderBy(query);
        }

        return query.AsQueryable();
    }

    public async Task TruncateTable(string tableName)
    {
        string cmd = $"TRUNCATE TABLE {tableName}";
        await _context.Database.ExecuteSqlRawAsync(cmd);
    }
}