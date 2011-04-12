using System.Windows;
using System.Windows.Input;
namespace SFKSubEditor
{
    /// <summary>
    /// Commands used in editor.
    /// </summary>
    public static class MainWindowCommands
    {
        
        public static RoutedUICommand NewOriginalCommand;
        public static RoutedUICommand NewTranslationCommand;
        public static RoutedUICommand OpenProjectCommand;
        public static RoutedUICommand OpenOriginalCommand;
        public static RoutedUICommand OpenTranslationCommand;
        public static RoutedUICommand SaveProjectCommand;
        public static RoutedUICommand SaveOriginalCommand;
        public static RoutedUICommand SaveTranslationCommand;
        public static RoutedUICommand SaveAsProjectCommand;
        public static RoutedUICommand SaveAsOriginalCommand;
        public static RoutedUICommand SaveAsTranslationCommand;
        public static RoutedUICommand CloseCommand;
        public static RoutedUICommand ExitCommand;

        public static RoutedUICommand OpenVideoCommand;
        public static RoutedUICommand CloseVideoCommand;

        public static RoutedUICommand VideoPlayCommand;
        public static RoutedUICommand VideoStopCommand;
        public static RoutedUICommand VideoMoreBackwardCommand;
        public static RoutedUICommand VideoBackwardCommand;
        public static RoutedUICommand VideoForwardCommand;
        public static RoutedUICommand VideoMoreForwardCommand;
        public static RoutedUICommand VideoPreviousSubtitleCommand;
        public static RoutedUICommand VideoNextSubtitleCommand;

