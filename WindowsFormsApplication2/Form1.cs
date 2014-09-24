/**
 * Jack Fabris and Ben Handanyan
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
        public Memory myMem;

        public Form1()
        {
            myCPU = new CPU();
            myMem = new Memory();

            InitializeComponent();
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
                        ipe.mem = myMem;
                        myCPU.mem = myMem;
                        ipe.ParseFile();

                        this.setCPUValuesToView();
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
            this.myCPU.executeInstruction();
            this.setCPUValuesToView();
        }

        public void setCPUValuesToView()
        {
            this.aLabel.Text = this.myCPU.A.ToString();
            this.bLabel.Text = this.myCPU.B.ToString();
            this.accLabel.Text = this.myCPU.ACC.ToString();
            this.zeroLabel.Text = this.myCPU.ZERO.ToString();
            this.oneLabel.Text = this.myCPU.ONE.ToString();
            this.pcLabel.Text = this.myCPU.PC.ToString();
            this.marLabel.Text = this.myCPU.MAR.ToString();
            this.mdrLabel.Text = this.myCPU.MDR.ToString();
            this.tempLabel.Text = this.myCPU.TEMP.ToString();
            this.irLabel.Text = this.myCPU.IR.ToString();
            this.ccLabel.Text = this.myCPU.CC.ToString();
            this.instructionIndexLabel.Text = this.myCPU.PC.ToString();
            this.nextInstructionLabel.Text = this.myCPU.nextInstToString();
        }

        private void runToEndButton_Click(object sender, EventArgs e)
        {
            this.myCPU.executeAllInstructions();
            this.setCPUValuesToView();
        }

        private void reloadButton_Click(object sender, EventArgs e)
        {

        }

        private void resetCPUButton_Click(object sender, EventArgs e)
        {
            
        }
    }
}
