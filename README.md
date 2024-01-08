# PoS-API

## Changes made to the specification

### User Entity

- Removed `password` field, as it is not the best practice to store plain text password. In the future, we will use OAuth 2.0
- Added more informative fields

---

### ItemOrder Entity

- Created pivot table with additional field to track of how many `items` are used in particular `order`.

### LoyaltyProgram Entity

- Added userId to LoyaltyProgram as the loyalty program is assigned to the user upon loyalty program's creation.

### Tax Entity

- Added a tax entity which contains taxId, desciption and percentage of the tax.

### TaxOrder Entity

- Created a pivot table which tracks different taxes applied to the order.

### Tip Entity

- Added a tip entity which contains id of the tip, orderId and tip amount.
