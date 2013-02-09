namespace WcfServiceCallLogger.Modules
{
	using System;
	using System.Web;

	/// <summary>
	/// HttpModule component of WcfMessageInspector
	/// </summary>
	public class WcfMessageCorrelationModule : IHttpModule
	{
		public void Init(HttpApplication context)
		{
			context.BeginRequest += (sender, args) => HttpContext.Current.Items.Add(Constants.HTTP_REQUEST_ID, Guid.NewGuid());
		}

		public void Dispose()
		{
		}
	}
}