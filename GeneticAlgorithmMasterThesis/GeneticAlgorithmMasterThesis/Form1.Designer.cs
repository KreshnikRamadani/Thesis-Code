namespace GeneticAlgorithmMasterThesis
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtPathFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnReadFile = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNumLocs = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSimulation = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNumOfColl = new System.Windows.Forms.TextBox();
            this.txtNumOfSat = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRunGeneticAlgorithm = new System.Windows.Forms.Button();
            this.btnGenerateSubmissionFile = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numTerminationThreshold = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.numMaxGenerations = new System.Windows.Forms.NumericUpDown();
            this.numTournamentSize = new System.Windows.Forms.NumericUpDown();
            this.numPopulationSize = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnChooseFolder = new System.Windows.Forms.Button();
            this.chbExternalInitialize = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTerminationThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxGenerations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTournamentSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPopulationSize)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPathFile
            // 
            this.txtPathFile.Location = new System.Drawing.Point(57, 27);
            this.txtPathFile.Margin = new System.Windows.Forms.Padding(4);
            this.txtPathFile.Name = "txtPathFile";
            this.txtPathFile.ReadOnly = true;
            this.txtPathFile.Size = new System.Drawing.Size(339, 22);
            this.txtPathFile.TabIndex = 44;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 17);
            this.label1.TabIndex = 45;
            this.label1.Text = "Path:";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(294, 64);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(102, 37);
            this.btnBrowse.TabIndex = 46;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnReadFile
            // 
            this.btnReadFile.Location = new System.Drawing.Point(294, 112);
            this.btnReadFile.Name = "btnReadFile";
            this.btnReadFile.Size = new System.Drawing.Size(102, 37);
            this.btnReadFile.TabIndex = 47;
            this.btnReadFile.Text = "Read file";
            this.btnReadFile.UseVisualStyleBackColor = true;
            this.btnReadFile.Click += new System.EventHandler(this.btnReadFile_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(294, 159);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(102, 37);
            this.btnClear.TabIndex = 59;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNumLocs);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtSimulation);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtNumOfColl);
            this.groupBox1.Controls.Add(this.txtNumOfSat);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtPathFile);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnBrowse);
            this.groupBox1.Controls.Add(this.btnReadFile);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Location = new System.Drawing.Point(14, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(410, 216);
            this.groupBox1.TabIndex = 74;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input data";
            // 
            // txtNumLocs
            // 
            this.txtNumLocs.Location = new System.Drawing.Point(176, 161);
            this.txtNumLocs.Name = "txtNumLocs";
            this.txtNumLocs.Size = new System.Drawing.Size(100, 22);
            this.txtNumLocs.TabIndex = 67;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 161);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(143, 17);
            this.label9.TabIndex = 66;
            this.label9.Text = "Number of Locations:";
            // 
            // txtSimulation
            // 
            this.txtSimulation.Location = new System.Drawing.Point(176, 64);
            this.txtSimulation.Name = "txtSimulation";
            this.txtSimulation.Size = new System.Drawing.Size(100, 22);
            this.txtSimulation.TabIndex = 65;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 64);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 17);
            this.label8.TabIndex = 64;
            this.label8.Text = "Simulation:";
            // 
            // txtNumOfColl
            // 
            this.txtNumOfColl.Location = new System.Drawing.Point(176, 129);
            this.txtNumOfColl.Name = "txtNumOfColl";
            this.txtNumOfColl.Size = new System.Drawing.Size(100, 22);
            this.txtNumOfColl.TabIndex = 63;
            // 
            // txtNumOfSat
            // 
            this.txtNumOfSat.Location = new System.Drawing.Point(176, 95);
            this.txtNumOfSat.Name = "txtNumOfSat";
            this.txtNumOfSat.Size = new System.Drawing.Size(100, 22);
            this.txtNumOfSat.TabIndex = 62;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 17);
            this.label3.TabIndex = 61;
            this.label3.Text = "Number of Collections:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 17);
            this.label2.TabIndex = 60;
            this.label2.Text = "Number of Satellites:";
            // 
            // btnRunGeneticAlgorithm
            // 
            this.btnRunGeneticAlgorithm.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRunGeneticAlgorithm.Location = new System.Drawing.Point(308, 252);
            this.btnRunGeneticAlgorithm.Name = "btnRunGeneticAlgorithm";
            this.btnRunGeneticAlgorithm.Size = new System.Drawing.Size(116, 57);
            this.btnRunGeneticAlgorithm.TabIndex = 75;
            this.btnRunGeneticAlgorithm.Text = "Run GenicAl";
            this.btnRunGeneticAlgorithm.UseVisualStyleBackColor = true;
            this.btnRunGeneticAlgorithm.Click += new System.EventHandler(this.btnRunGeneticAlgorithm_Click);
            // 
            // btnGenerateSubmissionFile
            // 
            this.btnGenerateSubmissionFile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerateSubmissionFile.Location = new System.Drawing.Point(308, 317);
            this.btnGenerateSubmissionFile.Name = "btnGenerateSubmissionFile";
            this.btnGenerateSubmissionFile.Size = new System.Drawing.Size(116, 57);
            this.btnGenerateSubmissionFile.TabIndex = 77;
            this.btnGenerateSubmissionFile.Text = "Submission File";
            this.btnGenerateSubmissionFile.UseVisualStyleBackColor = true;
            this.btnGenerateSubmissionFile.Click += new System.EventHandler(this.btnGenerateSubmissionFile_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numTerminationThreshold);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.numMaxGenerations);
            this.groupBox2.Controls.Add(this.numTournamentSize);
            this.groupBox2.Controls.Add(this.numPopulationSize);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(14, 238);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(276, 154);
            this.groupBox2.TabIndex = 78;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Parameters";
            // 
            // numTerminationThreshold
            // 
            this.numTerminationThreshold.Location = new System.Drawing.Point(177, 114);
            this.numTerminationThreshold.Name = "numTerminationThreshold";
            this.numTerminationThreshold.Size = new System.Drawing.Size(84, 22);
            this.numTerminationThreshold.TabIndex = 82;
            this.numTerminationThreshold.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 17);
            this.label7.TabIndex = 83;
            this.label7.Text = "Max Generations:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 116);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(159, 17);
            this.label10.TabIndex = 81;
            this.label10.Text = "Termination Threshold: ";
            // 
            // numMaxGenerations
            // 
            this.numMaxGenerations.Location = new System.Drawing.Point(177, 21);
            this.numMaxGenerations.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numMaxGenerations.Name = "numMaxGenerations";
            this.numMaxGenerations.Size = new System.Drawing.Size(84, 22);
            this.numMaxGenerations.TabIndex = 82;
            this.numMaxGenerations.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // numTournamentSize
            // 
            this.numTournamentSize.Location = new System.Drawing.Point(177, 82);
            this.numTournamentSize.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numTournamentSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTournamentSize.Name = "numTournamentSize";
            this.numTournamentSize.Size = new System.Drawing.Size(84, 22);
            this.numTournamentSize.TabIndex = 80;
            this.numTournamentSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numPopulationSize
            // 
            this.numPopulationSize.Location = new System.Drawing.Point(177, 51);
            this.numPopulationSize.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numPopulationSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPopulationSize.Name = "numPopulationSize";
            this.numPopulationSize.Size = new System.Drawing.Size(84, 22);
            this.numPopulationSize.TabIndex = 79;
            this.numPopulationSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 17);
            this.label5.TabIndex = 1;
            this.label5.Text = "Tournament Size:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Population Size:";
            // 
            // btnChooseFolder
            // 
            this.btnChooseFolder.Location = new System.Drawing.Point(546, 64);
            this.btnChooseFolder.Name = "btnChooseFolder";
            this.btnChooseFolder.Size = new System.Drawing.Size(102, 55);
            this.btnChooseFolder.TabIndex = 79;
            this.btnChooseFolder.Text = "Choose Folder";
            this.btnChooseFolder.UseVisualStyleBackColor = true;
            this.btnChooseFolder.Click += new System.EventHandler(this.btnChooseFolder_Click);
            // 
            // chbExternalInitialize
            // 
            this.chbExternalInitialize.AutoSize = true;
            this.chbExternalInitialize.Location = new System.Drawing.Point(430, 28);
            this.chbExternalInitialize.Name = "chbExternalInitialize";
            this.chbExternalInitialize.Size = new System.Drawing.Size(222, 21);
            this.chbExternalInitialize.TabIndex = 80;
            this.chbExternalInitialize.Text = "External Initialize of Population";
            this.chbExternalInitialize.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 403);
            this.Controls.Add(this.chbExternalInitialize);
            this.Controls.Add(this.btnChooseFolder);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnGenerateSubmissionFile);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnRunGeneticAlgorithm);
            this.Name = "Form1";
            this.Text = "Genetic Algorithm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTerminationThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxGenerations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTournamentSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPopulationSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPathFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnReadFile;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtNumOfColl;
        private System.Windows.Forms.TextBox txtNumOfSat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRunGeneticAlgorithm;
        private System.Windows.Forms.Button btnGenerateSubmissionFile;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numTournamentSize;
        private System.Windows.Forms.NumericUpDown numPopulationSize;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numMaxGenerations;
        private System.Windows.Forms.TextBox txtSimulation;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtNumLocs;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnChooseFolder;
        private System.Windows.Forms.CheckBox chbExternalInitialize;
        private System.Windows.Forms.NumericUpDown numTerminationThreshold;
        private System.Windows.Forms.Label label10;
    }
}

