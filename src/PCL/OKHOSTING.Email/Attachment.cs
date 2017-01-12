namespace OKHOSTING.Email
{
	/// <summary>
	/// An email message file attachement
	/// </summary>
	public class Attachment
	{
		public string Name;

		public Message Message;

		public byte[] Content;

		/// <summary>
		/// Returns the name of the attachment
		/// </summary>
		public override string ToString()
		{
			return Name;
		}
	}
}