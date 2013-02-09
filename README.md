WcfServiceCallLogger
====================

lightweight  logger for tracing WCF service invocations. Provides config file based logging of WCF service method invocations. Analyses clkient side SOAP message content during message despatch pipeline and logs output using NLog

TODOs (feel free to volunteer)  

1) Adding Service Side logging functionality  

2) Investigate performance of logging - message.tostring might not perform very well  

3) Investigate processing of json serialized messages  

4) Decouple logging implementation  

5) AOB?