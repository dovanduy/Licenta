using System;
using Automatonymous;

namespace Licenta.Order
{
    public class OrderSagaState : SagaStateMachineInstance
    {
        public State CurrentState { get; set; }
        public Guid CorrelationId { get; set; }
        public int ConfirmedStatus { get; set; }

        public string UserId { get; set; }
        public bool Verified { get; set; }
        public string OrderId { get; set; }
        public string PackageId { get; set; }
        public DateTime DeliveryDateAproximation { get; set; }
    }
}
