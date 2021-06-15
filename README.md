# JSON-Searcher

* [Starting the application](#Start)
* [Using the application](#Features)
* [Code Design](#Design)
* [Assumptions](#Assumptions)

## Start
Below are the steps to get this application started:
1. Clone this repo

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


