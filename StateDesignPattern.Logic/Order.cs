using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using StateDesignPattern.Logic.Interfaces;
using StateDesignPattern.Logic.OrderStates;

namespace StateDesignPattern.Logic
{
  public class Order : IOrder
  {
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

    public string CurrentState => State.Name;
    public string Customer { get; private set; } = string.Empty;
    public string Vehicle { get; private set; } = string.Empty;
    public List<string> Items { get; } = new();

    private Result SetStateAndForwardResult((IOrderState state, Result result) tuple)
    {
      State = tuple.state;
      return tuple.result;
    }

    private Result<T> SetStateAndForwardResult<T>((IOrderState state, Result<T> result) tuple)
    {
      State = tuple.state;
      return tuple.result;
    }

    private bool CanBeSetActive() => !string.IsNullOrWhiteSpace(Customer);
    private bool CanBeCompleted() => !string.IsNullOrWhiteSpace(Customer) && Items.Any();
    private bool CanBeCanceled() => true;

    public Result Activate() => SetStateAndForwardResult(State.Activate(CanBeSetActive, () =>
    {
      return Result.Success();
    }));
    
    public Result<Invoice> Complete() => SetStateAndForwardResult(State.Complete(CanBeCompleted, () =>
    {
      var invoice = new Invoice();
      return Result.Success(invoice);
    }));
    
    public Result Cancel() => SetStateAndForwardResult(State.Cancel(CanBeCanceled, () =>
    {
      return Result.Success();
    }));

    public Result UpdateItems(List<string> items) => SetStateAndForwardResult(State.UpdateItems(() =>
    {
      Items.Clear();
      Items.AddRange(items);
      return Result.Success();
    }));

    public Result ChangeCustomer(string customer) => SetStateAndForwardResult(State.ChangeCustomer(() =>
    {
      if (string.IsNullOrWhiteSpace(customer))
        return Result.Failure("Given customer is not valid.");

      Customer = customer;
      return Result.Success();
    }));

    public Result RemoveCustomer() => SetStateAndForwardResult(State.RemoveCustomer(() =>
    {
      Customer = string.Empty;
      return Result.Success();
    }));

    public Result ChangeVehicle(string vehicle) => SetStateAndForwardResult(State.ChangeVehicle(() =>
    {
      if (string.IsNullOrWhiteSpace(vehicle))
        return Result.Failure("Given vehicle is not valid.");

      Vehicle = vehicle;
      return Result.Success();
    }));

    public Result RemoveVehicle() => SetStateAndForwardResult(State.RemoveVehicle(() =>
    {
      Vehicle = string.Empty;
      return Result.Success();
    }));
  }
}