using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using static System.Net.Mime.MediaTypeNames;

namespace MVVM.VeiwModels
{
    public class RandTextViewModel : INotifyPropertyChanged
    {
        private string _generatedText;
        public RandTextViewModel()
        {
            GenText = new RelayCommand(GenerText);
            SaveCom = new RelayCommand(Save);
        }
        public ICommand GenText { get; }
        public ICommand SaveCom { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public string GeneratedText
        {
            get { return _generatedText; }
            set
            {
                if (_generatedText != value)
                {
                    _generatedText = value;
                    OnPropertyChanged();
                }
            }
        }

        public void GenerText()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var result = new char[random.Next(500)];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = chars[random.Next(chars.Length)];
            }
            GeneratedText = new string(result);
        }
        public void Save()
        {
            var saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                string sb;
                sb = GeneratedText;
                File.WriteAllText(saveFileDialog.FileName, GeneratedText);
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
