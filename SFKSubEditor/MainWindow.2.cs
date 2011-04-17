using System;
using System.Windows;
using System.IO;
using System.Text;
using System.Windows.Controls;

namespace SFKSubEditor
{
    // 
    public partial class MainWindow : Window
    {
       
        /// <summary>
        /// Check, if the subtitles were change. Ask if the user wants to save them.
        /// </summary>
        /// <returns>true - continue with action. false - cancel the action.</returns>
        private bool savedAllChanged()
        {
            if (!originalFile.saveFileChanged(subtitleList))
                return(false);
            if (translationFile != null && !translationFile.saveFileChanged(subtitleList))
                return (false);
            return(true);
        }       

        /// <summary>
        /// Visbility of column in the DataGrid.
        /// </summary>
        private void showColumns()
        {
            DataGridColumn translation = dataGrid1.ColumnFromDisplayIndex(3);
            if (translationFile != null)
            {
                translation.Visibility = Visibility.Visible;
                translationText.Visibility = Visibility.Visible;
            } 
            else 
            {
                translation.Visibility = Visibility.Hidden;
                translationText.Visibility = Visibility.Hidden;
            }

        }
        /// <summary>
        /// Try to look for a video file in the current diretory.
        /// 1. just take away file extension
        /// 2. it is possible that subtitles have language exstension (.cz, .dk)
        /// 3. in the directory is only one video file
        /// </summary>
        /// <param name="originalFile">Name of the file or null when it wasn't possible to find it.</param>
        internal string getVideoFileFromSubtitle(string fileName)
        {
            if (fileName.LastIndexOf(".") > -1)
            {
                string withoutExt = fileName.Substring(0, fileName.LastIndexOf("."));
                foreach (string vidExt in videoFileExtensions)
                {
                    if (File.Exists(withoutExt + "." + vidExt))
                        return (withoutExt + "." + vidExt);
                }

                if (withoutExt.LastIndexOf(".") > -1)
                {
                    withoutExt = withoutExt.Substring(0, withoutExt.LastIndexOf("."));
                    foreach (string vidExt in videoFileExtensions)
                    {
                        if (File.Exists(withoutExt + "." + vidExt))
                            return (withoutExt + "." + vidExt);
                    }
                }
            }
            string dir = ".";
            // the directory from the name of subtitles
            if (fileName.LastIndexOf(Path.DirectorySeparatorChar) > -1)
                dir = fileName.Substring(0, fileName.LastIndexOf(Path.DirectorySeparatorChar));
            DirectoryInfo di = new DirectoryInfo(dir);
            string vFile = null;
            foreach (string vidExt in videoFileExtensions)
            {
                FileInfo[] vFiles = di.GetFiles("*." + vidExt);
                if (vFiles.Length == 1)
                { // exist only one file of the exstension
                    if (vFile != null)
                        // there is allready a file
                        return (null);
                    else
                        vFile = vFiles[0].FullName;
                }
                else if (vFiles.Length > 1)
                    return (null);

            }
            if (vFile != null)
                return (vFile);
            else
                return (null);
        }

        internal string getVideoFileName()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            //dlg.InitialDirectory = MainWindow.lastDirectory;

            string fileFilter = "";
            foreach (string ext in videoFileExtensions)
            {
                fileFilter += ext + " files (*." + ext + ")|*." + ext + "|";
            }
            fileFilter += "All files|*.*";
            dlg.DefaultExt = ".srt"; // Default file extension
            dlg.Filter = fileFilter;
            dlg.Title = "Select video file";
            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();
            // Process open file dialog box results
            if (result == true)
            {
                MainWindow.lastDirectory = dlg.InitialDirectory;
                return (dlg.FileName);
            }
            else
                return (null);
        }
        internal void PlayVideo()
        {
            if (videoFile == null)
                return;
            video.Play();
            timer.Start();
            isVideoPlaying = true;
        }
        internal void StopVideo()
        {
            if (videoFile == null)
                return;
            video.Stop();
            timer.Stop();
            isVideoPlaying = false;
        }
        internal void PauseVideo()
        {
            if (videoFile == null)
                return;
            video.Pause();
            timer.Stop();
            isVideoPlaying = false;
        }
    }
 
}