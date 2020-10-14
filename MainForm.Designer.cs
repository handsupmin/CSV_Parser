namespace MarketBrowser
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonOpenCSV = new System.Windows.Forms.Button();
            this.textBoxCSVView = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonOpenCSV
            // 
            this.buttonOpenCSV.Location = new System.Drawing.Point(42, 36);
            this.buttonOpenCSV.Name = "buttonOpenCSV";
            this.buttonOpenCSV.Size = new System.Drawing.Size(82, 23);
            this.buttonOpenCSV.TabIndex = 0;
            this.buttonOpenCSV.Text = "CSV 열기";
            this.buttonOpenCSV.UseVisualStyleBackColor = true;
            this.buttonOpenCSV.Click += new System.EventHandler(this.Button1_Click);
            // 
            // textBoxCSVView
            // 
            this.textBoxCSVView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCSVView.Location = new System.Drawing.Point(42, 81);
            this.textBoxCSVView.Multiline = true;
            this.textBoxCSVView.Name = "textBoxCSVView";
            this.textBoxCSVView.Size = new System.Drawing.Size(573, 317);
            this.textBoxCSVView.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(131, 35);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 446);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxCSVView);
            this.Controls.Add(this.buttonOpenCSV);
            this.Name = "MainForm";
            this.Text = "전통시장 검색기";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOpenCSV;
        private System.Windows.Forms.TextBox textBoxCSVView;
        private System.Windows.Forms.Button button1;
    }
}

