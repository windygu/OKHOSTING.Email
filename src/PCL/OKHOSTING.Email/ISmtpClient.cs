using System;
using System.Text;
using System.Threading;

namespace OKHOSTING.Email
{
	public interface ISmtpClient : IClientBase
	{
		//
		// Summary:
		//	 Send the specified message.
		//
		// Parameters:
		//   options:
		//	 The formatting options.
		//
		//   message:
		//	 The message.
		//
		//   cancellationToken:
		//	 The cancellation token.
		//
		//   progress:
		//	 The progress reporting mechanism.
		//
		// Exceptions:
		//   T:System.ArgumentNullException:
		//	 options is null.
		//	 -or-
		//	 message is null.
		//
		//   T:System.ObjectDisposedException:
		//	 The MailKit.Net.Smtp.SmtpClient has been disposed.
		//
		//   T:MailKit.ServiceNotConnectedException:
		//	 The MailKit.Net.Smtp.SmtpClient is not connected.
		//
		//   T:MailKit.ServiceNotAuthenticatedException:
		//	 Authentication is required before sending a message.
		//
		//   T:System.InvalidOperationException:
		//	 A sender has not been specified.
		//	 -or-
		//	 No recipients have been specified.
		//
		//   T:System.NotSupportedException:
		//	 Internationalized formatting was requested but is not supported by the server.
		//
		//   T:System.OperationCanceledException:
		//	 The operation has been canceled.
		//
		//   T:System.IO.IOException:
		//	 An I/O error occurred.
		//
		//   T:MailKit.Net.Smtp.SmtpCommandException:
		//	 The SMTP command failed.
		//
		//   T:MailKit.Net.Smtp.SmtpProtocolException:
		//	 An SMTP protocol exception occurred.
		//
		// Remarks:
		//	 Sends the specified message.
		//	 The sender address is determined by checking the following message headers (in
		//	 order of precedence): Resent-Sender, Resent-From, Sender, and From.
		//	 If either the Resent-Sender or Resent-From addresses are present, the recipients
		//	 are collected from the Resent-To, Resent-Cc, and Resent-Bcc headers, otherwise
		//	 the To, Cc, and Bcc headers are used.
		void Send(Message message, CancellationToken cancellationToken = default(CancellationToken));
	}
}