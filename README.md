









BookBarn Online Bookstore Project

SOEN 4371 | E-Commerce

May 1, 2025
 

1. Introduction & Requirements
Objective: Build an ASP.NET Web Forms e commerce application backed by SQL Server, demonstrating server controls, validation controls, database integration, SQL injection prevention, master/content pages, navigation techniques, and menu usage.
Functional Requirements:
•	Browse and search books
•	View book details
•	Add books to a shopping cart
•	Checkout with user information form
•	Order confirmation and database persistence
Non functional Requirements:
•	Secure database access (parameterized queries)
•	Responsive master page layout
•	Clear navigation via menus and breadcrumbs
________________________________________
2. Architecture
Layers:
1.	Presentation Layer: ASP.NET Web Forms (.aspx + code behind), MasterPage for layout
2.	Business Layer: Simple helper classes (CartItem, CustomerInfo)
3.	Data Access Layer: ADO.NET (SqlConnection, SqlCommand, SqlDataAdapter)
4.	Database: SQL Server .mdf in App_Data
Diagram:
[Browser] → [ASP.NET Pages] → [Helpers / Session] → [SQL Server Database]
________________________________________


 
3. Database Schema
Tables:
•	Categories(CategoryID PK, Name)
•	Books(BookID PK, Title, Price, AuthorName, CategoryID FK)
•	Customers(CustomerID PK, Name, Email, Phone, Address)
•	Orders(OrderID PK, CustomerID FK, OrderDate, TotalAmount)
•	OrderItems(OrderItemID PK, OrderID FK, BookID FK, Quantity, Subtotal)
________________________________________
4. Key Pages & Server Controls
Page	Server Controls	Description
WebForm1.aspx	GridView, TextBox, Button, LinkButton	Book browsing, search, paging, add to cart
WebForm2.aspx	DetailsView, Button	Display book details, add to cart
WebForm3.aspx	GridView, CustomValidator, Button	Shopping cart (edit, delete, validate)
WebForm4.aspx	TextBox, RequiredFieldValidator, RegularExpressionValidator	Checkout form
WebForm5.aspx	Label, GridView	Order confirmation, summary
________________________________________


5. Validation & Security
•	Validation Controls: Ensured required fields (RequiredFieldValidator), email format (RegularExpressionValidator), cart non empty (CustomValidator).
•	SQL Injection Prevention: All database access via parameterized SqlCommand objects; no string concatenation.
 ________________________________________
 
6. Navigation & Menus
•	MasterPage: Shared header, <asp:Menu> bound to Web.sitemap
•	Breadcrumbs: <asp:SiteMapPath> on content pages
•	Data transfer: QueryString (BookID), Session (Cart, CustomerInfo), Cross-Page PostBack (optional)
________________________________________
7. Conclusion 
Conclusion: BookBarn successfully demonstrates a full-featured ADO.NET and ASP.NET Web Forms e-commerce application, integrating:
•	A consistent, responsive MasterPage layout with menus and breadcrumbs
•	Rich user interactions via GridView, DetailsView, and validation controls
•	Secure data access through parameterized ADO.NET commands preventing SQL injection
•	Dynamic shopping cart and checkout flows powered by session state and cross-page navigation
•	Robust order processing with relational database persistence for customers, orders, and items
________________________________________

