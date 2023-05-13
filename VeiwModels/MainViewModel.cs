using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace MVVM
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _text;

        public MainViewModel()
        {
            NewCommand = new RelayCommand(OnNew);
            OpenCommand = new RelayCommand(OnOpen);
            SaveCommand = new RelayCommand(OnSave);
            GenCommand = new RelayCommand(GenOpen);
            ImgCommand = new RelayCommand(ImgOpen);
        }

        private void GenOpen()
        {
            RadnomText imager = new RadnomText();
            imager.Show();
        }
        private void ImgOpen()
        {
            Imager imager = new Imager();
            imager.Show();
        }
        public ICommand NewCommand { get; }
        public ICommand OpenCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand GenCommand { get; }
        public ICommand ImgCommand { get; }

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnNew()
        {
            Text = string.Empty;
        }

        private void OnOpen()
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                using (var streamReader = new StreamReader(openFileDialog.FileName))
                {
                    Text = File.ReadAllText(openFileDialog.FileName);
                }
            }
        }

        private void OnSave()
        {
            var saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                string sb;
                sb = Text;
                MessageBox.Show(Text);
                File.WriteAllText(saveFileDialog.FileName, Text);
            }
        }
    }
}
