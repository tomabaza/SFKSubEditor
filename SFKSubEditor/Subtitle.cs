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
    public class Subtitle : INotifyPropertyChanged
    {
        /// <summary>
        /// Constructor without parameters.
        /// </summary>
        public Subtitle()
        {
            this.TimeBegin = new TimeSpan(0);
            this.TimeEnd = this.TimeBegin;
            this.Original = null;
            this.Translation = null;
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
            this.TimeBegin = tBegin;
            this.TimeEnd = tEnd;
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
            this.TimeBegin = tBegin;
            this.TimeEnd = tEnd;
            this.Original = text;
            this.Translation = translation;
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
   
    }    
}
