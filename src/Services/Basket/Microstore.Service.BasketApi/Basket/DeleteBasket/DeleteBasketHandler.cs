using Microstore.Service.BasketApi.Data;

namespace Microstore.Service.BasketApi.Basket.GetBasket;

public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;
public record DeleteBasketResult(bool IsSuccess);

public class DeleteBasketCommandHandler
    (IBasketRepository Repository)
    : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        bool result = await Repository.DeleteBasket(command.UserName, cancellationToken);
        return new DeleteBasketResult(result);
    }
}
