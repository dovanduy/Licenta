using System;
using System.Data;
using Automatonymous;
using Licenta.Messaging.Messages.Commands;
using Licenta.Messaging.Messages.Events;
using Licenta.Messaging.Messages.Responses;

namespace Licenta.Order
{
    public class OrderSaga : MassTransitStateMachine<OrderSagaState>
    {
        public State Recieved { get; private set; }
        public State WaitingConfirmation { get; private set; }
        public State PartiallyConfirmed { get; private set; }
        public State CheckingWithUser { get; private set; }
        public State Approved { get; private set; }
        public State Delivering { get; private set; }
        public State Canceled { get; private set; }
        
        public Event<IRegisterOrderCommand> RegisterOrder { get; private set; }
        public Event<IOrderRegisteredEvent> OrderRegistered { get; private set; }

        public Event<IStockVerifiedResponse> StockVerified { get; private set; }
        public Event<IDeliveryDateAproximationResponse> DeliveryDateAproximated { get; private set; }
        public Event<IConfirmOrderCommand> OrderConfirmationResult { get; private set; }

        public Event<IApproveOrderCommand> OrderApprovedByUser { get; private set; }
        public Event<IPackagePickedUpEvent> PackagePickedUp { get; private set; }
        public Event<IPackageDeliveredEvent> PackageDelivered { get; private set; }
        public Event<ICancelOrderCommand> OrderCanceled { get; private set; }

        public OrderSaga()
        {
            InstanceState(s => s.CurrentState);

            Event(() => RegisterOrder, cc => {
                cc.CorrelateBy(state => state.UserId, context => context.Message.UserId)
                    .SelectId(context => Guid.NewGuid());
            });
            Event(() => OrderRegistered, cc => cc.CorrelateById(context => context.Message.CorrelationId));

            Event(() => StockVerified, cc => cc.CorrelateBy(state => state.OrderId, context => context.Message.OrderId.ToString()));
            Event(() => DeliveryDateAproximated, cc => cc.CorrelateBy(state => state.OrderId, context => context.Message.OrderId.ToString()));
            Event(() => OrderConfirmationResult, cc => cc.CorrelateBy(state => state.OrderId, context => context.Message.OrderId.ToString()));
            Event(() => OrderApprovedByUser, cc => cc.CorrelateBy(state => state.OrderId, context => context.Message.OrderId.ToString()));
            Event(() => PackageDelivered, cc => cc.CorrelateBy(state => state.PackageId, context => context.Message.PackageId.ToString()));
            Event(() => OrderCanceled, cc => cc.CorrelateBy(state => state.OrderId, context => context.Message.OrderId.ToString()));

            Initially(
                When(RegisterOrder)
                    .Then(context =>
                    {
                        context.Instance.UserId = context.Data.UserId;
                    })
                    .TransitionTo(Recieved)
                    //.Publish(context => new NewOrderCommand(context.Instance))
            );

            During(Recieved,
                When(OrderRegistered)
                .Then(context =>
                    {  })
                .TransitionTo(WaitingConfirmation)
                //.Publish(context => new OrderNeedsConfirmationEvent(context.Instance))
            );

            During(WaitingConfirmation,
                When(StockVerified)
                    .Then(context =>
                    {
                        //context.Instance.Verified = context.Data.VerificationResult;
                    })
            );

            During(WaitingConfirmation,
                When(DeliveryDateAproximated)
                    .Then(context =>
                    {
                        //context.Instance.DeliveryDateAproximation = context.Data.DeliveryDateAproximation;
                    })
            );

            During(WaitingConfirmation,
                When(OrderConfirmationResult, context => context.Instance.Verified)
                    .TransitionTo(CheckingWithUser)
                    //.Publish(context => new OrderUpdatedEvent(context.Instance))
            );

            During(WaitingConfirmation,
                When(OrderConfirmationResult, context => !context.Instance.Verified)
                    .TransitionTo(PartiallyConfirmed)
                    //.Publish(context => new OrderUpdatedEvent(context.Instance))
            );

            During(CheckingWithUser,
                When(OrderApprovedByUser)
                    .TransitionTo(Approved)
                    //.Publish(context => new OrderUpdatedEvent(context.Instance))
                    //.Publish(context => new OrderApprovedEvent(context.Instance))
            );

            During(Approved,
                When(PackagePickedUp)
                    .TransitionTo(Delivering)
                    //.Publish(context => new OrderUpdatedEvent(context.Instance))
            );

            During(Delivering,
                When(PackageDelivered)
                    //.Publish(context => new OrderUpdatedEvent(context.Instance))
                    .Finalize()
            );

            During(new[] {Recieved, PartiallyConfirmed, WaitingConfirmation, CheckingWithUser, Approved},
                When(OrderCanceled)
                    .TransitionTo(Canceled)
                    //.Publish(context => new OrderCanceledEvent(context.Instance))        
            );

            CompositeEvent(() => OrderConfirmationResult, x => x.ConfirmedStatus, DeliveryDateAproximated, StockVerified);
        }
    }
}
