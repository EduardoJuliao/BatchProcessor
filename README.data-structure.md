# Data Structure

The information is saved using Entity Framework core.

## Table Structure

![alt text](_documentation-images/Batch%20Processor.png)

### Process Table

The `Process` table stores information about each Process started by the user.
Each process can have `n` amount of batches.

### Batch Table

The `Batch` table stores information about each batch inside a `Process`.
Each batch can have `n` amount of numbers.

### Number Table

The `Number` table stores information about each number inside each `Batch`.
`Multiplier` and `MultipliedValue` are not mandatory as they can be generated in a later step.
