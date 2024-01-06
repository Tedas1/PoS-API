# PoS-API

## Changes made to the specification

### User Entity

- Removed `password`` field, as it is not the best practice to store plain text password. In the future, we will use OAuth 2.0
- Added more informative fields

---

### ItemOrder Entity

- Created pivot table with additional field to track of how many `items` are used in particular `order`.
