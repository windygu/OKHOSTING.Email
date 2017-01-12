using System;
using System.Text;
using System.Threading;

namespace OKHOSTING.Email
{
	public interface ISmtpClient : IDisposable
	{
		//
		// Summary:
		//	 Get whether or not the client is currently authenticated with the SMTP server.
		//
		// Remarks:
		//	 Gets whether or not the client is currently authenticated with the SMTP server.
		//	 To authenticate with the SMTP server, use one of the Authenticate methods.
		bool IsAuthenticated { get; }
		//
		// Summary:
		//	 Get whether or not the client is currently connected to an SMTP server.
		//
		// Remarks:
		//	 When a MailKit.Net.Smtp.SmtpProtocolException is caught, the connection state
		//	 of the MailKit.Net.Smtp.SmtpClient should be checked before continuing.
		bool IsConnected { get; }
		//
		// Summary:
		//	 Get whether or not the connection is secure (typically via SSL or TLS).
		//
		// Remarks:
		//	 Gets whether or not the connection is secure (typically via SSL or TLS).
		bool IsSecure { get; }
		//
		// Summary:
		//	 Gets or sets the local domain.
		//
		// Remarks:
		//	 The local domain is used in the HELO or EHLO commands sent to the SMTP server.
		//	 If left unset, the local IP address will be used instead.
		string LocalDomain { get; set; }
		//
		// Summary:
		//	 Get the maximum message size supported by the server.
		//
		// Remarks:
		//	 The maximum message size will not be known until a successful connection has
		//	 been made and may change once the client is authenticated.
		//	 This value is only relevant if the MailKit.Net.Smtp.SmtpClient.Capabilities includes
		//	 the MailKit.Net.Smtp.SmtpCapabilities.Size flag.
		uint MaxSize { get; }
		//
		// Summary:
		//	 Gets an object that can be used to synchronize access to the SMTP server.
		//
		// Remarks:
		//	 Gets an object that can be used to synchronize access to the SMTP server.
		//	 When using the non-Async methods from multiple threads, it is important to lock
		//	 the MailKit.Net.Smtp.SmtpClient.SyncRoot object for thread safety when using
		//	 the synchronous methods.
		object SyncRoot { get; }
		//
		// Summary:
		//	 Get or set the timeout for network streaming operations, in milliseconds.
		//
		// Remarks:
		//	 Gets or sets the underlying socket stream's System.IO.Stream.ReadTimeout and
		//	 System.IO.Stream.WriteTimeout values.
		int Timeout { get; set; }

