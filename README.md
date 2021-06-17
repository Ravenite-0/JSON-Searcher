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
4. Move to the directory where the application is stored:
```
> cd bin/Debug/netcoreapp3.1
```
5. Starting the application:
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

5. **search**: Performs searches based on your input:
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
  1. TBC

&nbsp;
## Design
  1. The *JSON* folder contains raw files from which the application imports files. You can also provide customized inputs into the system by replacing content in the default .json files.
  2. The *model* folder contains class templates that represent their respective JSON object schemas.
  3. The *utils* folder contains utility classes and methods that assist the application and make the code cleaner and more readable.
  4. The *tests* folder contains various tests that cover multiple file and input scenarios.
  5. The *data* folder manages data storage and search functionalities.
  6. The commands and data are built using Dictionaries for minimal complexity and optimizes the load time.