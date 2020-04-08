# BlackBox
BlackBox Event Log Framework

## Introduction
For most of my projects I require logging of events. This project is based on an old, equally named logging framework of mine hosted on Codeplex. Since Codeplex is read-only and the library outdated I have updated it for dot NET standard.

I often use NLog, Log4Net and Serilog. But I missed mine since it was easy to use and extend. It is based of custom loggin code created over the years. Mostly the loggin code was very alike so I decided to create a framework for it and make it reusable.

The basic requirements of this framework are:
-	Usable in web, server, desktop and embedded applications.
-	Set-up in code without the use of a complex configuration file.
-	Log to MS-SQL server.
-	Fallback log when the SQL server is down (file log preferable).
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

Below describes the details. But it is very simple:

```
Log.Trace("Hello world.");
```

## Below here is work in progres:

Set up the manager:
```
BlackBoxManager manager = new BlackBoxManager();
```

It is possible to log now, but since there is no event writer nothing will be written. In this example a console writer will be created through an anonymous method:
```
manager.RegisterWriter(EventLevel.Debug, t =>
{
   System.Console.WriteLine(t.Content);
});
```

The first parameter in RegisterWriter is the event types where to trigger the writer on. In the utilities class there is a method who retrieves a combination of all types. The second parameter is to associate a delegate to a named or anonymous method. This delegate has one parameter of type EventMessage which holds the event to be written. In the example there is an anonymous method created which writes the Data parameter of the EventMessage class to the console.

To write an event:
```
manager.Write(new EventMessage(EventLevel.Debug, "Hello Debug World!"));
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

## Queue writer

## T-SQL writer

## File writer

## Event writer	

## Custom writers (delegate)

## Log
