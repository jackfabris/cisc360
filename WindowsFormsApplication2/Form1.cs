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
            myMem = new Memory();
            myCPU = new CPU();
            myCPU.OnFetchDone += myCPU_OnFetchDone;
            myCPU.OnDecodeDone += myCPU_OnDecodeDone;
            myCPU.OnExecuteDone += myCPU_OnExecuteDone;
            myCPU.OnStoreDone += myCPU_OnStoreDone;
            InitializeComponent();
        }

        void myCPU_OnFetchDone(object sender, OperationEventArgs args)
        {
            MethodInvoker method = delegate 
            {
                this.irLabel.Text = "0x" + args.CurrentIR.ToString("x8");
                this.fetchLabel.Text = myCPU.instToString(args.CurrentIR);
            };

            if (this.InvokeRequired)
            {
                this.Invoke(method);
            }
            else
            {
                method.Invoke();
            }
        }

        void myCPU_OnDecodeDone(object sender, OperationEventArgs args)
        {
            MethodInvoker method = delegate
            {
                this.decodeLabel.Text = myCPU.instToString(args.CurrentIR);
            };

            if (this.InvokeRequired)
            {
                this.Invoke(method);
            }
            else
            {
                method.Invoke();
            }
        }

        void myCPU_OnExecuteDone(object sender, OperationEventArgs args)
        {
            MethodInvoker method = delegate
            {
                this.executeLabel.Text = myCPU.instToString(args.CurrentIR);
            };

            if (this.InvokeRequired)
            {
                this.Invoke(method);
            }
            else
            {
                method.Invoke();
            }
        }

        void myCPU_OnStoreDone(object sender, OperationEventArgs args)
        {
            MethodInvoker method = delegate
            {
                this.storeLabel.Text = myCPU.instToString(args.CurrentIR);
            };

            if (this.InvokeRequired)
            {
                this.Invoke(method);
            }
            else
            {
                method.Invoke();
            }
        }

        void myCPU_OnProgramDone(object sender, ProgramEventArgs args)
        {
            MethodInvoker method = delegate
            {
                this.fetchLabel.Text = "- - -";
                this.decodeLabel.Text = "- - -";
                this.executeLabel.Text = "- - -";
                this.storeLabel.Text = "- - -";
                this.nextInstructionLabel.Text = "End of program";

            };

            if (this.InvokeRequired)
            {
                this.Invoke(method);
            }
            else
            {
                method.Invoke();
            }
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
                   
                    }
                }
            }
            myCPU.branchPredictionTable = new CPU.branchPredictionStruct[100];
            for (int i = 0; i < myCPU.branchPredictionTable.Length; i++)
            {
                CPU.branchPredictionStruct initial = new CPU.branchPredictionStruct();
                initial.branchPC = -1;
                myCPU.branchPredictionTable[i] = initial;
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
            setInitialCPUValuesToView();
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
            this.hitMissLabel.Text = "- - -";
            this.hitCountLabel.Text = this.myMem.hitCount.ToString();
            this.missCountLabel.Text = this.myMem.missCount.ToString();
            this.fetchLabel.Text = "- - -";
            this.decodeLabel.Text = "- - -";
            this.executeLabel.Text = "- - -";
            this.storeLabel.Text = "- - -";
            this.noopLabel.Text = "0";
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
            this.hitMissLabel.Text = this.myMem.hitMiss;
            this.hitCountLabel.Text = this.myMem.hitCount.ToString();
            this.missCountLabel.Text = this.myMem.missCount.ToString();
            this.noopLabel.Text = this.myCPU.no_op_count.ToString();
        }

        private void applyCacheButton_Click(object sender, EventArgs e)
        {
            int blockSize = Convert.ToInt32(this.blockSizeDropdown.SelectedItem.ToString());
            int cacheSize = Convert.ToInt32(this.cacheSizeDropdown.SelectedItem.ToString());
            int cacheType;
            if (this.cacheTypeDropdown.SelectedItem.ToString() == "Direct Mapped")
            {
                cacheType = 1;
            }
            else
            {
                cacheType = 2;
            }
            myMem.setCache(cacheSize,cacheType,blockSize);
        }
    }
}
