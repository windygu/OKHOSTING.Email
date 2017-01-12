#region Assembly MimeKit, Version=1.2.0.0, Culture=neutral, PublicKeyToken=bede1c8a46c66814
// C:\Desarrollo\OKHOSTING.Email\src\packages\MimeKit.1.2.22\lib\net45\MimeKit.dll
#endregion

namespace OKHOSTING.Email
{
	//
	// Summary:
	//	 An enumeration of common header fields.
	//
	// Remarks:
	//	 Comparing enum values is not only faster, but less error prone than comparing
	//	 strings.
	public enum HeaderId
	{
		//
		// Summary:
		//	 An unknown header field.
		Unknown = -1,
		//
		// Summary:
		//	 The Ad-Hoc header field.
		AdHoc = 0,
		//
		// Summary:
		//	 The Apparently-To header field.
		ApparentlyTo = 1,
		//
		// Summary:
		//	 The Approved header field.
		Approved = 2,
		//
		// Summary:
		//	 The Article header field.
		Article = 3,
		//
		// Summary:
		//	 The Bcc header field.
		Bcc = 4,
		//
		// Summary:
		//	 The Bytes header field.
		Bytes = 5,
		//
		// Summary:
		//	 The Cc header field.
		Cc = 6,
		//
		// Summary:
		//	 The Comments header field.
		Comments = 7,
		//
		// Summary:
		//	 The Content-Base header field.
		ContentBase = 8,
		//
		// Summary:
		//	 The Content-Class header field.
		ContentClass = 9,
		//
		// Summary:
		//	 The Content-Description header field.
		ContentDescription = 10,
		//
		// Summary:
		//	 The Content-Disposition header field.
		ContentDisposition = 11,
		//
		// Summary:
		//	 The Content-Duration header field.
		ContentDuration = 12,
		//
		// Summary:
		//	 The Content-Id header field.
		ContentId = 13,
		//
		// Summary:
		//	 The Content-Language header field.
		ContentLanguage = 14,
		//
		// Summary:
		//	 The Content-Length header field.
		ContentLength = 15,
		//
		// Summary:
		//	 The Content-Location header field.
		ContentLocation = 16,
		//
		// Summary:
		//	 The Content-Md5 header field.
		ContentMd5 = 17,
		//
		// Summary:
		//	 The Content-Transfer-Encoding header field.
		ContentTransferEncoding = 18,
		//
		// Summary:
		//	 The Content-Type header field.
		ContentType = 19,
		//
		// Summary:
		//	 The Control header field.
		Control = 20,
		//
		// Summary:
		//	 The Date header field.
		Date = 21,
		//
		// Summary:
		//	 The Deferred-Delivery header field.
		DeferredDelivery = 22,
		//
		// Summary:
		//	 The Disposition-Notification-Options header field.
		DispositionNotificationOptions = 23,
		//
		// Summary:
		//	 The Disposition-Notification-To header field.
		DispositionNotificationTo = 24,
		//
		// Summary:
		//	 The Distribution header field.
		Distribution = 25,
		//
		// Summary:
		//	 The DKIM-Signature header field.
		DkimSignature = 26,
		//
		// Summary:
		//	 The DomainKey-Signature header field.
		DomainKeySignature = 27,
		//
		// Summary:
		//	 The Encoding header field.
		Encoding = 28,
		//
		// Summary:
		//	 The Encrypted header field.
		Encrypted = 29,
		//
		// Summary:
		//	 The Expires header field.
		Expires = 30,
		//
		// Summary:
		//	 The Expiry-Date header field.
		ExpiryDate = 31,
		//
		// Summary:
		//	 The Followup-To header field.
		FollowupTo = 32,
		//
		// Summary:
		//	 The From header field.
		From = 33,
		//
		// Summary:
		//	 The Importance header field.
		Importance = 34,
		//
		// Summary:
		//	 The In-Reply-To header field.
		InReplyTo = 35,
		//
		// Summary:
		//	 The Keywords header field.
		Keywords = 36,
		//
		// Summary:
		//	 The Lines header field.
		Lines = 37,
		//
		// Summary:
		//	 The List-Help header field.
		ListHelp = 38,
		//
		// Summary:
		//	 The List-Subscribe header field.
		ListSubscribe = 39,
		//
		// Summary:
		//	 The List-Unsubscribe header field.
		ListUnsubscribe = 40,
		//
		// Summary:
		//	 The Message-Id header field.
		MessageId = 41,
		//
		// Summary:
		//	 The MIME-Version header field.
		MimeVersion = 42,
		//
		// Summary:
		//	 The Newsgroups header field.
		Newsgroups = 43,
		//
		// Summary:
		//	 The Nntp-Posting-Host header field.
		NntpPostingHost = 44,
		//
		// Summary:
		//	 The Organization header field.
		Organization = 45,
		//
		// Summary:
		//	 The Original-Recipient header field.
		OriginalRecipient = 46,
		//
		// Summary:
		//	 The Path header field.
		Path = 47,
		//
		// Summary:
		//	 The Precedence header field.
		Precedence = 48,
		//
		// Summary:
		//	 The Priority header field.
		Priority = 49,
		//
		// Summary:
		//	 The Received header field.
		Received = 50,
		//
		// Summary:
		//	 The References header field.
		References = 51,
		//
		// Summary:
		//	 The Reply-By header field.
		ReplyBy = 52,
		//
		// Summary:
		//	 The Reply-To header field.
		ReplyTo = 53,
		//
		// Summary:
		//	 The Resent-Bcc header field.
		ResentBcc = 54,
		//
		// Summary:
		//	 The Resent-Cc header field.
		ResentCc = 55,
		//
		// Summary:
		//	 The Resent-Date header field.
		ResentDate = 56,
		//
		// Summary:
		//	 The Resent-From header field.
		ResentFrom = 57,
		//
		// Summary:
		//	 The Resent-Message-Id header field.
		ResentMessageId = 58,
		//
		// Summary:
		//	 The Resent-Reply-To header field.
		ResentReplyTo = 59,
		//
		// Summary:
		//	 The Resent-Sender header field.
		ResentSender = 60,
		//
		// Summary:
		//	 The Resent-To header field.
		ResentTo = 61,
		//
		// Summary:
		//	 The Return-Path header field.
		ReturnPath = 62,
		//
		// Summary:
		//	 The Return-Receipt-To header field.
		ReturnReceiptTo = 63,
		//
		// Summary:
		//	 The Sender header field.
		Sender = 64,
		//
		// Summary:
		//	 The Sensitivity header field.
		Sensitivity = 65,
		//
		// Summary:
		//	 The Status header field.
		Status = 66,
		//
		// Summary:
		//	 The Subject header field.
		Subject = 67,
		//
		// Summary:
		//	 The Summary header field.
		Summary = 68,
		//
		// Summary:
		//	 The Supersedes header field.
		Supersedes = 69,
		//
		// Summary:
		//	 The To header field.
		To = 70,
		//
		// Summary:
		//	 The User-Agent header field.
		UserAgent = 71,
		//
		// Summary:
		//	 The X-Mailer header field.
		XMailer = 72,
		//
		// Summary:
		//	 The X-MSMail-Priority header field.
		XMSMailPriority = 73,
		//
		// Summary:
		//	 The X-Priority header field.
		XPriority = 74,
		//
		// Summary:
		//	 The X-Status header field.
		XStatus = 75
	}
}