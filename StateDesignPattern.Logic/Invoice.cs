using System;
using StateDesignPattern.Logic.Interfaces;

namespace StateDesignPattern.Logic;

public class Invoice : IInvoice
{
  public Guid Id { get; }
}