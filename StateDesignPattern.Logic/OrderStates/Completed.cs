using System;
using CSharpFunctionalExtensions;
using StateDesignPattern.Logic.Interfaces;

namespace StateDesignPattern.Logic.OrderStates;

public class Completed : IOrderState
{
  public string Name => nameof(Completed);
  public OrderStateType Type => OrderStateType.Completed;

  public Result<IOrderState> Activate(string customer) => Result.Failure<IOrderState>("State can not be changed to 'Active'.");

  public Result<(IOrderState State, IInvoice Invoice)> Complete(
    string customer, int itemsCount, Func<Result<Invoice>> createInvoice) =>
    Result.Failure<(IOrderState, IInvoice)>("State is already 'Completed'.");

  public Result<IOrderState> Cancel() => Result.Failure<IOrderState>("State can not be changed to 'Canceled'.");

  public Result UpdateItems(Func<Result> updateItems) => Result.Failure("Items can not be updated in state 'Completed'.");
  public Result ChangeCustomer(Func<Result> changeCustomer) => Result.Failure("Customer can not be changed in state 'Completed'.");
  public Result RemoveCustomer(Func<Result> removeCustomer) => Result.Failure("Customer can not be removed in state 'Completed'.");
  public Result ChangeVehicle(Func<Result> changeVehicle) => Result.Failure("Vehicle can not be changed in state 'Completed'.");
  public Result RemoveVehicle(Func<Result> removeVehicle) => Result.Failure("Vehicle can not be removed in state 'Completed'.");
}