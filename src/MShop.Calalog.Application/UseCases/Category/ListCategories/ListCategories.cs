using MediatR;
using MShop.Calalog.Application.UseCases.Category.Common;
using MShop.Catalog.Domain.Respositories;
using MShop.Core.Paginated;

namespace MShop.Calalog.Application.UseCases.Category.ListCategories
{
    public class ListCategories : IRequestHandler<ListCategoryInput, ListCategoriesOutput>
    {
        private readonly ICategoryRepository _categoryRepository;

        public ListCategories(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ListCategoriesOutput> Handle(ListCategoryInput request, CancellationToken cancellationToken)
        {
            var paginete = new PaginatedInPut(
                request.Page,
                request.PerPage,
                request.Search,
                request.OrderBy,
                request.Order);

            var categories = await _categoryRepository.FilterPaginate(paginete, cancellationToken);

            return new ListCategoriesOutput(
                categories.CurrentPage,
                categories.PerPage,
                categories.Total,
                categories.Itens.Select(x => new CategoryModelOutPut(
                    x.Id,
                    x.Name,
                    x.IsActive
                    )).ToList());

        }
    }
}
