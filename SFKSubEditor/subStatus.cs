using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SFKSubEditor
{
    /// <summary>
    /// Status pro cteni/zapis titulku.
    /// </summary>
    public class SubtitlesStatus
    {
        /// <summary>
        /// Jak dopadla operace s titulky.
        /// </summary>
        public Boolean IsOK;
        /// <summary>
        /// Chybova zprava pro vypis.
        /// </summary>
        public String ErrorMessage = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="status">Status operace</param>
        /// <param name="message">Chybova zprava</param>
        public SubtitlesStatus(Boolean status, String message = null)
        {
            IsOK = status;
            ErrorMessage = message;
        }

        public SubtitlesStatus()
        {
            IsOK = true;
            ErrorMessage = null;
        }
    }
 

}
