# BlackBox
BlackBox Event Log Framework

## Introduction
As a professional developer for years I used lots of logging code mostly custom developed. Project after project more experience is gained and also constant returning code is copy-pasted in new projects. The logging code didn’t change much after some projects and I decided to create a framework around it so I have never copy-paste it again. The result of that work is the BlackBox Event Log Framework.
Based on previous projects I used logging, there where several requirements which must be implemented. These basic requirements are:
-	Usable in web, server, desktop and embedded applications.
-	Set-up in code without the use of a complex configuration file.
-	Log to MS-SQL server.
-	Fallback log when the SQL server is down (windows event log preferable).
-	Log to file for portable and embedded software.
-	Log to console on all events but log to database on errors events.
-	Logging must not block business code.
-	Easy to trace where and what happened.

This framework is made open-source since I find it more useful when a lot of developers around the world can use it instead of a handful developers I team with.

## Getting started
This chapter will write about how to get BlackBox up and running.
BlackBox consists of two basic elements: Manager and EventWriter.
The manager handles all the events to be written to the right writer. Uses a fallback writer when one of the primary writers fail.

The EventWriter handles how the events are persist e.g. database, file or console.

Set up the manager:
```
BlackBoxManager manager = new BlackBoxManager();
```

It is possible to log now, but since there is no event writer nothing will be written. In this example a console writer will be created through an anonymous method:
```
manager.RegisterWriter(BlackBoxUtilities.GetAllEventTypes(), (t =>
{
   System.Console.WriteLine(t.Data);
}));
```

The first parameter in RegisterWriter is the event types where to trigger the writer on. In the utilities class there is a method who retrieves a combination of all types. The second parameter is to associate a delegate to a named or anonymous method. This delegate has one parameter of type EventMessage which holds the event to be written. In the example there is an anonymous method created which writes the Data parameter of the EventMessage class to the console.

To write an event:
```
manager.Write(EventTypes.Trace, "Hello World!");
```

In this example “Hello World!” will be written to the console. The first parameter is the event type of the event message. Every writer which is registered with that event type will be triggered. The second parameter is the data field of the event message as string type. There are more parameters but these are optional and will be threated further in this manual.
There are default writers on the framework and these writers all have a method which can be associated with the EventWrite delegate. In this example there will be a file writer registered with the manager:

```
EventFileWriter writer = new EventFileWriter();
manager.RegisterWriter(BlackBoxUtilities.GetAllEventTypes(), writer.Write);
```

There is an instance created of the EventFileWriter, default parameters will be used. The writer is registered at the manager, the “Write” method of the writer will be associated with the EventWrite delegate. When the above event write sample will be executed a file will be created with the data “Hello World!”.

More default writers with examples will be explained further in this manual. In the test project here is code to use all the default writers and managers.

Rest of this manual will be finished soon!

## The Manager
Whats is it
- Extensible
- Non threaded

Whats is is and hwo it works
Why u should choose
Threaded
Whats is it and how it works
Why u should choose
Event Writers
Abstract of event writers
Whats is it and how it works
Console wrtier
Whats is it and how it works

## Mock writer
Whats is it and how it works

## T-SQL writer
Whats is it and how it works

## File writer
Whats is it and how it works

## Event writer	
Whats is it and how it works

## Custom writers (delegate)
Whats is it and how it works
How to write your own

## Log
Whats is it and how it works

- Fluent configuation
- Think about level as flag or short
