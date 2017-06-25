using System;

namespace Licenta.Messaging.Messages.Commands
{
    public interface IApproveOrderCommand
    {
        int OrderId { get; set; }
    }
}