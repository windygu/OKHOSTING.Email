using System;
using System.Collections.Generic;
using System.Linq;

namespace OKHOSTING.Email.Shared.MailKit
{
	public static class Parser
	{
		public static SecureSocketOptions Parse(global::MailKit.Security.SecureSocketOptions value)
		{
			return (SecureSocketOptions) Enum.Parse(typeof(SecureSocketOptions), value.ToString());
		}

		public static global::MailKit.Security.SecureSocketOptions Parse(SecureSocketOptions value)
		{
			return (global::MailKit.Security.SecureSocketOptions) Enum.Parse(typeof(global::MailKit.Security.SecureSocketOptions), value.ToString());
		}

		public static MessageImportance Parse(MimeKit.MessageImportance value)
		{
			return (MessageImportance) Enum.Parse(typeof(MessageImportance), value.ToString());
		}

		public static MimeKit.MessageImportance Parse(MessageImportance value)
		{
			return (MimeKit.MessageImportance) Enum.Parse(typeof(MimeKit.MessageImportance), value.ToString());
		}

		public static MessagePriority Parse(MimeKit.MessagePriority value)
		{
			return (MessagePriority) Enum.Parse(typeof(MessagePriority), value.ToString());
		}

		public static MimeKit.MessagePriority Parse(MessagePriority value)
		{
			return (MimeKit.MessagePriority) Enum.Parse(typeof(MimeKit.MessagePriority), value.ToString());
		}

		public static HeaderId Parse(MimeKit.HeaderId value)
		{
			return (HeaderId) Enum.Parse(typeof(HeaderId), value.ToString());
		}

		public static MimeKit.HeaderId Parse(HeaderId value)
		{
			return (MimeKit.HeaderId) Enum.Parse(typeof(MimeKit.HeaderId), value.ToString());
		}

		public static Message Parse(MimeKit.MimeMessage content)
		{
			Message message = new Message();

			foreach (MimeKit.MailboxAddress temp in content.From)
			{
				message.From.Add(Parse(temp));
			}

			foreach (MimeKit.MailboxAddress temp in content.To)
			{
				message.To.Add(Parse(temp));
			}

			foreach (MimeKit.MailboxAddress temp in content.Cc)
			{
				message.Cc.Add(Parse(temp));
			}

			foreach (MimeKit.MailboxAddress temp in content.Bcc)
			{
				message.Bcc.Add(Parse(temp));
			}

			foreach (MimeKit.MailboxAddress temp in content.ReplyTo)
			{
				message.ReplyTo.Add(Parse(temp));
			}

		   
			foreach (MimeKit.MailboxAddress temp in content.ResentFrom)
			{
				message.ResentFrom.Add(Parse(temp));
			}


			foreach (MimeKit.MailboxAddress temp in content.ResentBcc)
			{
				message.ResentBcc.Add(Parse(temp));
			}

			foreach (MimeKit.MailboxAddress temp in content.ResentCc)
			{
				message.ResentCc.Add(Parse(temp));
			}

			foreach (MimeKit.MailboxAddress temp in content.ResentReplyTo)
			{
				message.ResentReplyTo.Add(Parse(temp));
			}

			foreach (MimeKit.Header temp in content.Headers)
			{
				message.Headers.Add(Parse(temp));
			}

			//info
			message.Sender = Parse(content.Sender);
			message.Subject = content.Subject;
			message.TextBody = content.TextBody;
			message.HtmlBody = content.HtmlBody;
			message.Date = content.Date;
			message.InReplyTo = content.InReplyTo;
			message.Importance = Parse(content.Importance);
			message.Priority = Parse(content.Priority);

			message.ResentMessageId = content.ResentMessageId;
			message.ResentDate = content.ResentDate;
			message.ResentSender = Parse(content.ResentSender);

			//attachments
			foreach (MimeKit.MimeEntity temp in content.Attachments)
			{
				message.Attachments.Add(Parse(temp));
			}

			return message;
		}

		public static MimeKit.MimeMessage Parse(Message content)
		{
			MimeKit.MimeMessage message = new MimeKit.MimeMessage();

			foreach (Address temp in content.From)
			{
				message.From.Add(Parse(temp));
			}

			foreach (Address temp in content.To)
			{
				message.To.Add(Parse(temp));
			}

			foreach (Address temp in content.Cc)
			{
				message.Cc.Add(Parse(temp));
			}

			foreach (Address temp in content.Bcc)
			{
				message.Bcc.Add(Parse(temp));
			}

			foreach (Address temp in content.ReplyTo)
			{
				message.ReplyTo.Add(Parse(temp));
			}

			foreach (Address temp in content.ResentFrom)
			{
				message.ResentFrom.Add(Parse(temp));
			}

			foreach (Address temp in content.ResentBcc)
			{
				message.ResentBcc.Add(Parse(temp));
			}

			foreach (Address temp in content.ResentCc)
			{
				message.ResentCc.Add(Parse(temp));
			}

			foreach (Address temp in content.ResentReplyTo)
			{
				message.ResentReplyTo.Add(Parse(temp));
			}

			foreach (Header temp in content.Headers)
			{
				message.Headers.Add(Parse(temp));
			}

			message.Subject = content.Subject;
			message.Sender = Parse(content.Sender);
			message.Date = content.Date;
			message.InReplyTo = content.InReplyTo;
			message.Importance = Parse(content.Importance);
			message.Priority = Parse(content.Priority);
			message.ResentDate = content.ResentDate;
			message.ResentSender = Parse(content.ResentSender);
			message.References.AddRange(content.References);

			if (content.MessageId != null)
			{
				message.MessageId = content.MessageId;
			}

			if (content.ResentMessageId != null)
			{
				message.ResentMessageId = content.ResentMessageId;
			}

			if (content.MimeVersion != null)
			{
				message.MimeVersion = content.MimeVersion;
			}

			MimeKit.BodyBuilder builder = new MimeKit.BodyBuilder();
			builder.TextBody = content.TextBody;
			builder.HtmlBody = content.HtmlBody;

			//attachments
			foreach (Attachment temp in content.Attachments)
			{
				builder.Attachments.Add(temp.Name, temp.Content);
			}

			message.Body = builder.ToMessageBody();

			return message;
		}

		private static Address Parse(MimeKit.MailboxAddress address)
		{
			return new Address(address.Address, address.Name);
		}

		private static MimeKit.MailboxAddress Parse(Address address)
		{
			if (address == null)
			{
				return null;
			}

			return new MimeKit.MailboxAddress(address.Name, address.FullAddress);
		}

		/// <summary>
		/// Reads a anmar.SharpMimeTools.SharpAttachment and extracts the name and content
		/// </summary>
		public static Attachment Parse(MimeKit.MimeEntity att)
		{
			Attachment newAtt = new Attachment();

			global::System.IO.MemoryStream memory = new global::System.IO.MemoryStream();
			att.WriteTo(memory);
			newAtt.Content = memory.ToArray();

			newAtt.Name = att.ContentDisposition.FileName;

			return newAtt;
		}

		private static MimeKit.Header Parse(Header header)
		{
			return new MimeKit.Header(Parse(header.Id), header.Value);
		}

		public static Header Parse(MimeKit.Header header)
		{
			return new Header() { Id = Parse(header.Id), Value = header.Value };
		}
	}
}