# Application Process Flow

The application is divided into three parts.

One front-end facing application build in Angular.
Two back-end web apis build in dotnet core.

## Background Worker

![Alt text](_documentation-images/Batch%20Processir%20-%20Application%20Flow%20-%20Background%20Worker.png)

### Request for a new Process

Once the request is received, the `Manager Api` will create a new Process
with the required amount of batches and save it in the database.
After the process is stored in the DB, the WebApi will return the new
Process object to the client

### Queue the process

Once the client receives the new process, it can start the process
by queueing it with the web api.
This will start the background worker task and process all queued
process.

### Running the Batches

The system will now process the batches in parallel.

#### Creating a new Number

For each batch, the `Manager Api` will call the `Processor Api`, passing the amount
of numbers to be generated.
For each number generated, the `Processor Api` will sent a notification to the
`Manager Api`, containing the new Number.
Once the `Manager Api` received the number, it'll do this two things in order:

1. Store the new number in the database.
2. Request the number to be multiplied.

#### Multiplying an updating the Number

To Multiply the number, the `Manager Api` will make a request to `Process Api`
with the value of the number as an argument.
When the `Manager Api` receives the number, it'll update the number in the database.

#### Finishing the process

Once all batches are completed by having the required amount of numbers and all
have its value multiplied, the `Manager Api` will update the process with the
finished date update it in the database.
