using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using jeah = System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Savify_Desktop
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            textbox_output_directory.Text = Properties.Settings.Default.defaultpath;

            ProcessStartInfo startInfo = new ProcessStartInfo("savify", "--version")
            {
                UseShellExecute = false,
                CreateNoWindow = false,
                RedirectStandardOutput = true
            };
            Process process_get_version = new Process { StartInfo = startInfo };

            //process_get_version.OutputDataReceived += Process_get_version_OutputDataReceived;
            //process_get_version.EnableRaisingEvents = true;
            //process_get_version.Exited += Process_get_version_Exited;
            process_get_version.Start();
            label_version.Content = process_get_version.StandardOutput.ReadToEnd();
            //Process.Start("savify", "--version").StandardOutput.ReadToEndAsync();
        }

        private void Process_get_version_Exited(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show((sender as Process).StandardOutput.ReadToEnd());
                //label_version.Content = "jeah";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Process_get_version_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            try
            {
                label_version.Content = e.Data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                jeah.FolderBrowserDialog output_directory = new jeah.FolderBrowserDialog();
                output_directory.Description = "Ziel-Verzeichnis";

                if (output_directory.ShowDialog().Equals(jeah.DialogResult.OK))
                {
                    textbox_output_directory.Text = output_directory.SelectedPath;
                }

                //OpenFileDialog output_directory = new OpenFileDialog();

                //if (output_directory.ShowDialog() == true)
                //{
                //    textbox_output_directory.Text = output_directory.FileName;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Process savify = Process.Start("savify",
                    " --type " + combobox_query_type.Text
                    + " --quality " + combobox_bitrate.Text
                    + " --format " + combobox_format.Text
                    + (textbox_grouping.Text != "" ? " --group " + textbox_grouping.Text : "")
                    + " --output " + textbox_output_directory.Text
                    + " " + textbox_query.Text
                    + (checkbox_no_cover_art.IsChecked == true ? " --skip-cover-art" : "")
                    );
                string test =
                    " --type " + combobox_query_type.Text
                    + " --quality " + combobox_bitrate.Text
                    + " --format " + combobox_format.Text
                    + (textbox_grouping.Text == "" ? " --group " + textbox_grouping.Text : "")
                    + " --output " + textbox_output_directory.Text
                    + " " + textbox_query.Text
                    + (checkbox_no_cover_art.IsChecked == true ? " --skip-cover-art" : "")
                    ;
                //MessageBox.Show(savify.StandardOutput.ReadLine());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("explorer", textbox_output_directory.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
