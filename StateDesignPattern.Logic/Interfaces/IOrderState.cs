using System;
using CSharpFunctionalExtensions;
using StateDesignPattern.Logic.OrderStates;

namespace StateDesignPattern.Logic.Interfaces
{
  public interface IOrderState
  {
    string Name { get; }
    OrderStateType Type { get; }

    (IOrderState state, Result result) Activate(Func<bool> preconditionsMet, Func<Result> transitionFunc);
    (IOrderState state, Result<Invoice> result) Complete(Func<bool> preconditionsMet, Func<Result<Invoice>> transitionFunc);
    (IOrderState state, Result result) Cancel(Func<bool> preconditionsMet, Func<Result> transitionFunc);

    (IOrderState state, Result result) UpdateItems(Func<Result> updateItems);
    (IOrderState state, Result result) ChangeCustomer(Func<Result> changeCustomer);
    (IOrderState state, Result result) RemoveCustomer(Func<Result> removeCustomer);
    (IOrderState state, Result result) ChangeVehicle(Func<Result> changeVehicle);
    (IOrderState state, Result result) RemoveVehicle(Func<Result> removeVehicle);
  }
}