        static MainWindowCommands()
        {
            // Menu file
            // new original
            InputGestureCollection newOriginalInputs = new InputGestureCollection();
            newOriginalInputs.Add(new KeyGesture(Key.N, ModifierKeys.Control));
            NewOriginalCommand = new RoutedUICommand("New original", "NewOriginal",
                typeof(MainWindowCommands), newOriginalInputs);
            // new translation
            InputGestureCollection newTranslationInputs = new InputGestureCollection();
            newTranslationInputs.Add(new KeyGesture(Key.N, ModifierKeys.Control | ModifierKeys.Shift));
            NewTranslationCommand = new RoutedUICommand("New translation", "NewTranslation",
                typeof(MainWindowCommands), newTranslationInputs);
            // open project
            InputGestureCollection openProjectInputs = new InputGestureCollection();
            openProjectInputs.Add(new KeyGesture(Key.P, ModifierKeys.Control));
            OpenProjectCommand = new RoutedUICommand("Open project", "OpenProject",
                typeof(MainWindowCommands), openProjectInputs);
            // open original
            InputGestureCollection openOriginalInputs = new InputGestureCollection();
            openOriginalInputs.Add(new KeyGesture(Key.O, ModifierKeys.Control));
            OpenOriginalCommand = new RoutedUICommand("Open original", "OpenOriginal",
                typeof(MainWindowCommands), openOriginalInputs);
            // open translation
            InputGestureCollection openTranslationInputs = new InputGestureCollection();
            openTranslationInputs.Add(new KeyGesture(Key.O, ModifierKeys.Control | ModifierKeys.Shift));
            OpenTranslationCommand = new RoutedUICommand("Open translation", "OpenTranslation",
                typeof(MainWindowCommands), openTranslationInputs);
            // save project
            InputGestureCollection saveProjectInputs = new InputGestureCollection();
            saveProjectInputs.Add(new KeyGesture(Key.S, ModifierKeys.Control | ModifierKeys.Alt));
            SaveProjectCommand = new RoutedUICommand("Save project", "SaveProject",
                typeof(MainWindowCommands), saveProjectInputs);
            // save original
            InputGestureCollection saveOriginalInputs = new InputGestureCollection();
            saveOriginalInputs.Add(new KeyGesture(Key.S, ModifierKeys.Control));
            SaveOriginalCommand = new RoutedUICommand("Save original", "SaveOriginal",
                typeof(MainWindowCommands), saveOriginalInputs);
            // save translation
            InputGestureCollection saveTranslationInputs = new InputGestureCollection();
            saveTranslationInputs.Add(new KeyGesture(Key.S, ModifierKeys.Control | ModifierKeys.Shift));
            SaveTranslationCommand = new RoutedUICommand("Save translation", "SaveTranslation",
                typeof(MainWindowCommands), saveTranslationInputs);
            //save as project
            SaveAsProjectCommand = new RoutedUICommand("Save As project", "SaveAsProject",
                typeof(MainWindowCommands));
            // save original
            SaveAsOriginalCommand = new RoutedUICommand("Save As original", "SaveAsOriginal",
                typeof(MainWindowCommands));
            // save translation
            SaveAsTranslationCommand = new RoutedUICommand("Save As translation", "SaveAsTranslation",
                typeof(MainWindowCommands));
            // close
            CloseCommand = new RoutedUICommand("Close", "Close",
                typeof(MainWindowCommands));
            // exit 
            InputGestureCollection exitInputs = new InputGestureCollection();
            exitInputs.Add(new KeyGesture(Key.F4, ModifierKeys.Alt));
            ExitCommand = new RoutedUICommand("Exit application", "ExitApplication",
                typeof(MainWindowCommands), exitInputs);

            // Menu Video
            // open video
            InputGestureCollection openVideoInputs = new InputGestureCollection();
            openVideoInputs.Add(new KeyGesture(Key.P, ModifierKeys.Alt));
            OpenVideoCommand = new RoutedUICommand("Open video", "OpenVideo",
                typeof(MainWindowCommands), openVideoInputs);
            // close video
            InputGestureCollection closeVideoInputs = new InputGestureCollection();
            closeVideoInputs.Add(new KeyGesture(Key.P, ModifierKeys.Alt | ModifierKeys.Shift));
            CloseVideoCommand = new RoutedUICommand("Close video", "CloseVideo",
                typeof(MainWindowCommands), closeVideoInputs);
            // play/pause video
            InputGestureCollection VideoPlayInputs = new InputGestureCollection();
            VideoPlayInputs.Add(new KeyGesture(Key.Space, ModifierKeys.Control));
            VideoPlayCommand = new RoutedUICommand("Play Video", "PlayVideo",
                typeof(MainWindowCommands), VideoPlayInputs);
            // stop video
            InputGestureCollection VideoStopInputs = new InputGestureCollection();
            VideoStopInputs.Add(new KeyGesture(Key.Back, ModifierKeys.Control));
            VideoStopCommand = new RoutedUICommand("Stop Video", "StopVideo",
                typeof(MainWindowCommands), VideoStopInputs);
            // skip more backward
            InputGestureCollection VideoMoreBackwardInputs = new InputGestureCollection();
            VideoMoreBackwardInputs.Add(new KeyGesture(Key.Left, ModifierKeys.Alt | ModifierKeys.Shift));
            VideoMoreBackwardCommand = new RoutedUICommand("Skip more backward", "SkipMoreBackward",
                typeof(MainWindowCommands), VideoMoreBackwardInputs);
            // skip backaward
            InputGestureCollection VideoBackwardInputs = new InputGestureCollection();
            VideoBackwardInputs.Add(new KeyGesture(Key.Left, ModifierKeys.Alt));
            VideoBackwardCommand = new RoutedUICommand("Skip backward", "SkipBackward",
                typeof(MainWindowCommands), VideoBackwardInputs);
            // skip forward
            InputGestureCollection VideoForwardInputs = new InputGestureCollection();
            VideoForwardInputs.Add(new KeyGesture(Key.Right, ModifierKeys.Alt));
            VideoForwardCommand = new RoutedUICommand("Skip forward", "SkipForward",
                typeof(MainWindowCommands), VideoForwardInputs);
            // skip more forward
            InputGestureCollection VideoMoreForwardInputs = new InputGestureCollection();
            VideoMoreForwardInputs.Add(new KeyGesture(Key.Right, ModifierKeys.Alt | ModifierKeys.Shift));
            VideoMoreForwardCommand = new RoutedUICommand("Skip more forward", "SkipMoreForward",
                typeof(MainWindowCommands), VideoMoreForwardInputs);
            // skip to previous subtitle
            InputGestureCollection VideoPreviousSubtitleInputs = new InputGestureCollection();
            VideoPreviousSubtitleInputs.Add(new KeyGesture(Key.Left, ModifierKeys.Control));
            VideoPreviousSubtitleCommand = new RoutedUICommand("Skip to previous subtitle", "SkipPreviousSubtitle",
                typeof(MainWindowCommands), VideoPreviousSubtitleInputs);
            // skip to next subtitle
            InputGestureCollection VideoNextSubtitleInputs = new InputGestureCollection();
            VideoNextSubtitleInputs.Add(new KeyGesture(Key.Right, ModifierKeys.Control));
            VideoNextSubtitleCommand = new RoutedUICommand("Skip to next subtitle", "SkipNextSubtitle",
                typeof(MainWindowCommands), VideoNextSubtitleInputs);

        }
    }

   
}