# Task 3 - Written Answers

## Task 3.1 - Question 1

A constructor with about 20 parameters is difficult to read at the call site. Someone reading a long list of strings and decimal values cannot easily tell which value is the billing city, shipping city, discount or tax.

Values with the same type can also be swapped accidentally. The compiler still accepts two strings or two decimals in the wrong order, even though the invoice now contains incorrect data.

Adding one more optional property makes the constructor and its calls harder to maintain. It may require another overload, default arguments or changes to existing calls. Fluent builder methods give the values meaningful names and let optional values be omitted clearly.

## Task 3.1 - Question 2

The deeper problem is low cohesion: customer identity, billing address, shipping address and payment details are all represented by one large flat class. The class has several reasons to change, and its validation becomes one long block of unrelated rules.

A builder improves construction, but splitting related data into smaller objects improves the design itself. An address should own its address fields, and order information should own its payment and amount fields.

## Task 3.2 - Mandatory and Optional Values

The invoice requires a positive ID, customer name and email, complete billing and shipping addresses, an order date, payment method and currency. Each address requires street, city, state, ZIP code and country. The order-building method requires a subtotal; a zero subtotal is allowed.

Phone number is optional and defaults to an empty string. Discount and tax are optional and default to zero. Subtotal, discount and tax must not be negative, and discount must not exceed subtotal. Total is calculated as subtotal minus discount plus tax.

`Build()` validates the required information and throws an exception if it is missing or invalid.

## Task 3.3 - Why Composed Builders Are Better

- **Single responsibility:** `AddressBuilder` constructs and validates an address. `OrderBuilder` handles the date, payment, currency and amounts. The final `InvoiceBuilder` connects these objects with customer information.
- **Independent validation:** an address can be checked on its own, before it is attached to an invoice. Order amounts can be checked independently too. The parent builder asks the smaller objects to validate themselves, rather than copying their field rules.
- **Reuse:** the same `AddressBuilder` creates both billing and shipping addresses. The flat builder needs separate billing and shipping assignments and checks for the same five fields.
- **Readability:** the flat version puts customer, both addresses and payment details in one chain. The composed version gives billing, shipping and order information their own named variables, then combines them with `.Billing(billing).Shipping(shipping).Order(order)`.

The program demonstrates both versions and calculates each total from its values.
