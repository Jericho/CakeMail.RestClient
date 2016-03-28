using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace CakeMail.RestClient.Exceptions
{
	/// <summary>
	/// This class represents an exception thrown by the CakeMail API when the posted data is determined to be invalid.
	/// </summary>
	[Serializable]
	public class CakeMailPostException : CakeMailException
	{
		/// <summary>
		/// The data that was posted
		/// </summary>
		public string PostData { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CakeMailPostException"/> class.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="postData">The data that was posted</param>
		public CakeMailPostException(string message, string postData)
			: base(message)
		{
			this.PostData = postData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CakeMailPostException"/> class.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		/// <param name="postData">The data that was posted</param>
		/// <param name="innerException">The inner exception.</param>
		public CakeMailPostException(string message, string postData, Exception innerException)
			: base(message, innerException)
		{
			this.PostData = postData;
		}

		[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
		// Constructor should be protected for unsealed classes, private for sealed classes.
		// (The Serializer invokes this constructor through reflection, so it can be private)
		protected CakeMailPostException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			PostData = info.GetString("PostData");
		}

		[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null) throw new ArgumentNullException("info");

			info.AddValue("PostData", this.PostData);

			base.GetObjectData(info, context);
		}
	}
}
