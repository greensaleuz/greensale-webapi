using Dapper;
using GreenSale.Application.Utils;
using GreenSale.DataAccess.Interfaces.Storages;
using GreenSale.Domain.Entites.BuyerPosts;
using GreenSale.Domain.Entites.Storages;

namespace GreenSale.DataAccess.Repositories.Storages;

public class StorageStarRepository:BaseRepository,IStorageStarRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT COUNT(*) FROM storagepoststars;";
            var result = await _connection.QuerySingleAsync<long>(query);

            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> CreateAsync(StoragePostStars entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "INSERT INTO public.storagepoststars( " +
                "user_id, post_id, stars, created_at, updated_at) " +
                    "VALUES(@UserId, @PostId, @Stars, @CreatedAt, @UpdatedAt); ";

            var result = await _connection.ExecuteScalarAsync<int>(query, entity);

            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> DeleteAsync(long Id)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"DELETE FROM public.storagepoststars WHERE id = @ID;";
            var result = await _connection.ExecuteAsync(query, new { ID = Id });

            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<List<StoragePostStars>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"SELECT * FROM storagepoststars order by id desc " +
                $"offset {@params.GetSkipCount()} limit {@params.PageSize}";

            var result = (await _connection.QueryAsync<StoragePostStars>(query)).ToList();

            return result;
        }
        catch
        {
            return new List<StoragePostStars>() { };
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<long> GetAllPostIdCountAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT COUNT(*) FROM storagepoststars whwre post_id=@Id;";
            var result = await _connection.QuerySingleAsync<long>(query, new { Id = id });

            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<StoragePostStars> GetByIdAsync(long Id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM public.storagepoststars where id=@ID;";
            var result = await _connection.QuerySingleAsync<StoragePostStars>(query, new { ID = Id });

            return result;
        }
        catch
        {
            return new StoragePostStars();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<long> GetIdAsync(long userid, long postid)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT id as Id FROM public.storagepoststars where user_id=@USERID and post_id = @POSTID;";
            var result = await _connection.QuerySingleAsync<long>(query, new { USERID = userid, POSTID = postid });

            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> UpdateAsync(long Id, StoragePostStars entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"UPDATE public.storagepoststars " +
                $"SET stars = @Stars, created_at = @CreatedAt, updated_at = @UpdatedAt " +
                    $"WHERE user_id = and post_id =; ";

            var result = await _connection.ExecuteScalarAsync<int>(query, entity);

            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }
}
