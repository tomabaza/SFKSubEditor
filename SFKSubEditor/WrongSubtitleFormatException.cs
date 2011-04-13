using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SFKSubEditor
{
    /// <summary>
    /// Exception for wrong subtitles format.
    /// </summary>
    public class WrongSubtitleFormatException : ApplicationException
    {
        public WrongSubtitleFormatException(String message)
            : base(message)
        {
        }
        public WrongSubtitleFormatException(String message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
