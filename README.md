# Studio71 Backend Developer Assignment
Hello, I'm Kevin Hewitt and this is my attempt to complete the Studio71 first-in first-out multiqueue challenge. The guidelines are as follows: 

> create a FiFo multi-queue with messages stored in a MySQL database,
> with an implementation that is independent of test data or use case.
> **In addition your application should...**
> 1. Fill in the logic for all of [the stubbed functions](https://s71.link/aSNHX9) modifying the namespace as needed to develop the queue application.
> 2. Submit a SQL file that includes the table structure and any other data needed for your application.
> 3. Add additional functionality as needed so that the namespace can be run easily (start up script, examples, etc).
> 4. While not required, any notes on the development or optimizations you built in to your application would be a plus.
## Program Overview
### Running the program
The only thing you'll need to set this up (assuming you already have sqlserver ready to go) is to execute my create table script (found [here](https://github.com/Neuron1681/s71Challenge/tree/master/MySQL)) and adjust the App.config file to point at the right direction. Please note that if the table is password protected, the `DestinationCredentials` config item is a `;`-delimited field for a username + password combo: `<username>;<password>`. 
### So what's inside the repo?
In this repo you will find a weekend-engineered c# program attempting to flesh out [the stubbed functions](https://s71.link/aSNHX9) and follow the project requirement listed in the Studio71 email.
Since the project is independent of test data or use case, I placed some code under the main method to demonstrate the functionality of the required functions as well as the system requirements.
### Challenges and decisions
* **Pandemic!**  Due to the nature of the current crisis, I was spending my time this weekend helping out my parents who are basically living fossils. I absolutely had enough time to get a healthy program finished, tested, and commented, but -- as with all things -- more could be done with more time, and I intend to describe what more could be done in the following sections. 
* **C#** I had the option of improving my proficiency with Clojure, or programming in a language I was fluent in. I decided to go with the latter considering the continuation of my job search along with the ongoing pandemic. It unfortunately wasn't feasible to attempt to learn and implement a new language within my time constraints over the weekend. Additionally, I could tell by the phrasing of the [the stubbed functions](https://s71.link/aSNHX9) that Clojure is a dynamically-typed language. I was sure of this when I read the following: `message-type - filters for message of the given type`. C# is a strongly-typed language, and the standard Microsoft `Queue` library does not allow for a dynamic type. I anticipated the typing issue would come into play regarding the project requirements, so I had to decide to normalize the type to `<string>`. 
* **Native Microsoft library queues** was what I decided to use due to the time constraint. I would very much like to sit down and knock out a custom `Queue<MyType>`  data structure that accepts dynamic types and inherits from `IEnumerable` so I could access particular indices and allows dynamic queue messages, but I had to make decisions based on my resources.
* **Multithreading** was something I wanted to do in order to set up the multi-queue. In fact, I actually use a multithreaded multi-queue at my current job. I also believed multithreading was crucial for this requirement: `Messages are hidden for the duration (in sec)`. While threads could have been operating off the queues, another set of threads could have hidden and revealed queue messages. Multithreading would have also introduced some issues due to the nature of C# combined with the project requirements. The native queue library prevents `push`-ing to the front of a queue (technically that would be a constrained linkedlist). I was forced to create a new queue and assign it to the old one. If my program was multithreaded, this could have introduced a race condition.
* **Confirm()** was an awkward function to fill in thanks to the nature of C# -- it felt like it absolutely belonged to a Clojure program because 1) the method is made redundant with `.Dequeue` in a traditional C# queue and 2) in C# it is also impossible to delete any element from a queue except from the last position in the queue. 


