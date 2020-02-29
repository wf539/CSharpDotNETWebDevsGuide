﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.2914.16
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

namespace jokesClient.jokes {
    using System.Diagnostics;
    using System.Xml.Serialization;
    using System;
    using System.Web.Services.Protocols;
    using System.Web.Services;
    
    
    [System.Web.Services.WebServiceBindingAttribute(Name="jokesSoap", Namespace="urn:schemas-syngress-com-soap")]
    public class jokes : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        public jokes() {
            this.Url = "http://localhost/jokesService/jokes.asmx";
        }
        
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("addJoke", RequestNamespace="urn:schemas-syngress-com-soap:jokes", ResponseNamespace="urn:schemas-syngress-com-soap:jokes", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void addJoke(string userName, string password, string joke) {
            this.Invoke("addJoke", new object[] {
                        userName,
                        password,
                        joke});
        }
        
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        public System.IAsyncResult BeginaddJoke(string userName, string password, string joke, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("addJoke", new object[] {
                        userName,
                        password,
                        joke}, callback, asyncState);
        }
        
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        public void EndaddJoke(System.IAsyncResult asyncResult) {
            this.EndInvoke(asyncResult);
        }
        
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("getJokes", RequestNamespace="urn:schemas-syngress-com-soap:jokes", ResponseNamespace="urn:schemas-syngress-com-soap:jokes", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("jokeData", IsNullable=false)]
        public xmlJokesReturn[] getJokes(string userName, string password, int howMany) {
            object[] results = this.Invoke("getJokes", new object[] {
                        userName,
                        password,
                        howMany});
            return ((xmlJokesReturn[])(results[0]));
        }
        
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        public System.IAsyncResult BegingetJokes(string userName, string password, int howMany, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("getJokes", new object[] {
                        userName,
                        password,
                        howMany}, callback, asyncState);
        }
        
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        public xmlJokesReturn[] EndgetJokes(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((xmlJokesReturn[])(results[0]));
        }
        
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("addRating", RequestNamespace="urn:schemas-syngress-com-soap:jokes", ResponseNamespace="urn:schemas-syngress-com-soap:jokes", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void addRating(string userName, string password, int rating, int jokeID) {
            this.Invoke("addRating", new object[] {
                        userName,
                        password,
                        rating,
                        jokeID});
        }
        
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        public System.IAsyncResult BeginaddRating(string userName, string password, int rating, int jokeID, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("addRating", new object[] {
                        userName,
                        password,
                        rating,
                        jokeID}, callback, asyncState);
        }
        
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        public void EndaddRating(System.IAsyncResult asyncResult) {
            this.EndInvoke(asyncResult);
        }
        
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("getUnmoderated", RequestNamespace="urn:schemas-syngress-com-soap:jokes", ResponseNamespace="urn:schemas-syngress-com-soap:jokes", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("jokeData", IsNullable=false)]
        public xmlJokesReturn[] getUnmoderated(string userName, string password, int howMany) {
            object[] results = this.Invoke("getUnmoderated", new object[] {
                        userName,
                        password,
                        howMany});
            return ((xmlJokesReturn[])(results[0]));
        }
        
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        public System.IAsyncResult BegingetUnmoderated(string userName, string password, int howMany, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("getUnmoderated", new object[] {
                        userName,
                        password,
                        howMany}, callback, asyncState);
        }
        
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        public xmlJokesReturn[] EndgetUnmoderated(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((xmlJokesReturn[])(results[0]));
        }
        
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("addModerated", RequestNamespace="urn:schemas-syngress-com-soap:jokes", ResponseNamespace="urn:schemas-syngress-com-soap:jokes", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void addModerated(string userName, string password, int jokeID) {
            this.Invoke("addModerated", new object[] {
                        userName,
                        password,
                        jokeID});
        }
        
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        public System.IAsyncResult BeginaddModerated(string userName, string password, int jokeID, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("addModerated", new object[] {
                        userName,
                        password,
                        jokeID}, callback, asyncState);
        }
        
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        public void EndaddModerated(System.IAsyncResult asyncResult) {
            this.EndInvoke(asyncResult);
        }
        
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("deleteUnmoderated", RequestNamespace="urn:schemas-syngress-com-soap:jokes", ResponseNamespace="urn:schemas-syngress-com-soap:jokes", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void deleteUnmoderated(string userName, string password, int jokeID) {
            this.Invoke("deleteUnmoderated", new object[] {
                        userName,
                        password,
                        jokeID});
        }
        
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        public System.IAsyncResult BegindeleteUnmoderated(string userName, string password, int jokeID, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("deleteUnmoderated", new object[] {
                        userName,
                        password,
                        jokeID}, callback, asyncState);
        }
        
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        public void EnddeleteUnmoderated(System.IAsyncResult asyncResult) {
            this.EndInvoke(asyncResult);
        }
    }
    
    public class xmlJokesReturn {
        
        public string jokeID;
        
        public string joke;
        
        public string rating;
    }
}