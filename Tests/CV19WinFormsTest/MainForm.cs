using System;
using System.Threading;
using System.Windows.Forms;

namespace CV19WinFormsTest
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            new Thread(ComputeValue).Start();
        }

        private void ComputeValue()
        {
            var value = LognProcess(DateTime.Now);
            SetResultValue(value);
            //if (ResultLabel.InvokeRequired)
            //    ResultLabel.Invoke(new Action(() => ResultLabel.Text = value));
            //else
            //    ResultLabel.Text = value;
        }

        private void SetResultValue(string Value)
        {
            if (ResultLabel.InvokeRequired)
                ResultLabel.Invoke(new Action<string>(SetResultValue), Value);
            else
                ResultLabel.Text = Value;
        }

        private static string LognProcess(DateTime Time)
        {
            Thread.Sleep(3000);

            return $"Value: {Time}";
        }
    }
}
