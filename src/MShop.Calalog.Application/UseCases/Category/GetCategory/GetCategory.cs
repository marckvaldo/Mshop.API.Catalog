using MediatR;
using MShop.Calalog.Application.UseCases.Category.Common;
using MShop.Catalog.Domain.Respositories;
using MShop.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MShop.Calalog.Application.UseCases.Category.GetCategory
{
    public class GetCategory : BaseUseCase, IRequestHandler<GetCategoryInput,CategoryModelOutPut>
    {
        private readonly ICategoryRepository _categoryRepository;
        public GetCategory(Core.Message.INotification notification, ICategoryRepository categoryRepository) : base(notification)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryModelOutPut> Handle(GetCategoryInput request, CancellationToken cancellationToken)
        {
            if(request.Id == Guid.Empty)
            {
                Notify("Id Invalid!");
                return CategoryModelOutPut.Error();
            }

            var category  = await _categoryRepository.GetById(request.Id);

            if (category == null)
            {
                Notify("Categoria não encontrada!");
                return CategoryModelOutPut.Error();
            }

            return CategoryModelOutPut.FromCategory(category);
        }
    }
}
