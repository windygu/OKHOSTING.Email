using System;
using global::System.Net.Mail;

namespace OKHOSTING.Email.Shared.System.Net.Mail
{
	public static class Parser
	{
		/// <summary>
		/// Parses a System.Net.Mail.MailAddress and extracts the DisplayName and Email out of it
		/// </summary>
		/// <param name="address">A System.Net.Mail.MailAddress that contains an e-mail address</param>
		public static Address Parse(MailAddress address)
		{
			return new Address(address.Address, address.DisplayName);
		}

		/// <summary>
		/// Creates a new instance of System.Net.Mail.MailAddress that can be used to send emails
		/// </summary>
		public static MailAddress Parse(Address address)
		{
			return new MailAddress(address.FullAddress, address.Name);
		}

		/// <summary>
		/// Reads a System.Net.Mail.MailMessage and extracts all it's information
		/// </summary>
		/// <param name="content">System.Net.Mail.MailMessage email message</param>
		public static Message Parse(MailMessage content)
		{
			Message message = new Message();

			foreach (MailAddress temp in content.To)
			{
				message.To.Add(Parse(temp));
			}

			foreach (MailAddress temp in content.CC)
			{
				message.Cc.Add(Parse(temp));
			}

			foreach (MailAddress temp in content.Bcc)
			{
				message.Bcc.Add(Parse(temp));
			}

			foreach (MailAddress temp in content.ReplyToList)
			{
				message.ReplyTo.Add(Parse(temp));
			}

			foreach (var temp in content.Headers.AllKeys)
			{
				Header header = new Header();
				header.Field = temp;
				header.Value = content.Headers[temp];

				message.Headers.Add(header);
			}

			if (content.IsBodyHtml)
			{
				message.HtmlBody = content.Body;
			}
			else
			{
				message.TextBody = content.Body;
			}

			message.Subject = content.Subject;
			message.Sender = Parse(content.Sender);
			message.Priority = Parse(content.Priority);
			//message.Date = DateTime.Now;

			//attachments
			foreach (global::System.Net.Mail.Attachment temp in content.Attachments)
			{
				message.Attachments.Add(Parse(temp));
			}

			return message;
		}

		/// <summary>
		/// Creates a new System.Net.Mail.MailMessage containing all the information of the current Message
		/// </summary>
		/// <returns></returns>
		public static MailMessage Parse(Message content)
		{
			MailMessage message = new MailMessage();

			foreach (Address temp in content.To)
			{
				message.To.Add(Parse(temp));
			}

			foreach (Address temp in content.Cc)
			{
				message.CC.Add(Parse(temp));
			}

			foreach (Address temp in content.Bcc)
			{
				message.Bcc.Add(Parse(temp));
			}

			foreach (Address temp in content.ReplyTo)
			{
				message.ReplyToList.Add(Parse(temp));
			}

			foreach (Header temp in content.Headers)
			{
				message.Headers.Add(temp.Field, temp.Value);
			}

			if (!string.IsNullOrWhiteSpace(content.HtmlBody))
			{
				message.Body = content.HtmlBody;
				message.IsBodyHtml = true;
			}
			else
			{
				message.Body = content.TextBody;
				message.IsBodyHtml = false;
			}

			message.Subject = content.Subject;
			message.Sender = Parse(content.Sender);
			message.Priority = Parse(content.Priority);

			//attachments
			foreach (Attachment temp in content.Attachments)
			{
				message.Attachments.Add(Parse(temp));
			}

			return message;
		}

		private static MessagePriority Parse(MailPriority priority)
		{
			switch (priority)
			{
				case MailPriority.Low:
					return MessagePriority.NonUrgent;

				case MailPriority.Normal:
					return MessagePriority.Normal;

				case MailPriority.High:
					return MessagePriority.Urgent;

				default:
					return MessagePriority.Normal;
			}
		}

		private static MailPriority Parse(MessagePriority priority)
		{
			switch (priority)
			{
				case MessagePriority.NonUrgent:
					return MailPriority.Low;

				case MessagePriority.Normal:
					return MailPriority.Normal;

				case MessagePriority.Urgent:
					return MailPriority.High;

				default:
					return MailPriority.Normal;
			}
		}

		/// <summary>
		/// Reads a System.Net.Mail.Attachment and extracts the name and content
		/// </summary>
		public static Attachment Parse(global::System.Net.Mail.Attachment att)
		{
			Attachment newAtt = new Attachment();
			newAtt.Name = att.Name;
			global::System.IO.MemoryStream memory = new global::System.IO.MemoryStream();
			att.ContentStream.CopyTo(memory);
			newAtt.Content = memory.ToArray();

			return newAtt;
		}

		public static global::System.Net.Mail.Attachment Parse(Attachment att)
		{
			return new global::System.Net.Mail.Attachment(new global::System.IO.MemoryStream(att.Content), att.Name);
		}
	}
}