using System;
using System.Collections.Generic;

namespace OKHOSTING.Email
{
	/// <summary>
	/// An email message that can be stored in the database, can be sent or can be recieved
	/// </summary>
	public class Message
	{
		/// <summary>
		/// Initializes a new instance of the MimeKit.MimeMessage class.
		/// </summary>
		/// <param name="from">The list of addresses in the From header.</param>
		/// <param name="to">The list of addresses in the To header.</param>
		/// <param name="subject">The subject of the message.</param>
		/// <param name="body">The body of the message.</param>
		/// <remarks>Creates a new MIME message, specifying details at creation time.</remarks>
		public Message(IEnumerable<Address> from, IEnumerable<Address> to, string subject, string textBody)
		{
			From.AddRange(from);
			To.AddRange(to);
			Subject = subject;
			TextBody = textBody;
		}

		/// <summary>
		/// Initializes a new instance of the MimeKit.MimeMessage class.
		/// </summary>
		public Message()
		{
		}

		/// <summary>
		/// Gets or sets the message identifier.
		/// </summary>
		/// <remarks>
		/// The Message-Id is meant to be a globally unique identifier for a message.
		/// MimeKit.Utils.MimeUtils.GenerateMessageId can be used to generate this value.
		/// </remarks>
		public string MessageId { get; set; }

		/// <summary>
		/// Gets or sets the subject of the message.
		/// </summary>
		/// <remarks>
		/// The Subject is typically a short string denoting the topic of the message.
		/// Replies will often use "Re: " followed by the Subject of the original message.
		/// </remarks>
		public string Subject { get; set; }

		/// <summary>
		/// Gets or sets the text body of the message.
		/// </summary>
		public string TextBody { get; set; }

		/// <summary>
		/// Gets or sets the html body of the message
		/// </summary>
		public string HtmlBody { get; set; }

		/// <summary>
		/// Gets or sets the address in the Sender header.
		/// </summary>
		/// <remarks>
		/// The sender may differ from the addresses in MimeKit.MimeMessage.From if the message
		/// was sent by someone on behalf of someone else.
		/// </remarks>
		public Address Sender { get; set; }

		/// <summary>
		/// Gets the list of addresses in the From header.
		/// </summary>
		/// <remarks>
		/// The "From" header specifies the author(s) of the message.
		/// If more than one MimeKit.MailboxAddress is added to the list of "From" addresses,
		/// the MimeKit.MimeMessage.Sender should be set to the single MimeKit.MailboxAddress
		/// of the personal actually sending the message.
		/// </remarks>
		public readonly List<Address> From = new List<Address>();

		/// <summary>
		/// Gets the list of addresses in the To header.
		/// </summary>
		/// <remarks>
		/// The addresses in the To header are the primary recipients of the message.
		/// </remarks>
		public readonly List<Address> To = new List<Address>();

		/// <summary>
		/// Gets the list of addresses in the Cc header.
		/// </summary>
		/// <remarks>
		/// The addresses in the Cc header are secondary recipients of the message and are
		/// usually not the individuals being directly addressed in the content of the message.
		/// </remarks>
		public readonly List<Address> Cc = new List<Address>();

		/// <summary>
		/// Gets the list of addresses in the Bcc header.
		/// </summary>
		/// <remarks>
		/// Recipients in the Blind-Carpbon-Copy list will not be visible to the other recipients
		/// of the message.
		/// </remarks>
		public readonly List<Address> Bcc = new List<Address>();

		/// <summary>
		/// Gets the list of addresses in the Reply-To header.
		/// </summary>
		/// <remarks>
		///	 When the list of addresses in the Reply-To header is not empty, it contains the
		///	 address(es) where the author(s) of the message prefer that replies be sent.
		///	 When the list of addresses in the Reply-To header is empty, replies should be
		///	 sent to the mailbox(es) specified in the From header.
		/// </remarks>
		public readonly List<Address> ReplyTo = new List<Address>();

		/// <summary>
		/// Gets or sets the date of the message.
		/// </summary>
		/// <remarks>
		/// If the date is not explicitly set before the message is written to a stream,
		/// the date will default to the exact moment when it is written to said stream.
		/// </remarks>
		public DateTimeOffset Date { get; set; }

