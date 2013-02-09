namespace WcfServiceCallLogger
{
	/// <summary>
	/// Contsnts used within WcfMessageInspector
	/// </summary>
	public class Constants
	{
		/// <summary>
		/// name of item in HttpCOntext.Items collection that is used by message inspector to records service invocations occurring per request
		/// </summary>
		public const string HTTP_REQUEST_ID = "HttpRequestId";

		/// <summary>
		/// The name of the xml element within the WCF message that indicates the service method being invoked
		/// </summary>
		public const string WCF_ACTION_NAME = "Action";


        public const string WCF_SERVICE_NAME = "ServiceName";
        
	}
}