using System.Collections.Generic;
using FluentAssertions;
using StateDesignPattern.Logic;
using StateDesignPattern.Logic.OrderStates;
using Xunit;

namespace StateDesignPattern.Tests
{
  public class OrderTests
  {
    [Fact]
    public void Properties_are_initialized_correctly()
    {
      var order = new Order();

      order.Customer.Should().BeEmpty();
      order.Vehicle.Should().BeEmpty();
      order.Items.Should().HaveCount(0);
    }
    
    [Fact]
    public void Initial_order_state_is_new()
    {
      var order = new Order();
      
      order.CurrentState.Should().Be(nameof(New));
    }
    
    public class Order_in_state_new
    {
      private readonly Order _order;
      public Order_in_state_new()
      {
        _order = new Order();
      }

      [Fact]
      public void can_not_be_set_active_if_prerequisites_are_not_met()
      {
        var result = _order.Activate();

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Preconditions not met to change from 'New' to 'Active'.");
      }

      [Fact]
      public void can_be_set_active_if_prerequisites_are_met()
      {
        _order.ChangeCustomer("John Doe");
        var result = _order.Activate();

        result.IsSuccess.Should().BeTrue();
        _order.CurrentState.Should().Be(nameof(Active));
      }

      [Fact]
      public void can_not_be_completed()
      {
        var result = _order.Complete();

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("State can not be changed to 'Complete'.");
      }

      [Fact]
      public void can_be_canceled()
      {
        var result = _order.Cancel();

        result.IsSuccess.Should().BeTrue();
        _order.CurrentState.Should().Be(nameof(Canceled));
      }

      [Fact]
      public void can_not_update_items()
      {
        var result = _order.UpdateItems(new List<string>{ "Sample" });

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Items can not be updated in state 'New'.");
        _order.Items.Should().HaveCount(0);
      }
    }
  }
}