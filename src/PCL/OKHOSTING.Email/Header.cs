using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKHOSTING.Email
{
	public class Header
	{
		// Summary:
		//	 Gets the name of the header field.
		//
		// Remarks:
		//	 Represents the field name of the header.
		public string Field { get; set; }
		
		//
		// Summary:
		//	 Gets the header identifier.
		//
		// Remarks:
		//	 This property is mainly used for switch-statements for performance reasons.
		public HeaderId Id { get; set; }
		
		//
		// Summary:
		//	 Gets or sets the header value.
		//
		// Exceptions:
		//   T:System.ArgumentNullException:
		//	 value is null.
		//
		// Remarks:
		//	 Represents the decoded header value and is suitable for displaying to the user.
		public string Value { get; set; }
	}
}