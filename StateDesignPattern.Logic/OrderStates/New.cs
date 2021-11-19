using System;
using CSharpFunctionalExtensions;
using StateDesignPattern.Logic.Interfaces;

namespace StateDesignPattern.Logic.OrderStates
{
  public class New : IOrderState
  {
    public string Name => nameof(New);
    public OrderStateType Type => OrderStateType.New;
    public (IOrderState state, Result result) Activate(Func<bool> preconditionsMet, Func<Result> transitionFunc)
    {
      return preconditionsMet()
        ? (new Active(), transitionFunc())
        : (this, Result.Failure("Preconditions not met to change from 'New' to 'Active'."));
    }

    public (IOrderState state, Result<Invoice> result) Complete(
      Func<bool> preconditionsMet, Func<Result<Invoice>> transitionFunc)
    {
      return (this, Result.Failure<Invoice>("State can not be changed to 'Complete'."));
    }

    public (IOrderState state, Result result) Cancel(Func<bool> preconditionsMet, Func<Result> transitionFunc)
    {
      return preconditionsMet()
        ? (new Canceled(), transitionFunc())
        :(this, Result.Failure("Preconditions not met to change from 'New' to 'Canceled'."));
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