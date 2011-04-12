using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SFKSubEditor
{
    interface IFileSubtitle
    {
        /// <summary>
        /// Name of the subtitles format.
        /// </summary>
        string SubtitleFormatName { get; }

        /// <summary>
        /// File exstension of the subtitle format.
        /// </summary>
        string SubtitleFormatExtension { get; }

        /// <summary>
        /// Type of subtitles: frame, time, both.
        /// </summary>
        Subtitles.SubtitleTimeTypes SubtitleType { get; }

        /// <summary>
        /// Frame rate. Not used for text subtitles.
        /// </summary>
        float FrameRate { get; set; }

        void readHeader(StreamReader file);

        /// <summary>
        /// Read a subtitle.
        /// </summary>
        /// <param name="file">File with subtitles</param>
        /// <returns></returns>
        Subtitle readSubtitle(StreamReader file);

        /// <summary>
        /// Write a subtitle.
        /// </summary>
        /// <param name="file">File with subtitles</param>
        /// <param name="sub">A subtitle to write.</param>
        void writeSubtitle(StreamReader file, Subtitle sub);

    }
}
