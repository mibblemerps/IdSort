using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IdSort
{
    public partial class Importer : Form
    {
        private readonly Action<Workspace> _importComplete;
        private CancellationTokenSource _cancel;

        public Importer(Action<Workspace> importComplete)
        {
            _importComplete = importComplete;

            InitializeComponent();
        }

        private void ImportForm_Load(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Music Root Folder";
            folderBrowserDialog.AutoUpgradeEnabled = true;
            folderBrowserDialog.UseDescriptionForTitle = true;
            folderBrowserDialog.ShowNewFolderButton = false;
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyMusic;

            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
            {
                // Folder browse cancel
                Close();
                return;
            }

            StartImport(folderBrowserDialog.SelectedPath);
        }

        private async void StartImport(string path)
        {
            if (ImportCache.IsCachedWorkspaceAvailable(path, out DateTime modifiedAt))
            {
                // Cached workspace available
                var result = MessageBox.Show($"This folder has cached file and tag information from {modifiedAt}.\n" +
                                             $"Do you want to use this cached data?", "Cached Data", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                {
                    _importComplete?.Invoke(ImportCache.GetCachedWorkspace(path));
                    Close();
                    return;
                }

                if (result != DialogResult.No)
                {
                    // Cancel
                    Close();
                    return;
                }
            }

            var workspace = new Workspace();

            importingFromLabel.Text = path;

            _cancel = new CancellationTokenSource();

            // Index path on another thread
            await Task.Run(() => workspace.Index(path, _cancel.Token, OnIndexReport));

            importProgressBar.Value = importProgressBar.Maximum;

            // Try to cache imported workspace
            try
            {
                ImportCache.CacheWorkspace(workspace);
            }
            catch (Exception e)
            {
                MessageBox.Show("An exception occurred trying to cache directory!\n" + e.Message, "Cache Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (!_cancel.IsCancellationRequested) // only say import is complete if we didn't cancel
                _importComplete?.Invoke(workspace);

            Close();
        }

        private void OnIndexReport(string file, int completed, int outOf)
        {
            if (_cancel.IsCancellationRequested)
                return;

            importProgressBar.Invoke((MethodInvoker) delegate
            {
                currentlyWorkingOnLabel.Text = $"Importing: {Path.GetFileName(file)}";

                progressLabel.Text = $"Imported Files: {completed}";

                importProgressBar.Maximum = outOf;
                importProgressBar.Value = completed;
            });
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            _cancel?.Cancel();
            Close();
        }
    }
}
