namespace WcfServiceCallLogger.BehaviourExtensions
{
	using System;
	using WcfServiceCallLogger.Behaviours;

	public class ServiceBehaviourExtension : System.ServiceModel.Configuration.BehaviorExtensionElement
	{
		protected override object CreateBehavior()
		{
			return new ServiceBehaviour();
		}

		public override Type BehaviorType
		{
			get { return typeof (ServiceBehaviour); }
		}
	}
}