﻿using Licenta.Messaging.Messages.Commands;
using Licenta.Messaging.Model;

namespace LicentaHighLevelApi.Model.Messages
{
    public class AddCategoryCommand : IAddCategoryCommand
    {
        public Category Category { get; set; }
    }
}