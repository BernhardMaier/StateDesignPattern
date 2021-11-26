using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using StateDesignPattern.Logic.Interfaces;

namespace StateDesignPattern.Logic
{
  public class NoOrder : IOrder
  {
    private NoOrder(Guid id)
    {
      Id = id;
    }

    private string Error => $"Operation not possible: No order with id {Id}";

    public Result CanBeMapped => Result.Failure($"Operation not possible: No order with id {Id}");

    public Guid Id { get; }
    public string CurrentState => string.Empty;
    public string Customer => string.Empty;
    public string Vehicle => string.Empty;
    public List<string> Items => new ();

    public static IOrder Instance(Guid id) => new NoOrder(id);

    public Result Activate() => Result.Failure(Error);
    public Result<IInvoice> Complete() => Result.Failure<IInvoice>(Error);
    public Result Cancel() => Result.Failure(Error);
    public Result UpdateItems(List<string> items) => Result.Failure(Error);
    public Result ChangeCustomer(string customer) => Result.Failure(Error);
    public Result RemoveCustomer() => Result.Failure(Error);
    public Result ChangeVehicle(string vehicle) => Result.Failure(Error);
    public Result RemoveVehicle() => Result.Failure(Error);
  }
}