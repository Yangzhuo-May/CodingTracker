# Coding Tracker
This is a very simple app that will teach you how to perform CRUD operations against a real database. 

---------
# Features
- SQLite database connection.
    - The program uses a SQLite db connection to store and read information.
    - If no database exists, or the correct table does not exist they will be created on program start.
- A console based UI where users can navigate by key presses, used the "Spectre.Console" library to makes it prettier.
    - ![Screenshot 2025-02-02 120934](https://github.com/user-attachments/assets/f1d94639-6245-4f49-8b75-8b3b1247a286)
    - ![Screenshot 2025-02-02 121437](https://github.com/user-attachments/assets/359433db-c1ee-46d6-b7a0-d301364cc972)

- CRUD DB functions
    - From the main menu users can Create, Read, Update or Delete records for whichever date they want, entered in dd-MM-yy format. 
    - Input validation is implemented: a warning message will appear if an incorrect date or unavailable menu option is entered, also when user entered a Startime smaller than Endtime.
      
- Implemented Separation of concerns.
    - The application is designed with a clear separation of concerns, where different classes are responsible for distinct functionalities. Below is an overview of the key classes and their roles:
        - Program.cs
           ->The entre point of the application.
        - CodingSession.cs
           -> Contains the properties of a coding session.
        - CodingController.cs
           -> Handles the logic for CRUD (Create, Read, Update, Delete) operations.
        - ConfigHelper.cs
           -> Assists other classes in retrieving the connection string from the configuration.
        - DatabaseManager.cs
           -> Creates the database and required tables if they do not already exist.
        - UserInput.cs
           -> Manages the logic for the main menu.
        - Validation.cs
           -> Includs all the validation functions, such as GetDateInput(), to ensure all user inputs are in the correct format.
        - TableVisualisationEngine.cs
           -> Generates and displays tables using the Spectre.Console library.
 --------------
 # Challenges
- **Separating functions into different files**: I initially struggled with understanding how to separate functions into different files and make them work together. Later after I started to build it, I could see how it works.
- **Debugging syntax errors**: Debugging syntax errors that were not flagged by VS Code was frustrating. However, I eventually noticed similar errors and learned how to confirm they wouldn't happen again.
- **DateTime conversion**: I faced challenges with DateTime conversion. Ultimately, I decided to use strings to store time and date, which allowed me to extract numerical values using indexes.
- **Handling unexpected bugs**: At first, I found it overwhelming to deal with so many bugs. Some bugs occurred without any clear explanation, and VS Code didn't highlight them with red warnings. I had to figure them out on my own. Fortunately, I often found others who had encountered the same issues, and their solutions were helpful.
 ---------------
 # Lessons Learned
- **Application structure**: The application starts with a main point, such as Program.cs.
- **Configuration and database**: We use appsetting.json to store the path and connectString of database, some other classes to hlep others get connectionString. The controller is responsible for controlling the way that a user interacts with our application, in here they are CURD operations. 
- **Encapsulation**: Encapsulate different functional codes in different classses, and the call the method when you need.
- **Data type consistency**: Makes sure the type of the variable is correct, and it coresspond the type in the database.
- **String interpolation**: Be careful with sting interpolation, it won't mentioned you when there are syntax errors.
- **Problem-solving approach**: Write some pseudocodes when you don't know how to deal with the problem, and break the big problem into small pecies, and do it one by one.
- **Debugging mindset**: As long as there a bug, it means there are a reason, so keep looking the reason, thinking the possiblity of the reason. By paintetion.
 ---------------
 # Areas to Improve
- [ ] Add the possibility of tracking the coding time via a stopwatch so the user can track the session as it happens.
- [ ] Let the users filter their coding records per period (weeks, days, years) and/or order ascending or descending.
- [ ] Create reports where the users can see their total and average coding session per period.
- [ ] Create the ability to set coding goals and show how far the users are from reaching their goal, along with how many hours a day they would have to code to reach their goal. You can do it via SQL queries or with C#.
