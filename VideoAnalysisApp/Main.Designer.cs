namespace VideoAnalysisApp
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.backgroundWorker_Video = new System.ComponentModel.BackgroundWorker();
            this.pictureBox = new OpenCvSharp.UserInterface.PictureBoxIpl();
            this.StartButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.SearchButton = new System.Windows.Forms.Button();
            this.VideoPathTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.VideoFramePictureBox = new System.Windows.Forms.PictureBox();
            this.lastDetectionLabel = new System.Windows.Forms.Label();
            this.backgroundWorker_Camera = new System.ComponentModel.BackgroundWorker();
            this.videoRadioButton = new System.Windows.Forms.RadioButton();
            this.cameraRadioButton = new System.Windows.Forms.RadioButton();
            this.detectionIntervalNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.panel = new System.Windows.Forms.Panel();
            this.AnalyzeTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.AdvertisingPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VideoFramePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detectionIntervalNumericUpDown)).BeginInit();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AdvertisingPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // backgroundWorker_Video
            // 
            this.backgroundWorker_Video.WorkerReportsProgress = true;
            this.backgroundWorker_Video.WorkerSupportsCancellation = true;
            this.backgroundWorker_Video.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_Video_DoWork);
            this.backgroundWorker_Video.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker_Video.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(12, 72);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(619, 454);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(1064, 23);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 1;
            this.StartButton.Text = "Iniciar";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(1145, 23);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(75, 23);
            this.StopButton.TabIndex = 2;
            this.StopButton.Text = "Parar";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(566, 7);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(75, 23);
            this.SearchButton.TabIndex = 3;
            this.SearchButton.Text = "Procurar";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // VideoPathTextBox
            // 
            this.VideoPathTextBox.Location = new System.Drawing.Point(228, 9);
            this.VideoPathTextBox.Name = "VideoPathTextBox";
            this.VideoPathTextBox.Size = new System.Drawing.Size(332, 20);
            this.VideoPathTextBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(187, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Vídeo:";
            // 
            // VideoFramePictureBox
            // 
            this.VideoFramePictureBox.Location = new System.Drawing.Point(637, 72);
            this.VideoFramePictureBox.Name = "VideoFramePictureBox";
            this.VideoFramePictureBox.Size = new System.Drawing.Size(619, 454);
            this.VideoFramePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.VideoFramePictureBox.TabIndex = 6;
            this.VideoFramePictureBox.TabStop = false;
            this.VideoFramePictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.VideoFramePictureBox_Paint);
            // 
            // lastDetectionLabel
            // 
            this.lastDetectionLabel.AutoSize = true;
            this.lastDetectionLabel.Location = new System.Drawing.Point(1148, 53);
            this.lastDetectionLabel.Name = "lastDetectionLabel";
            this.lastDetectionLabel.Size = new System.Drawing.Size(0, 13);
            this.lastDetectionLabel.TabIndex = 7;
            // 
            // backgroundWorker_Camera
            // 
            this.backgroundWorker_Camera.WorkerReportsProgress = true;
            this.backgroundWorker_Camera.WorkerSupportsCancellation = true;
            this.backgroundWorker_Camera.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_Camera_DoWork);
            this.backgroundWorker_Camera.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker_Camera.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // videoRadioButton
            // 
            this.videoRadioButton.AutoSize = true;
            this.videoRadioButton.Checked = true;
            this.videoRadioButton.Location = new System.Drawing.Point(660, 10);
            this.videoRadioButton.Name = "videoRadioButton";
            this.videoRadioButton.Size = new System.Drawing.Size(54, 17);
            this.videoRadioButton.TabIndex = 8;
            this.videoRadioButton.TabStop = true;
            this.videoRadioButton.Text = "Vídeo";
            this.videoRadioButton.UseVisualStyleBackColor = true;
            // 
            // cameraRadioButton
            // 
            this.cameraRadioButton.AutoSize = true;
            this.cameraRadioButton.Location = new System.Drawing.Point(720, 10);
            this.cameraRadioButton.Name = "cameraRadioButton";
            this.cameraRadioButton.Size = new System.Drawing.Size(61, 17);
            this.cameraRadioButton.TabIndex = 9;
            this.cameraRadioButton.Text = "Câmera";
            this.cameraRadioButton.UseVisualStyleBackColor = true;
            // 
            // detectionIntervalNumericUpDown
            // 
            this.detectionIntervalNumericUpDown.Location = new System.Drawing.Point(973, 10);
            this.detectionIntervalNumericUpDown.Name = "detectionIntervalNumericUpDown";
            this.detectionIntervalNumericUpDown.Size = new System.Drawing.Size(62, 20);
            this.detectionIntervalNumericUpDown.TabIndex = 10;
            this.detectionIntervalNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(793, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Intervalo no reconhecimento (seg.):";
            // 
            // timer
            // 
            this.timer.Interval = 5000;
            this.timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // panel
            // 
            this.panel.Controls.Add(this.AnalyzeTypeComboBox);
            this.panel.Controls.Add(this.label3);
            this.panel.Controls.Add(this.detectionIntervalNumericUpDown);
            this.panel.Controls.Add(this.label2);
            this.panel.Controls.Add(this.SearchButton);
            this.panel.Controls.Add(this.VideoPathTextBox);
            this.panel.Controls.Add(this.cameraRadioButton);
            this.panel.Controls.Add(this.label1);
            this.panel.Controls.Add(this.videoRadioButton);
            this.panel.Location = new System.Drawing.Point(12, 15);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(1046, 38);
            this.panel.TabIndex = 12;
            // 
            // AnalyzeTypeComboBox
            // 
            this.AnalyzeTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AnalyzeTypeComboBox.FormattingEnabled = true;
            this.AnalyzeTypeComboBox.Items.AddRange(new object[] {
            "Análise de Vídeos",
            "Análise de Propagandas"});
            this.AnalyzeTypeComboBox.Location = new System.Drawing.Point(38, 8);
            this.AnalyzeTypeComboBox.Name = "AnalyzeTypeComboBox";
            this.AnalyzeTypeComboBox.Size = new System.Drawing.Size(143, 21);
            this.AnalyzeTypeComboBox.TabIndex = 13;
            this.AnalyzeTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.AnalyzeTypeComboBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Tipo:";
            // 
            // AdvertisingPictureBox
            // 
            this.AdvertisingPictureBox.Location = new System.Drawing.Point(12, 72);
            this.AdvertisingPictureBox.Name = "AdvertisingPictureBox";
            this.AdvertisingPictureBox.Size = new System.Drawing.Size(619, 454);
            this.AdvertisingPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.AdvertisingPictureBox.TabIndex = 13;
            this.AdvertisingPictureBox.TabStop = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 549);
            this.Controls.Add(this.AdvertisingPictureBox);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.lastDetectionLabel);
            this.Controls.Add(this.VideoFramePictureBox);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.pictureBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Video Analysis App";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VideoFramePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detectionIntervalNumericUpDown)).EndInit();
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AdvertisingPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.ComponentModel.BackgroundWorker backgroundWorker_Video;
        private OpenCvSharp.UserInterface.PictureBoxIpl pictureBox;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.TextBox VideoPathTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox VideoFramePictureBox;
        private System.Windows.Forms.Label lastDetectionLabel;
        private System.ComponentModel.BackgroundWorker backgroundWorker_Camera;
        private System.Windows.Forms.RadioButton videoRadioButton;
        private System.Windows.Forms.RadioButton cameraRadioButton;
        private System.Windows.Forms.NumericUpDown detectionIntervalNumericUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.ComboBox AnalyzeTypeComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox AdvertisingPictureBox;
    }
}

