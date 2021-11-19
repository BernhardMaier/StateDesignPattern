using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace StateDesignPattern.Logic.Interfaces
{
  public interface IOrder
  {
    string CurrentState { get; }
    
    Result Activate();
    Result<Invoice> Complete();
    Result Cancel();

    Result UpdateItems(List<string> items);
    Result ChangeCustomer(string customer);
    Result RemoveCustomer();
    Result ChangeVehicle(string vehicle);
    Result RemoveVehicle();
  }
}