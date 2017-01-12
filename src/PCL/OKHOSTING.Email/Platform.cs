namespace OKHOSTING.Email
{
	public static class Platform
	{
		private static readonly string[] KnownPlatforms = new string[]
		{
			"Net4",
			"UWP",
			"Xamarin.Android",
			"Xamarin.iOS",
		};

		public static ISmtpClient CreateSmtpClient()
		{
			return Core.BaitAndSwitch.Create<ISmtpClient>(KnownPlatforms);
		}
	}
}