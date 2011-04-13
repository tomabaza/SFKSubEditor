using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SFKSubEditor
{
    /// <summary>
    /// Rozhrani pro praci s titulkovymi soubory.
    /// </summary>
    public interface ISubFile
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

        /// <summary>
        /// Read subtitles from file.
        /// </summary>
        /// <param name="file">File with subtitles (full path).</param>
        /// <param name="encoding">Encoding of file.</param>
        /// <param name="subAList">List for adding subtitles.</param>
        /// <returns>Result of the reading. NULL - everything is OK.</returns>
        void ReadSubFile(String fileName, Encoding fileEncoding,BindingList<Subtitle> subList);

        /// <summary>
        /// Write subtitles to the file.
        /// </summary>
        /// <param name="file">File with subtitles (full path).</param>
        /// <param name="encoding">Encoding of file.</param>
        /// <param name="subAList">List with subtitles.</param>
        void WriteSubFile(String fileName, Encoding fileEncoding, BindingList<Subtitle> subList,SubtitlesFile.WhichText what);
    }
}
