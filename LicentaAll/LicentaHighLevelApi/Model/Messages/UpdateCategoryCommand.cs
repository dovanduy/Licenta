using Licenta.Messaging.Messages.Commands;
using Licenta.Messaging.Model;

namespace LicentaHighLevelApi.Model.Messages
{
    public class UpdateCategoryCommand : IUpdateCategoryCommand
    {
        public Category Category { get; set; }
    }
}