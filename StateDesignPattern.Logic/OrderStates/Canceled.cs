using System;
using CSharpFunctionalExtensions;
using StateDesignPattern.Logic.Interfaces;

namespace StateDesignPattern.Logic.OrderStates;

public class Canceled : IOrderState
{
  public string Name => nameof(Canceled);
  public OrderStateType Type => OrderStateType.Canceled;

  public Result<IOrderState> Activate(string customer) => Result.Failure<IOrderState>("State can not be changed to 'Active'.");

  public Result<(IOrderState State, IInvoice Invoice)> Complete(
    string customer, int itemCount, Func<Result<Invoice>> createInvoice) =>
    Result.Failure<(IOrderState, IInvoice)>("State can not be changed to 'Completed'.");

  public Result<IOrderState> Cancel() => Result.Failure<IOrderState>("State is already 'Canceled'.");

  public Result UpdateItems(Func<Result> updateItems) => Result.Failure("Items can not be updated in state 'Canceled'.");
  public Result ChangeCustomer(Func<Result> changeCustomer) => Result.Failure("Customer can not be changed in state 'Canceled'.");
  public Result RemoveCustomer(Func<Result> removeCustomer) => Result.Failure("Customer can not be removed in state 'Canceled'.");
  public Result ChangeVehicle(Func<Result> changeVehicle) => Result.Failure("Vehicle can not be changed in state 'Canceled'.");
  public Result RemoveVehicle(Func<Result> removeVehicle) => Result.Failure("Vehicle can not be removed in state 'Canceled'.");
}