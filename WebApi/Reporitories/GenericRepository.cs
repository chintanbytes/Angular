using MyShop.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using MyShop.WebApi.ResourceParameters;
using MyShop.WebApi.Helpers;

namespace MyShop.WebApi.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<IGenericRepository<T>> logger;

    public GenericRepository(ApplicationDbContext context, ILogger<IGenericRepository<T>> logger)
    {
        this.dbContext = context;
        this.logger = logger;
    }

    //create repo method for getting all entiries
    public async Task<Result<PagedList<T>>> GetAllAsync(BaseResourceParameters parameters)
    {
        try
        {
            var collection = dbContext.Set<T>() as IQueryable<T>;
            var entity = await PagedList<T>.CreateAsync(collection, parameters.PageNumber, parameters.PageSize);
            return Result<PagedList<T>>.SetSuccess(entity);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving a resource {type}.", typeof(T).Name);
            return Result<PagedList<T>>.SetFailure("Error retrieving a resource.");
        }
    }

    public async Task<Result<T>> GetByIdAsync(long id)
    {
        var entity = await dbContext.Set<T>().FindAsync(id);

        if (entity == null)
        {
            return Result<T>.SetFailure("The requested resource was not found.");
        }

        return Result<T>.SetSuccess(entity);
    }

    public virtual async Task<Result<T>> CreateAsync(T entity)
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

    public async Task<Result<T>> UpdateAsync(long id, T entity)
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
    public async Task<Result<T>> DeleteAsync(long id)
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