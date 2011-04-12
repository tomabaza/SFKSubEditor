using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Windows;

namespace SFKSubEditor
{
    public class SubtitlesFile
    {

        /// <summary>
        /// Which text of subtitles will be written to the file.
        /// </summary>
        public enum WhichText { Original, Translation };

        private static string FileFilter = "SRT subtitles (.srt)|*.srt|SUB subtitles (.sub, .txt)|*.sub|IDX/SUB subtitles (.idx)|*.idx"; 
        /// <summary>
        /// Path to the file (without name of the file}.
        /// </summary>
        public String FilePath { get; set; }

        /// <summary>
        /// Name of the file.
        /// </summary>
        public String FileName { get; set; }

        /// <summary>
        /// Encoding.
        /// </summary>
        public Encoding FileEncoding { get; set; }

        /// <summary>
        /// Type of subtitles: frame, time, both
        /// </summary>
        //public Subtitles.SubtitleTimeTypes SubtitleType { get; set; }

        /// <summary>
        /// If subtitles were changed
        /// </summary>
        public bool Changed { get; set; }

        /// <summary>
        /// If it is original or translation.
        /// </summary>
        public WhichText What { get; set; }

        private ISubFile subFile;
        /// <summary>
        /// Real file with subtitles.
        /// </summary>
        public ISubFile SubFile {
            get { return (subFile); }
        }

        public SubtitlesFile(WhichText what, String fileName, Encoding fileEncoding)
        {
            What = what;
            FileName = fileName;
            FileEncoding = fileEncoding;
            Changed = false;
        }
        public SubtitlesFile(WhichText what) :this(what,null,null)
        {

        }
        public SubtitlesFile(WhichText what, string fileName) : this(what, fileName, null)
        {
            
        }

        /// <summary>
        /// Get the file extension from the file name.
        /// </summary>
        /// <param name="name">name of the file</param>
        /// <returns>extension without .</returns>
        public static string getExtension(string name)
        {
            return (name.Substring(name.LastIndexOf(".") + 1));
        }

        public bool getOpenFileName(String windowName)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            //dlg.InitialDirectory = MainWindow.lastDirectory;
            
