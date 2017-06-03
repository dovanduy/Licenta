﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Licenta.Messaging;
using Licenta.Messaging.Messages.Commands;
using Licenta.Messaging.Model;
using LicentaHighLevelApi.Model.DTOs;
using LicentaHighLevelApi.Model.Messages;
using LicentaHighLevelApi.Services.Interfaces;
using MassTransit;

namespace LicentaHighLevelApi.Services
{
    public class CategoryService : ICategoryService
    {
        public async Task Add(Category category)
        {
            using (BusMessagingService busService = new BusMessagingService())
            {
                await busService.BusControl.Publish<IAddCategoryCommand>(new AddCategoryCommand { Category = category });
            }
        }

        public async Task Update(Category category)
        {
            using (BusMessagingService busService = new BusMessagingService())
            {
                await busService.BusControl.Publish<IUpdateCategoryCommand>(new UpdateCategoryCommand { Category = category });
            }
        }

        public async Task Delete(Category category)
        {
            await Delete(category.CategoryId);
        }

        public async Task Delete(int categoryId)
        {
            using (BusMessagingService busService = new BusMessagingService())
            {
                await busService.BusControl.Publish<IDeleteCategoryCommand>(new DeleteCategoryCommand { CategoryId = categoryId });
            }
        }
    }
}