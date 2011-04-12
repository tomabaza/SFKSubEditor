using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SFKSubEditor
{
    /// <summary>
    /// Subtitles.
    /// </summary>
    public partial class Subtitles : BindingList<Subtitle>
        //, INotifyPropertyChanged
    {
        /// <summary>
        /// Type of the subtitles:
        /// time - begin and end of a subtitle is in the time
        /// frame - begin and of a subtitle is in the frames
        /// both - both time and frames are used (known)
        /// </summary>
        public enum SubtitleTimeTypes { Time, Frame, Both };
        /*
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }
         */
    }
}
