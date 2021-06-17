# JSON-Searcher

* [Setting up the application](#Setup)
* [Using the application](#Features)
* [Assumptions & Limitations](#Assumptions)
* [Code Design](#Design)

## Setup
These are the steps to set up the project on your local machine:
1. Clone the project into your local machine:
```
> git clone https://github.com/HZP1997/JSON-Searcher.git
```
2. Change your working directory to the cloned folder:
```
> cd JSON-Searcher
```
3. Build the application:
```
> dotnet build
```
4. Create the logs folder in Searcher:
```
> cd Searcher
```
```
> mkdir logs
```
5. Move to the directory where the application is stored:
```
> cd bin/Debug/netcoreapp3.1
```
6. Starting the application:
```
> .\JSON-Searcher.exe
```
* You can prevent typo errors by typing JSON and keep pressing the TAB key until you get the correct file to execute.
* *OR* alternatively, you can go to {host-directory-path}/bin/Debug/netcoreapp3.1 and click on JSON-Searcher.exe directly.  

&nbsp;
## Features
### The application provides several commands:
1.  **help**: Displays all the available commands and how to use them.
```
> help
```
2.  **exit**: Closes the application.
```
> exit
```
3. **clear**: Remove all existing console text.
```
> clear
```
4. **reload**: Reimports JSON files from the allocated folder.
```
> reload
```
  * *Note*: Files are imported from {host-directory-path}/JSON folder. So make sure you have the right files in it before reloading.
  * Starting the application will automatically load the existing files once.

5. **fields**: Grabs all the fields within a json object:
```
> fields [tableName]...
```
  * No tableNames provided will give all currently existing entities' fields.
6. **search**: Performs searches based on your input:
    * search table [field, value]... - Return results in the table that matches all field-value filters:
```
> search organizations [field, value]...
```
#### Note:
  - You can supply multiple field-value pairs, but all fields must have a corresponding value **(Use "%" to represent searching empty fields!)**.
```
> search organizations _id 101 tags %
```
  * Commands, tables, fields, and values are case insensitive, and values do not have to match fully.
```
> SeARcH orGanIZAtiOns _iD 1 NAmE eX
```
  * Date filters only matches year, month, and date. The search value also have to follow the format **yyyy/mm/dd**.
```
> search organizations created_at 2016/05/22
```

&nbsp;
## Assumptions
### Files
  1. The JSON objects must be stored as an array [], even if the number of objects is one.
  2. The application can only import .json files (.txt files will not be picked up for example).
  3. The supplied JSON objects must be parsable to one of the classes in the *model* folder.
  4. File names must also be one of the class names in the *model* folder (Case insensitive).
### Data
  1. "_id" field is Mandatory. All other JSON object fields are optional.
  2. No values in the JSON fields are "%".
### Search
  1. Date searches are searched, filtered, and returned based on Melbourne timezone (GMT +10).

&nbsp;
## Design
  * The _Configs.cs contains some settings you may find useful should you ever find the need to change it:
    - The closeApp config is for checking if the app is running. **Do not touch**.
    - The debugging config determines the exception displayed. *FALSE* only shows the exception message and *TRUE* will display the entire stacktrace.
    - The logging config determines whether logs are stored per each **search**. *FALSE* will prevent any logging and *TRUE* will save the search results in /logs folder as a .yaml file.
  * The *libs* folder contains library files retrieved from third-party source with references. As per the requirement no search libraries will be imported.
  * The *JSON* folder contains raw files from which the application imports files. You can also provide customized inputs into the system by replacing content in the default .json files.
  * The *model* folder contains class templates that represent their respective JSON object schemas.
  * The *utils* folder contains utility classes and methods that assist the application and make the code cleaner and more readable.
  * The *tests* folder contains various tests that cover multiple file and input scenarios.
  * The *data* folder manages data storage and search functionalities.
  * The commands and data are built using Dictionaries for minimal complexity and optimizes the load time.