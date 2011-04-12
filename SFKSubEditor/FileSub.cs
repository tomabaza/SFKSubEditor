using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;

namespace SFKSubEditor
{
    /// <summary>
    /// Class for reading, writing sub (MicroDVD) subtitles.
    /// Format of subtitles:   
    /// {2}{30}first line|second line
    /// </summary>
    class FileSub : FileSubtitle
    {
        /// <summary>
        /// Static contructor: define name and file extension.
        /// </summary>
        static FileSub()
        {
            SubtitleFormatName = "Micro DVD";
            SubtitleFormatExtension = "sub";
        }       

        /// <summary>
        /// Read a header: the first line is framerate
        /// </summary>
        /// <param name="file">Input file</param>
        public override void readHeader(StreamReader file)
        {
            String input;
            if ((input = file.ReadLine()) != null)
            {
                // line allways starts with {
                if (input[0] != '{')
                {
                    throw new WrongSubtitleFormatException("Wrong subtitle");
                }
                String[] parts = input.Split('}');
                try
                {
                    int beg = int.Parse(parts[0].Substring(1, parts[0].Length - 1));
                    int end = int.Parse(parts[1].Substring(1, parts[1].Length - 1));
                    String sub = parts[2];
                    // first line can be frame rate {1}{1}25.00
                    if (beg == end)
                    {
                        // In Czech windows there is decimal delimiter ",". Therefore CultureInfo.
                        FrameRate = float.Parse(sub, new CultureInfo("en-US"));
                        HasHeader = true;
                    }
                    else
                    {
                        HasHeader = false;
                        FrameRate = 0.0F;
                    }
                }
                catch (System.FormatException)
                {
                    throw new WrongSubtitleFormatException("Wrong subtitle");
                }
                catch (System.OverflowException)
                {
                    throw new WrongSubtitleFormatException("Wrong subtitle");
                }
            } else {
                throw new WrongSubtitleFormatException("Wrong subtitle");
            }
        }

        /// <summary>
        /// Read a subtitle: {12}{34}first line|second line
        /// </summary>
        /// <param name="file">input file</param>
        /// <returns>a subtitle</returns>
        public override Sub readSubtitle(StreamReader file)
        {
            String input;
            if ((input = file.ReadLine()) != null)
            {
                // line allways starts with {
                if (input[0] != '{')
                {
                    throw new WrongSubtitleFormatException("Wrong subtitle");
                }
                String[] parts = input.Split('}');
                try
                {
                    int begin = int.Parse(parts[0].Substring(1, parts[0].Length - 1));
                    int end = int.Parse(parts[1].Substring(1, parts[1].Length - 1));
                    String sub = parts[2];
                    return (new Sub(frameToTimeSpan(begin,FrameRate), frameToTimeSpan(end,FrameRate), sub));
                }
                catch (System.FormatException)
                {
                    throw new WrongSubtitleFormatException("Wrong subtitle");
                }
                catch (System.OverflowException)
                {
                    throw new WrongSubtitleFormatException("Wrong subtitle");
                }
            }
            else
            {
                return (null);
            }
        }

        /// <summary>
        /// Write a header: the first line is framerate
        /// </summary>
        /// <param name="file">Output file</param>
        public override void writeHeader(StreamWriter file)
        {
            // In the number there has to be point. Therefore CultureInof en-US (in some locals there coma).
            file.WriteLine("{1}{1}" + FrameRate.ToString("0.000", new CultureInfo("en-US")));
        }

        /// <summary>
        /// Write a subtitle to the file.
        /// </summary>
        /// <param name="file">output file</param>
        /// <param name="row">row number - not used</param>
        /// <param name="sub">subtitle to write</param>
        public override void writeSubtitle(StreamWriter file, int row, Sub sub)
        {
            file.WriteLine("{" + timeSpanToFrame(sub.Begin,FrameRate) + "}{" + timeSpanToFrame(sub.End,FrameRate) + "}" + sub.Text);
        }

        /// <summary>
        /// Convert frame to TimeSpan.
        /// </summary>
        /// <param name="frame">frame</param>
        /// <returns>frame converted to TimeSpan.</returns>
        private static TimeSpan frameToTimeSpan(int frame,float fr)
        {
            float unit = 1000 / fr;
            int mili = (int) Math.Round(frame * unit);
            TimeSpan time = new TimeSpan(0, 0, 0, 0, mili);
            return (time);
        }
        /// <summary>
        /// Convert TimeSpan to frame.
        /// </summary>
        /// <param name="time">timeSpan</param>
        /// <returns>frame</returns>
        private static int timeSpanToFrame(TimeSpan time, float fr)
        {
            float unit = 1000 / fr;
            int frame = (int) Math.Round(time.Hours * 3600 * fr + time.Minutes * 60 * fr + time.Seconds * fr + time.Milliseconds / unit);
            return (frame);
        }
    }
}
