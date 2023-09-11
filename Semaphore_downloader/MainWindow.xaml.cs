using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;

namespace Semaphore_downloader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Random rnd = new Random();
        private Semaphore download_locker = new Semaphore(3, 3);

        ObservableCollection<DownloadInfo> downloads = new ObservableCollection<DownloadInfo>();

        public MainWindow()
        {
            InitializeComponent();

            operationsList.ItemsSource = downloads;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DownloadInfo info = new DownloadInfo()
            {
                SourceFile = urlTextBox.Text,
                Progress = 0
            };

            downloads.Add(info);

            //Thread thread = new Thread(Download);
            //thread.Start(info);
            ThreadPool.QueueUserWorkItem(Download, info);
        }

        private void Download(object obj)
        {
            if (!(obj is DownloadInfo info)) return;

            download_locker.WaitOne(); // wait for unlock...
            // do operaiton...
            while (info.Progress < 99)
            {
                info.Progress += rnd.Next(5);
                Thread.Sleep(rnd.Next(250));
            }
            info.Progress = 100;
            download_locker.Release(); // unlock
        }
    }
}
