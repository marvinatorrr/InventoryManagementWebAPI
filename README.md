# InventoryManagementWebAPI

* This is the webapi of the the inventory management system
* It processes requests from whoever and sends back an appropriate response

# SQL Server
* The application connects to an SQL server for data persistance

* To access methods in the employee and admin controllers, a basic authentication header must be attached to the requests, else an unauthorised response is sent back.

# Requirements
* Requirements of the inventory management system
* An item should have the following properties:
  - ID
  - Name
  - Manufacturer
  - Manufacture date
  - Expiry date
  - Base price
  - MSRP
* Two entities can access this system: Employee and Owner.
* For employee: 
  - He can search for an item via ID or name.
  - He can sell an item.
  - A bill is generated where the base price, MSRP, sale price, tax, and profit are logged.
  - The sale price cannot be more than MSRP and less than the base price.
  - He can receive an item.
  - Increment the relevant stock in the inventory.
* For owner:
  - He can view all items in the inventory.
  - He can view the activities that an employee has performed.
  - Selling and receiving information.
  - He can view the sell and receive information aggregated into a daily format.
* The application can be accessed through the owner or employee role by logging in with the relevant credentials. 

# Class Diagram
![InventoryClassDiagram drawio (2)](https://user-images.githubusercontent.com/23392356/138315437-2ba18c54-502c-499a-b013-84d89fa56935.png)

# Use Case Diagram
![UseCaseDiagram](https://user-images.githubusercontent.com/23392356/138314083-212a15f0-7bd4-4657-ad7e-7b2672c342d6.png)
