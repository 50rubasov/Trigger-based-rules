namespace FacebookApiView
{
    partial class CreateRuleForm
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
            this.ConditionGroupBox = new System.Windows.Forms.GroupBox();
            this.AddCondition = new System.Windows.Forms.Button();
            this.ConditionValueTextBox = new System.Windows.Forms.NumericUpDown();
            this.ConditionOperatorComboBox = new System.Windows.Forms.ComboBox();
            this.ConditionFieldComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.CreateButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.EntityTypeComboBox = new System.Windows.Forms.ComboBox();
            this.ActionOnRadioButton = new System.Windows.Forms.RadioButton();
            this.ActionOffRadioButton = new System.Windows.Forms.RadioButton();
            this.ConditionGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ConditionValueTextBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // ConditionGroupBox
            // 
            this.ConditionGroupBox.Controls.Add(this.AddCondition);
            this.ConditionGroupBox.Controls.Add(this.ConditionValueTextBox);
            this.ConditionGroupBox.Controls.Add(this.ConditionOperatorComboBox);
            this.ConditionGroupBox.Controls.Add(this.ConditionFieldComboBox);
            this.ConditionGroupBox.Location = new System.Drawing.Point(12, 196);
            this.ConditionGroupBox.Name = "ConditionGroupBox";
            this.ConditionGroupBox.Size = new System.Drawing.Size(362, 81);
            this.ConditionGroupBox.TabIndex = 7;
            this.ConditionGroupBox.TabStop = false;
            this.ConditionGroupBox.Text = "Условия";
            // 
            // AddCondition
            // 
            this.AddCondition.Location = new System.Drawing.Point(4, 51);
            this.AddCondition.Name = "AddCondition";
            this.AddCondition.Size = new System.Drawing.Size(75, 23);
            this.AddCondition.TabIndex = 13;
            this.AddCondition.Text = "+";
            this.AddCondition.UseVisualStyleBackColor = true;
            this.AddCondition.Click += new System.EventHandler(this.AddCondition_Click);
            // 
            // ConditionValueTextBox
            // 
            this.ConditionValueTextBox.Location = new System.Drawing.Point(280, 21);
            this.ConditionValueTextBox.Name = "ConditionValueTextBox";
            this.ConditionValueTextBox.Size = new System.Drawing.Size(75, 22);
            this.ConditionValueTextBox.TabIndex = 4;
            // 
            // ConditionOperatorComboBox
            // 
            this.ConditionOperatorComboBox.FormattingEnabled = true;
            this.ConditionOperatorComboBox.Items.AddRange(new object[] {
            "Больше, чем",
            "Меньше, чем",
            "Равно"});
            this.ConditionOperatorComboBox.Location = new System.Drawing.Point(162, 21);
            this.ConditionOperatorComboBox.Name = "ConditionOperatorComboBox";
            this.ConditionOperatorComboBox.Size = new System.Drawing.Size(112, 24);
            this.ConditionOperatorComboBox.TabIndex = 3;
            this.ConditionOperatorComboBox.Text = "Больше, чем";
            this.ConditionOperatorComboBox.SelectedIndexChanged += new System.EventHandler(this.ConditionOperatorComboBox_SelectedIndexChanged);
            // 
            // ConditionFieldComboBox
            // 
            this.ConditionFieldComboBox.FormattingEnabled = true;
            this.ConditionFieldComboBox.Items.AddRange(new object[] {
            "Цена за результат",
            "Результаты",
            "Расходы"});
            this.ConditionFieldComboBox.Location = new System.Drawing.Point(6, 21);
            this.ConditionFieldComboBox.Name = "ConditionFieldComboBox";
            this.ConditionFieldComboBox.Size = new System.Drawing.Size(150, 24);
            this.ConditionFieldComboBox.TabIndex = 2;
            this.ConditionFieldComboBox.Text = "Цена за результат";
            this.ConditionFieldComboBox.SelectedIndexChanged += new System.EventHandler(this.ConditionFieldComboBox_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ActionOffRadioButton);
            this.groupBox1.Controls.Add(this.ActionOnRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 138);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(362, 52);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Действие";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.NameTextBox);
            this.groupBox4.Location = new System.Drawing.Point(12, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(363, 59);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Название правила";
            // 
            // NameTextBox
            // 
            this.NameTextBox.Location = new System.Drawing.Point(7, 22);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(348, 22);
            this.NameTextBox.TabIndex = 0;
            // 
            // CreateButton
            // 
            this.CreateButton.Location = new System.Drawing.Point(16, 451);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(99, 28);
            this.CreateButton.TabIndex = 10;
            this.CreateButton.Text = "Добавить";
            this.CreateButton.UseVisualStyleBackColor = true;
            this.CreateButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(281, 451);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(91, 29);
            this.CloseButton.TabIndex = 11;
            this.CloseButton.Text = "Отменить";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.EntityTypeComboBox);
            this.groupBox5.Location = new System.Drawing.Point(12, 77);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(362, 55);
            this.groupBox5.TabIndex = 12;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Область применения правила";
            // 
            // EntityTypeComboBox
            // 
            this.EntityTypeComboBox.FormattingEnabled = true;
            this.EntityTypeComboBox.Items.AddRange(new object[] {
            "Компания",
            "Группа объявлений",
            "Объявление"});
            this.EntityTypeComboBox.Location = new System.Drawing.Point(6, 21);
            this.EntityTypeComboBox.Name = "EntityTypeComboBox";
            this.EntityTypeComboBox.Size = new System.Drawing.Size(265, 24);
            this.EntityTypeComboBox.TabIndex = 0;
            this.EntityTypeComboBox.Text = "Группа объявлений";
            this.EntityTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.EntityTypeComboBox_SelectedIndexChanged);
            // 
            // ActionOnRadioButton
            // 
            this.ActionOnRadioButton.AutoSize = true;
            this.ActionOnRadioButton.Location = new System.Drawing.Point(116, 21);
            this.ActionOnRadioButton.Name = "ActionOnRadioButton";
            this.ActionOnRadioButton.Size = new System.Drawing.Size(93, 21);
            this.ActionOnRadioButton.TabIndex = 0;
            this.ActionOnRadioButton.Text = "Включить";
            this.ActionOnRadioButton.UseVisualStyleBackColor = true;
            this.ActionOnRadioButton.CheckedChanged += new System.EventHandler(this.ActionOnRadioButton_CheckedChanged);
            // 
            // ActionOffRadioButton
            // 
            this.ActionOffRadioButton.AutoSize = true;
            this.ActionOffRadioButton.Checked = true;
            this.ActionOffRadioButton.Location = new System.Drawing.Point(7, 21);
            this.ActionOffRadioButton.Name = "ActionOffRadioButton";
            this.ActionOffRadioButton.Size = new System.Drawing.Size(103, 21);
            this.ActionOffRadioButton.TabIndex = 1;
            this.ActionOffRadioButton.TabStop = true;
            this.ActionOffRadioButton.Text = "Выключить";
            this.ActionOffRadioButton.UseVisualStyleBackColor = true;
            this.ActionOffRadioButton.CheckedChanged += new System.EventHandler(this.ActionOffRadioButton_CheckedChanged);
            // 
            // CreateRuleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 492);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.CreateButton);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.ConditionGroupBox);
            this.Controls.Add(this.groupBox1);
            this.Name = "CreateRuleForm";
            this.Text = "Добавление авто-правила";
            this.ConditionGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ConditionValueTextBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox ConditionGroupBox;
        private System.Windows.Forms.NumericUpDown ConditionValueTextBox;
        private System.Windows.Forms.ComboBox ConditionOperatorComboBox;
        private System.Windows.Forms.ComboBox ConditionFieldComboBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.Button CreateButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox EntityTypeComboBox;
        private System.Windows.Forms.Button AddCondition;
        private System.Windows.Forms.RadioButton ActionOffRadioButton;
        private System.Windows.Forms.RadioButton ActionOnRadioButton;
    }
}