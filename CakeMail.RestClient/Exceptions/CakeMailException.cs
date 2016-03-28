using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace CakeMail.RestClient.Exceptions
{
	/// <summary>
	/// This class represents a generic CakeMail error. All other exceptions thrown by
	/// the CakeMail API subclass this exception
	/// </summary>
	[Serializable]
	public class CakeMailException : ApplicationException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CakeMailException"/> class.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		public CakeMailException(string message)
			: base(message) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="CakeMailException"/> class.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="innerException">The inner exception.</param>
		public CakeMailException(string message, Exception innerException)
			: base(message, innerException) { }

		[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
		// Constructor should be protected for unsealed classes, private for sealed classes.
		// (The Serializer invokes this constructor through reflection, so it can be private)
		protected CakeMailException(SerializationInfo info, StreamingContext context)
			: base(info, context) { }

		[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
