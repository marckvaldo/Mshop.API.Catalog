﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity = MShop.Catalog.Domain.Entity;

namespace MShop.Calalog.Application.UseCases.Category.Common;

public class CategoryModelOutPut
{
    public CategoryModelOutPut(Guid id, string name, bool isActive)
    {
        Id = id;
        Name = name;
        IsActive = isActive;
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public bool IsActive { get; private set; }

    public static CategoryModelOutPut FromCategory(Entity.Category category)
        => new CategoryModelOutPut(category.Id, category.Name, category.IsActive);
    
}
