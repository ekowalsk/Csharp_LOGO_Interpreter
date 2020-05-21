namespace Turtle_Graphics_Example
{
    partial class TurtleGraphicsDemoForm
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
            this.buttonEnter = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonShowHideTurtle = new System.Windows.Forms.Button();
            this.buttonShowHideTextBox = new System.Windows.Forms.Button();
            this.textBox = new System.Windows.Forms.TextBox();

            this.SuspendLayout();
            // 
            // buttonDraw
            // 
            this.buttonEnter.Location = new System.Drawing.Point(11, 12);
            this.buttonEnter.Name = "buttonDraw";
            this.buttonEnter.Size = new System.Drawing.Size(78, 35);
            this.buttonEnter.TabIndex = 0;
            this.buttonEnter.Text = "Enter";
            this.buttonEnter.UseVisualStyleBackColor = true;
            this.buttonEnter.Click += new System.EventHandler(this.buttonEnter_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(11, 116);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(78, 35);
            this.buttonReset.TabIndex = 1;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonShowHideTurtle
            // 
            this.buttonShowHideTurtle.Location = new System.Drawing.Point(12, 168);
            this.buttonShowHideTurtle.Name = "buttonShowHideTurtle";
            this.buttonShowHideTurtle.Size = new System.Drawing.Size(78, 34);
            this.buttonShowHideTurtle.TabIndex = 2;
            this.buttonShowHideTurtle.Text = "Hide Turtle";
            this.buttonShowHideTurtle.UseVisualStyleBackColor = true;
            this.buttonShowHideTurtle.Click += new System.EventHandler(this.buttonShowHideTurtle_Click);
            // 
            // buttonShowHideTextBox
            // 
            this.buttonShowHideTextBox.Location = new System.Drawing.Point(11, 220);
            this.buttonShowHideTextBox.Name = "ShowHideTextBox";
            this.buttonShowHideTextBox.Size = new System.Drawing.Size(78, 35);
            this.buttonShowHideTextBox.TabIndex = 0;
            this.buttonShowHideTextBox.Text = "Hide Text";
            this.buttonShowHideTextBox.UseVisualStyleBackColor = true;
            this.buttonShowHideTextBox.Click += new System.EventHandler(this.buttonShowHideTextBox_Click);
            // 
            // textBox
            // 
            this.textBox.AcceptsReturn = true;
            this.textBox.AcceptsTab = true;
            this.textBox.Multiline = true;
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox.Location = new System.Drawing.Point(0, 400);
            this.textBox.Size = new System.Drawing.Size(688, 100);
            this.textBox.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right 
                | System.Windows.Forms.AnchorStyles.Bottom;

            // 
            // TurtleGraphicsDemoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 509);
            this.Controls.Add(this.buttonEnter);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonShowHideTurtle);
            this.Controls.Add(this.buttonShowHideTextBox);
            this.Controls.Add(this.textBox);
            this.Name = "Interpreter LOGO";
            this.Text = "Interpreter LOGO";
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button buttonEnter;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonShowHideTurtle;
        private System.Windows.Forms.Button buttonShowHideTextBox;
        private System.Windows.Forms.TextBox textBox;
    }
}

