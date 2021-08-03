using System;
using System.Windows.Forms;
using IdSort.FileVars;

namespace IdSort.Restructure
{
    public partial class RestructureControl : UserControl
    {
        public event EventHandler<RequestPreviewRestructureEventArgs> RequestPreviewRestructure;

        public RestructureControl()
        {
            InitializeComponent();
        }

        private void RestructureControl_Load(object sender, EventArgs e)
        {
            foreach (IFileVar var in FileVarHelper.Vars)
            {
                var button = new Button
                {
                    Text = var.Description,
                };
                button.Click += (o, args) =>
                {
                    int selectionStart = outputFormatString.SelectionStart;
                    outputFormatString.Text = outputFormatString.Text.Insert(selectionStart, "{" + var.Name + "}");
                    outputFormatString.Focus();
                    outputFormatString.SelectionStart = selectionStart + ("{" + var.Name + "}").Length;
                    outputFormatString.SelectionLength = 0;
                };
                fileVarButtons.Controls.Add(button);
            }
        }

        private void restructureButton_Click(object sender, EventArgs e)
        {
            RequestPreviewRestructure?.Invoke(this, new RequestPreviewRestructureEventArgs
            {
                Settings = new Restructurer.RestructureSettings(outputFormatString.Text.Split('\n'))
                {
                    NeverUseVariousArtists = neverUseVariousArtistsCheckbox.Checked
                }
            });
        }

        public class RequestPreviewRestructureEventArgs : EventArgs
        {
            public Restructurer.RestructureSettings Settings;
        }
    }
}
