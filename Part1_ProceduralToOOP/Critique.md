# Task 1.1 - Procedural Order System Critique

Source reviewed: [order_system.cpp](https://github.com/SimulationEG/cpp-procedural-order/blob/main/order_system.cpp).

1. **Shared global state.** Functions directly change arrays such as `productStock` and counters such as `orderCount`. Any function can accidentally change another part of the system. Running two independent order systems would also require separate copies of every global variable.

2. **Parallel arrays represent one object.** A customer's ID, name, email, city and VIP flag live in separate arrays. They are related only because they use the same index. If an update uses the wrong index, one customer's name can be paired with another customer's email.

3. **Relationships depend on array positions.** `orderCustomerIndexes` and `lineProductIndexes` store indexes rather than references to customers and products. Sorting or removing an array entry could make an existing order refer to the wrong customer or product.

4. **Fixed capacities and manual counters.** Customers, products and orders have hard limits of 50, 50 and 100. Each operation must also keep its counter and array entries synchronized. A new feature must remember these limits and maintain the same bookkeeping.

5. **Data and its rules are separated.** `addLineToOrder`, `markOrderPaid` and `calculateOrderTotal` all work on the same order arrays. Nothing stops another function from writing to those arrays and bypassing the stock, payment or quantity checks. An `Order` should protect its own lines and paid status, while a `Product` should protect its stock.

6. **Validation is incomplete.** `addProduct` accepts a negative price or stock, and `createOrder` stores a date as unchecked text. The menu also does not recover from a failed numeric input. These inputs can leave invalid data or stop the menu from working normally.

7. **Business logic and console output are mixed.** Functions print errors and return instead of clearly reporting failure to the caller. Reusing the same operations in another interface or testing them without console output becomes harder. Monetary values also use `double`; C# `decimal` is a better fit for decimal prices.

The C# version uses `Customer`, `Product`, `Order` and `OrderLine` objects. One `OrderSystem` instance owns the collections and ID lookups. The domain objects validate their actions, and the console menu catches errors and displays them.
