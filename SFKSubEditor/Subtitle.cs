using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SFKSubEditor
{
    /// <summary>
    /// A subtitle.
    /// </summary>
    public class Subtitle : INotifyPropertyChanged, IFormattable
    {
        /// <summary>
        /// Constructor without parameters.
        /// </summary>
        public Subtitle()
        {
            this.FrameBegin = 0;
            this.FrameEnd = this.FrameBegin;
            this.TimeBegin = new TimeSpan(0);
            this.TimeEnd = this.TimeBegin;
            this.Original = null;
            this.Translation = null;
        }

        /// <summary>
        /// Constructor for text subtitles based on frames.
        /// </summary>
        /// <param name="fBegin">Frame of the begin.</param>
        /// <param name="fEnd">Frame of the end.</param>
        /// <param name="text">Text of the subtitle.</param>
        /// <param name="id">Id if exists.</param>
        public Subtitle(int fBegin, int fEnd, String text)
        {
            this.FrameBegin = fBegin;
            this.FrameEnd = fEnd;
            this.TimeBegin = new TimeSpan(0);
            this.TimeEnd = this.TimeBegin;
            this.Original = text;
            this.Translation = Properties.Settings.Default.newTranslationText;
        }

        /// <summary>
        /// Constructor for text subtitle based on time.
        /// </summary>
        /// <param name="tBegin">Time of the begin.</param>
        /// <param name="tEnd">Time of the end.</param>
        /// <param name="text">Text.</param>
        /// <param name="id">Id if exists.</param>
        public Subtitle(TimeSpan tBegin, TimeSpan tEnd, String text)
        {
            this.FrameBegin = 0;
            this.FrameEnd = 0;
            this.TimeBegin = tBegin;
            this.TimeEnd = tEnd;
            this.Original = text;
            this.Translation = Properties.Settings.Default.newTranslationText;
        }

        /// <summary>
        /// Constructor for text subtitles based on frames.
        /// </summary>
        /// <param name="fBegin">Frame of the begin.</param>
        /// <param name="fEnd">Frame of the end.</param>
        /// <param name="text">Text of the subtitle.</param>
        /// <param name="translation">Text of the translation.</param>
        /// <param name="id">Id if exists.</param>
        public Subtitle(int fBegin, int fEnd, String text, String translation)
        {
            this.FrameBegin = fBegin;
            this.FrameEnd = fEnd;
            this.TimeBegin = new TimeSpan(0);
            this.TimeEnd = this.TimeBegin;
            this.Original = text;
            this.Translation = Properties.Settings.Default.newTranslationText;
        }

        /// <summary>
        /// Constructor for text subtitle based on time.
        /// </summary>
        /// <param name="tBegin">Time of the begin.</param>
        /// <param name="tEnd">Time of the end.</param>
        /// <param name="text">Text.</param>
        /// <param name="translation">Text of the translation.</param>
        public Subtitle(TimeSpan tBegin, TimeSpan tEnd, String text, String translation)
        {
            this.FrameBegin = 0;
            this.FrameEnd = 0;
            this.TimeBegin = tBegin;
            this.TimeEnd = tEnd;
            this.Original = text;
            this.Translation = translation;
        }
        
        private int frameBegin;
        /// <summary>
        /// Frame of the begin.
        /// </summary>
        public int FrameBegin
        {
            get { return (frameBegin); }
            set
            {
                frameBegin = value;
                this.NotifyPropertyChanged("FrameBegin");
            }
        }

        private int frameEnd;
        /// <summary>
        /// Frame of the end.
        /// </summary>
        public int FrameEnd 
        { 
            get { return(frameEnd); }
            set {
                frameEnd = value;
                this.NotifyPropertyChanged("FrameEnd");
            }
        }
        
        private TimeSpan timeBegin;
        /// /// <summary>
        /// Time of the begin.
        /// </summary>
        public TimeSpan TimeBegin { 
            get { return(timeBegin); }
            set {
                timeBegin = value;
                this.NotifyPropertyChanged("TimeBegin");
            }
        }
        
        private TimeSpan timeEnd;
        /// <summary>
        /// Time of the end.
        /// </summary>
        public TimeSpan TimeEnd 
        { 
            get {return(timeEnd); }
            set { 
                timeEnd = value;
                this.NotifyPropertyChanged("TimeEnd");
            }
        }

        private String original;
        /// <summary>
        /// An original subtitle as text.
        /// </summary>
        /// 
        public String Original 
        { 
            get { return(original); }
            set {   original = value;
                    this.NotifyPropertyChanged("Original"); 
            }
        }

        private String translation;
        /// <summary>
        /// An translation of the subtitle.
        /// </summary>
        public String Translation 
        {
            get { return (translation); }
            set
            {
                translation = value;
                this.NotifyPropertyChanged("Translation");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        /// <summary>
        /// TODO Bylo by dobre to udelat, tak aby se daly časy/framy tisknout
        /// podle pozadavku.
        /// public class CustomerFormatter : IFormatProvider, ICustomFormatter
        /// http://msdn.microsoft.com/en-us/library/1ksz8yb7.aspx
        /// Formatovany vystup
        /// E - frame
        /// H - hodiny
        /// M - minuty
        /// S - sekundy
        /// ,3:M - milisekundy
        /// R - framy v posledni vterine
        /// </summary>
        /// <param name="format"></param>
        /// <param name="formatProvader"></param>
        /// <returns></returns>
        public String ToString(String format, IFormatProvider formatProvader)
        {
            if (format == null)
            {
                return(ToString());
            }
            String formatUpper = format.ToUpper();
            switch (formatUpper)
            {
                case "F" : 
                    return("AA");
                default:
                    return (ToString());
            }

        }   
    }    
}
