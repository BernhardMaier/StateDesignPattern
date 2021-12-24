using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using StateDesignPattern.Logic.Interfaces;
using StateDesignPattern.Logic.OrderStates;

namespace StateDesignPattern.Logic;

public class Order : IOrder
{
  private Order()
  {
    Id = Guid.NewGuid();
  }

  public static Result<Order> Create() =>
    Result.Success(new Order());

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

  public Guid Id { get; }
  public string CurrentState => State.Name;
  public string Customer { get; private set; } = string.Empty;
  public string Vehicle { get; private set; } = string.Empty;
  public List<string> Items { get; } = new();

  private void SetState(IOrderState newState) => State = newState;

  public Result Activate() =>
    State
      .Activate(Customer)
      .OnSuccessTry(SetState);

  public Result<IInvoice> Complete() =>
    State
      .Complete(Customer, Items.Count, () =>
      {
        var invoice = new Invoice();
        return Result.Success(invoice);
      })
      .Tap(tuple => SetState(tuple.State))
      .Map(tuple => tuple.Invoice);

  public Result Cancel() =>
    State
      .Cancel()
      .OnSuccessTry(SetState);

  public Result<IOrder> UpdateItems(List<string> items) =>
    State
      .UpdateItems(() =>
      {
        Items.Clear();
        Items.AddRange(items);
        return Result.Success();
      })
      .Bind(() => Result.Success<IOrder>(this));

  public Result<IOrder> ChangeCustomer(string customer) =>
    State
      .ChangeCustomer(() =>
      {
        if (string.IsNullOrWhiteSpace(customer))
          return Result.Failure("Given customer is not valid.");

        Customer = customer;
        return Result.Success();
      })
      .Bind(() => Result.Success<IOrder>(this));

  public Result<IOrder> RemoveCustomer() =>
    State
      .RemoveCustomer(() =>
      {
        Customer = string.Empty;
        return Result.Success();
      })
      .Bind(() => Result.Success<IOrder>(this));

  public Result<IOrder> ChangeVehicle(string vehicle) =>
    State
      .ChangeVehicle(() =>
      {
        if (string.IsNullOrWhiteSpace(vehicle))
          return Result.Failure("Given vehicle is not valid.");

        Vehicle = vehicle;
        return Result.Success();
      })
      .Bind(() => Result.Success<IOrder>(this));

  public Result<IOrder> RemoveVehicle() =>
    State
      .RemoveVehicle(() =>
      {
        Vehicle = string.Empty;
        return Result.Success();
      })
      .Bind(() => Result.Success<IOrder>(this));

  public Result<T> Map<T>(Func<IOrder, T> mapping) => Result.Success(mapping(this));
}