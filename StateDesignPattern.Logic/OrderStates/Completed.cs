﻿using System;
using CSharpFunctionalExtensions;
using StateDesignPattern.Logic.Interfaces;

namespace StateDesignPattern.Logic.OrderStates
{
  public class Completed : IOrderState
  {
    public string Name => nameof(Completed);
    public OrderStateType Type => OrderStateType.Completed;
    
    public (IOrderState state, Result result) Activate(Func<bool> preconditionsMet, Func<Result> transitionFunc)
    {
      return (this, Result.Failure("State can not be changed to 'Active'."));
    }

    public (IOrderState state, Result<Invoice> result) Complete(
      Func<bool> preconditionsMet, Func<Result<Invoice>> transitionFunc)
    {
      return (this, Result.Failure<Invoice>("State is already 'Completed'."));
    }

    public (IOrderState state, Result result) Cancel(Func<bool> preconditionsMet, Func<Result> transitionFunc)
    {
      return (this, Result.Failure("State can not be changed to 'Canceled'."));
    }

    public (IOrderState state, Result result) UpdateItems(Func<Result> updateItems)
    {
      return (this, Result.Failure("Items can not be updated in state 'Completed'."));
    }

    public (IOrderState state, Result result) ChangeCustomer(Func<Result> changeCustomer)
    {
      return (this, Result.Failure("Customer can not be updated in state 'Completed'."));
    }

    public (IOrderState state, Result result) RemoveCustomer(Func<Result> removeCustomer)
    {
      return (this, Result.Failure("Customer can not be removed in state 'Completed'."));
    }

    public (IOrderState state, Result result) ChangeVehicle(Func<Result> changeVehicle)
    {
      return (this, Result.Failure("Vehicle can not be updated in state 'Completed'."));
    }

    public (IOrderState state, Result result) RemoveVehicle(Func<Result> removeVehicle)
    {
      return (this, Result.Failure("Vehicle can not be removed in state 'Completed'."));
    }
  }
}