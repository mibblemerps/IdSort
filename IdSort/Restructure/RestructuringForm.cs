using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IdSort.Restructure
{
    public partial class RestructuringForm : Form
    {
        public event EventHandler Success;

        private Plan _plan;

        public RestructuringForm(Plan plan, bool revert = false)
        {
            InitializeComponent();

            _plan = plan;

            if (revert)
                Reverting();
        }

        private void RestructuringForm_Load(object sender, EventArgs e)
        {
            if (_plan.Operations.Count == 0)
            {
                MessageBox.Show("No changes staged.", "IdSort", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Close();
                return;
            }

            sortProgressBar.Maximum = _plan.Operations.Count;

            Task.Run(() => Sort());
        }

        private void Sort()
        {
            try
            {
                _plan.Execute(OnOperationStart, out var revertPlan);

                // Save plan to revert this later
                revertPlan.Save(_plan.Root + Path.DirectorySeparatorChar + Plan.RevertFileName);

                // Success
                Invoke((MethodInvoker)delegate
                {
                    Success?.Invoke(this, EventArgs.Empty);

                    Process.Start("explorer.exe", _plan.Root);
                    Close();
                });
            }
            catch (PlanException e)
            {
                // Failed to sort
                Invoke((MethodInvoker) delegate
                {
                    MessageBox.Show("Failed to sort music! IdSort will attempt to revert changes.\n\n" + e.Operation.ToString() + "\n\n" + e.InnerException?.Message, "Sort Failure",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    sortProgressBar.Value = 0;
                    sortProgressBar.Maximum = _plan.Operations.Count;
                    Reverting();
                });

                // Attempt to revert changes
                try
                {
                    e.RevertPlan.Execute(OnOperationStart, out _);
                }
                catch (PlanException revertEx)
                {
                    // Failed to revert changes. Music folder left in an unknown state! Not good!
                    MessageBox.Show("Failed to revert changes!\n\n" + revertEx.Message, "Sort Failure",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Reverting()
        {
            titleLabel.Text = "Reverting...";
        }

        private void OnOperationStart(Operation obj)
        {
            Invoke((MethodInvoker) delegate
            {
                sortProgressBar.Increment(1);
                workingOnLabel.Text = obj.FromPath;
            });
        }
    }
}
