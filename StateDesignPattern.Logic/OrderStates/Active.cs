using System;
using CSharpFunctionalExtensions;
using StateDesignPattern.Logic.Interfaces;

namespace StateDesignPattern.Logic.OrderStates
{
  public class Active : IOrderState
  {
    public string Name => nameof(Active);
    public OrderStateType Type => OrderStateType.Active;

    public Result<IOrderState> Activate(string customer) => Result.Failure<IOrderState>("State is already 'Active'.");

    public Result<(IOrderState State, IInvoice Invoice)> Complete(
      string customer, int itemCount, Func<Result<Invoice>> createInvoice)
    {
      if (string.IsNullOrWhiteSpace(customer) || itemCount <= 0)
        return Result.Failure<(IOrderState, IInvoice)>(
          "Customer must be set and at least one item must be present to change from 'Active' to 'Completed'.");
      
      var createInvoiceResult = createInvoice();
      return createInvoiceResult.IsSuccess
        ? Result.Success<(IOrderState, IInvoice)>((new Completed(), createInvoiceResult.Value))
        : createInvoiceResult.ConvertFailure<(IOrderState, IInvoice)>();
    }

    public Result<IOrderState> Cancel() => new Canceled();

    public Result UpdateItems(Func<Result> updateItems) => updateItems();
    public Result ChangeCustomer(Func<Result> changeCustomer) => changeCustomer();
    public Result RemoveCustomer(Func<Result> removeCustomer) => Result.Failure("Customer can not be removed in state 'Active'.");
    public Result ChangeVehicle(Func<Result> changeVehicle) => changeVehicle();
    public Result RemoveVehicle(Func<Result> removeVehicle) => removeVehicle();
  }
}