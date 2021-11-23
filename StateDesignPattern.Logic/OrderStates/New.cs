using System;
using CSharpFunctionalExtensions;
using StateDesignPattern.Logic.Interfaces;

namespace StateDesignPattern.Logic.OrderStates
{
  public class New : IOrderState
  {
    public string Name => nameof(New);
    public OrderStateType Type => OrderStateType.New;

    public Result<IOrderState> Activate(string customer)
    {
      return !string.IsNullOrWhiteSpace(customer)
        ? new Active()
        : Result.Failure<IOrderState>("Customer must be set to change from 'New' to 'Active'.");
    }

    public Result<(IOrderState State, IInvoice Invoice)> Complete(
      string customer, int itemsCount, Func<Result<Invoice>> createInvoice) =>
      Result.Failure<(IOrderState, IInvoice)>("State can not be changed to 'Complete'.");

    public Result<IOrderState> Cancel() => new Canceled();

    public Result UpdateItems(Func<Result> updateItems) => Result.Failure("Items can not be updated in state 'New'.");
    public Result ChangeCustomer(Func<Result> changeCustomer) => changeCustomer();
    public Result RemoveCustomer(Func<Result> removeCustomer) => removeCustomer();
    public Result ChangeVehicle(Func<Result> changeVehicle) => changeVehicle();
    public Result RemoveVehicle(Func<Result> removeVehicle) => removeVehicle();
  }
}