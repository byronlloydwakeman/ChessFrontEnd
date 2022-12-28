# ChessFrontEnd
This application is built completely in C# using WPF and Caliburn Micro as a framework. 
It also includes AutoFac which is a library which can create containers which can be used to implement Dependency Injection. 
The User Interface was built with xaml in a WPF project.
The application reads the moves the user inputs and then sends them off to the back-end application where it then gets processed.
Seperating the front-end and the back-end allows a seperation of concerns, this not only makes it easier to update and change the application in terms of maintaining code
but also as technologies develop we may want a different user interface (Website or .NetCore), As a result of the way its been built we can simply just create a new user
interface which calls the back-end. Instead of creating a compeletely new application altogether, we can just take out bits we need to change and add new bits on.
