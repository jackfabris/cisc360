/**
 * Seth Morecraft
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using GeminiCore;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public CPU myCPU;

        public Form1()
        {
            myCPU = new CPU();

            InitializeComponent();

#if DEBUG
            loadFileButton.Text = "Hello";
#endif
        }

        #region Events
        private void loadFileButton_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        var ipe = new IPE(ofd.FileName);
                        ipe.ParseFile();
                    }
                    catch (Exception err)
                    {
                        // show a dialog with error
                    }
                }
            }
        }
        #endregion

        private void nextInstructionButton_Click(object sender, EventArgs e)
        {
            this.myCPU.nextInstruction();
            this.setCPUValuesToView();
        }

        public void setCPUValuesToView()
        {
            this.accLabel.Text = this.myCPU.ACC.ToString();
        }
    }
}
