# JSON-Searcher

* [Setting up the application](#Setup)
* [Using the application](#Features)
* [Code Design](#Design)
* [Assumptions](#Assumptions)

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
> cd bin/Debug/netcoreapp3.1
5. Starting the application:
```
> .\JSON-Searcher.exe
```
* You can prevent typo errors at by typing JSON and keep pressing the TAB key until you get the right file to execute.
* *OR* alternatively you can go to {host-directory-path}/bin/Debug/netcoreapp3.1 and click on JSON-Searcher.exe directly.

## Features
1. Once you have the application set-up. You can use the list of available commands to start searching.
  * You can always type HELP into the application to view all available commands.


## Design
  * The *json* folder contains the raw json files which can be picked up by the application. You can also provide customized inputs into the system.
  * The *model* folder contains class files that represents the json schema so they can store JSON objects.
  * The *data* folder stores data parsed from the JSON files once they are loaded. It also contains the main logic for searching those fields.
  * The *utils* folder contains different methods that assists the functions in the above folders.

## Assumptions
  * Invalid JSON files are detected, but valid JSON strings must have a corresponding class in the *model* folder.
  * JSON object list must be stored within an array [].
  * Only JSON files are supplied into the *json* folder.
  * Your search values cannot contain spaces.
  * All JSON schema attributes are compulsory.
  * Only _id fields are compulsory, all other fields are optional.
  * Since multiple-search fields are included, user needs to type '%' only to indicate that this searches for fields that are empty.
  * Because of this constraint, I assume no values in any JSON files will contain %.


