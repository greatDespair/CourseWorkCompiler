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
            this.ConsoleView = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сборкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CompileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.запускToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RunCurrentMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RunOtherMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CodeField
            // 
            this.CodeField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CodeField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(83)))), ((int)(((byte)(107)))));
            this.CodeField.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CodeField.ForeColor = System.Drawing.SystemColors.Control;
            this.CodeField.Location = new System.Drawing.Point(12, 41);
            this.CodeField.Multiline = true;
            this.CodeField.Name = "CodeField";
            this.CodeField.Size = new System.Drawing.Size(1214, 571);
            this.CodeField.TabIndex = 0;
            // 
            // ConsoleView
            // 
            this.ConsoleView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ConsoleView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(83)))), ((int)(((byte)(107)))));
            this.ConsoleView.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ConsoleView.ForeColor = System.Drawing.SystemColors.Control;
            this.ConsoleView.FormattingEnabled = true;
            this.ConsoleView.HorizontalScrollbar = true;
            this.ConsoleView.ItemHeight = 21;
            this.ConsoleView.Location = new System.Drawing.Point(12, 648);
            this.ConsoleView.Name = "ConsoleView";
            this.ConsoleView.Size = new System.Drawing.Size(1214, 130);
            this.ConsoleView.TabIndex = 3;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(107)))), ((int)(((byte)(145)))));
            this.menuStrip1.Font = new System.Drawing.Font("Yu Gothic UI Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.сборкаToolStripMenuItem,
            this.запускToolStripMenuItem,
            this.справкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1245, 38);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenFileMenuItem,
            this.SaveAsMenuItem,
            this.ExitMenuItem});
            this.файлToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(73, 34);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // OpenFileMenuItem
            // 
            this.OpenFileMenuItem.Name = "OpenFileMenuItem";
            this.OpenFileMenuItem.Size = new System.Drawing.Size(217, 34);
            this.OpenFileMenuItem.Text = "Открыть";
            this.OpenFileMenuItem.Click += new System.EventHandler(this.OpenFileMenuItem_Click);
            // 
            // SaveAsMenuItem
            // 
            this.SaveAsMenuItem.Name = "SaveAsMenuItem";
            this.SaveAsMenuItem.Size = new System.Drawing.Size(217, 34);
            this.SaveAsMenuItem.Text = "Сохранить как";
            this.SaveAsMenuItem.Click += new System.EventHandler(this.SaveAsMenuItem_Click);
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.Size = new System.Drawing.Size(217, 34);
            this.ExitMenuItem.Text = "Выход";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // сборкаToolStripMenuItem
            // 
            this.сборкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CompileMenuItem});
            this.сборкаToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.сборкаToolStripMenuItem.Name = "сборкаToolStripMenuItem";
            this.сборкаToolStripMenuItem.Size = new System.Drawing.Size(93, 34);
            this.сборкаToolStripMenuItem.Text = "Сборка";
            // 
            // CompileMenuItem
            // 
            this.CompileMenuItem.Name = "CompileMenuItem";
            this.CompileMenuItem.Size = new System.Drawing.Size(163, 34);
            this.CompileMenuItem.Text = "Собрать";
            this.CompileMenuItem.Click += new System.EventHandler(this.CompileMenuItem_Click);
            // 
            // запускToolStripMenuItem
            // 
            this.запускToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RunCurrentMenuItem,
            this.RunOtherMenuItem});
            this.запускToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.запускToolStripMenuItem.Name = "запускToolStripMenuItem";
            this.запускToolStripMenuItem.Size = new System.Drawing.Size(85, 34);
            this.запускToolStripMenuItem.Text = "Запуск";
            // 
            // RunCurrentMenuItem
            // 
            this.RunCurrentMenuItem.Name = "RunCurrentMenuItem";
            this.RunCurrentMenuItem.Size = new System.Drawing.Size(406, 34);
            this.RunCurrentMenuItem.Text = "Запуск текущей сборки";
            this.RunCurrentMenuItem.Click += new System.EventHandler(this.RunCurrentMenuItem_Click);
            // 
            // RunOtherMenuItem
            // 
            this.RunOtherMenuItem.Name = "RunOtherMenuItem";
            this.RunOtherMenuItem.Size = new System.Drawing.Size(406, 34);
            this.RunOtherMenuItem.Text = "Запуск последней удачной сборки";
            this.RunOtherMenuItem.Click += new System.EventHandler(this.RunOtherMenuItem_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(12, 615);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 30);
            this.label1.TabIndex = 6;
            this.label1.Text = "Окно вывода";
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(100, 34);
            this.справкаToolStripMenuItem.Text = "Справка";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(107)))), ((int)(((byte)(145)))));
            this.ClientSize = new System.Drawing.Size(1245, 804);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ConsoleView);
            this.Controls.Add(this.CodeField);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(700, 843);
            this.Name = "MainForm";
            this.Text = "CourseWork 20VP1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox CodeField;
        private ListBox ConsoleView;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem файлToolStripMenuItem;
        private ToolStripMenuItem OpenFileMenuItem;
        private ToolStripMenuItem SaveAsMenuItem;
        private ToolStripMenuItem ExitMenuItem;
        private ToolStripMenuItem сборкаToolStripMenuItem;
        private ToolStripMenuItem CompileMenuItem;
        private ToolStripMenuItem запускToolStripMenuItem;
        private ToolStripMenuItem RunCurrentMenuItem;
        private ToolStripMenuItem RunOtherMenuItem;
        private Label label1;
        private ToolStripMenuItem справкаToolStripMenuItem;
    }
}