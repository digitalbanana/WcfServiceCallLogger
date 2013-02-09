namespace WcfServiceCallLogger.Test
{
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using WcfServiceCallLogger.Entities;

	[TestClass]
	public class WcfServiceCallTests
	{
		[TestMethod]
		public void WcfServiceCall_DeserializesStringCorrectly()
		{
			//Arrange
			string testMessage = "<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
			                 "<s:Header>" +
			                 "<Action s:mustUnderstand=\"1\" xmlns=\"http://schemas.microsoft.com/ws/2005/05/addressing/none\">http://tempuri.org/IService/DoSomething</Action>" +
			                 "</s:Header>" +
			                 "<s:Body>" +
							 "<DoSomething xmlns=\"http://tempuri.org/\">" +
			                 "<aaa>SOMEVAL1</aaa>" +
							 "<bbb>SOMEVAL2</bbb>" +
							 "</DoSomething>" +
			                 "</s:Body>" +
			                 "</s:Envelope>";

			//Act
			var result = new WcfServiceCall(testMessage);

			//Assert
			Assert.IsNotNull(result);
			Assert.AreEqual("DoSomething", result.ActionName);

			Assert.AreEqual("IService", result.ServiceName);

			Assert.AreEqual(2, result.Parameters.Count);
			Assert.AreEqual("aaa", result.Parameters[0].Name);
			Assert.AreEqual("SOMEVAL1", result.Parameters[0].value);
			Assert.AreEqual("bbb", result.Parameters[1].Name);
			Assert.AreEqual("SOMEVAL2", result.Parameters[1].value);

			Assert.AreEqual("IService:DoSomething(aaa,bbb)->(SOMEVAL1,SOMEVAL2)", result.ToString());
		}
	}
}
