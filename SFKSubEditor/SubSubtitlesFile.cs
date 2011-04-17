using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SFKSubEditor
{
    /// <summary>
    /// Class for reading sub (MicroDVD) subtitles.
    /// Format of subtitles:   
    /// {2}{30}first line|second line
    /// </summary>
    public class SubSubtitlesFile : ISubFile
    {
        // I am not sure, if those proparties has any meaning
        #region SubSubtitlesFile properties
        /// <summary>
        /// Name of the subtitles format.
        /// </summary>
        public string SubtitleFormatName
        {
            get { return ("MicroDVD subtitles"); }
        }

        /// <summary>
        /// File exstension of the subtitle format.
        /// </summary>
        public string SubtitleFormatExtension
        {
            get { return ("sub"); }
        }

        /// <summary>
        /// Type of subtitles: frame, time, both
        /// </summary>
        public Subtitles.SubtitleTimeTypes SubtitleType
        {
            get { return (Subtitles.SubtitleTimeTypes.Frame); }
        }

        #endregion
        /// <summary>
        /// Frame rate titulku
        /// </summary>
        public float FrameRate { get; set; }           

        /// <summary>
        /// Read sub subtitles from file.
        /// IO error has to be catch in upper levels.
        /// </summary>
        /// <param name="file">File with subtitles (full path).</param>
        /// <param name="encoding">Encoding of file.</param>
        /// <param name="subAList">List for adding subtitles.</param>
        /// <returns>Result of the reading. NULL - everything is OK.</returns>
        void ISubFile.ReadSubFile(String fileName, Encoding fileEncoding, BindingList<Subtitle> subList)
        {
            if (!File.Exists(fileName))
            {
                throw new WrongSubtitleFormatException("File " + fileName + " does not exists");
            }
            using (StreamReader sr = new StreamReader(fileName, fileEncoding))
            {
                String input;
                int i = 0;
                int beg = 0;
                int end = 0;
                float unit = 1000 / FrameRate;
                String sub = null;
                while ((input = sr.ReadLine()) != null)
                {
                    // line allways starts with {
                    if (input[0] != '{')
                    {
                        throw new WrongSubtitleFormatException("Wrong subtitle at line " + i + 1);
                    }
                    String[] parts = input.Split('}');
                    try
                    {
                        beg = int.Parse(parts[0].Substring(1, parts[0].Length - 1));
                        end = int.Parse(parts[1].Substring(1, parts[1].Length - 1));
                        sub = parts[2];
                        // first line of the file is often famerate - begin and end is 1
                        if (i == 0 && beg == end)
                        {
                            // In Czech windows there is decimal delimiter ",". Therefore CultureInfo.
                            FrameRate = float.Parse(sub, new CultureInfo("en-US"));
                            unit = 1000 / FrameRate;
                        }
                    }
                    catch (System.FormatException)
                    {
                        throw new WrongSubtitleFormatException("Wrong subtitle at line " + i + 1);
                    }
                    catch (System.OverflowException)
                    {
                        throw new WrongSubtitleFormatException("Wrong subtitle at line " + i + 1);
                    }
                    if (i > 0 || (beg != end))
                    {
                        subList.Add(new Subtitle(frameToTimeSpan(beg, FrameRate, unit), frameToTimeSpan(end, FrameRate, unit), sub));
                    }
                    // line count - only for bad line
                    i++;
                }               
            }
        }
       
        /// <summary>
        /// Write sub subtitles to the file.
        /// IO error has to be catch in upper levels.
        /// </summary>
        /// <param name="file">File with subtitles (full path).</param>
        /// <param name="encoding">Encoding of file.</param>
        /// <param name="subAList">List with subtitles.</param>
        void ISubFile.WriteSubFile(String fileName, Encoding fileEncoding, BindingList<Subtitle> subList, SubtitlesFile.WhichText what)
        {
            float unit = 1000 / FrameRate;
            using (StreamWriter sw = new StreamWriter(fileName, false, fileEncoding,32000))
            {
                // first line of the file is famerate - begin and end is 1
                sw.WriteLine("{1}{1}" + FrameRate.ToString("0.000", new CultureInfo("en-US")));
                foreach (Subtitle sub in subList)
                {
                    if (what == SubtitlesFile.WhichText.Original)
                        sw.WriteLine("{" + timeSpanToFrame(sub.TimeBegin, FrameRate, unit) + "}{" + timeSpanToFrame(sub.TimeEnd, FrameRate, unit) + "}" + sub.Original);
                    else
                        sw.WriteLine("{" + timeSpanToFrame(sub.TimeBegin, FrameRate, unit) + "}{" + timeSpanToFrame(sub.TimeEnd, FrameRate, unit) + "}" + sub.Translation);
                }
            }
        }

        /// <summary>
        /// Convert frame to TimeSpan.
        /// </summary>
        /// <param name="frame">frame</param>
        /// <returns>frame converted to TimeSpan.</returns>
        private static TimeSpan frameToTimeSpan(int frame, float fr, float unit)
        {
            int mili = (int)Math.Round(frame * unit);
            TimeSpan time = new TimeSpan(0, 0, 0, 0, mili);
            return (time);
        }
        /// <summary>
        /// Convert TimeSpan to frame.
        /// </summary>
        /// <param name="time">timeSpan</param>
        /// <returns>frame</returns>
        private static int timeSpanToFrame(TimeSpan time, float fr, float unit)
        {   
            int frame = (int)Math.Round(time.Hours * 3600 * fr + time.Minutes * 60 * fr + time.Seconds * fr + time.Milliseconds / unit);
            return (frame);
        }
    }
}
