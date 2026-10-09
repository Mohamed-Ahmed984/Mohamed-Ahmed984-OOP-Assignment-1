# Task 3 - Written Answers

## Task 3.1 - Question 1

- A constructor with 20 parameters is hard to read.
- Similar values can be mixed up, like billing city and shipping city, or discount and tax. The compiler may accept them because they have the same type.
- Adding an optional value may require changes to the constructor and its calls.

## Task 3.1 - Question 2

The class holds customer details, addresses and payment details. Each group has different rules and can change separately. Smaller classes make this easier to manage.

## Task 3.2 - Required and Optional Values

- **Required:** positive ID, customer name and email, both addresses, date, payment method, currency and subtotal. Subtotal can be zero.
- Each address needs street, city, state, ZIP code and country.
- **Optional:** phone is empty by default; discount and tax are zero by default.
- Amounts cannot be negative. Discount cannot exceed subtotal.
- `Build()` throws an exception for missing or invalid values.
- Total = subtotal - discount + tax.

## Task 3.3 - Why Smaller Builders Are Better

- **One job each:** `AddressBuilder` handles addresses, `OrderBuilder` handles order/payment details, and `InvoiceBuilder` joins them with customer details.
- **Separate checks:** each small builder checks its own data. The invoice builder does not repeat these rules.
- **Reuse:** the same `AddressBuilder` builds billing and shipping addresses, avoiding repeated code.
- **Clearer calls:** instead of one long chain, we build named addresses and order information first, then use `.Billing(billing).Shipping(shipping).Order(order)`.
