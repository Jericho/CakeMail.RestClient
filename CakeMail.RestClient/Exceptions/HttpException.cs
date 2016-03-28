using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace CakeMail.RestClient.Exceptions
{
	/// <summary>
	/// This class represents an HTTP transport error. This is not an error returned
	/// by the web service itself. As such, it is a IOException.
	/// </summary>
	[Serializable]
	public class HttpException : IOException
	{
		/// <summary>
		/// The HTTP status code returned by the web service.
		/// </summary>
		public HttpStatusCode HttpStatus { get; private set; }

		/// <summary>
		/// The URI queried by the web service.
		/// </summary>
		public Uri Uri { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="HttpException"/> class.
		/// </summary>
		/// <param name="message">A message describing the reason why the exception was thrown.</param>
		/// <param name="httpStatus">The HTTP status of the response that caused the exception.</param>
		/// <param name="uri">The URL queried.</param>
		public HttpException(string message, HttpStatusCode httpStatus, Uri uri)
			: base(message)
		{
			HttpStatus = httpStatus;
			Uri = uri;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HttpException"/> class.
		/// </summary>
		/// <param name="message">A message describing the reason why the exception was thrown.</param>
		/// <param name="httpStatus">The HTTP status of the response that caused the exception.</param>
		/// <param name="uri">The URL queried.</param>
		/// <param name="innerException">The underlying exception that caused this one.</param>
		public HttpException(string message, HttpStatusCode httpStatus, Uri uri, Exception innerException)
			: base(message, innerException)
		{
			HttpStatus = httpStatus;
			Uri = uri;
		}

		[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
		// Constructor should be protected for unsealed classes, private for sealed classes.
		// (The Serializer invokes this constructor through reflection, so it can be private)
		protected HttpException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			HttpStatus = (HttpStatusCode)info.GetInt32("HttpStatus");
			Uri = new Uri(info.GetString("Uri"));
		}

		[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null) throw new ArgumentNullException("info");

			info.AddValue("HttpStatus", ((int)this.HttpStatus).ToString(CultureInfo.InvariantCulture));
			info.AddValue("Uri", this.Uri.PathAndQuery);

			base.GetObjectData(info, context);
		}
	}
}
