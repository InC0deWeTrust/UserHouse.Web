using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace UserHouse.Application.Helpers
{
    public class CustomUserFriendlyException : Exception
    {
        public CustomUserFriendlyException()
            : base()
        {

        }

        public CustomUserFriendlyException(string message)
            : base(message)
        {

        }

        public CustomUserFriendlyException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {

        }
    }
}
