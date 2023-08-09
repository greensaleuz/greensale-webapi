using Dapper;
using GreenSale.Application.Utils;
using GreenSale.DataAccess.Interfaces.BuyerPosts;
using GreenSale.DataAccess.ViewModels.BuyerPosts;
using GreenSale.Domain.Entites.BuyerPosts;

namespace GreenSale.DataAccess.Repositories.BuyerPosts;

public class BuyerPostsRepository : BaseRepository, IBuyerPostRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT COUNT(*) FROM buyer_posts;";
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

    public async Task<int> CreateAsync(BuyerPost entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "INSERT INTO public.buyer_posts( user_id, title, description, " +
                " price, capacity, capacity_measure, type, region, district, address, status," +
                    " category_id, phone_number, created_at, updated_at)\r\n\tVALUES (@UserId, " +
                        " @Title, @Description, @Price, @Capacity, @CapacityMeasure, @Type, @Region," +
                            " @District, @Address, @Status, @CategoryId, @PhoneNumber, @CreatedAt, " +
                                " @UpdatedAt) RETURNING id  ";

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
            string query = $"DELETE FROM public.buyer_posts WHERE id = @ID;";
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

    public async Task<List<BuyerPostViewModel>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"SELECT * FROM public.buyer_posts order by id desc " +
                $"offset {@params.GetSkipCount()} limit {@params.PageSize}";

            var result = (await _connection.QueryAsync<BuyerPostViewModel>(query)).ToList();

            return result;
        }
        catch
        {
            return new List<BuyerPostViewModel>() { };
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<BuyerPostViewModel> GetByIdAsync(long Id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM public.buyer_posts where id=@ID;";
            var result = await _connection.QuerySingleAsync<BuyerPostViewModel>(query, new { ID = Id });

            return result;
        }
        catch
        {
            return new BuyerPostViewModel();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<(int ItemsCount, List<BuyerPostViewModel>)> SearchAsync(string search, PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();

            string query = @" SELECT *  FROM seller_posts  WHERE title ILIKE '%' || @Search ||'%' " +
                $" OFFSET @offset LIMIT @limit";

            var parameters = new
            {
                Search = search,
                offset = @params.PageNumber * @params.PageSize,
                limit = @params.PageSize
            };

            var result = await _connection.QueryAsync<BuyerPostViewModel>(query, parameters);
            int Count = result.Count();

            return (Count, result.ToList());
        }
        catch
        {
            return (0, new List<BuyerPostViewModel>());
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> UpdateAsync(long Id, BuyerPost entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"UPDATE public.buyer_posts" +
                $" SET id=@Id, user_id=@UserId, title=@Title, " +
                    $"description=@Description, price=@Price, capacity=@Capacity, " +
                        $"capacity_measure=@CapasityMeansure, type=@Type, region=@Region," +
                            $" district=@District, address=@Address, status=@Status," +
                                $" category_id=@CategoryId, phone_number=@PhoneNumber, " +
                                    $"created_at=@CreatedAt, updated_at=@UpdatedAt " +
                                        $"WHERE id={Id} RETURNING id ";

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
