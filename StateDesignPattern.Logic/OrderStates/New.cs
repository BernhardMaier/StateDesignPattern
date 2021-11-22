using System;
using CSharpFunctionalExtensions;
using StateDesignPattern.Logic.Interfaces;

namespace StateDesignPattern.Logic.OrderStates
{
  public class New : IOrderState
  {
    public string Name => nameof(New);
    public OrderStateType Type => OrderStateType.New;

    public (IOrderState state, Result result) Activate(string customer, Func<Result> transitionFunc)
    {
      return !string.IsNullOrWhiteSpace(customer)
        ? (new Active(), transitionFunc())
        : (this, Result.Failure("Customer must be set to change from 'New' to 'Active'."));
    }

    public (IOrderState state, Result<Invoice> result) Complete(
      string customer, int itemsCount, Func<Result<Invoice>> transitionFunc)
    {
      return (this, Result.Failure<Invoice>("State can not be changed to 'Complete'."));
    }

    public (IOrderState state, Result result) Cancel(Func<Result> transitionFunc)
    {
      return (new Canceled(), transitionFunc());
    }

    public (IOrderState state, Result result) UpdateItems(Func<Result> updateItems)
    {
      return (this, Result.Failure("Items can not be updated in state 'New'."));
    }

    public (IOrderState state, Result result) ChangeCustomer(Func<Result> changeCustomer)
    {
      var result = changeCustomer();
      return (this, result);
    }

    public (IOrderState state, Result result) RemoveCustomer(Func<Result> removeCustomer)
    {
      var result = removeCustomer();
      return (this, result);
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