using GreenSale.Application.Exceptions.Categories;
using GreenSale.Application.Utils;
using GreenSale.DataAccess.Interfaces.Categories;
using GreenSale.Domain.Entites.Categories;
using GreenSale.Persistence.Dtos.CategoryDtos;
using GreenSale.Service.Helpers;
using GreenSale.Service.Interfaces.Categories;
using GreenSale.Service.Interfaces.Common;

namespace GreenSale.Service.Service.Categories
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _repository;
        private IPaginator _paginator;

        public CategoryService(
            ICategoryRepository repository,
            IPaginator paginator)
        {
            _repository = repository;
            _paginator = paginator;
        }

        public async Task<long> CountAsync()
        {
            return await _repository.CountAsync();
        }

        public async Task<bool> CreateAsync(CategoryCreateDto dto)
        {
            Category category = new Category()
            {
                Name = dto.Name,
                CreatedAt = TimeHelper.GetDateTime(),
                UpdatedAt = TimeHelper.GetDateTime()
            };

            var result = await _repository.CreateAsync(category);

            return result > 0;
        }

        public async Task<bool> DeleteAsync(long categoryId)
        {
            var category = await _repository.GetByIdAsync(categoryId);

            if (category is null)
            {
                throw new CategoryNotFoundException();
            }

            var result = await _repository.DeleteAsync(categoryId);

            return result > 0;
        }

        public async Task<List<Category>> GetAllAsync(PaginationParams @params)
        {
            var categories = await _repository.GetAllAsync(@params);
            var count = await _repository.CountAsync();
            _paginator.Paginate(count, @params);

            return categories;
        }

        public async Task<Category> GetBYIdAsync(long categoryId)
        {
            var categories = await _repository.GetByIdAsync(categoryId);

            if (categories is null)
                throw new CategoryNotFoundException();

            return categories;
        }

        public async Task<bool> UpdateAsync(long categoryID, CategoryUpdateDto dto)
        {
            var categories = await _repository.GetByIdAsync(categoryID);

            if (categories is null)
                throw new CategoryNotFoundException();

            categories.Name = dto.Name;
            categories.UpdatedAt = TimeHelper.GetDateTime();
            var result = await _repository.UpdateAsync(categoryID, categories);

            return result > 0;
        }
    }
}
