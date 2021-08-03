using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace IdSort
{
    public partial class FileInfoControl : UserControl
    {
        private GenericFile _file;

        public FileInfoControl()
        {
            InitializeComponent();

            SetFile(null);
        }

        public void SetFile(GenericFile file)
        {
            _file = file;

            if (file == null)
            {
                Visible = false;
                return;
            }

            Visible = true;

            fileNameLabel.Text = file.Name;
            fileDescriptionLabel.Text = file.Description;

            if (file is AudioFile audioFile && audioFile.Tags != null)
            {
                // Show play button
                playButton.Visible = true;

                // Build tags label
                var labels = new StringBuilder();
                var values = new StringBuilder();

                foreach (FieldInfo field in typeof(Tags).GetFields())
                {
                    var tagAttrib = field.GetCustomAttribute<Tags.TagFieldAttribute>();
                    if (tagAttrib == null) continue;

                    labels.AppendLine(tagAttrib.Name);

                    object val = field.GetValue(audioFile.Tags);
                    if (val is string[] strArray)
                        values.AppendLine(string.Join(", ", strArray).Replace("\n", ""));
                    else if (val is null)
                        values.AppendLine(); // blank line (no data)
                    else
                        values.AppendLine(val.ToString()?.Replace("\n", ""));

                }

                tagNamesLabels.Text = labels.ToString();
                tagValuesLabel.Text = values.ToString();

                tagNamesLabels.Visible = true;
                tagValuesLabel.Visible = true;
            }
            else
            {
                playButton.Visible = false;

                tagNamesLabels.Visible = false;
                tagValuesLabel.Visible = false;
            }
        }

        private void FileInfoControl_Load(object sender, EventArgs e)
        {
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            if (!(_file is AudioFile)) return;

            Process.Start(@"C:\Program Files\VideoLAN\VLC\vlc.exe", $"--started-from-file \"{_file.Path}\"");
        }
    }
}