            dlg.DefaultExt = ".srt"; // Default file extension
            dlg.Filter = FileFilter;
            dlg.Title = windowName;


            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();
            // Process open file dialog box results
            if (result == true)
            {
                MainWindow.lastDirectory = dlg.InitialDirectory;
                FileName = dlg.FileName;
                return (true);
            }
            else
            {
                return (false);
            }
        }
        public bool getSaveFileName()
        {
            String windowName;
            if (What == WhichText.Original)
                windowName = "Save original subtitles as";
            else
                windowName = "Save translation subtitles as";
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            //dlg.InitialDirectory = MainWindow.lastDirectory;
            if (FileName != null)
            {
                dlg.FileName = FileName;
                dlg.DefaultExt = getExtension(FileName);
            }
            else
            {
                dlg.FileName = "new";
                dlg.DefaultExt = ".srt"; // Default file extension
            }
            dlg.Filter = FileFilter;
            dlg.Title = windowName;

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();
            // Process open file dialog box results
            if (result == true)
            {
                MainWindow.lastDirectory = dlg.InitialDirectory;
                FileName = dlg.FileName;
                return (true);
            }
            else
            {
                return (false);
            }
        }
        /// <summary>
        /// Tests file for its encoding comparing first four bytes (BOM) with Encoding.GetPreamble.
        /// </summary>
        /// <returns>Encoding of the file.</returns>
        private Encoding getEncodingFromFile()
        {
            byte[] bom = new byte[4];
            // test encoding - utf-8,Unicode
            using (FileStream binaryStream = File.Open(FileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {

                int byteReaded = binaryStream.Read(bom, 0, 4);
                if (byteReaded < 4)
                {
                    throw new IOException("File not seem to be a valid subtitle file");
                }
            }
            if (bomComparing(bom, Encoding.UTF8))
                return (Encoding.UTF8);
            else if (bomComparing(bom, Encoding.Unicode))
                return (Encoding.Unicode);
            else if (bomComparing(bom, Encoding.UTF32))
                return (Encoding.UTF32);
            else
                return (Encoding.ASCII);
        }

        /// <summary>
        /// Comparing BOM from text file with Preamble.
        /// </summary>
        /// <param name="bom">BOM from text file</param>
        /// <param name="encoding">Encoding for comparing</param>
        /// <returns>true - BOM founded, false - BOM not founded</returns>
        private Boolean bomComparing(byte[] bom, Encoding encoding)
        {
            byte[] preamble = encoding.GetPreamble();
            if (preamble.Length == 0)
                return (false);
            for (int i = 0; i < preamble.Length; i++)
            {
                if (bom[i] != preamble[i])
                    return (false);
            }
            return (true);
        }
        private void getFileEncoding()
        {
            FileEncoding = getEncodingFromFile();
            if (FileEncoding == Encoding.ASCII)
            {
                // now the user must choise the right encoding
                ChoiceEncodingWindow choiceWindow = new ChoiceEncodingWindow(FileName);
                //choiceWindow.Owner = this;
                bool? choiceResult = choiceWindow.ShowDialog();
                if (choiceResult == true)
                {
                    FileEncoding = choiceWindow.getActualEncoding();
                }
            }

        }
        
        public Subtitles fileOpen(Subtitles subList)
        {
            if (subList != null)
            {
                subList.Clear();
            } 
            else 
            {
                subList = new Subtitles();
            }
            if (FileName == null)
                return(null);
            if (FileEncoding == null)
            {
                getFileEncoding();
            }
            // type of subtiles depends on the extension 
            // not perfect but practic
            string extension = getExtension(FileName);
            switch (extension)
            {
                case "srt":
                    subFile = new SrtSubtitlesFile();
                    break;
                case "txt":
                case "sub":
                    subFile = new SubSubtitlesFile();
                    break;
                default:
                    // spatna/nepodporovany format
                    //return (SubOpenStatus.BadFormat);
                    return (null);
            }
            subFile.ReadSubFile(FileName, FileEncoding, subList);
            //SubtitleType = sub.SubtitleType;
            
            return (subList);
        }

        internal Subtitles createNewSubtitles(Subtitles subList)
        {
            if (subList != null)
                subList.Clear();
            else
                subList = new Subtitles();
            subFile = new SrtSubtitlesFile();
            FileEncoding = Encoding.UTF8;
            subList.Add(new Subtitle(new TimeSpan(0, 0, 0, 1), new TimeSpan(0, 0, 0, MainWindow.newSubtitleLength), "new"));
            return (subList);
        }
        internal bool saveFileChanged(Subtitles subList)
        {
            if (!Changed)
                return(true);
            string message;
            if (What == WhichText.Original)
                message = "Original subtitles were changed. Save them?";
            else
                message = "Translation subtitles were changed. Save them?";
            MessageBoxResult result = MessageBox.Show(message, "Attention!", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Cancel)
            {
                return (false);
            }
            else if (result == MessageBoxResult.Yes)
            {
                if (!saveFunction(subList))
                    return (false);
            }
            Changed = false;
            return (true);
        }
        /// <summary>
        /// Save subtitles to file.
        /// Asks for name if it is a new file.
        /// </summary>
        /// <param name="subFile">File with subtitles.</param>
        /// <param name="what">if it is original or translation</param>
        /// <returns></returns>
        internal bool saveFunction(Subtitles subList)
        {
            if (subFile == null)
            {
                if (!getSaveFileName())
                    return (false);
            }
            SubFile.WriteSubFile(FileName, FileEncoding, subList,What);
            return (true);
        }
        internal void saveAsFunction(Subtitles subList)
        {
            if (getSaveFileName())
                SubFile.WriteSubFile(FileName, FileEncoding, subList, What);
        }
        
    }
}
