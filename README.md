<p align="center">
    <img src="./Images/logo.png"/>
</p>
<p>&nbsp;</p>
<p>&nbsp;</p>

# Code Smells and Refactoring

<img src="./Images/Couplers/feature-envy.png" />
<p>&nbsp;</p>

## About

Feature Envy is a common code smell in object-oriented programming. It occurs when a method in one class excessively uses methods or data from another class. 

### Refactorings to address Feature Envy:
- `Move Method`
- `Extract Method`
- `Use Delegation`

## Warning Signs

Feature Envy is detrimental in object-oriented designs because it violates encapsulation, leading to tightly coupled code that is hard to maintain and understand.

Look out for the following:

- A method seems more interested in a class other than the one it actually resides in.
- Excessive use of accessor methods of another object.

**Example in C#:**

```csharp
class Customer {
    public int DiscountRate { get; set; }
    public int TotalOrders { get; set; }
}

class Order {
    private Customer customer;

    public decimal CalculateDiscount() {
        return this.customer.DiscountRate * this.customer.TotalOrders;
    }
}
```

## Causes

Feature Envy typically arises due to:

- Poor class design, where responsibilities are not well-distributed.
- Failure to properly encapsulate data and behavior.
- Misunderstanding of object-oriented principles.

## Deodorising
To eliminate Feature Envy:

- Identify the envy, recognise methods that are overly reliant on another class.
- Apply `Move Method` and move the envious method to the class it is most interested in.

Step-by-Step C# Example:

```csharp
class Customer {
    public decimal DiscountRate { get; set; }
    public decimal TotalOrders { get; set; }
}

class Order {
    private Customer customer;

    public decimal CalculateDiscount() {
        return this.customer.DiscountRate * this.customer.TotalOrders;
    }
}
```

**Apply `Move Method`**:
```csharp
// The object that has the data now does the work
class Customer {
    public int DiscountRate { get; set; }
    public int TotalOrders { get; set; }

    public decimal CalculateDiscount() {
        return this.DiscountRate * this.TotalOrders;
    }
}
```

**Public properties can be made private**:
```csharp
// Client code now communicates via Customer's public API and has no knowledge of its internal representation
class Customer {
    private int _discountRate;
    private int _totalOrders;

    public decimal CalculateDiscount() {
        return _discountRate * _totalOrders;
    }
}
```
<p>&nbsp;</p>

<img src="./Images/Couplers/message-chains.png" />
<p>&nbsp;</p>

<img src="./Images/Couplers/middle-man.png" />
<p>&nbsp;</p>

<img src="./Images/Couplers/inappropriate-intimacy.png" />
<p>&nbsp;</p>