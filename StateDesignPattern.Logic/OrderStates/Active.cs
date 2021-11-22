using System;
using CSharpFunctionalExtensions;
using StateDesignPattern.Logic.Interfaces;

namespace StateDesignPattern.Logic.OrderStates
{
  public class Active : IOrderState
  {
    public string Name => nameof(Active);
    public OrderStateType Type => OrderStateType.Active;

    public (IOrderState state, Result result) Activate(string customer, Func<Result> transitionFunc)
    {
      return (this, Result.Failure("State is already 'Active'."));
    }

    public (IOrderState state, Result<Invoice> result) Complete(
      string customer, int itemCount, Func<Result<Invoice>> transitionFunc)
    {
      return !string.IsNullOrWhiteSpace(customer) && itemCount > 0
        ? (new Completed(), transitionFunc())
        : (this, Result.Failure<Invoice>(
          "Customer must be set and at least one item must be present to change from 'Active' to 'Completed'."));
    }

    public (IOrderState state, Result result) Cancel(Func<Result> transitionFunc)
    {
      return (new Canceled(), transitionFunc());
    }

    public (IOrderState state, Result result) UpdateItems(Func<Result> updateItems)
    {
      var result = updateItems();
      return (this, result);
    }

    public (IOrderState state, Result result) ChangeCustomer(Func<Result> changeCustomer)
    {
      var result = changeCustomer();
      return (this, result);
    }

    public (IOrderState state, Result result) RemoveCustomer(Func<Result> removeCustomer)
    {
      return (this, Result.Failure("Customer can not be removed in state 'Active'."));
    }

    public (IOrderState state, Result result) ChangeVehicle(Func<Result> changeVehicle)
    {
      var result = changeVehicle();
      return (this, result);
    }

    public (IOrderState state, Result result) RemoveVehicle(Func<Result> removeVehicle)
    {
      var result = removeVehicle();
      return (this, result);
    }
  }
}