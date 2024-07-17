using FluentValidation.AspNetCore;
using FluentValidation.Results;
using MShop.Core.Message;
using System.Collections.ObjectModel;


namespace MShop.Core.DomainObject
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }

        protected void AddId(Guid id)
        {
            Id = id;
        }

    }
}

