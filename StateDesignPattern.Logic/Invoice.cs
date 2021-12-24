using System;
using StateDesignPattern.Logic.Interfaces;

namespace StateDesignPattern.Logic;

public class Invoice : IInvoice
{
  public Invoice()
  {
    Id = Guid.NewGuid();
  }

  public Guid Id { get; }
}