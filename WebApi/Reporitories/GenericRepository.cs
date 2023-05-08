using Angular.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Angular.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly NorthwindContext dbContext;
    private readonly ILogger<IGenericRepository<T>> logger;

    public GenericRepository(NorthwindContext context, ILogger<IGenericRepository<T>> logger)
    {
        this.dbContext = context;
        this.logger = logger;
    }

    //create repo method for getting all entiries
    public async Task<Result<IEnumerable<T>>> GetAllAsync()
    {
        try
        {
            var entity = await dbContext.Set<T>().ToListAsync();

            if (entity == null || entity.Count == 0)
            {
                return Result<IEnumerable<T>>.SetFailure("The requested resource was not found.");
            }

            return Result<IEnumerable<T>>.SetSuccess(entity);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving a resource {type}.", typeof(T).Name);
            return Result<IEnumerable<T>>.SetFailure("Error retrieving a resource.");
        }
    }

    public async Task<Result<T>> GetByIdAsync(int id)
    {
        var entity = await dbContext.Set<T>().FindAsync(id);

        if (entity == null)
        {
            return Result<T>.SetFailure("The requested resource was not found.");
        }

        return Result<T>.SetSuccess(entity);
    }

    public async Task<Result<T>> CreateAsync(T entity)
    {
        try
        {
            await dbContext.Set<T>().AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return Result<T>.SetSuccess(entity);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating a resource of type {type}.", typeof(T).Name);
            return Result<T>.SetFailure("Error creating a resource.");
        }
    }

    public async Task<Result<T>> UpdateAsync(int id, T entity)
    {
        try
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();

            return Result<T>.SetSuccess(entity);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating a resource of type {type}.", typeof(T).Name);
            return Result<T>.SetFailure("Error updating a resource.");
        }
    }
    public async Task<Result<T>> DeleteAsync(int id)
    {
        var entity = await dbContext.Set<T>().FindAsync(id);
        if (entity == null)
        {
            return Result<T>.SetFailure("The resource was not found.");
        }

        dbContext.Set<T>().Remove(entity);
        await dbContext.SaveChangesAsync();
        return Result<T>.SetSuccess(null);
    }
}