using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System;
namespace SFKSubEditor
{
    // 
    public partial class MainWindow : Window
    {

        void InitializeCommands()
        {
            // new original
            CommandBinding cmdBindingNewOriginal = new CommandBinding(MainWindowCommands.NewOriginalCommand);
            cmdBindingNewOriginal.Executed += new ExecutedRoutedEventHandler(cmdBindingNewOriginal_Executed);
            this.CommandBindings.Add(cmdBindingNewOriginal);
            // new translation
            CommandBinding cmdBindingNewTranslation = new CommandBinding(MainWindowCommands.NewTranslationCommand);
            cmdBindingNewTranslation.Executed += new ExecutedRoutedEventHandler(cmdBindingNewTranslation_Executed);
            this.CommandBindings.Add(cmdBindingNewTranslation);
            // open project
            CommandBinding cmdBindingOpenProject = new CommandBinding(MainWindowCommands.OpenProjectCommand);
            cmdBindingOpenProject.Executed += new ExecutedRoutedEventHandler(cmdBindingOpenProject_Executed);
            this.CommandBindings.Add(cmdBindingOpenProject);
            // open original
            CommandBinding cmdBindingOpenOriginal = new CommandBinding(MainWindowCommands.OpenOriginalCommand);
            cmdBindingOpenOriginal.Executed += new ExecutedRoutedEventHandler(cmdBindingOpenOriginal_Executed);
            this.CommandBindings.Add(cmdBindingOpenOriginal);
            // open translation
            CommandBinding cmdBindingOpenTranslation = new CommandBinding(MainWindowCommands.OpenTranslationCommand);
            cmdBindingOpenTranslation.Executed += new ExecutedRoutedEventHandler(cmdBindingOpenTranslation_Executed);
            this.CommandBindings.Add(cmdBindingOpenTranslation);
            // save project
            CommandBinding cmdBindingSaveProject = new CommandBinding(MainWindowCommands.SaveProjectCommand);
            cmdBindingSaveProject.Executed += new ExecutedRoutedEventHandler(cmdBindingSaveProject_Executed);
            this.CommandBindings.Add(cmdBindingSaveProject);
            // save original
            CommandBinding cmdBindingSaveOriginal = new CommandBinding(MainWindowCommands.SaveOriginalCommand);
            cmdBindingSaveOriginal.Executed += new ExecutedRoutedEventHandler(cmdBindingSaveOriginal_Executed);
            this.CommandBindings.Add(cmdBindingSaveOriginal);
            // save translation
            CommandBinding cmdBindingSaveTranslation = new CommandBinding(MainWindowCommands.SaveTranslationCommand);
            cmdBindingSaveTranslation.Executed += new ExecutedRoutedEventHandler(cmdBindingSaveTranslation_Executed);
            cmdBindingSaveTranslation.CanExecute += new CanExecuteRoutedEventHandler(cmdBindingSaveTranslation_CanExecute);
            this.CommandBindings.Add(cmdBindingSaveTranslation);
            // save as project
            CommandBinding cmdBindingSaveAsProject = new CommandBinding(MainWindowCommands.SaveAsProjectCommand);
            cmdBindingSaveAsProject.Executed += new ExecutedRoutedEventHandler(cmdBindingSaveAsProject_Executed);
            this.CommandBindings.Add(cmdBindingSaveAsProject);
            // save as original
            CommandBinding cmdBindingSaveAsOriginal = new CommandBinding(MainWindowCommands.SaveAsOriginalCommand);
            cmdBindingSaveAsOriginal.Executed += new ExecutedRoutedEventHandler(cmdBindingSaveAsOriginal_Executed);
            this.CommandBindings.Add(cmdBindingSaveAsOriginal);
            // save as translation
            CommandBinding cmdBindingSaveAsTranslation = new CommandBinding(MainWindowCommands.SaveAsTranslationCommand);
            cmdBindingSaveAsTranslation.Executed += new ExecutedRoutedEventHandler(cmdBindingSaveAsTranslation_Executed);
            cmdBindingSaveAsTranslation.CanExecute += new CanExecuteRoutedEventHandler(cmdBindingSaveTranslation_CanExecute);
            this.CommandBindings.Add(cmdBindingSaveAsTranslation);
            // close
            CommandBinding cmdBindingClose = new CommandBinding(MainWindowCommands.CloseCommand);
            cmdBindingClose.Executed += new ExecutedRoutedEventHandler(cmdBindingClose_Executed);
            this.CommandBindings.Add(cmdBindingClose);
            // exit
            CommandBinding cmdBindingExit = new CommandBinding(MainWindowCommands.ExitCommand);
            cmdBindingExit.Executed += new ExecutedRoutedEventHandler(cmdBindingExit_Executed);
            this.CommandBindings.Add(cmdBindingExit);

            // Menu video
            // open video
            CommandBinding cmdBindingOpenVideo = new CommandBinding(MainWindowCommands.OpenVideoCommand);
            cmdBindingOpenVideo.Executed += new ExecutedRoutedEventHandler(cmdBindingOpenVideo_Executed);
            this.CommandBindings.Add(cmdBindingOpenVideo);
            // close video
            CommandBinding cmdBindingCloseVideo = new CommandBinding(MainWindowCommands.CloseVideoCommand);
            cmdBindingCloseVideo.Executed += new ExecutedRoutedEventHandler(cmdBindingCloseVideo_Executed);
            cmdBindingCloseVideo.CanExecute += new CanExecuteRoutedEventHandler(cmdBindingVideo_CanExecute);
            this.CommandBindings.Add(cmdBindingCloseVideo);
            // play video
            CommandBinding cmdBindingVideoPlay = new CommandBinding(MainWindowCommands.VideoPlayCommand);
            cmdBindingVideoPlay.Executed += new ExecutedRoutedEventHandler(cmdBindingVideoPlay_Executed);
            cmdBindingVideoPlay.CanExecute += new CanExecuteRoutedEventHandler(cmdBindingVideo_CanExecute);
            this.CommandBindings.Add(cmdBindingVideoPlay);
            // stop video
            CommandBinding cmdBindingVideoStop = new CommandBinding(MainWindowCommands.VideoStopCommand);
            cmdBindingVideoStop.Executed += new ExecutedRoutedEventHandler(cmdBindingVideoStop_Executed);
            cmdBindingVideoStop.CanExecute += new CanExecuteRoutedEventHandler(cmdBindingVideo_CanExecute);
            this.CommandBindings.Add(cmdBindingVideoStop);
            // skip more backward
            CommandBinding cmdBindingVideoMoreBackward = new CommandBinding(MainWindowCommands.VideoMoreBackwardCommand);
            cmdBindingVideoMoreBackward.Executed += new ExecutedRoutedEventHandler(cmdBindingVideoMoreBackward_Executed);
            cmdBindingVideoMoreBackward.CanExecute += new CanExecuteRoutedEventHandler(cmdBindingVideo_CanExecute);
            this.CommandBindings.Add(cmdBindingVideoMoreBackward);
            // skip backward
            CommandBinding cmdBindingVideoBackward = new CommandBinding(MainWindowCommands.VideoBackwardCommand);
            cmdBindingVideoBackward.Executed += new ExecutedRoutedEventHandler(cmdBindingVideoBackward_Executed);
            cmdBindingVideoBackward.CanExecute += new CanExecuteRoutedEventHandler(cmdBindingVideo_CanExecute);
            this.CommandBindings.Add(cmdBindingVideoBackward);
            // skip forward
            CommandBinding cmdBindingVideoForward = new CommandBinding(MainWindowCommands.VideoForwardCommand);
            cmdBindingVideoForward.Executed += new ExecutedRoutedEventHandler(cmdBindingVideoForward_Executed);
            cmdBindingVideoForward.CanExecute += new CanExecuteRoutedEventHandler(cmdBindingVideo_CanExecute);
            this.CommandBindings.Add(cmdBindingVideoForward);
            // skip more forward
            CommandBinding cmdBindingVideoMoreForward = new CommandBinding(MainWindowCommands.VideoMoreForwardCommand);
            cmdBindingVideoMoreForward.Executed += new ExecutedRoutedEventHandler(cmdBindingVideoMoreForward_Executed);
            cmdBindingVideoMoreForward.CanExecute += new CanExecuteRoutedEventHandler(cmdBindingVideo_CanExecute);
            this.CommandBindings.Add(cmdBindingVideoMoreForward);
            // skip to previous subtitle 
            CommandBinding cmdBindingVideoPreviousSubtitle = new CommandBinding(MainWindowCommands.VideoPreviousSubtitleCommand);
            cmdBindingVideoPreviousSubtitle.Executed += new ExecutedRoutedEventHandler(cmdBindingVideoPreviousSubtitle_Executed);
            cmdBindingVideoPreviousSubtitle.CanExecute += new CanExecuteRoutedEventHandler(cmdBindingVideo_CanExecute);
            this.CommandBindings.Add(cmdBindingVideoPreviousSubtitle);
            // skip to next subtitle
            CommandBinding cmdBindingVideoNextSubtitle = new CommandBinding(MainWindowCommands.VideoNextSubtitleCommand);
            cmdBindingVideoNextSubtitle.Executed += new ExecutedRoutedEventHandler(cmdBindingVideoNextSubtitle_Executed);
            cmdBindingVideoNextSubtitle.CanExecute += new CanExecuteRoutedEventHandler(cmdBindingVideo_CanExecute);
            this.CommandBindings.Add(cmdBindingVideoNextSubtitle);

        }
            
