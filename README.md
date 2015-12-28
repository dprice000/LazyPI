# PIWebSharp
A client side library for interfacing with the OSIsoft PI WebAPI 

This library is built against PI WebAPI version 1.3.0 based on the documentation at the OSIsoft website(https://techsupport.osisoft.com/Documentation/PI-Web-API/help/changelog.html). 

To check the web api version use:
```
GET https://myserver/piwebapi/system/versions
```

The PI WebAPI uses WebIDs as unique identifies. These are tied to an objects GUID so these are the most reliable way of searching for a object.