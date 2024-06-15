using MShop.Catalog.Domain.Entity;
using MShop.Core.DomainObject;
using MShop.Core.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MShop.Catalog.Domain.Validator
{
    public class CategoryValidator : Notification
    {
        private readonly Category _category;
        public CategoryValidator(Category category, INotification notifications) : base(notifications)
        {
            _category = category;
        }

        public override INotification Validate()
        {
            ValidationDefault.NotNullOrEmpty(_category.Name, nameof(_category.Name), _notifications);
            ValidationDefault.MaxLength(_category.Name, 30, nameof(_category.Name), _notifications);
            ValidationDefault.MinLength(_category.Name, 3, nameof(_category.Name), _notifications);
            ValidationDefault.NotNullGuid(_category.Id, nameof(_category.Id), _notifications);  
            return _notifications;
        }
    }
}
