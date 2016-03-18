SynoDs
=

A C# Universal Windows Platform library for the Synology DiskStation


Implemented API's
=========

- SYNO.Api.Auth

   The authentication API is implemented, you can login and authenticate with the Synology. A session ID is returned

- SYNO.Api.INFO
 
   The information API (which returns all API information) is implemented with caching. When an initial request is made, if the Cache is empty, we will request ALL of the API information and store it to avoid future requests.
   

Features to be implemented
==========================================

- Error dictionary for known Synology Errors.
- Exception handling
- Download station API
- FileStation API
- UWP application currently under development.
- Unit tests

HTTP Client
==========================================

Because of how HTTP client is implemented in portable class libraries and the limitations of SSL Certificate handling of Windows 10 Apps, it is up to the developer to implement the HTTP Client with the appropriate ways to handle Certificate validations.

The current implementation ignores Certificate validation errors because the Synology provided SSL certificate is Self Signed.


UWP Features so far
==========================================

Using Template 10 most of the boilerplate code is done. 

A ViewModelLocator is implemented and Unity is used as a Container for the App.

The SessionId token is stored as Singleton in the container for the whole application lifecycle for sending Requests.

Usage
==========================================

See source code for the UWP App. 

