# RestHelper
--------------
RestHelper is a sleek and simple Wrapper library for providing simple access to Rest API, by hiding away the complexities of HTTPClient Class.It is particularly designed to work along with Xamarin PCL Projects. The Project is an open-source solution created by EcSolvo Technologies and maintained by contribution from Open-Source Community.

Nuget Package : https://www.nuget.org/packages/EcSolvo.RestHelper/

The following section outlines how to use EcSolvo.RestHelper Libraries.

RestHelper Constructor and ExecuteAsync
-----------------------------------------

The RestHelper Constructor and ExecuteAsync Method is the heart of RestHelper Library. It is build on 'Pit of Success' philosophy, removing erronous steps by the consuming Developer.
The RestHelper Constructor takes a single Parameter, which is the base address of your API.The ExecuteAsync is an Asynchronous Method which take in two Parameters, 
a) HTTP Verb (GET,POST)
b) Resource (URL to the exact API Resource)

For GET Request
----------------

For Handling URI Parameters, the EcSolvo.RestHelper Library exposes a method called 'AddURLParameters', which helps to add parameters to the QueryString dictionary.

private string _BaseAddress = "http://localhost:8888/";
var resourceURL = "api/user/SingleParamStringResponse";
var restHelper = new EcSolvo.RestHelper(_BaseAddress);
string ParameterKey = "VariableStr";
string ParameterValue = "DummyString";
restHelper.AddURLParameters(ParameterKey, ParameterValue);
var result = await restHelper.ExecuteAsync<string>(HttpMethod.Get, resourceURL);

The library works even if the parameter passed is a Complex Parameter (Controller uses FromUri Attribute).

For POST Request
-----------------

For Handling OnBody Parameter in POST Request, the library exposes a method called 'AssignMessageBodyParameter'.

restHelper.AssignMessageBodyParameter(Parameter);
var result = await restHelper.ExecuteAsync<string>(HttpMethod.Post, resourceURL);

If the POST Request have additional URI Parameters, it can use the 'AddURLParameters' Method to add it to the dictionary.
