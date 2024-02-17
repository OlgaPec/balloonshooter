namespace BalloonShooter
{
    partial class ScoresForm
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
            this.listViewScores = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // listViewScores
            // 
            this.listViewScores.HideSelection = false;
            this.listViewScores.Location = new System.Drawing.Point(12, 12);
            this.listViewScores.Name = "listViewScores";
            this.listViewScores.Size = new System.Drawing.Size(300, 628);
            this.listViewScores.TabIndex = 0;
            this.listViewScores.UseCompatibleStateImageBehavior = false;
            // 
            // ScoresForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(324, 652);
            this.Controls.Add(this.listViewScores);
            this.Name = "ScoresForm";
            this.Text = "Score";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewScores;
    }
}