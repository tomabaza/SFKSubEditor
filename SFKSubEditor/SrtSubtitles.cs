using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SFKSubEditor
{
    /// <summary>
    /// Subtitles in SRT format.
    /// 
    /// SRT format:
    /// 1
    /// 00:00:10,890 --> 00:00:12,939
    /// line 1
    /// line 2
    ///
    /// Empty line is delimiter.
    /// </summary>
    public class SrtSubtitlesFile : ISubFile
    {
        /// <summary>
        /// Name of the subtitles format.
        /// </summary>
        public string SubtitleFormatName
        {
            get { return ("SupRip subtitles"); } 
        }

        /// <summary>
        /// File exstension of the subtitle format.
        /// </summary>
        public string SubtitleFormatExtension 
        {
            get { return ("srt"); }
        }

        /// <summary>
        /// Type of subtitles: frame, time, both
        /// </summary>
        public Subtitles.SubtitleTimeTypes SubtitleType
        {
            get { return (Subtitles.SubtitleTimeTypes.Time); } 
        }

        /// <summary>
        /// Form of subtitles: text, graphic.
        /// </summary>
        public Subtitles.SubtitleFormTypes SubtitleForm
        {
            get { return (Subtitles.SubtitleFormTypes.Text); }
        }

       
        /// <summary>
        /// Frame rate titulku
        /// </summary>
        public float FrameRate { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public SrtSubtitlesFile()
        {
            FrameRate = 0.0F;
        }

        /// <summary>
        /// Transfor text form of the the time to the (00:00:10,890) do TimeSpan.
        /// </summary>
        /// <param name="s">Time as string</param>
        /// <returns>Time as TimeSpan</returns>
        public static TimeSpan stringToTimeSpan(String s) 
        {
            String[] parts = s.Split(':');
            int hour = int.Parse(parts[0]);
            int minute = int.Parse(parts[1]);
            String[] sec = parts[2].Split(',');
            int second = int.Parse(sec[0]);
            int milisecond = int.Parse(sec[1]);
            return (new TimeSpan(0,hour, minute, second, milisecond));
        }

        /// <summary>
        /// Read srt subtitles from file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fileEncoding">Encoding of the file.</param>
        /// <param name="subList">List for subtitles.</param>
        /// <returns>Result of the reading. NULL - everything is OK.</returns>
        SubtitlesStatus ISubFile.ReadSubFile(String fileName, Encoding fileEncoding, BindingList<Subtitle> subList)
        {
            
            if (!File.Exists(fileName))
            {
                return (new SubtitlesStatus(false, "File " + fileName + " does not exists"));
            }
            using (StreamReader sr = new StreamReader(fileName, fileEncoding))
            {
                String input;
                int i = 0;
                while ((input = sr.ReadLine()) != null)
                {
                    try
                    {
                        // in case there are more empty line
                        while (!sr.EndOfStream && input == "")
                        {
                            input = sr.ReadLine();
                        }
                        // we don't need it
                        int count = int.Parse(input);
                        // nacteme radek s casy
                        input = sr.ReadLine();
                        String beg = input.Substring(0, 12);
                        String end = input.Substring(17);
                        String sub = "";
                        // nacteme radek s textem
                        // test na konec souboru pro pripad, ze by za poslednim titulkem
                        // nebyla oddleovaci mezera
                        while (!sr.EndOfStream && (input = sr.ReadLine()) != "")
                        { // join the text together
                            if (sub != "") sub += "|";
                            sub += input;
                        }
                        subList.Add(new Subtitle(stringToTimeSpan(beg), stringToTimeSpan(end), sub));
                    } // catch only error cause by wrong subtitles
                    catch (System.FormatException)
                    { 
                        return (new SubtitlesStatus(false, "Wrong subtitle at line " + (i + 1)));
                    }
                    catch (System.ArgumentOutOfRangeException)
                    {
                        return (new SubtitlesStatus(false, "Wrong subtitle at line " + (i + 1)));
                    }
                    catch (System.OverflowException)
                    {
                        return (new SubtitlesStatus(false, "Wrong subtitle at line " + (i + 1)));
                    }
                    i++;
                }
            }
            return (null);
        }

        /// <summary>
        /// Write srt subtitles to file. If already exists is overwritten.
        /// </summary>
        /// <param name="file">File with subtitles (full path).</param>
        /// <param name="encoding">Encoding of file.</param>
        /// <param name="subAList">List with subtitles.</param>
        void ISubFile.WriteSubFile(String fileName, Encoding fileEncoding, BindingList<Subtitle> subList)
        {
           
            using (StreamWriter sw = new StreamWriter(fileName, false, fileEncoding, 32000))
            {
                int i = 1;
                foreach (Subtitle sub in subList)
                {
                    sw.WriteLine(i);
                    sw.WriteLine(sub.TimeBegin.ToString("HH:mm:ss,fff") + " --> " + sub.TimeEnd.ToString("HH:mm:ss,fff"));
                    String[] lines = sub.Original.Split('|');
                    for (int j = 0; j < lines.Length; j++)
                    {
                        sw.WriteLine(lines[j]);
                    }
                    sw.WriteLine();
                    i++;
                }
            }
        }
    }
}
