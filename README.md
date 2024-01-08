# PoS-API

## Changes made to the specification

### User Entity

- Removed `password` field, as it is not the best practice to store plain text password. In the future, we will use OAuth 2.0
- Added more informative fields

---

### ItemOrder Entity

- Created pivot table with additional field to track of how many `items` are used in particular `order`.

---

### LoyaltyProgram Entity

- Added userId to LoyaltyProgram as the loyalty program is assigned to the user upon loyalty program's creation.

---

### Tax Entity

- Added a tax entity which contains taxId, desciption and percentage of the tax.

---

### TaxOrder Entity

- Created a pivot table which tracks different taxes applied to the order.

---

### Tip Entity

- Added a tip entity which contains id of the tip, orderId and tip amount.

---

### Payment entity 

- Removed the option 'other' as a payment method as it is not clearly defined in the API and not mentioned in the document.

---

### Reservation Entity

- Replaced `isCancelled` with `status` field to provide more detail and allow for scalability.
- `dateTime` is split into two fields: `date` and `timeSlot`. `date` represents the reservation date. The `timeSlot` value represents the reserved time slot. Each business decides for itself how many slots it needs and how to number them. For this example, the schedule consists of slots numbered 9 through 20 (12 slots in total), with each slot representing a 1-hour reservation between 9:00 and 21:00.
