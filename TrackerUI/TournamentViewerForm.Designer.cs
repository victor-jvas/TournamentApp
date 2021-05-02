namespace TrackerUI
{
    partial class TournamentViewerForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
            this.headerLabel = new System.Windows.Forms.Label();
            this.tournamentNameLabel = new System.Windows.Forms.Label();
            this.roundLabel = new System.Windows.Forms.Label();
            this.roundDropDown = new System.Windows.Forms.ComboBox();
            this.unplayedOnlyCheckBox = new System.Windows.Forms.CheckBox();
            this.matchupListBox = new System.Windows.Forms.ListBox();
            this.teamOneNameLabel = new System.Windows.Forms.Label();
            this.scoreTeamOneLabel = new System.Windows.Forms.Label();
            this.teamOneScoreValue = new System.Windows.Forms.TextBox();
            this.teamTwoNameLabel = new System.Windows.Forms.Label();
            this.scoreTeamTwoLabel = new System.Windows.Forms.Label();
            this.teamTwoScoreValue = new System.Windows.Forms.TextBox();
            this.vsLabel = new System.Windows.Forms.Label();
            this.scoreButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // headerLabel
            // 
            this.headerLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F, System.Drawing.FontStyle.Bold);
            this.headerLabel.ForeColor = System.Drawing.Color.Coral;
            this.headerLabel.Location = new System.Drawing.Point(12, 29);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(175, 30);
            this.headerLabel.TabIndex = 0;
            this.headerLabel.Text = "Tournament:";
            // 
            // tournamentNameLabel
            // 
            this.tournamentNameLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F);
            this.tournamentNameLabel.ForeColor = System.Drawing.Color.Coral;
            this.tournamentNameLabel.Location = new System.Drawing.Point(182, 29);
            this.tournamentNameLabel.Name = "tournamentNameLabel";
            this.tournamentNameLabel.Size = new System.Drawing.Size(163, 30);
            this.tournamentNameLabel.TabIndex = 0;
            this.tournamentNameLabel.Text = "<none>";
            // 
            // roundLabel
            // 
            this.roundLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F);
            this.roundLabel.ForeColor = System.Drawing.Color.Coral;
            this.roundLabel.Location = new System.Drawing.Point(12, 79);
            this.roundLabel.Name = "roundLabel";
            this.roundLabel.Size = new System.Drawing.Size(96, 31);
            this.roundLabel.TabIndex = 0;
            this.roundLabel.Text = "Round";
            // 
            // roundDropDown
            // 
            this.roundDropDown.FormattingEnabled = true;
            this.roundDropDown.Location = new System.Drawing.Point(114, 86);
            this.roundDropDown.Name = "roundDropDown";
            this.roundDropDown.Size = new System.Drawing.Size(121, 24);
            this.roundDropDown.TabIndex = 1;
            this.roundDropDown.SelectedIndexChanged += new System.EventHandler(this.roundDropDown_SelectedIndexChanged);
            // 
            // unplayedOnlyCheckBox
            // 
            this.unplayedOnlyCheckBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.unplayedOnlyCheckBox.ForeColor = System.Drawing.Color.Coral;
            this.unplayedOnlyCheckBox.Location = new System.Drawing.Point(114, 116);
            this.unplayedOnlyCheckBox.Name = "unplayedOnlyCheckBox";
            this.unplayedOnlyCheckBox.Size = new System.Drawing.Size(130, 24);
            this.unplayedOnlyCheckBox.TabIndex = 2;
            this.unplayedOnlyCheckBox.Text = "Unplayed Only";
            this.unplayedOnlyCheckBox.UseVisualStyleBackColor = true;
            this.unplayedOnlyCheckBox.CheckedChanged += new System.EventHandler(this.unplayedOnlyCheckBox_CheckedChanged);
            // 
            // matchupListBox
            // 
            this.matchupListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.matchupListBox.FormattingEnabled = true;
            this.matchupListBox.ItemHeight = 16;
            this.matchupListBox.Location = new System.Drawing.Point(19, 146);
            this.matchupListBox.Name = "matchupListBox";
            this.matchupListBox.Size = new System.Drawing.Size(216, 178);
            this.matchupListBox.TabIndex = 3;
            this.matchupListBox.SelectedIndexChanged += new System.EventHandler(this.matchupListBox_SelectedIndexChanged);
            // 
            // teamOneNameLabel
            // 
            this.teamOneNameLabel.AutoSize = true;
            this.teamOneNameLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F);
            this.teamOneNameLabel.ForeColor = System.Drawing.Color.Coral;
            this.teamOneNameLabel.Location = new System.Drawing.Point(287, 146);
            this.teamOneNameLabel.Name = "teamOneNameLabel";
            this.teamOneNameLabel.Size = new System.Drawing.Size(159, 31);
            this.teamOneNameLabel.TabIndex = 0;
            this.teamOneNameLabel.Text = "<team one>";
            this.teamOneNameLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // scoreTeamOneLabel
            // 
            this.scoreTeamOneLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F);
            this.scoreTeamOneLabel.ForeColor = System.Drawing.Color.Coral;
            this.scoreTeamOneLabel.Location = new System.Drawing.Point(287, 177);
            this.scoreTeamOneLabel.Name = "scoreTeamOneLabel";
            this.scoreTeamOneLabel.Size = new System.Drawing.Size(79, 31);
            this.scoreTeamOneLabel.TabIndex = 0;
            this.scoreTeamOneLabel.Text = "Score";
            this.scoreTeamOneLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // teamOneScoreValue
            // 
            this.teamOneScoreValue.Location = new System.Drawing.Point(364, 184);
            this.teamOneScoreValue.Name = "teamOneScoreValue";
            this.teamOneScoreValue.Size = new System.Drawing.Size(83, 21);
            this.teamOneScoreValue.TabIndex = 4;
            // 
            // teamTwoNameLabel
            // 
            this.teamTwoNameLabel.AutoSize = true;
            this.teamTwoNameLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F);
            this.teamTwoNameLabel.ForeColor = System.Drawing.Color.Coral;
            this.teamTwoNameLabel.Location = new System.Drawing.Point(287, 249);
            this.teamTwoNameLabel.Name = "teamTwoNameLabel";
            this.teamTwoNameLabel.Size = new System.Drawing.Size(158, 31);
            this.teamTwoNameLabel.TabIndex = 0;
            this.teamTwoNameLabel.Text = "<team two>";
            this.teamTwoNameLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // scoreTeamTwoLabel
            // 
            this.scoreTeamTwoLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F);
            this.scoreTeamTwoLabel.ForeColor = System.Drawing.Color.Coral;
            this.scoreTeamTwoLabel.Location = new System.Drawing.Point(287, 280);
            this.scoreTeamTwoLabel.Name = "scoreTeamTwoLabel";
            this.scoreTeamTwoLabel.Size = new System.Drawing.Size(79, 31);
            this.scoreTeamTwoLabel.TabIndex = 0;
            this.scoreTeamTwoLabel.Text = "Score";
            this.scoreTeamTwoLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // teamTwoScoreValue
            // 
            this.teamTwoScoreValue.Location = new System.Drawing.Point(364, 287);
            this.teamTwoScoreValue.Name = "teamTwoScoreValue";
            this.teamTwoScoreValue.Size = new System.Drawing.Size(83, 21);
            this.teamTwoScoreValue.TabIndex = 4;
            // 
            // vsLabel
            // 
            this.vsLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F);
            this.vsLabel.ForeColor = System.Drawing.Color.Coral;
            this.vsLabel.Location = new System.Drawing.Point(338, 218);
            this.vsLabel.Name = "vsLabel";
            this.vsLabel.Size = new System.Drawing.Size(69, 31);
            this.vsLabel.TabIndex = 0;
            this.vsLabel.Text = "-VS-";
            this.vsLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // scoreButton
            // 
            this.scoreButton.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.scoreButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int) (((byte) (64)))), ((int) (((byte) (64)))), ((int) (((byte) (64)))));
            this.scoreButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.scoreButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.scoreButton.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.scoreButton.ForeColor = System.Drawing.Color.Coral;
            this.scoreButton.Location = new System.Drawing.Point(481, 222);
            this.scoreButton.Name = "scoreButton";
            this.scoreButton.Size = new System.Drawing.Size(83, 35);
            this.scoreButton.TabIndex = 5;
            this.scoreButton.Text = "Score";
            this.scoreButton.UseVisualStyleBackColor = true;
            this.scoreButton.Click += new System.EventHandler(this.scoreButton_Click);
            // 
            // TournamentViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(596, 353);
            this.Controls.Add(this.scoreButton);
            this.Controls.Add(this.teamTwoScoreValue);
            this.Controls.Add(this.teamOneScoreValue);
            this.Controls.Add(this.matchupListBox);
            this.Controls.Add(this.unplayedOnlyCheckBox);
            this.Controls.Add(this.roundDropDown);
            this.Controls.Add(this.scoreTeamTwoLabel);
            this.Controls.Add(this.vsLabel);
            this.Controls.Add(this.teamTwoNameLabel);
            this.Controls.Add(this.scoreTeamOneLabel);
            this.Controls.Add(this.teamOneNameLabel);
            this.Controls.Add(this.roundLabel);
            this.Controls.Add(this.tournamentNameLabel);
            this.Controls.Add(this.headerLabel);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "TournamentViewerForm";
            this.Text = "Tournament Viewer";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button scoreButton;


        private System.Windows.Forms.Label vsLabel;
        private System.Windows.Forms.Label teamTwoNameLabel;
        private System.Windows.Forms.Label scoreTeamTwoLabel;
        private System.Windows.Forms.TextBox teamTwoScoreValue;

        private System.Windows.Forms.TextBox teamOneScoreValue;

        private System.Windows.Forms.Label scoreTeamOneLabel;

        private System.Windows.Forms.Label teamOneNameLabel;

        private System.Windows.Forms.ListBox matchupListBox;

        private System.Windows.Forms.Label tournamentNameLabel;
        private System.Windows.Forms.Label roundLabel;
        private System.Windows.Forms.ComboBox roundDropDown;
        private System.Windows.Forms.CheckBox unplayedOnlyCheckBox;

        private System.Windows.Forms.Label headerLabel;


        #endregion
    }
}