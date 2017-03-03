# LazyPI
[![Gitter](https://badges.gitter.im/dprice809/LazyPI.svg)](https://gitter.im/dprice809/LazyPI?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge) [![Coverage Status](https://coveralls.io/repos/github/dprice809/LazyPI/badge.svg?branch=master)](https://coveralls.io/github/dprice809/LazyPI?branch=master)

**Summary**

The goal of this project is to create a high level abstraction on top of the the AFSDK. LazyPI acts as a wrapper class and uses interfaces to define a general set of operations that must be implemented. These interfaces can be implemented for a WebAPI, WCF, OLEDB, or native DLL. The goal is to create a standard API which can interact with all OSIsoft AFSDK implementations. 

**PI WebAPI**

Using the WebAPIConnection object configures LazyPI to make use of the OSIsoft WebAPI. The library will make use of the interfraces defined in the WebAPI namespace.

     WebAPIConnection conn = new WebAPIConnection(AuthType.Basic, "MyHost", "Username", "Password");
