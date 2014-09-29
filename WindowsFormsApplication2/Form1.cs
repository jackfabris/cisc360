﻿/**
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
            myCPU.resetCPU();
            myMem.clear();
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
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Error loading assembly file", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            setInitialCPUValuesToView();
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
            myCPU.resetCPU();
            myMem.clear();
            this.setCPUValuesToView();
        }
        #endregion

        private void nextInstructionButton_Click(object sender, EventArgs e)
        {
            this.myCPU.executeInstruction();
            this.setCPUValuesToView();
        }

        public void setInitialCPUValuesToView()
        {
            this.aLabel.Text = "0x" + this.myCPU.A.ToString("x8");
            this.bLabel.Text = "0x" + this.myCPU.B.ToString("x8");
            this.accLabel.Text = "0x" + this.myCPU.ACC.ToString("x8");
            this.zeroLabel.Text = "0x" + this.myCPU.ZERO.ToString("x8");
            this.oneLabel.Text = "0x" + this.myCPU.ONE.ToString("x8");
            this.pcLabel.Text = "0x" + this.myCPU.PC.ToString("x8");
            this.marLabel.Text = "0x" + this.myCPU.MAR.ToString("x8");
            this.mdrLabel.Text = "0x" + this.myCPU.MDR.ToString("x8");
            this.tempLabel.Text = "0x" + this.myCPU.TEMP.ToString("x8");
            this.irLabel.Text = "0x" + this.myCPU.IR.ToString("x8");
            this.ccLabel.Text = "0x" + this.myCPU.CC.ToString("x8");
            this.instructionIndexLabel.Text = this.myCPU.PC.ToString();
            this.nextInstructionLabel.Text = this.myCPU.firstInstToString();
        }

        public void setCPUValuesToView()
        {
            this.aLabel.Text = "0x" + this.myCPU.A.ToString("x8");
            this.bLabel.Text = "0x" + this.myCPU.B.ToString("x8");
            this.accLabel.Text = "0x" + this.myCPU.ACC.ToString("x8");
            this.zeroLabel.Text = "0x" + this.myCPU.ZERO.ToString("x8");
            this.oneLabel.Text = "0x" + this.myCPU.ONE.ToString("x8");
            this.pcLabel.Text = "0x" + this.myCPU.PC.ToString("x8");
            this.marLabel.Text = "0x" + this.myCPU.MAR.ToString("x8");
            this.mdrLabel.Text = "0x" + this.myCPU.MDR.ToString("x8");
            this.tempLabel.Text = "0x" + this.myCPU.TEMP.ToString("x8");
            this.irLabel.Text = "0x" + this.myCPU.IR.ToString("x8");
            this.ccLabel.Text = "0x" + this.myCPU.CC.ToString("x8");
            this.instructionIndexLabel.Text = this.myCPU.PC.ToString();
            this.nextInstructionLabel.Text = this.myCPU.nextInstToString();
        }
    }
}
