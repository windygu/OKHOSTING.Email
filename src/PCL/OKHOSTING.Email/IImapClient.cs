using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace OKHOSTING.Email
{
	public interface IImapClient : IClientBase
	{
		/// <summary>
		/// Returns a list of messages available in the mailbox
		/// </summary>
		IEnumerable<Message> GetMessages();

		IEnumerable<Message> GetMessages(IList<int> indexes);

		//
		// Resumen:
		//     Mark the specified message for deletion.
		//
		// Parámetros:
		//   index:
		//     The index of the message.
		//
		//   cancellationToken:
		//     The cancellation token.
		//
		// Excepciones:
		//   T:System.ArgumentOutOfRangeException:
		//     index is not a valid message index.
		//
		//   T:System.ObjectDisposedException:
		//     The MailKit.Net.Pop3.Pop3Client has been disposed.
		//
		//   T:MailKit.ServiceNotConnectedException:
		//     The MailKit.Net.Pop3.Pop3Client is not connected.
		//
		//   T:MailKit.ServiceNotAuthenticatedException:
		//     The MailKit.Net.Pop3.Pop3Client is not authenticated.
		//
		//   T:System.OperationCanceledException:
		//     The operation was canceled via the cancellation token.
		//
		//   T:System.IO.IOException:
		//     An I/O error occurred.
		//
		//   T:MailKit.Net.Pop3.Pop3CommandException:
		//     The POP3 command failed.
		//
		//   T:MailKit.Net.Pop3.Pop3ProtocolException:
		//     A POP3 protocol error occurred.
		//
		// Comentarios:
		//     Messages marked for deletion are not actually deleted until the session is cleanly
		//     disconnected (see MailKit.Net.Pop3.Pop3Client.Disconnect(System.Boolean,System.Threading.CancellationToken)).
		void DeleteMessages(IList<int> indexes);

		void DeleteAllMessages();
	}
}