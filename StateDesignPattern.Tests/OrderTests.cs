using System;
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
        _order.CurrentState.Should().Be(nameof(New));
        _order.Items.Should().HaveCount(0);
      }

      [Fact]
      public void can_change_customer_if_value_is_valid()
      {
        var newCustomer = "John Doe";
        var result = _order.ChangeCustomer(newCustomer);

        result.IsSuccess.Should().BeTrue();
        _order.CurrentState.Should().Be(nameof(New));
        _order.Customer.Should().Be(newCustomer);
      }

      [Fact]
      public void can_not_change_customer_if_value_is_invalid()
      {
        var newCustomer = string.Empty;
        var result = _order.ChangeCustomer(newCustomer);

        result.IsFailure.Should().BeTrue();
        _order.CurrentState.Should().Be(nameof(New));
      }

      [Fact]
      public void can_remove_customer()
      {
        var result = _order.RemoveCustomer();

        result.IsSuccess.Should().BeTrue();
        _order.CurrentState.Should().Be(nameof(New));
        _order.Customer.Should().BeEmpty();
      }

      [Fact]
      public void can_change_vehicle_if_value_is_valid()
      {
        var newVehicle = "Mercedes E-Class";
        var result = _order.ChangeVehicle(newVehicle);

        result.IsSuccess.Should().BeTrue();
        _order.CurrentState.Should().Be(nameof(New));
        _order.Vehicle.Should().Be(newVehicle);
      }

      [Fact]
      public void can_not_change_vehicle_if_value_is_invalid()
      {
        var newVehicle = string.Empty;
        var result = _order.ChangeVehicle(newVehicle);

        result.IsFailure.Should().BeTrue();
        _order.CurrentState.Should().Be(nameof(New));
      }

      [Fact]
      public void can_remove_vehicle()
      {
        var result = _order.RemoveVehicle();

        result.IsSuccess.Should().BeTrue();
        _order.CurrentState.Should().Be(nameof(New));
        _order.Vehicle.Should().BeEmpty();
      }
    }

    public class Order_in_state_active
    {
      private readonly Order _order;
      private const string InitialCustomer = "John Doe";

      public Order_in_state_active()
      {
        _order = new Order();
        _order.ChangeCustomer(InitialCustomer);
        _order.Activate();
      }

      [Fact]
      public void can_not_be_set_active()
      {
        var result = _order.Activate();

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("State is already 'Active'.");
      }

      [Fact]
      public void can_not_be_completed_if_prerequisites_are_not_met()
      {
        var result = _order.Complete();

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Preconditions not met to change from 'Active' to 'Completed'.");
      }

      [Fact]
      public void can_be_completed_if_prerequisites_are_met()
      {
        _order.UpdateItems(new List<string>{ "Sample" });
        var result = _order.Complete();

        result.IsSuccess.Should().BeTrue();
        _order.CurrentState.Should().Be(nameof(Completed));
      }

      [Fact]
      public void can_be_canceled()
      {
        var result = _order.Cancel();

        result.IsSuccess.Should().BeTrue();
        _order.CurrentState.Should().Be(nameof(Canceled));
      }

      [Fact]
      public void can_update_items()
      {
        var newItems = new List<string> { "Sample" };
        var result = _order.UpdateItems(newItems);

        result.IsSuccess.Should().BeTrue();
        _order.CurrentState.Should().Be(nameof(Active));
        _order.Items.Should().HaveCount(newItems.Count);
      }

      [Fact]
      public void can_change_customer_if_value_is_valid()
      {
        var newCustomer = "Johny Doe";
        var result = _order.ChangeCustomer(newCustomer);

        result.IsSuccess.Should().BeTrue();
        _order.CurrentState.Should().Be(nameof(Active));
        _order.Customer.Should().Be(newCustomer);
      }

      [Fact]
      public void can_not_change_customer_if_value_is_invalid()
      {
        var newCustomer = string.Empty;
        var result = _order.ChangeCustomer(newCustomer);

        result.IsFailure.Should().BeTrue();
        _order.CurrentState.Should().Be(nameof(Active));
      }

      [Fact]
      public void can_not_remove_customer()
      {
        var result = _order.RemoveCustomer();

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Customer can not be removed in state 'Active'.");
        _order.CurrentState.Should().Be(nameof(Active));
        _order.Customer.Should().Be(InitialCustomer);
      }

      [Fact]
      public void can_change_vehicle_if_value_is_valid()
      {
        var newVehicle = "Mercedes E-Class";
        var result = _order.ChangeVehicle(newVehicle);

        result.IsSuccess.Should().BeTrue();
        _order.CurrentState.Should().Be(nameof(Active));
        _order.Vehicle.Should().Be(newVehicle);
      }

      [Fact]
      public void can_not_change_vehicle_if_value_is_invalid()
      {
        var newVehicle = string.Empty;
        var result = _order.ChangeVehicle(newVehicle);

        result.IsFailure.Should().BeTrue();
        _order.CurrentState.Should().Be(nameof(Active));
      }

      [Fact]
      public void can_remove_vehicle()
      {
        var result = _order.RemoveVehicle();

        result.IsSuccess.Should().BeTrue();
        _order.CurrentState.Should().Be(nameof(Active));
        _order.Vehicle.Should().BeEmpty();
      }
    }
  }
}