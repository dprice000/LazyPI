# LazyPI
[![Coverage Status](https://coveralls.io/repos/github/dprice809/LazyPI/badge.svg?branch=master)](https://coveralls.io/github/dprice809/LazyPI?branch=master)

A client side library for interfacing with the OSIsoft PI WebAPI. This project originally started out as a tool for deserializing PI WebAPI requests. 
In the future the goal of this project will be to provide a library of Lazy objects. This will allow developers to work with the WebAPI in a more object oriented way. 

For those interested in just deserializing responses see code here(https://github.com/dprice809/LazyPI/tree/08ff5dba8d2689c44aed9d774433f9ac5359f227)

This library is built against PI WebAPI version 1.3.0 based on the documentation at the OSIsoft website(https://techsupport.osisoft.com/Documentation/PI-Web-API/help/changelog.html). 

To check the web api version use:
```
GET https://myserver/piwebapi/system/versions
```

The PI WebAPI uses WebIDs as unique identifies. These are tied to an objects GUID so these are the most reliable way of searching for a object.