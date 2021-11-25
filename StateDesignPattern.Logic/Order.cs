using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using StateDesignPattern.Logic.Interfaces;
using StateDesignPattern.Logic.OrderStates;

namespace StateDesignPattern.Logic
{
  public class Order : IOrder
  {
    public Order()
    {
      Id = Guid.NewGuid();
    }
    
    private int StateType { get; set; }
    private IOrderState? _state;
    private IOrderState State
    {
      get => _state ??= OrderStateFactory.Create(StateType);
      set
      {
        _state = value;
        StateType = (int)_state.Type;
      }
    }

    public Guid Id { get; private set; }
    public string CurrentState => State.Name;
    public string Customer { get; private set; } = string.Empty;
    public string Vehicle { get; private set; } = string.Empty;
    public List<string> Items { get; } = new();

    private void SetState(IOrderState newState) => State = newState;

    public Result Activate()
    {
      return State
        .Activate(Customer)
        .OnSuccessTry(SetState);
    }
    
    public Result<IInvoice> Complete()
    {
      return State
        .Complete(Customer, Items.Count, () =>
        {
          var invoice = new Invoice();
          return Result.Success(invoice);
        })
        .Tap(tuple => SetState(tuple.State))
        .Map(tuple => tuple.Invoice);
    }

    public Result Cancel()
    {
      return State
        .Cancel()
        .OnSuccessTry(SetState);
    }

    public Result UpdateItems(List<string> items)
    {
      return State
        .UpdateItems(() =>
        {
          Items.Clear();
          Items.AddRange(items);
          return Result.Success();
        });
    }

    public Result ChangeCustomer(string customer)
    {
      return State
        .ChangeCustomer(() =>
        {
          if (string.IsNullOrWhiteSpace(customer))
            return Result.Failure("Given customer is not valid.");

          Customer = customer;
          return Result.Success();
        });
    }

    public Result RemoveCustomer()
    {
      return State
        .RemoveCustomer(() =>
        {
          Customer = string.Empty;
          return Result.Success();
        });
    }

    public Result ChangeVehicle(string vehicle)
    {
      return State
        .ChangeVehicle(() =>
        {
          if (string.IsNullOrWhiteSpace(vehicle))
            return Result.Failure("Given vehicle is not valid.");

          Vehicle = vehicle;
          return Result.Success();
        });
    }

    public Result RemoveVehicle()
    {
      return State
        .RemoveVehicle(() =>
        {
          Vehicle = string.Empty;
          return Result.Success();
        });
    }
  }
}