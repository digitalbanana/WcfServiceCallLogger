namespace WcfServiceCallLogger.Entities
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.ServiceModel.Channels;
	using System.Text;
	using System.Xml.Linq;

	/// <summary>
	/// Class representing invocation of Wcf Service
	/// </summary>
	public class WcfServiceCall
	{

		/// <summary>
		/// Gets or sets the name of the service.
		/// </summary>
		/// <value>
		/// The name of the service.
		/// </value>
		public string ServiceName { get; set; }

		/// <summary>
		/// Gets or sets the name of the method being invoked.
		/// </summary>
		/// <value>
		/// The name of the method.
		/// </value>
		public string MethodName { get; set; }

		/// <summary>
		/// Gets or sets the parameters passed into the method invocation.
		/// </summary>
		/// <value>
		/// The parameters.
		/// </value>
		public List<WcfMethodParameter> Parameters { get; set; }

		/// <summary>
		/// Gets or sets the error if an error occurs during serialization.
		/// </summary>
		/// <value>
		/// The error.
		/// </value>
		public string Error { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="WcfServiceCall"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <remarks>Provides convenience overload but must be a more efficent way of converting than ToString()</remarks>
		public WcfServiceCall(Message message) : this(message.ToString())
		{
		}


		/// <summary>
		/// Initializes a new instance of the <see cref="WcfServiceCall"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		public WcfServiceCall(string message)
		{
			Parameters = new List<WcfMethodParameter>();


			try
			{
				var linqedMessage = XDocument.Parse(message.ToString());
				var action =
					linqedMessage.Root.DescendantsAndSelf().Single(x => x.Name.LocalName.Equals(Constants.WCF_MESSAGE_SERVICE_METHOD_NAME, StringComparison.InvariantCultureIgnoreCase));

				string actionNodeValue = action.Value;

				int preActionNameSlashPosition = actionNodeValue.LastIndexOf("/");
				int preServiceNameSlashPosition = actionNodeValue.Substring(0, preActionNameSlashPosition).LastIndexOf("/");

				ServiceName = actionNodeValue
					.Substring(preServiceNameSlashPosition + 1, actionNodeValue.Length - preServiceNameSlashPosition - (actionNodeValue.Length - preActionNameSlashPosition) - 1 );

				MethodName = actionNodeValue
					.Substring(preActionNameSlashPosition + 1, actionNodeValue.Length - preActionNameSlashPosition - 1);

				var ps =
					linqedMessage.Root.DescendantsAndSelf()
								 .Where(x => x.Name.LocalName.Equals(MethodName, StringComparison.InvariantCultureIgnoreCase))
								 .Single()
								 .Descendants()
								 .ToList();
				ps.ForEach(x => Parameters.Add(new WcfMethodParameter()
				{
					Name = x.Name.LocalName,
					value = x.Value
				}));

			}
			catch (Exception ex)
			{
				Error = string.Format("ERROR: {0}", ex);
			}

		}

		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			var result = new StringBuilder();
			var paramsResult = new StringBuilder();

			result.AppendFormat("{0}:{1}(", ServiceName, MethodName);

			for (int i = 0; i < Parameters.Count; i++)
			{
				var p = Parameters[i];
				result.Append(p.Name);
				paramsResult.Append(p.value);

				if (i < Parameters.Count - 1)
				{
					result.Append(",");
					paramsResult.Append(",");
				}
			}
			result.Append(")->(");
			result.Append(paramsResult);
			result.Append(")");

			return result.ToString();

		}
	}
}