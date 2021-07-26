# Application Process Flow

The application is divided into three parts.

One front-end facing application build in Angular.
Two back-end web apis build in dotnet core.

## The flow

![Alt text](_documentation-images/Batch%20Processor%20-%20Application%20Flow.png)

The process starts once a user requests a X amount of `Batches` with an Y amount
of `Numbers` in it to be processed.

### Creating a process

Once the request is received, the `Manager Api` will create a new Process
with the required amount of batches and save it in the database.
After the process is stored in the DB, the WebApi will raise an event notifying
the `Web App` that the Process was created.

### Processing the Batches

The system will no process the batches in parallel.

#### Generating a new Number

For each batch, the `Manager Api` will call the `Processor Api`, passing the amount
of numbers to be generated.
For each number generated, the `Processor Api` will sent a notification to the
`Manager Api`, containing the new Number.
Once the `Manager Api` received the number, it'll do this three things in order:

1. Store the new number in the database.
2. Notify the `Web App` with the new number.
3. Request the number to be multiplied.

#### Multiplying the Number

To Multiply the number, the `Manager Api` will make a request to `Process Api`
with the value of the number as an argument.
When the `Manager Api` receives the number, it'll update the number in the database
and notify the `Web App` with the multiplied number.

#### Completing the process

Once all batches are completed by having the required amount of numbers and all
have its value multiplied, the `Manager Api` will update the process with the
finished date update it in the database and notify the `Web App` about it's
completion.
