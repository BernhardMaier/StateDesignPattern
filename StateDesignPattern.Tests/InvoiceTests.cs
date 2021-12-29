using FluentAssertions;
using StateDesignPattern.Logic;
using Xunit;

namespace StateDesignPattern.Tests;

public class InvoiceTests
{
  [Fact]
  public void Properties_are_initialized_correctly()
  {
    var invoice = new Invoice();

    invoice.Id.Should().NotBeEmpty();
  }
}