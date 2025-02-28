namespace Microstore.Service.OrderingApplication.Orders.Commands.CreateOrder;

public class CreateOrderHandler
    : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public Task<CreateOrderResult> Handle
    (
        CreateOrderCommand request, 
        CancellationToken cancellationToken
    )
    {
        // Create Orer entity from command object
        // Save data to database
        // return result

        throw new NotImplementedException();
    }
}