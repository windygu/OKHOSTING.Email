using System.Threading;

namespace OKHOSTING.Email.Shared.MailKit
{
	public class SmtpClient : global::MailKit.Net.Smtp.SmtpClient, ISmtpClient
	{
		#if NETFX_CORE
		public void Authenticate(System.Text.Encoding encoding, System.Net.ICredentials credentials, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.Authenticate(Portable.Text.Encoding.GetEncoding(encoding.CodePage), credentials, cancellationToken);
		}
		#endif

		public void Connect(string host, int port = 0, SecureSocketOptions options = SecureSocketOptions.Auto, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.Connect(host, port, Parser.Parse(options), cancellationToken);
		}

		public void Send(Message message, CancellationToken cancellationToken = default(CancellationToken))
		{
			base.Send(Parser.Parse(message), cancellationToken, null);
		}
	}
} 