1) Main entry point for this application displays the list of all books.

2) The application has been developed by addressing the clean separation of concerns. The Business Logic used in the application can be easily re-used in any application. 

3) Application Architecture/Patterns used –

         Model View Architecture
         Repository Pattern
         Service Layer to decouple the Business Logic from the Repositories
         Singleton Pattern to maintain the list of Books, Borrowers and other operations in the application.
         
4) The entire application has been divided into different layers. Below is the high-level overview of each layer.

         Views - Get inputs from user and display the error messages.
         Core
             Repositories – Directly connects to the underlying collections and has implemented all the Business logic inside this layer.
             Interfaces – Interfaces have been created for all repositories to mock the functionalities during Unit testing scenarios.
             Errors – Centralised place to look-up for Error Messages across the application.
             ServiceLayer – Entire Business Logic for this application has been maintained in this folder.
             Models – Underlying Business entities for the application.
             ViewModels – Combination of multiple models has been defined as ViewModels.
        Infrastructure - This layer initializes data into all the collections and persist the state across various operations in the application.
        Controllers - Presentation logic has been defined inside this layer.
        Unit Testing - Important test scenarios have been covered under the Unit Testing scenario.

5) Technology Stack used – 

        Target Framework - 4.5
        Moq
        JQuery
        MVC 5.0
        BootStrap 3.0
