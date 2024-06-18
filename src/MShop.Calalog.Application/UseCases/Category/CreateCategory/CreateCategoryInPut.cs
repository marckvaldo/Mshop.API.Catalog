using MediatR;
using MShop.Calalog.Application.UseCases.Category.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MShop.Calalog.Application.UseCases.Category.CreateCategory
{
    public class CreateCategoryInPut : IRequest<bool>
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O Campo {0} Obrigatório")]
        [StringLength(30, ErrorMessage = "O Campo {0} precisa ter no minimo {2} caracter e no maximo {1}", MinimumLength = 3)]
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public CreateCategoryInPut(Guid id, string name, bool isActive)
        {
            Id = id;
            Name = name;
            IsActive = isActive;
        }

    }
}
