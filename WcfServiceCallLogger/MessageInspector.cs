namespace WcfServiceCallLogger
{
	using System.ServiceModel;
	using System.ServiceModel.Channels;
	using System.ServiceModel.Dispatcher;
	using System.Web;
	using NLog;
	using WcfServiceCallLogger.Entities;

	/// <summary>
	/// Class for intercepting contents of of WCF messages and recording service invocations (with parameters)
	/// </summary>
	public class MessageInspector : IClientMessageInspector, IDispatchMessageInspector
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

		#region IClientMessageInspector implementation

		/// <summary>
		/// Enables inspection or modification of a message before a request message is sent to a service.
		/// </summary>
		/// <param name="request">The message to be sent to the service.</param>
		/// <param name="channel">The  client object channel.</param>
		/// <returns>
		/// The object that is returned as the <paramref name="correlationState " />argument of the <see cref="M:System.ServiceModel.Dispatcher.IClientMessageInspector.AfterReceiveReply(System.ServiceModel.Channels.Message@,System.Object)" /> method. This is null if no correlation state is used.The best practice is to make this a <see cref="T:System.Guid" /> to ensure that no two <paramref name="correlationState" /> objects are the same.
		/// </returns>
		public object BeforeSendRequest(ref Message request, IClientChannel channel)
		{

			var m = new WcfServiceCall(request);

			string requestId = "Not set";

			var s = HttpContext.Current.Items[Constants.HTTP_REQUEST_ID];
			if (s != null)
			{
				requestId = s.ToString();
			}

			LogEventInfo logEvent = new LogEventInfo(LogLevel.Debug, "MessageInspector", m.ToString());
			logEvent.Properties[Constants.HTTP_REQUEST_ID] = requestId;
			logger.Log(logEvent);

			return null;
		}

		/// <summary>
		/// Enables inspection or modification of a message after a reply message is received but prior to passing it back to the client application.
		/// </summary>
		/// <param name="reply">The message to be transformed into types and handed back to the client application.</param>
		/// <param name="correlationState">Correlation state data.</param>
		public void AfterReceiveReply(ref Message reply, object correlationState)
		{
			
		}

		#endregion

		#region IDispatchMessageInspector implementation

		public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
		{
			logger.Debug("AfterReceiveRequest");
			return null;
		}

		public void BeforeSendReply(ref Message reply, object correlationState)
		{

		}

		#endregion

	}

	
}