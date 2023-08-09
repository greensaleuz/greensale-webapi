using Dapper;
using GreenSale.Application.Utils;
using GreenSale.DataAccess.Interfaces.Storages;
using GreenSale.DataAccess.ViewModels.Storages;
using GreenSale.Domain.Entites.Storages;

namespace GreenSale.DataAccess.Repositories.Storages
{
    public class StorageRepository : BaseRepository, IStorageRepository
    {
        public async Task<long> CountAsync()
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"SELECT COUNT(*) FROM storages;";
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

        public async Task<int> CreateAsync(Storage entity)
        {
            try
            {
                await _connection.OpenAsync();

                string query = "INSERT INTO public.storages(name, description, region, district, address, " +
                    " address_latitude, address_longitude, info, image_path, user_id, created_at, updated_at) " +
                        " VALUES (@Name, @Description, @Region, @District, @Address, @AddressLatitude, " +
                            " @AddressLongitude, @Info, @ImagePath, @UserId, @CreatedAt, @UpdatedAt) RETURNING id ";

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

                string query = "DELETE FROM storages WHERE id=@ID ;";
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

        public async Task<List<StoragesViewModel>> GetAllAsync(PaginationParams @params)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "SELECT * FROM storage_viewmodel;";

                return (await _connection.QueryAsync<StoragesViewModel>(query)).ToList();
            }
            catch
            {
                return new List<StoragesViewModel>();
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<StoragesViewModel> GetByIdAsync(long Id)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"SELECT * FROM storage_viewmodel WHERE id={Id};";

                return await _connection.QuerySingleAsync<StoragesViewModel>(query);

            }
            catch
            {
                return new StoragesViewModel();
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<(int ItemsCount, List<StoragesViewModel>)> SearchAsync(string search, PaginationParams @params)
        {
            try
            {
                await _connection.OpenAsync();

                string query = @"
                            SELECT * FROM storages
                            WHERE info ILIKE '%'  @search  '%'
                            OFFSET @offset
                            LIMIT @limit";

                var parameters = new
                {
                    search,
                    offset = @params.PageNumber * @params.PageSize,
                    limit = @params.PageSize
                };

                var result = (await _connection.QueryAsync<StoragesViewModel>(query, parameters)).ToList();

                return (result.Count(), result);
            }
            catch
            {
                return (0, new List<StoragesViewModel>());
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<int> UpdateAsync(long Id, Storage entity)
        {
            try
            {
                await _connection.OpenAsync();

                string query = "UPDATE public.storages " +
                    "SET name=@Name, description=@Description, region=@Region, district=@District, address=@Address, " +
                        " address_latitude=@AddressLatitude, address_longitude=@AddressLongitude, info=@Info, " +
                            " image_path=@ImagePath, user_id=@UserId, created_at=@CreatedAt, updated_at=@UpdatedAt " +
                            $" WHERE id={Id} RETURNING id ";

                return await _connection.ExecuteScalarAsync<int>(query, entity);
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
}