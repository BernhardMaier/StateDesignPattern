using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace StateDesignPattern.Logic.Interfaces
{
  public interface IOrder
  {
    Guid Id { get; }
    string CurrentState { get; }
    string Customer { get; }
    string Vehicle { get; }
    List<string> Items { get; }

    Result Activate();
    Result<IInvoice> Complete();
    Result Cancel();

    Result<IOrder> UpdateItems(List<string> items);
    Result<IOrder> ChangeCustomer(string customer);
    Result<IOrder> RemoveCustomer();
    Result<IOrder> ChangeVehicle(string vehicle);
    Result<IOrder> RemoveVehicle();

    Result<T> Map<T>(Func<IOrder, T> mapping);
  }
}