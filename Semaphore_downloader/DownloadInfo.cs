﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Semaphore_downloader
{
    // клас, який містить необхідну інформацію про процес завантажування
    public class DownloadInfo : INotifyPropertyChanged
    {
        private int progress;

        public string SourceFile { get; set; }
        public string SourceFileName => Path.GetFileName(SourceFile);

        // прогрес виконання операції
        // до цієї властивості "біндиться" ProgressBar, тому викликаємо PropertyChanged
        public int Progress
        {
            get { return progress; }
            set
            {
                progress = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
