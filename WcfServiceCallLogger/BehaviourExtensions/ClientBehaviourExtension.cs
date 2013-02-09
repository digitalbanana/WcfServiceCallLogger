namespace WcfServiceCallLogger.BehaviourExtensions
{
	using System;
	using WcfServiceCallLogger.Behaviours;

	public class ClientBehaviourExtension : System.ServiceModel.Configuration.BehaviorExtensionElement
	{
		protected override object CreateBehavior()
		{
			return new EndpointBehavior();
		}

		public override Type BehaviorType
		{
			get { return typeof(EndpointBehavior); }
		}
	}
}