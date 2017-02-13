using System;

namespace CakeMail.RestClient.Exceptions
{
	/// <summary>
	/// This class represents an exception thrown by the CakeMail API when the posted data is determined to be invalid.
	/// </summary>
	public class CakeMailPostException : CakeMailException
	{
		/// <summary>
		/// Gets or sets the data that was posted
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
	}
}