		//
		// Summary:
		//	 Authenticates using the supplied credentials.
		//
		// Parameters:
		//   encoding:
		//	 The text encoding to use for the user's credentials.
		//
		//   credentials:
		//	 The user's credentials.
		//
		//   cancellationToken:
		//	 The cancellation token.
		//
		// Exceptions:
		//   T:System.ArgumentNullException:
		//	 encoding is null.
		//	 -or-
		//	 credentials is null.
		//
		//   T:MailKit.ServiceNotConnectedException:
		//	 The MailKit.Net.Smtp.SmtpClient is not connected.
		//
		//   T:System.InvalidOperationException:
		//	 The MailKit.Net.Smtp.SmtpClient is already authenticated.
		//
		//   T:System.NotSupportedException:
		//	 The SMTP server does not support authentication.
		//
		//   T:System.OperationCanceledException:
		//	 The operation was canceled via the cancellation token.
		//
		//   T:MailKit.Security.AuthenticationException:
		//	 Authentication using the supplied credentials has failed.
		//
		//   T:MailKit.Security.SaslException:
		//	 A SASL authentication error occurred.
		//
		//   T:System.IO.IOException:
		//	 An I/O error occurred.
		//
		//   T:MailKit.Net.Smtp.SmtpCommandException:
		//	 The SMTP command failed.
		//
		//   T:MailKit.Net.Smtp.SmtpProtocolException:
		//	 An SMTP protocol error occurred.
		//
		// Remarks:
		//	 If the SMTP server supports authentication, then the SASL mechanisms that both
		//	 the client and server support are tried in order of greatest security to weakest
		//	 security. Once a SASL authentication mechanism is found that both client and
		//	 server support, the credentials are used to authenticate.
		//	 If, on the other hand, authentication is not supported by the SMTP server, then
		//	 this method will throw System.NotSupportedException. The MailKit.Net.Smtp.SmtpClient.Capabilities
		//	 property can be checked for the MailKit.Net.Smtp.SmtpCapabilities.Authentication
		//	 flag to make sure the SMTP server supports authentication before calling this
		//	 method.
		//	 To prevent the usage of certain authentication mechanisms, simply remove them
		//	 from the MailKit.Net.Smtp.SmtpClient.AuthenticationMechanisms hash set before
		//	 calling this method.
		void Authenticate(Encoding encoding, System.Net.ICredentials credentials, CancellationToken cancellationToken = default(CancellationToken));
		//
		// Summary:
		//	 Establishes a connection to the specified SMTP or SMTP/S server.
		//
		// Parameters:
		//   host:
		//	 The host name to connect to.
		//
		//   port:
		//	 The port to connect to. If the specified port is 0, then the default port will
		//	 be used.
		//
		//   options:
		//	 The secure socket options to when connecting.
		//
		//   cancellationToken:
		//	 The cancellation token.
		//
		// Exceptions:
		//   T:System.ArgumentNullException:
		//	 host is null.
		//
		//   T:System.ArgumentOutOfRangeException:
		//	 port is not between 0 and 65535.
		//
		//   T:System.ArgumentException:
		//	 The host is a zero-length string.
		//
		//   T:System.ObjectDisposedException:
		//	 The MailKit.Net.Smtp.SmtpClient has been disposed.
		//
		//   T:System.InvalidOperationException:
		//	 The MailKit.Net.Smtp.SmtpClient is already connected.
		//
		//   T:System.NotSupportedException:
		//	 options was set to MailKit.Security.SecureSocketOptions.StartTls and the SMTP
		//	 server does not support the STARTTLS extension.
		//
		//   T:System.OperationCanceledException:
		//	 The operation was canceled.
		//
		//   T:System.IO.IOException:
		//	 An I/O error occurred.
		//
		//   T:MailKit.Net.Smtp.SmtpCommandException:
		//	 An SMTP command failed.
		//
		//   T:MailKit.Net.Smtp.SmtpProtocolException:
		//	 An SMTP protocol error occurred.
		//
		// Remarks:
		//	 Establishes a connection to the specified SMTP or SMTP/S server.
		//	 If the port has a value of 0, then the options parameter is used to determine
		//	 the default port to connect to. The default port used with MailKit.Security.SecureSocketOptions.SslOnConnect
		//	 is 465. All other values will use a default port of 25.
		//	 If the options has a value of MailKit.Security.SecureSocketOptions.Auto, then
		//	 the port is used to determine the default security options. If the port has a
		//	 value of 465, then the default options used will be MailKit.Security.SecureSocketOptions.SslOnConnect.
		//	 All other values will use MailKit.Security.SecureSocketOptions.StartTlsWhenAvailable.
		//	 Once a connection is established, properties such as MailKit.Net.Smtp.SmtpClient.AuthenticationMechanisms
		//	 and MailKit.Net.Smtp.SmtpClient.Capabilities will be populated.
		//	 The connection established by any of the Connect methods may be re-used if an
		//	 application wishes to send multiple messages to the same SMTP server. Since connecting
		//	 and authenticating can be expensive operations, re-using a connection can significantly
		//	 improve performance when sending a large number of messages to the same SMTP
		//	 server over a short period of time.
		void Connect(string host, int port = 0, SecureSocketOptions options = SecureSocketOptions.Auto, CancellationToken cancellationToken = default(CancellationToken));
		//
		// Summary:
		//	 Disconnect the service.
		//
		// Parameters:
		//   quit:
		//	 If set to true, a QUIT command will be issued in order to disconnect cleanly.
		//
		//   cancellationToken:
		//	 The cancellation token.
		//
		// Exceptions:
		//   T:System.ObjectDisposedException:
		//	 The MailKit.Net.Smtp.SmtpClient has been disposed.
		//
		// Remarks:
		//	 If quit is true, a QUIT command will be issued in order to disconnect cleanly.
		void Disconnect(bool quit, CancellationToken cancellationToken = default(CancellationToken));
		//
		// Summary:
		//	 Ping the SMTP server to keep the connection alive.
		//
		// Parameters:
		//   cancellationToken:
		//	 The cancellation token.
		//
		// Exceptions:
		//   T:System.ObjectDisposedException:
		//	 The MailKit.Net.Smtp.SmtpClient has been disposed.
		//
		//   T:MailKit.ServiceNotConnectedException:
		//	 The MailKit.Net.Smtp.SmtpClient is not connected.
		//
		//   T:System.OperationCanceledException:
		//	 The operation was canceled.
		//
		//   T:System.IO.IOException:
		//	 An I/O error occurred.
		//
		//   T:MailKit.Net.Smtp.SmtpCommandException:
		//	 The SMTP command failed.
		//
		//   T:MailKit.Net.Smtp.SmtpProtocolException:
		//	 An SMTP protocol error occurred.
		//
		// Remarks:
		//	 Mail servers, if left idle for too long, will automatically drop the connection.
		void NoOp(CancellationToken cancellationToken = default(CancellationToken));
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