# Todo

## Backend 
- [ ] Implement Identity, authentication & authorisation 
- [ ] Implement Mediator Design Pattern for Blood Donation Requests 
- [ ] Add Email Service to the contact-us page
- [ ] Hospital 
	- [ ] Entity - Hospital Class with attributes and relations to other entities -> wil be added to the database using Entity Framework 
	- [ ] Repository - Contains the code & functions that interact with the database
	- [ ] Controller - Contains Actions or functions which interact with the Views (the frontend) and the repositories (the backend)
	- [ ] Hospital Views
		- [ ] SignInViewModel - used to Map between the input coming from the View and the Application User Entity to create a new user
		- [ ] HospitalSignIn Action - AccountController
		- [ ] HospitalsView - AdminController
		- [ ] Details View - HospitalController
		- [ ] Update view - HospitalController
- [ ] Notification
	- [ ] Entity - Notification Class with attributes and relations to other entities -> wil be added to the database using Entity Framework
	- [ ] Repository - Contains the code & functions that interact with the database
	- [ ] Controller - Contains Actions or functions which interact with the Views (the frontend) and the repositories (the backend)
	- [ ] Notification Views
		- [ ] NotificationViewModel - used to map between the input from the View and the Notification Entity 
		- [ ] AccountNotificationsView - AccountController
		- [ ] Notifications View - NotificationController
		- [ ] Details View - NotificationController

- [ ] Announcement
	- [ ] Entity - Announcement Class with attributes and relations to other entities -> wil be added to the database using Entity Framework 
	- [ ] Repository - Contains the code & functions that interact with the database
	- [ ] Controller - Contains Actions or functions which interact with the Views (the frontend) and the repositories (the backend)
	- [ ] Announcement Views
		- [ ] AnnouncementViewModel - used to map between the input from the view and the Announcement Entity
		- [ ] AnnouncementsView - HomepageController
		- [ ] Details View - AnnouncementController
		- [ ] Update View - AnnouncementController 
---
## Frontend 
- [ ] Modify style.css File
- [ ] Modify Layout Files
	- [ ] _Layout.cshtml
	- [ ] admin_Layout.cshtml
- [ ] Modify Views Files
- [ ] Homepage Views
	- [ ] Blood Donation Eligibility Information Page - HomepageController
---
## Extra 
- [ ] Hosting & Deployment 
- [ ] Video & Presentation 


## Resources 
1. View Models: https://www.scholarhat.com/tutorial/mvc/understanding-viewmodel-in-aspnet-mvc
2. Entities: https://www.c-sharpcorner.com/article/entity-framework-with-net-mvc-1-code-first/
3. Repositories: https://www.c-sharpcorner.com/UploadFile/3d39b4/crud-using-the-repository-pattern-in-mvc/
4. Controllers: https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/controller-methods-views?view=aspnetcore-8.0
5. Views: https://www.tutorialsteacher.com/mvc/mvc-view
6. Layouts: https://learn.microsoft.com/en-us/aspnet/core/mvc/views/layout?view=aspnetcore-8.0
7. MVC Video Tutorial: https://www.freecodecamp.org/news/learn-asp-net-core-mvc-net-6/