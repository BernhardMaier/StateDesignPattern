using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace StateDesignPattern.Logic.Interfaces
{
  public interface IOrder
  {
    Result CanBeMapped { get; }

    Guid Id { get; }
    string CurrentState { get; }
    string Customer { get; }
    string Vehicle { get; }
    List<string> Items { get; }

    Result Activate();
    Result<IInvoice> Complete();
    Result Cancel();

    Result UpdateItems(List<string> items);
    Result ChangeCustomer(string customer);
    Result RemoveCustomer();
    Result ChangeVehicle(string vehicle);
    Result RemoveVehicle();
  }
}