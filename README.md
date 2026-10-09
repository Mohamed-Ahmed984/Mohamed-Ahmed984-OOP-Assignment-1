# OOP Assignment 1

- **Name:** Mohamed Ahmed
- **ID (GitHub / submission username):** Mohamed-Ahmed984
- **Track:** Backend .NET - Simulation Academy
- **Branch:** `assignment/1-5`
- **Pull request title:** `[S1-A5] OOP Assignment 1`
- **LeetCode profile:** https://leetcode.com/u/7V5DqS2LTd/

This assignment practices turning real requirements into C# objects: deciding who owns the data, protecting that data with validation, and building complex objects clearly.

**[Open the completed assignment branch](https://github.com/Mohamed-Ahmed984/Mohamed-Ahmed984-OOP-Assignment-1/tree/assignment/1-5)** to browse the source code and submission evidence.

## Assignment Parts

| Part | Files | What it demonstrates |
| --- | --- | --- |
| 1 | [Critique.md](https://github.com/Mohamed-Ahmed984/Mohamed-Ahmed984-OOP-Assignment-1/blob/assignment/1-5/Part1_ProceduralToOOP/Critique.md), [src](https://github.com/Mohamed-Ahmed984/Mohamed-Ahmed984-OOP-Assignment-1/tree/assignment/1-5/Part1_ProceduralToOOP/src) | C++ design critique and a C# order system with customers, products, orders, stock, payment and sales totals. |
| 2 | [src](https://github.com/Mohamed-Ahmed984/Mohamed-Ahmed984-OOP-Assignment-1/tree/assignment/1-5/Part2_HotelReservationSystem/src) | Encapsulated guests, rooms and reservations, validated status transitions, maintenance checks and overlap prevention. |
| 3 | [Answers.md](https://github.com/Mohamed-Ahmed984/Mohamed-Ahmed984-OOP-Assignment-1/blob/assignment/1-5/Part3_BuilderPattern/Answers.md), [src](https://github.com/Mohamed-Ahmed984/Mohamed-Ahmed984-OOP-Assignment-1/tree/assignment/1-5/Part3_BuilderPattern/src) | A fluent invoice builder and composed address/order builders with independent validation. |
| 4 | [Solution.cs](https://github.com/Mohamed-Ahmed984/Mohamed-Ahmed984-OOP-Assignment-1/blob/assignment/1-5/Part4_LeetCode/1679_MaxNumberOfKSumPairs/Solution.cs), [Accepted screenshot](https://github.com/Mohamed-Ahmed984/Mohamed-Ahmed984-OOP-Assignment-1/blob/assignment/1-5/Part4_LeetCode/1679_MaxNumberOfKSumPairs/accepted_screenshot.png) | LeetCode 1679 using sorting and two pointers, with the actual supplied Accepted submission image. |

Each console project is separate and targets **.NET 9**. There are no external NuGet dependencies.

## What Each Part Teaches

### Part 1 - From Procedures to Objects

The original C++ program stores customer, product and order data in global parallel arrays. The C# version groups related data and behavior into real objects:

| Class | Responsibility |
| --- | --- |
| `Customer` | Customer identity, contact information and VIP status. |
| `Product` | Product price and stock; validates stock changes. |
| `OrderLine` | One product and quantity, with a calculated line total. |
| `Order` | Its customer, date, lines, payment status and discounted total. |
| `OrderSystem` | Owns the collections, finds objects by ID and calculates paid sales. |

For example, adding a line goes through `order.AddLine(product, quantity)`. That action checks the order's paid status and available stock before changing anything. The menu catches rejected actions and displays the reason.

### Part 2 - Protecting Hotel Data

`Guest`, `Room` and `Reservation` model a separate domain. Guest details and reservation dates are set once. Room price, maintenance and reservation status change through dedicated methods.

The lifecycle is `Pending -> Confirmed -> CheckedIn -> CheckedOut`. A Pending or Confirmed reservation may also be cancelled. Calling `CheckIn()` too early or cancelling after check-in throws an exception.

Each room checks its existing active reservations before accepting a new booking. This protects against overlapping stays even when two different guests book the same room. Guests expose a read-only history, so callers can inspect it safely.

### Part 3 - Building Invoices Clearly

The flat invoice contains about 20 properties. The first fluent builder replaces a long constructor call with named steps: `.Customer(...)`, `.Billing(...)`, `.Shipping(...)`, `.Order(...)` and `.Build()`.

The composed version creates billing and shipping using the same `AddressBuilder`, creates payment/amount information using `OrderBuilder`, and passes the results into the final invoice builder. Each smaller object validates its own data. Both examples produce a total of **1040** from a subtotal of 1000, discount of 100 and tax of 140.

### Part 4 - Sorting and Two Pointers

The LeetCode solution sorts the array and starts a pointer at each end. If their sum equals `k`, it counts a pair and moves both pointers. A smaller sum moves the left pointer; a larger sum moves the right pointer. Each matched number is used once.

Sorting takes `O(n log n)` time, and the pointer scan takes `O(n)`. The supplied Accepted screenshot is included as an actual PNG file.

## Run

Install a .NET 9 SDK, then download the assignment branch:

```bash
git clone --branch assignment/1-5 https://github.com/Mohamed-Ahmed984/Mohamed-Ahmed984-OOP-Assignment-1.git
cd Mohamed-Ahmed984-OOP-Assignment-1
```

Run these commands from the repository root, one project at a time:

```bash
dotnet run --project Part1_ProceduralToOOP/src/Part1_ProceduralToOOP.csproj
dotnet run --project Part2_HotelReservationSystem/src/Part2_HotelReservationSystem.csproj
dotnet run --project Part3_BuilderPattern/src/Part3_BuilderPattern.csproj
```

Alternatively, open an individual `.csproj` in Visual Studio.

Part 4 is a separate LeetCode solution, so it is submitted to LeetCode rather than run as a console project.

## Part 1 - Console Menu

The application seeds the same three customers, four products and three demo orders as the original [C++ program](https://github.com/SimulationEG/cpp-procedural-order). It then opens the menu:

| Option | Action |
| ---: | --- |
| 1 | Print customers |
| 2 | Print products |
| 3 | Print all orders |
| 4 | Print one order by ID |
| 5 | Create an order |
| 6 | Add a product line to an order |
| 7 | Mark an order paid |
| 8 | Show paid sales total |
| 9 | Add a customer |
| 10 | Add a product |
| 0 | Exit |

Enter dates as `yyyy-MM-dd`, for example `2026-10-09`. Enter prices using a decimal point and answer the VIP prompt with `yes` or `no`.

`OrderSystem` owns its own collections. There are no static data fields or shared global collections in Part 1, so independent instances do not share customers, products or orders. The domain classes own their corresponding behavior and validation.

## Notes and Assumptions

- All data is held in memory and starts fresh when a program is restarted.
- Part 1 uses caller-supplied unique positive IDs, dynamically sized lists and the original limit of 20 lines per order. VIP customers receive a 10% discount. Adding a line reduces stock immediately, and paid orders cannot be changed. Order lines retain their unit price.
- Part 2 represents one hotel per process. Guest IDs, reservation IDs and physical room numbers are unique during that run. Failed construction does not reserve an ID or add a history entry.
- Reservation dates represent calendar nights. Overlap checks use check-in inclusive and check-out exclusive, so one stay may start on another stay's checkout date. Cancelled and checked-out reservations remain in history and do not block another booking.
- Reservation totals use the room's current nightly rate, as a computed property. Maintenance blocks creation of new reservations.
- For Part 3, customer phone is optional. Discount and tax default to zero. Required values and amount rules are described in [Answers.md](https://github.com/Mohamed-Ahmed984/Mohamed-Ahmed984-OOP-Assignment-1/blob/assignment/1-5/Part3_BuilderPattern/Answers.md).
- Written answers are stored in Markdown files. Build outputs are ignored and are not part of the submission.

## Verification

Verified on 9 October 2026 with .NET SDK **9.0.100**:

- All three console projects built in Release with **0 errors and 0 warnings**, and all three ran successfully.
- The original C++ program was compiled and its menu exercised. The C# demo produces the same order totals: **315.00**, **1600.00** and **250.00**, with paid sales of **565.00** and matching product stock.
- Every C# menu option, invalid IDs/numbers/dates, paid-order edits, insufficient stock and end-of-input handling were exercised.
- A temporary verification program passed **197 behavior checks** covering independent order-system state, stock/payment rules, hotel encapsulation, identity/date validation, every legal reservation transition, rejected transitions, maintenance, cross-guest overlaps, adjacent stays, builder validation and LeetCode cases.
- The LeetCode solution was compared with an independent exhaustive pairing search on 100 deterministic small inputs, and checked at the maximum array length. The supplied screenshot shows **Accepted, 51/51 tests passed** and is preserved byte-for-byte.

![LeetCode Accepted submission](https://github.com/Mohamed-Ahmed984/Mohamed-Ahmed984-OOP-Assignment-1/blob/assignment/1-5/Part4_LeetCode/1679_MaxNumberOfKSumPairs/accepted_screenshot.png?raw=true)
