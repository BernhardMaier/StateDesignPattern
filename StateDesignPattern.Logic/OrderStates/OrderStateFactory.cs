using StateDesignPattern.Logic.Interfaces;

namespace StateDesignPattern.Logic.OrderStates;

public static class OrderStateFactory
{
  public static IOrderState Create(int orderStateTypeId)
  {
    return orderStateTypeId switch
    {
      (int)OrderStateType.New => new New(),
      (int)OrderStateType.Active => new Active(),
      (int)OrderStateType.Completed => new Completed(),
      (int)OrderStateType.Canceled => new Canceled(),
      _ => new New()
    };
  }
}