        void cmdBindingExit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (savedAllChanged())
                Application.Current.Shutdown();
        }

        void cmdBindingNewOriginal_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (savedAllChanged())
            {
                newOriginalFunction();
                showColumns();
            }
        }
        void newOriginalFunction()
        {
            originalFile = new SubtitlesFile(SubtitlesFile.WhichText.Original);
            subtitleList = originalFile.createNewSubtitles(subtitleList);
            translationFile = null;
            if (video.IsLoaded)
                StopVideo();
        }
       void cmdBindingNewTranslation_Executed(object sender, ExecutedRoutedEventArgs e)
        {
           translationFile = new SubtitlesFile(SubtitlesFile.WhichText.Translation);
           Subtitles translationSubtitles = translationFile.createNewSubtitles(null);
           foreach (Subtitle sub in subtitleList)
           {
               sub.Translation = Properties.Settings.Default.newTranslationText;
           }
           showColumns();
        }

       
       void cmdBindingOpenProject_Executed(object sender, ExecutedRoutedEventArgs e)
       {
           if (savedAllChanged())
           {
               MessageBox.Show("Open project");
           }
       }
       void cmdBindingOpenOriginal_Executed(object sender, ExecutedRoutedEventArgs e)
       {
           if (savedAllChanged())
           {
               // if user cancel the selection the original remains unchanged
               SubtitlesFile newOriginalFile = new SubtitlesFile(SubtitlesFile.WhichText.Original);
               if (!newOriginalFile.getOpenFileName("Choose original subtitles"))
                    return;
               newOriginalFunction();
               originalFile = newOriginalFile;
               subtitleList = originalFile.fileOpen(subtitleList);               
               videoFile = getVideoFileFromSubtitle(originalFile.FileName);
               if (videoFile != null)
                    video.Source = new Uri(videoFile, UriKind.Relative);
               showColumns();
           }
       }
       void cmdBindingOpenTranslation_Executed(object sender, ExecutedRoutedEventArgs e)
       {
           translationFile = new SubtitlesFile(SubtitlesFile.WhichText.Translation);

           if (translationFile.getOpenFileName("Choose translation subtitles"))
               {
                    Subtitles translationSubtitles = translationFile.fileOpen(null);
                    addTranslation(translationSubtitles);
                    showColumns();
               }
       }
       /// <summary>
       /// Add translation to the list with original subtitles.
       /// </summary>
       /// <param name="translationSubtitles">List with translation.</param>
       private void addTranslation(Subtitles translationSubtitles)
       {
           if (savedAllChanged())
           {
               int i = 0;
               foreach (Subtitle sub in subtitleList)
               {
                   if (i < translationSubtitles.Count)
                   {
                       sub.Translation = translationSubtitles[i].Original;
                   }
                   else
                   {
                       sub.Translation = Properties.Settings.Default.newTranslationText;
                   }
                   i++;    
               }
               // in the translation can be more than rows then in the original
               // it is not good to just ignore them
               for (int j = i; j < translationSubtitles.Count; j++)
               {
                   Subtitle sub = new Subtitle();
                   sub.Translation = translationSubtitles[j].Original;
                   subtitleList.Add(sub);
               }
               i++;
           }
           
       }
       void cmdBindingSaveProject_Executed(object sender, ExecutedRoutedEventArgs e)
       {
           if (savedAllChanged())
           {
               MessageBox.Show("Save project");
           }
       }
       void cmdBindingSaveOriginal_Executed(object sender, ExecutedRoutedEventArgs e)
       {
           originalFile.saveFunction(subtitleList);
       }
       void cmdBindingSaveTranslation_Executed(object sender, ExecutedRoutedEventArgs e)
       {
          translationFile.saveFunction(subtitleList);
       }
       void cmdBindingSaveTranslation_CanExecute(object sender, CanExecuteRoutedEventArgs e)
       {
           e.CanExecute = translationFile != null;
       }

        
       void cmdBindingSaveAsProject_Executed(object sender, ExecutedRoutedEventArgs e)
       {
           if (savedAllChanged())
           {
               MessageBox.Show("Save As project");
           }
       }
       void cmdBindingSaveAsOriginal_Executed(object sender, ExecutedRoutedEventArgs e)
       {
           originalFile.saveAsFunction(subtitleList);
           /*
           if (originalFile.FileName != null)
           {
               if (!originalFile.getSaveFileName())
               {
                   return;
               }
               originalFile.SubFile.WriteSubFile(originalFile.FileName, originalFile.FileEncoding, subtitleList, SubtitlesFile.WhichText.Original);
           }
            */ 
       }
       void cmdBindingSaveAsTranslation_Executed(object sender, ExecutedRoutedEventArgs e)
       {
           translationFile.saveAsFunction(subtitleList);
           /*
           if (translationFile.FileName != null)
           {
               if (!translationFile.getSaveFileName())
               {
                   return;
               }
               translationFile.SubFile.WriteSubFile(originalFile.FileName, originalFile.FileEncoding, subtitleList, SubtitlesFile.WhichText.Translation);
           }
            */
       }
       void cmdBindingClose_Executed(object sender, ExecutedRoutedEventArgs e)
       {
           newOriginalFunction();
           showColumns();
           if (video.IsLoaded)
               video.Close();
           videoFile = null;
       }

       void cmdBindingOpenVideo_Executed(object sender, ExecutedRoutedEventArgs e)
       {
           string fileName = getVideoFileName();
           if (fileName == null)
               return;
           videoFile = fileName;
           video.Source = new Uri(videoFile, UriKind.Relative);
           PlayVideo();
       }
       void cmdBindingCloseVideo_Executed(object sender, ExecutedRoutedEventArgs e)
       {
           if (savedAllChanged())
           {
               MessageBox.Show("Close video");
           }
       }
       void cmdBindingVideo_CanExecute(object sender, CanExecuteRoutedEventArgs e)
       {
           e.CanExecute = videoFile != null;
       }
       void cmdBindingVideoPlay_Executed(object sender, ExecutedRoutedEventArgs e)
       {
           if (isVideoPlaying)
               PauseVideo();
           else
               PlayVideo();
       }
       void cmdBindingVideoStop_Executed(object sender, ExecutedRoutedEventArgs e)
       {
           StopVideo();
       }
       void cmdBindingVideoMoreBackward_Executed(object sender, ExecutedRoutedEventArgs e)
       {
       }
       void cmdBindingVideoBackward_Executed(object sender, ExecutedRoutedEventArgs e)
       {
       }
       void cmdBindingVideoForward_Executed(object sender, ExecutedRoutedEventArgs e)
       {
       }
       void cmdBindingVideoMoreForward_Executed(object sender, ExecutedRoutedEventArgs e)
       {
       }
       void cmdBindingVideoPreviousSubtitle_Executed(object sender, ExecutedRoutedEventArgs e)
       {
       }
       void cmdBindingVideoNextSubtitle_Executed(object sender, ExecutedRoutedEventArgs e)
       {
       }
    }
}