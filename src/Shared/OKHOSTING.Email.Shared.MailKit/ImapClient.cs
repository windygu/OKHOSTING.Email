using System.Collections.Generic;
using System.Threading;

namespace OKHOSTING.Email.Shared.MailKit
{
	public class ImapClient : global::MailKit.Net.Imap.ImapClient, IImapClient
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

		public void DeleteAllMessages()
		{
			
		}

		public void DeleteMessages(IList<int> indexes)
		{
			base.DeleteMessages(indexes);
		}

		public IEnumerable<Message> GetMessages()
		{
			for(int i = 0; i < base.Count; i++)
			{
				var m = base.GetMessage(i);

				yield return Parser.Parse(m);
			}
		}

		public IEnumerable<Message> GetMessages(IList<int> indexes)
		{
			foreach (var m in base.GetMessages(indexes))
			{
				yield return Parser.Parse(m);
			}
		}
	}
} 