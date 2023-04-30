namespace Compiler
{
    partial class HelpForm
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("О программе");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Основная информация");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Компиляция");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Синтаксис");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Работа с данными");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("О языке \"ZaHaR 2.0\"", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Запуск");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Справка", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode6,
            treeNode7});
            this.panel1 = new System.Windows.Forms.Panel();
            this.DescriptionLabel = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(83)))), ((int)(((byte)(107)))));
            this.panel1.Controls.Add(this.DescriptionLabel);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.TitleLabel);
            this.panel1.Location = new System.Drawing.Point(329, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(573, 700);
            this.panel1.TabIndex = 0;
            // 
            // DescriptionLabel
            // 
            this.DescriptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DescriptionLabel.Font = new System.Drawing.Font("Yu Gothic UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DescriptionLabel.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.DescriptionLabel.Location = new System.Drawing.Point(25, 87);
            this.DescriptionLabel.Name = "DescriptionLabel";
            this.DescriptionLabel.Size = new System.Drawing.Size(530, 598);
            this.DescriptionLabel.TabIndex = 2;
            this.DescriptionLabel.Text = "Заголовок";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.Location = new System.Drawing.Point(25, 69);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(530, 1);
            this.panel3.TabIndex = 1;
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("Yu Gothic UI Light", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.TitleLabel.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.TitleLabel.Location = new System.Drawing.Point(15, 16);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(210, 54);
            this.TitleLabel.TabIndex = 0;
            this.TitleLabel.Text = "Заголовок";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(83)))), ((int)(((byte)(107)))));
            this.panel2.Controls.Add(this.treeView1);
            this.panel2.Location = new System.Drawing.Point(12, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(311, 700);
            this.panel2.TabIndex = 1;
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(83)))), ((int)(((byte)(107)))));
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView1.Font = new System.Drawing.Font("Yu Gothic UI Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.treeView1.ForeColor = System.Drawing.SystemColors.Control;
            this.treeView1.Location = new System.Drawing.Point(3, 3);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "DescriptionNode";
            treeNode1.Text = "О программе";
            treeNode2.Name = "LanguageMainNode";
            treeNode2.Text = "Основная информация";
            treeNode3.Name = "LanguageCompileNode";
            treeNode3.Text = "Компиляция";
            treeNode4.Name = "LanguageSyntaxNode";
            treeNode4.Text = "Синтаксис";
            treeNode5.Name = "LanguageDataNode";
            treeNode5.Text = "Работа с данными";
            treeNode6.Name = "LanguageNode";
            treeNode6.Text = "О языке \"ZaHaR 2.0\"";
            treeNode7.Name = "RunNode";
            treeNode7.Text = "Запуск";
            treeNode8.Name = "Root";
            treeNode8.Text = "Справка";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode8});
            this.treeView1.Size = new System.Drawing.Size(305, 694);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // HelpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(107)))), ((int)(((byte)(145)))));
            this.ClientSize = new System.Drawing.Size(914, 724);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(930, 763);
            this.Name = "HelpForm";
            this.Text = "HelpForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private TreeView treeView1;
        private Label DescriptionLabel;
        private Panel panel3;
        private Label TitleLabel;
    }
}