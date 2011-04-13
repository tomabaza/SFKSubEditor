using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SFKSubEditor
{
    public class Sub
    {
        private TimeSpan timeSpan;
        private TimeSpan timeSpan_2;
        private string sub;

        /// <summary>
        /// The beginning of the subtitle - as a time.
        /// </summary>
        public TimeSpan Begin { get; set; }
        /// <summary>
        /// The end of the subtitle - as a time.
        /// </summary>
        public TimeSpan End { get; set;  } 
        /// <summary>
        /// The text of the subtitle. End of line is |.
        /// </summary>
        public String Text { get; set; }

        public Sub(TimeSpan begin, TimeSpan end, String text)
        {
            Begin = begin;
            End = end;
            Text = text;
        }
    }

    abstract class FileSubtitle
    {
        /// <summary>
        /// Name of the subtitles format.
        /// </summary>
        public static String SubtitleFormatName;

        /// <summary>
        /// File exstension of the subtitle format.
        /// </summary>
        public static String SubtitleFormatExtension;

        /// <summary>
        /// Frame rate. Not used for text subtitles.
        /// </summary>
        public float FrameRate { get; set; }

        /// <summary>
        /// If used is frame rate used.
        /// </summary>
        public bool UsedFrameRate { get; set; }
        /// <summary>
        /// If the file has header.
        /// </summary>
        public bool HasHeader { get; set; }

        /// <summary>
        /// Read a header of the file
        /// </summary>
        /// <param name="file"></param>
        public abstract void readHeader(StreamReader file);

        /// <summary>
        /// Write a header of the file
        /// </summary>
        /// <param name="file"></param>
        public abstract void writeHeader(StreamWriter file);

        /// <summary>
        /// Read a subtitle.
        /// </summary>
        /// <param name="file">File with subtitles</param>
        /// <returns></returns>
        public abstract Sub readSubtitle(StreamReader file);

        /// <summary>
        /// Write a subtitle.
        /// </summary>
        /// <param name="file">File with subtitles</param>
        /// <param name="sub">A subtitle to write.</param>
        public abstract void writeSubtitle(StreamWriter file, int row, Sub sub);
    }
}
