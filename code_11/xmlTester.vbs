myWebService = "http://localhost/soapExamples/simpleService.asmx"
myMethod = "xmlTester"

'** create the SOAP envelope with the request
s = ""
s = s & "<?xml version=""1.0"" encoding=""utf-8""?>" & vbCrLf
s = s & "<soap:Envelope "
s = s & "  xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"""
s = s & "  xmlns:xsd=""http://www.w3.org/2001/XMLSchema"""
s = s & "  xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">"
s = s & vbCrLf
s = s & "  <soap:Body>" & vbCrLf
s = s & "    <rootElement>" & vbCrLf
s = s & "      <someNode someAttribute=""random"">" & vbCrLf
s = s & "        <someOtherNode>some data</someOtherNode>" & vbCrLf
s = s & "      </someNode>" & vbCrLf
s = s & "    </rootElement>" & vbCrLf
s = s & "  </soap:Body>" & vbCrLf
s = s & "</soap:Envelope>" & vbCrLf

msgbox(s)

set requestHTTP = CreateObject("Microsoft.XMLHTTP")

msgbox("xmlhttp object created")

requestHTTP.open "POST", myWebService, false
requestHTTP.setrequestheader "Content-Type", "text/xml"
requestHTTP.setrequestheader "SOAPAction", myMethod
requestHTTP.Send s

msgbox("request sent")

set responseDocument = requestHTTP.responseXML

msgbox("http return status code: " & requestHTTP.status)
msgbox(responseDocument.xml)
