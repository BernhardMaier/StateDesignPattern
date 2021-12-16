using System;
using CSharpFunctionalExtensions;
using StateDesignPattern.Logic.OrderStates;

namespace StateDesignPattern.Logic.Interfaces;

public interface IOrderState
{
  string Name { get; }
  OrderStateType Type { get; }

  Result<IOrderState> Activate(string customer);
  Result<(IOrderState State, IInvoice Invoice)> Complete(string customer, int itemsCount,
    Func<Result<Invoice>> createInvoice);
  Result<IOrderState> Cancel();

  Result UpdateItems(Func<Result> updateItems);
  Result ChangeCustomer(Func<Result> changeCustomer);
  Result RemoveCustomer(Func<Result> removeCustomer);
  Result ChangeVehicle(Func<Result> changeVehicle);
  Result RemoveVehicle(Func<Result> removeVehicle);
}