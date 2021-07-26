# Projects

The solution is build with 3 main projects.

## Front facing projects

### Batch Processor Web app

The `Web App` is the application that the user will communicate with
the back end.

What the user can do with the application is:

#### Request a new Process

To start a process, the user needs to inform two numbers:

1. Amount of Batches
2. Amount of Numbers per Batch

Both of these values must be between 1 and 10.

These values will be displayed in a table, which contains

* Rows containing each batch
* For each batch
  * The amount of numbers remaining to process
  * Sum of all numbers

#### Read last processed Batch

The user can view the last processed batch in a separate page.

The grid will display the same data as the one in the process
page.

## Back end projects

### Batch Processor Manager Api

This is the Api that the Web Application will connect to.



### Batch Processor Processor Api

The `Processor Api` is in charge of:

* Generating a new number
* Multiply a number
