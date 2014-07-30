SynoDs
=

A C#, Asynchronous, Portable Class Library for the Synology DiskStation public APIs


The APIs:
=========

An initial implementation supporting the basic operations of the following APIs is planned:

- Basic Apis: Information and Authentication APIs implemented.
- DownloadStation: CRPD (create, read, pause, delete) operation on Download TASKs using Async calls to the DS.
- FileStation: Create, List, Info, Rename, CopyMove operations on files and folders using Async calls to the DS.


Features under Development
===================

- Exception and handling of known error responses.
- Additional wrapper methods to facilitate the API calls with multiple requests (For example, create multiple download tasks).


Some additional features further down the road.
================================================

Additional modularity to be added in order to allow the client to create their own implementation of some of the used classes. (For example, Logging, Http Communication. This will be handled by a very simple IoC / Factory pattern which will also be open to custom implementation.


Usage
======= 

Usage information to be added soon. 


Limitations
============

Currently, portable class libraries can't handle SSL certificate manipulation / validation. Until MS decides to implement a portable SSL on the BCL this functionality will be abstracted and handled by the clients.

A basic Http Client is provided and handles all communication thourgh non-secure HTTP get requests. 

