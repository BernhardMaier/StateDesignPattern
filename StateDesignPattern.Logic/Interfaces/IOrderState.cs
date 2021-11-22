using System;
using CSharpFunctionalExtensions;
using StateDesignPattern.Logic.OrderStates;

namespace StateDesignPattern.Logic.Interfaces
{
  public interface IOrderState
  {
    string Name { get; }
    OrderStateType Type { get; }

    (IOrderState state, Result result) Activate(string customer, Func<Result> transitionFunc);
    (IOrderState state, Result<Invoice> result) Complete(string customer, int itemsCount, Func<Result<Invoice>> transitionFunc);
    (IOrderState state, Result result) Cancel(Func<Result> transitionFunc);

    (IOrderState state, Result result) UpdateItems(Func<Result> updateItems);
    (IOrderState state, Result result) ChangeCustomer(Func<Result> changeCustomer);
    (IOrderState state, Result result) RemoveCustomer(Func<Result> removeCustomer);
    (IOrderState state, Result result) ChangeVehicle(Func<Result> changeVehicle);
    (IOrderState state, Result result) RemoveVehicle(Func<Result> removeVehicle);
  }
}