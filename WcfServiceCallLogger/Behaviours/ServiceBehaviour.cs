namespace WcfServiceCallLogger.Behaviours
{
	using System.Collections.ObjectModel;
	using System.Linq;
	using System.ServiceModel;
	using System.ServiceModel.Channels;
	using System.ServiceModel.Description;
	using System.ServiceModel.Dispatcher;

	public class ServiceBehaviour : IServiceBehavior
	{
		public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			//throw new NotImplementedException();
		}

		public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase,
		                                 Collection<ServiceEndpoint> endpoints,
		                                 BindingParameterCollection bindingParameters)
		{
			//throw new NotImplementedException();
		}

		public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			serviceHostBase.ChannelDispatchers.ToList().ForEach(x =>
				{
					(x as ChannelDispatcher).Endpoints.ToList().ForEach(y =>
						{
							y.DispatchRuntime.MessageInspectors.Add(new MessageInspector());
						});
				});
		}
	}
}