		/// <summary>
		/// Gets the list of headers.
		/// </summary>
		/// <remarks>
		/// Represents the list of headers for a message. Typically, the headers of a message
		/// will contain transmission headers such as From and To along with metadata headers
		/// such as Subject and Date, but may include just about anything.
		/// To access any MIME headers other than MimeKit.HeaderId.MimeVersion, you will
		/// need to access the MimeKit.MimeEntity.Headers property of the MimeKit.MimeMessage.Body.
		/// </remarks>
		public readonly List<Header> Headers = new List<Header>();

		/// <summary>
		/// Get or set the value of the Importance header.
		/// </summary>
		public MessageImportance Importance { get; set; }

		/// <summary>
		/// Get or set the value of the Priority header.
		/// </summary>
		public MessagePriority Priority { get; set; }

		/// <summary>
		/// Gets or sets the MIME-Version.
		/// </summary>
		/// <remarks>
		///	 The MIME-Version header specifies the version of the MIME specification that
		///	 the message was created for.
		/// </remarks>
		public Version MimeVersion { get; set; }

		/// <summary>
		/// Gets or sets the Message-Id that this message is in reply to.
		/// </summary>
		/// <remarks>
		/// If the message is a reply to another message, it will typically use the In-Reply-To
		/// header to specify the Message-Id of the original message being replied to.
		/// </remarks>
		public string InReplyTo { get; set; }

		/// <summary>
		/// Gets or sets the list of references to other messages.
		/// </summary>
		/// <remarks>
		/// The References header contains a chain of Message-Ids back to the original message
		/// that started the thread.
		/// </remarks>
		public readonly List<string> References = new List<string>();

		/// <summary>
		/// Gets the attachments.
		/// </summary>
		public readonly List<Attachment> Attachments = new List<Attachment>();

		#region Resent

		/// <summary>
		/// Gets or sets the Resent-Message-Id header.
		/// </summary>
		/// <remarks>
		/// The Resent-Message-Id is meant to be a globally unique identifier for a message.
		/// MimeKit.Utils.MimeUtils.GenerateMessageId can be used to generate this value.
		/// </remarks>
		public string ResentMessageId { get; set; }

		/// <summary>
		/// Gets or sets the address in the Resent-Sender header.
		/// </summary>
		/// <remarks>
		/// The resent sender may differ from the addresses in MimeKit.MimeMessage.ResentFrom
		/// if the message was sent by someone on behalf of someone else.
		/// </remarks>
		public Address ResentSender { get; set; }

		/// <summary>
		/// Gets the list of addresses in the Resent-From header.
		/// </summary>
		/// <remarks>
		/// The "Resent-From" header specifies the author(s) of the messagebeing resent.
		/// If more than one MimeKit.MailboxAddress is added to the list of "Resent-From"
		/// addresses, the MimeKit.MimeMessage.ResentSender should be set to the single MimeKit.MailboxAddress
		/// of the personal actually sending the message.
		/// </remarks>
		public readonly List<Address> ResentFrom = new List<Address>();

		/// <summary>
		/// Gets the list of addresses in the Resent-To header.
		/// </summary>
		/// <remarks>
		/// The addresses in the Resent-To header are the primary recipients of the message.
		/// </remarks>
		public readonly List<Address> ResentTo = new List<Address>();

		/// <summary>
		/// Gets the list of addresses in the Resent-Cc header.
		/// </summary>
		/// <remarks>
		/// The addresses in the Resent-Cc header are secondary recipients of the message
		/// and are usually not the individuals being directly addressed in the content of
		/// the message.
		/// </remarks>
		public readonly List<Address> ResentCc = new List<Address>();

		/// <summary>
		/// Gets the list of addresses in the Resent-Bcc header.
		/// </summary>
		/// <remarks>
		/// Recipients in the Resent-Bcc list will not be visible to the other recipients
		/// of the message.
		/// </remarks>
		public readonly List<Address> ResentBcc = new List<Address>();

		/// <summary>
		/// Gets the list of addresses in the Resent-Reply-To header.
		/// </summary>
		/// <remarks>
		/// When the list of addresses in the Resent-Reply-To header is not empty, it contains
		/// the address(es) where the author(s) of the resent message prefer that replies
		/// be sent.
		/// When the list of addresses in the Resent-Reply-To header is empty, replies should
		/// be sent to the mailbox(es) specified in the Resent-From header.
		/// </remarks>
		public readonly List<Address> ResentReplyTo = new List<Address>();

		/// <summary>
		/// Gets or sets the Resent-Date of the message.
		/// </summary>
		public DateTimeOffset ResentDate { get; set; }

		#endregion
	}
}