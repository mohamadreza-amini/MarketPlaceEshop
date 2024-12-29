# 📦 Marketplace Project

A complete e-commerce platform project with admin management, supplier sales, and customer purchases. This project includes the following features:

## 🚀 Main Features of the Project
- **Multi-vendor System:** Each product can have multiple vendors with different prices and discounts.
- **Report Management:** Includes reports related to sales and product views.
- **Multi-level Access:**
  - `/supplier` for suppliers.
  - `/admin` is hidden and can only be accessed through a direct link.
  - `Customer` is the main store for users.

- **Technologies Used:**
   - **ASP.NET Core 8** for building the application.
  - **SQL Server** for database management.
  - **Entity Framework Core** for database management and migrations.
  - **Identity** for user management and authentication.
  - **Hangfire** for scheduled task management.
  - **In-Memory Cache** for performance improvement and reducing database requests.
  - **Serilog** for logging.
  - **Seq** for log viewing.



## 📸 Sample Images for Quick Overview

To help you get a quicker understanding of the project, sample images of different sections are provided below, followed by further explanations of the project.


<div style="display: flex; justify-content: space-between;">
       <img src="./MarketPlaceEshop/wwwroot/upload/readme/Screenshot (57).png" alt="Image 3" style="width: 49%;">
       <img src="./MarketPlaceEshop/wwwroot/upload/readme/Screenshot (70).png" alt="Image 3" style="width: 49%;">  
  </div>

 <div style="display: flex; justify-content: space-between;">
     <img src="./MarketPlaceEshop/wwwroot/upload/readme/Screenshot (48).png" alt="Image 3" style="width: 49%;">
     <img src="./MarketPlaceEshop/wwwroot/upload/readme/Screenshot (60).png" alt="Image 3" style="width: 49%;">  
  </div>

<div style="display: flex; justify-content: space-between;">
     <img src="./MarketPlaceEshop/wwwroot/upload/readme/Screenshot (79).png" alt="Image 3" style="width: 49%;">
     <img src="./MarketPlaceEshop/wwwroot/upload/readme/Screenshot (77).png" alt="Image 3" style="width: 49%;">  
 </div>

---

<div align="center">

  <img src="./MarketPlaceEshop/wwwroot/upload/readme/Screenshot (31).png" alt="Image 3" style="width: 75%;">
  <img src="./MarketPlaceEshop/wwwroot/upload/readme/Screenshot (36).png" alt="Image 3" style="width: 75%;">
  <img src="./MarketPlaceEshop/wwwroot/upload/readme/Screenshot (45).png" alt="Image 3" style="width: 75%;">
  <img src="./MarketPlaceEshop/wwwroot/upload/readme/Screenshot (68).png" alt="Image 3" style="width: 75%;">
  <img src="./MarketPlaceEshop/wwwroot/upload/readme/Screenshot (38).png" alt="Image 3" style="width: 75%;">

</div>


---
## 🛒 Features for Different Roles
- **For Sellers:**
  - Add products.
  - Modify product stock and prices.
  - Manage orders and shipments.
  - View inventory and sales reports.
  - Add product to a vendor list after admin approval.
  - Price adjustments, stock quantity, and discounts for products.
  - View orders once approved by admin, mark them as sent once dispatched.
  
- **For Buyers:**
  - Shopping cart.
  - Product reviews and ratings.
  - Order history and tracking.
  - View order status and stage.

- **For Products:**
  - Product specifications.
  - Product reviews and ratings.
  - Product features.
  - Categorization and brand details.
  - **Category Management:** Ability to create new categories with hierarchical parent-child features and attributes.

- **For Admin:**
  - Manage sellers and products.
  - Manage product reviews (approve or reject).
  - View sales and inventory reports.
  - Monitor daily view statistics.
  - Approve new suppliers before they can start selling.
  - Generate sales reports for specified time frames, daily sales charts, and daily site visit charts.

---

## 📊 Additional Features
- **Comments & Ratings:**  
  Sellers and customers can leave comments and rate products. Admins must approve comments before they appear publicly.
  
- **Supplier Approval:**  
  Suppliers can add products and start selling only after being approved by the admin.
  
- **Order Management:**  
  Suppliers can view orders after approval by the admin and mark them as sent once dispatched.

- **Order Tracking for Customers:**  
  Customers can view the history and status of their orders at any time.

- **Category Hierarchy:**  
  Admins can create categories in a hierarchical structure, with features and attributes that can be applied to parent and child categories.

- **Sales and Report Management:**  
  Admins and suppliers can generate sales reports for specific periods, and view daily sales and site visit charts.



---

## 🖥 Access Areas
- **Admin Area:**  
  This area is hidden and can only be accessed by admins through a direct link.
- **Supplier Area:**  
  Visible to suppliers through the top tab.
- **Customer Area:**  
  The regular store for users.

---

## 📊 Additional Features
- **Hangfire:**  
  For scheduling tasks like processing reports.
- **Serilog & Seq:**  
  For advanced log viewing and error tracking.
- **Caching:**  
  Use In-Memory Cache to improve speed and reduce load on the database.

---



## 🛠 Setup Guide

### Prerequisites
1. **SQL Server**
   - Create two databases:
     - The main database for the project.
     - A separate database for **Hangfire**.
2. **.NET 8 SDK** or higher.

---

### Setup Steps
1. **Database Connection Settings:**
   Open the `appsettings.json` file and add the connection strings for the databases in the following sections:
   ```json
   "ConnectionStrings": {
     "AppDbContextConnection": "Your_Main_Database_Connection_String",
     "HangfireConnection": "Your_Hangfire_Database_Connection_String"
   }

2. **Run Migrations:**
   In the terminal, run the following command:
   ```bash
   dotnet ef database update
   ```
   This command will update the database using the project's migrations.

3. **Run SQL File (Optional):**
   A separate SQL file for sample data (Seed Data) is provided in the project.  
   You can run this file in **SQL Server Management Studio** or a similar tool.

4. **Run the Project:**
   Execute the project.
