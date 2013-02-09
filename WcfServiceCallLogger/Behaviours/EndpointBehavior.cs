namespace WcfServiceCallLogger.Behaviours
{
	using System.ServiceModel.Channels;
	using System.ServiceModel.Description;
	using System.ServiceModel.Dispatcher;

	/// <summary>
	/// Endpoint behavior will apply to any configured client endpoints
	/// </summary>
	/// <remarks>could also be called ClientBehavior</remarks>
	public class EndpointBehavior : IEndpointBehavior
	{
		public void Validate(ServiceEndpoint endpoint)
		{
			//throw new System.NotImplementedException();
		}

		public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
			//throw new System.NotImplementedException();
		}

		public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
		{
			//throw new System.NotImplementedException();
		}

		public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
			clientRuntime.MessageInspectors.Add(new MessageInspector());
		}
	}
}