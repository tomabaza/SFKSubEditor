using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Threading;
using System.ComponentModel;
using Microsoft.Windows.Controls;


namespace SFKSubEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Subtitles subtitleList = null;
        public string videoFile = null;
        public SubtitlesFile originalFile = null;
        public SubtitlesFile translationFile = null;

        /// <summary>
        /// If video is playing
        /// </summary>
        public bool isVideoPlaying = false;
        //public bool originalChanged = false;
        //public bool translationChanged = false;

        // properties
        /// <summary>
        /// Length of a new subtitle in secunds
        /// </summary>
        public static int newSubtitleLength = 2;
        /// <summary>
        /// Text of a new translation.
        /// </summary>
        //public static string newTranslationText = "not yet translated";
        
        public static string projectFileExtension = "spr";
        /// <summary>
        /// File extensions of video files.
        /// Actully it should be tested.
        /// </summary>
        public static string[] videoFileExtensions = { "avi", "mkv", "mp4" };

        /// <summary>
        /// Last used directory.
        /// </summary>
        public static String lastDirectory;

        DispatcherTimer timer;
            
        public MainWindow()
        {
            string org = null;
            string trans = null;
            string vid = null;
            string proj = null;
            string[] arg = Environment.GetCommandLineArgs();
            if (arg.Length > 1)
            {
                if (arg[1] == "/h" || arg[1] == "/?")
                {
                    Console.WriteLine("SFKSubEditor [project|original [translation] [movie]]");
                    Console.WriteLine("project - project file");
                    Console.WriteLine("original - original subtitles");
                    Console.WriteLine("translation - translation subtitles");
                    Console.WriteLine("video - video file");
                    Application.Current.Shutdown();
                    Environment.Exit(0);
                }
                if (arg[1].Substring(arg[1].LastIndexOf(".") + 1) == MainWindow.projectFileExtension)
                    proj = arg[1];
                else
                    org = arg[1];
                if (arg.Length > 2)
                    trans = arg[2];
                if (arg.Length > 3)
                    vid = arg[3];
            }

            InitializeComponent();
            InitializeCommands();
            
            
            if (org != null)
            {
                originalFile = new SubtitlesFile(SubtitlesFile.WhichText.Original,org);
                subtitleList = originalFile.fileOpen(subtitleList);
                if (trans != null)
                {
                    translationFile = new SubtitlesFile(SubtitlesFile.WhichText.Translation,trans);
                    Subtitles translationSubtitles = translationFile.fileOpen(null);
                    addTranslation(translationSubtitles);
                }
                if (vid != null)
                {
                    videoFile = vid;
                    video.Source = new Uri(videoFile,UriKind.Relative);
                }
                else
                {
                    videoFile = getVideoFileFromSubtitle(originalFile.FileName);
                    if (videoFile != null)
                    {
                            video.Source = new Uri(videoFile, UriKind.Relative);
                    }
                }
            }
            else if (proj != null)
            {
            }
            else
            {
                // from the beginning we have new subtitles
                newOriginalFunction();
            }

            dataGrid1.ItemsSource = subtitleList;
            dataGrid1.DataContext = subtitleList;
            showColumns();
             
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(50);
            timer.Tick += new EventHandler(timer_Tick);
            PlayVideo();
        }

        void OnMediaFailed(object o, ExceptionRoutedEventArgs e)
        {
            MediaElement me = (MediaElement)o;
            Microsoft.Windows.Controls.MessageBox.Show("Error loading file " + me.Source + ".\n" + e.ErrorException.ToString());
        }
        
        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //DataGrid s = (DataGrid)sender;    
            //textBox1.Text = sl[s.SelectedIndex].SubText;
        }

        // Jump to different parts of the media (seek to)
        // according to the slider.
        private void SeekToMediaPosition(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            /*
            double sliderValue = timelineSlider.Value;
            double miliVideoDuration = video.NaturalDuration.TimeSpan.TotalMilliseconds;
            int newPosition = (int) (miliVideoDuration * sliderValue / 100);
            double sec = video.NaturalDuration.TimeSpan.TotalSeconds;
            label1.Content = miliVideoDuration + " - " + newPosition;
            
            // Overloaded constructor takes the arguments days, hours, minutes, seconds, miniseconds.
            // Create a TimeSpan with miliseconds. The slider value means procentage of the lenght of the film.
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, newPosition);
            video.Position = ts;
             */
        }
        void timer_Tick(object sender, EventArgs e)
        {
            TimeSpan videoPosition = video.Position;
            var currentSubtitle = from Subtitle in subtitleList 
                                  where Subtitle.TimeBegin <= videoPosition && Subtitle.TimeEnd > videoPosition
                                  select Subtitle;
            if (currentSubtitle.Count() > 0)
            {
                foreach (Subtitle sub in currentSubtitle)
                {
                    label2.Content = sub.Original;
                }
            }
            else
                label2.Content = "";
            //double sliderValue = timelineSlider.Value;
            //double miliVideoDuration = video.NaturalDuration.TimeSpan.TotalMilliseconds;
            //int newPosition = (int)(miliVideoDuration * sliderValue / 100);
            //double sec = video.NaturalDuration.TimeSpan.TotalSeconds;
           // if (video.NaturalDuration.TimeSpan.TotalSeconds > 0)
            //{
             //   label1.Content = string.Format("{0:00}:{1:00}", video.Position.Minutes, video.Position.Seconds);

                //timelineSlider.Value = video.Position.TotalSeconds / video.NaturalDuration.TimeSpan.TotalSeconds;
           // }
        }

        private void tableDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            Subtitle sub = (Subtitle)dg.SelectedItem;
            video.Position = sub.TimeBegin;
        }

        private void originalText_KeyDown(object sender, KeyEventArgs e)
        {
            originalFile.Changed = true;
        }

        private void translationText_KeyDown(object sender, KeyEventArgs e)
        {
            translationFile.Changed = true;
        }

        private void video_Loaded(object sender, RoutedEventArgs e)
        {
            if (!video.HasAudio || !video.HasVideo)
            {
                // HasAudio and HasVideo do not work correctly all the time.
                // Therefore this general message.
                string str = "";
                str += "Program can't play all streams of the video file\n";
                str += "It is probably caused due missing codecs";
                PauseVideo();
                Microsoft.Windows.Controls.MessageBox.Show(str);
                if (video.HasVideo || video.HasAudio)
                    PlayVideo();
            }
        }

    }
    
    [ValueConversion(typeof(string), typeof(string))]
    public class SubtitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return (value);
            else
            {
                String s = (String)value;
                return (s.Replace("|", Environment.NewLine));
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return (value);
            else
            {
                String s = (String)value;
                return (s.Replace(Environment.NewLine, "|"));
            }
        }
    }
    [ValueConversion(typeof(DateTime), typeof(string))]
    public class TimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            TimeSpan time = (TimeSpan)value;
            return (string.Format("{0:00}:{1:00}:{2:00},{3:000}",
                (int)time.TotalHours, time.Minutes, time.Seconds, time.Milliseconds));
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            String s = (String)value;
            return (SrtSubtitlesFile.stringToTimeSpan(s));
        }
    }
    [ValueConversion(typeof(DateTime), typeof(string))]
    public class SubtitleTimeLength : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[0] == DependencyProperty.UnsetValue || values[1] == DependencyProperty.UnsetValue)
                return (values);
          
            TimeSpan begin = (TimeSpan)values[0];
            TimeSpan end = (TimeSpan)values[1];
            TimeSpan time = end - begin;
            return (string.Format("{0:00}:{1:00}:{2:00},{3:000}",
                (int)time.TotalHours, time.Minutes, time.Seconds, time.Milliseconds));
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return (null);
        }
    }
}
