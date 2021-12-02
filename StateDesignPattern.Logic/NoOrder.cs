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

    public Guid Id { get; }
    public string CurrentState => string.Empty;
    public string Customer => string.Empty;
    public string Vehicle => string.Empty;
    public List<string> Items => new ();

    public static IOrder Instance(Guid id) => new NoOrder(id);

    public Result Activate() => Result.Failure(Error);
    public Result<IInvoice> Complete() => Result.Failure<IInvoice>(Error);
    public Result Cancel() => Result.Failure(Error);
    public Result<IOrder> UpdateItems(List<string> items) => Result.Failure<IOrder>(Error);
    public Result<IOrder> ChangeCustomer(string customer) => Result.Failure<IOrder>(Error);
    public Result<IOrder> RemoveCustomer() => Result.Failure<IOrder>(Error);
    public Result<IOrder> ChangeVehicle(string vehicle) => Result.Failure<IOrder>(Error);
    public Result<IOrder> RemoveVehicle() => Result.Failure<IOrder>(Error);
    
    public Result<T> Map<T>(Func<IOrder, T> mapping) => Result.Failure<T>(Error);
  }
}