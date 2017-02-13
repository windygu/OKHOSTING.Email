using System.Linq;

namespace OKHOSTING.Email
{
	/// <summary>
	/// An internet email address
	/// </summary>
	public class Address
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="OKHOSTING.Email.Address"/> class.
		/// </summary>
		public Address() : this(null, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="OKHOSTING.Email.Address"/> class.
		/// </summary>
		/// <param name="address">The address of the mailbox.</param>
		public Address(string address) : this(address, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="OKHOSTING.Email.Address"/> class.
		/// </summary>
		/// <param name="name">The name of the mailbox.</param>
		/// <param name="address">The address of the mailbox.</param>
		public Address(string address, string name)
		{
			FullAddress = address;
			Name = name;
		}

		//
		// Summary:
		//	 Gets or sets the display name of the address.
		//
		// Remarks:
		//	 A name is optional and is typically set to the name of the person or group that
		//	 own the internet address.
		public string Name { get; set; }

		//
		// Summary:
		//	 Gets or sets the mailbox address.
		//
		// Exceptions:
		//   T:System.ArgumentNullException:
		//	 value is null.
		//
		// Remarks:
		//	 Represents the actual email address and is in the form of "name@example.com".
		public string FullAddress { get; set; }

		/// <summary>
		/// Returns a string representation of this object
		/// </summary>
		public override string ToString()
		{
			if (string.IsNullOrWhiteSpace(Name))
			{
				return FullAddress;
			}
			else
			{
				return Name + " <" + FullAddress + ">";
			}
		}

		public string User
		{
			get
			{
				return FullAddress.Split('@').First();
			}
		}

		public string Domain
		{
			get
			{
				return FullAddress.Split('@').Last();
			}
		}
	}
}