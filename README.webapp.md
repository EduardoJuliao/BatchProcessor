# Web application

The web application is where the user interacts with the system.

It has two pages, `New Process` and `Last Process`.

## New Process page

![New Process Page](./_documentation-images/New%20Process.png)

The `New Process` page displays two fields for the user.

1. Amount of Batches
2. Numbers Per Batch

Both numbers must be between 1 and 10.
Numbers outside this range won't be accepted and will block the user.

Once the user inputs these numbers and hits start, a grid will appear
with the amount of batches, Numbers remaining to be processed and the
sum of all the numbers.

Only one process can be started at the time.

If the user leaves the page and comes back with a process still running,
the system will pick up from where the user left.

## Last Process page

![Last Process page](./_documentation-images/Last%20Process.png)

In the `Last Process` page, the user can check the last process executed.
