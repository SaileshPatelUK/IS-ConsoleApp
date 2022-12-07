# IS ConsoleApp

# Introduction 
IS Console App

# Assumptions Made
- .Net Core 6.0 - I have primarily I worked 3.1 for the past 3 years but have upgraded to 6.0 recently. FYI Upgrading to newer versions of .Net requires a newer version of Visual Studio.
- I have assumed that the table could include multiple items in the future, so I have tried to make the solution expandable as possible with this in mind. For a better look at how I've created the models to reflect this, please take a look at the Models created in DomainModels Project. 

![image](https://user-images.githubusercontent.com/42835367/204230370-d6cc472c-b632-4fac-af0d-d84890b5ac98.png)

# Areas for Improvement
 - Unit Testing - Could do with covering more use cases end to end.
 - Automation Testing 
 - Database - The solution should allow to expand and create a Repository and Database layer which would allow us to save and retrieve data from a database.  
 - Code Improvements - Add more comments explaining some parts of the code. 
 - Solution is still to be setup to allow for User Input. Commands can only be provided using the JSON file named commands.json, located in the ISConsoleApp Project. 
 
# Known Issues
 - Misspelling of ISConsoleApp. Should be renamed to IS.ConsoleApp.
 - For an unknown reason, I wasn't able to populate the following parameters from a launchSettings.json file. For now, I've hard coded the values in code as it was just a minor issue.

![image](https://user-images.githubusercontent.com/42835367/204231340-4624e5b7-9972-420e-b676-ec067a2f093c.png)


# Simple summary of solution

UnitTests - Inside this folder are Unit Tests for the Validator, Services and Domain Model methods.

ISConsoleApp - Top level Executable Project

IS.Core - Class Library to allow developers to store code such as Constants, Exceptions, StringConverters etc. Is used to prevent duplicating code and hardcoding.

IS.Domain - Currently only storing Domain Models.

IS.Services - Class Library that is called by the top level project. If the solution is expanded, It would have access to the Repository and Database Project.

IS.Validator - Class Library for validating the fields of a request.

![image](https://user-images.githubusercontent.com/42835367/204232222-df329bb6-ff87-4a82-b47d-80733ab926b3.png)


# Getting Started
Requirements:
- .Net Core 6.0
- Visual Studio 2022

1. Clone Repository

2. Setup ISConsoleApp as startup project with Current Test Data

![image](https://user-images.githubusercontent.com/42835367/204238865-29d49a95-3203-4a29-b3eb-97da5a05a4e6.png)

3.  Change Test Data if required.

![image](https://user-images.githubusercontent.com/42835367/204237934-26fbe7e0-13ed-4e4f-b3b6-db2c4b004cb6.png)

 - Add Movement Commands by adding a JSON Object.
 E.g.
    {
        "XCoordinate": "0",
        "YCoordinate": "0",
        "FaceDirection": "NORTH",
        "EventType": "5"
    }

Event Type is an Enum and can be defined using the following integers.

![image](https://user-images.githubusercontent.com/42835367/204238135-9f33fc6c-9533-4bbf-b984-ba8212c55944.png)

If Test Data has not been amended, you should get the following output.

![image](https://user-images.githubusercontent.com/42835367/204238404-207dfcf7-3613-4e64-9018-b5660dac1380.png)

