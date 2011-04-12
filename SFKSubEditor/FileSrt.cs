using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SFKSubEditor
{
    class FileSrt : FileSubtitle
    {
        /// <summary>
        /// Static contructor: define name and file extension.
        /// </summary>
        static FileSrt()
        {
            SubtitleFormatName = "Suprip";
            SubtitleFormatExtension = "srt";
        }

        /// <summary>
        /// Read a header: not exists
        /// </summary>
        /// <param name="file">Input file</param>
        public override void readHeader(StreamReader file)
        {
            HasHeader = false;
        }
        /// <summary>
        /// Write a header: not exists
        /// </summary>
        /// <param name="file">Output file</param>
        public override void writeHeader(StreamWriter file)
        {
            return;
        }

        public override Sub readSubtitle(StreamReader file)
        {
            String input;
            if ((input = file.ReadLine()) != null)
            {
                // in case there are more empty line
                while (!file.EndOfStream && input == "")
                {
                    input = file.ReadLine();
                }
                try
                {
                    // we don't need line number
                    int count = int.Parse(input);
                    // read row with times
                    input = file.ReadLine();
                    String beg = input.Substring(0, 12);
                    String end = input.Substring(17);
                    String sub = "";
                    // nacteme radek s textem
                    // test na konec souboru pro pripad, ze by za poslednim titulkem
                    // nebyla oddleovaci mezera
                    while (!file.EndOfStream && (input = file.ReadLine()) != "")
                    { // join the text together
                        if (sub != "") sub += "|";
                        sub += input;
                    }
                    return(new Sub(stringToTimeSpan(beg), stringToTimeSpan(end), sub));
                } // catch only error cause by wrong subtitles
                catch (System.FormatException)
                {
                    throw new WrongSubtitleFormatException("Wrong subtitle");
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    throw new WrongSubtitleFormatException("Wrong subtitle");
                }
                catch (System.OverflowException)
                {
                    throw new WrongSubtitleFormatException("Wrong subtitle");
                }
            }
            return (null);
        }

        /// <summary>
        /// Write a subtitle to the file.
        /// </summary>
        /// <param name="file">output file</param>
        /// <param name="row">number of row</param>
        /// <param name="sub">subtitle</param>
        public override void writeSubtitle(StreamWriter file, int row, Sub sub)
        {
            file.WriteLine(row);
            file.WriteLine(sub.Begin.ToString("hh':'mm':'ss','fff") + " --> " + sub.End.ToString("hh':'mm':'ss','fff"));
            String[] lines = sub.Text.Split('|');
            for (int j = 0; j < lines.Length; j++)
            {
                file.WriteLine(lines[j]);
            }
            file.WriteLine();
        }

        /// <summary>
        /// Transfor text form of the the time to the (00:00:10,890) do TimeSpan.
        /// </summary>
        /// <param name="s">Time as string</param>
        /// <returns>Time as TimeSpan</returns>
        private static TimeSpan stringToTimeSpan(String s)
        {
            String[] parts = s.Split(':');
            int hour = int.Parse(parts[0]);
            int minute = int.Parse(parts[1]);
            String[] sec = parts[2].Split(',');
            int second = int.Parse(sec[0]);
            int milisecond = int.Parse(sec[1]);
            return (new TimeSpan(0, hour, minute, second, milisecond));
        }
    }
}
