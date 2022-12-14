namespace Compiler
{
    partial class MainForm
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CodeField = new System.Windows.Forms.TextBox();
            this.CompileButton = new System.Windows.Forms.Button();
            this.ConsoleView = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CodeField
            // 
            this.CodeField.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CodeField.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CodeField.Location = new System.Drawing.Point(12, 12);
            this.CodeField.Multiline = true;
            this.CodeField.Name = "CodeField";
            this.CodeField.Size = new System.Drawing.Size(722, 465);
            this.CodeField.TabIndex = 0;
            // 
            // CompileButton
            // 
            this.CompileButton.Font = new System.Drawing.Font("Yu Gothic UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CompileButton.Location = new System.Drawing.Point(12, 483);
            this.CompileButton.Name = "CompileButton";
            this.CompileButton.Size = new System.Drawing.Size(351, 41);
            this.CompileButton.TabIndex = 2;
            this.CompileButton.Text = "Компилировать";
            this.CompileButton.UseVisualStyleBackColor = true;
            this.CompileButton.Click += new System.EventHandler(this.CompileButton_Click);
            // 
            // ConsoleView
            // 
            this.ConsoleView.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ConsoleView.FormattingEnabled = true;
            this.ConsoleView.HorizontalScrollbar = true;
            this.ConsoleView.ItemHeight = 21;
            this.ConsoleView.Location = new System.Drawing.Point(12, 529);
            this.ConsoleView.Name = "ConsoleView";
            this.ConsoleView.Size = new System.Drawing.Size(722, 172);
            this.ConsoleView.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Yu Gothic UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button1.Location = new System.Drawing.Point(378, 483);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(356, 41);
            this.button1.TabIndex = 4;
            this.button1.Text = "Запустить";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 718);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ConsoleView);
            this.Controls.Add(this.CompileButton);
            this.Controls.Add(this.CodeField);
            this.Name = "MainForm";
            this.Text = "Course Project 20VP1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox CodeField;
        private Button CompileButton;
        private ListBox ConsoleView;
        private Button button1;
    }
}