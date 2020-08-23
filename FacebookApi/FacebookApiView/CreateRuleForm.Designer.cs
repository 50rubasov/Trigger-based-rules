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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateRuleForm));
            this.AddCondition = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.CreateButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.EntityTypeComboBox = new System.Windows.Forms.ComboBox();
            this.TableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.ConditionValueTextBox1 = new System.Windows.Forms.NumericUpDown();
            this.ConditionFieldComboBox1 = new System.Windows.Forms.ComboBox();
            this.ConditionOperatorComboBox1 = new System.Windows.Forms.ComboBox();
            this.FiltersLabel = new System.Windows.Forms.Label();
            this.ConditionGroupBox = new System.Windows.Forms.GroupBox();
            this.DeleteCondition = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TimeRangeComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.TableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ConditionValueTextBox1)).BeginInit();
            this.ConditionGroupBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddCondition
            // 
            this.AddCondition.Location = new System.Drawing.Point(7, 21);
            this.AddCondition.Name = "AddCondition";
            this.AddCondition.Size = new System.Drawing.Size(137, 22);
            this.AddCondition.TabIndex = 13;
            this.AddCondition.Text = "+";
            this.AddCondition.UseVisualStyleBackColor = true;
            this.AddCondition.Click += new System.EventHandler(this.AddConditionClick);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.NameTextBox);
            this.groupBox4.Location = new System.Drawing.Point(12, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(281, 59);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Название правила";
            // 
            // NameTextBox
            // 
            this.NameTextBox.Location = new System.Drawing.Point(7, 22);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(264, 22);
            this.NameTextBox.TabIndex = 0;
            // 
            // CreateButton
            // 
            this.CreateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CreateButton.Location = new System.Drawing.Point(12, 305);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(143, 28);
            this.CreateButton.TabIndex = 10;
            this.CreateButton.Text = "Создать правило";
            this.CreateButton.UseVisualStyleBackColor = true;
            this.CreateButton.Click += new System.EventHandler(this.CreateButtonClick);
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.Location = new System.Drawing.Point(161, 304);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(132, 29);
            this.CloseButton.TabIndex = 11;
            this.CloseButton.Text = "Закрыть";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButtonClick);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.EntityTypeComboBox);
            this.groupBox5.Location = new System.Drawing.Point(12, 77);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(281, 55);
            this.groupBox5.TabIndex = 12;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Область применения правила";
            // 
            // EntityTypeComboBox
            // 
            this.EntityTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EntityTypeComboBox.FormattingEnabled = true;
            this.EntityTypeComboBox.Items.AddRange(new object[] {
            "Кампания",
            "Группа объявлений",
            "Объявление"});
            this.EntityTypeComboBox.Location = new System.Drawing.Point(6, 21);
            this.EntityTypeComboBox.Name = "EntityTypeComboBox";
            this.EntityTypeComboBox.Size = new System.Drawing.Size(265, 24);
            this.EntityTypeComboBox.TabIndex = 0;
            this.EntityTypeComboBox.Leave += new System.EventHandler(this.EntityTypeComboBoxLeave);
            // 
            // TableLayoutPanel
            // 
            this.TableLayoutPanel.ColumnCount = 3;
            this.TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.04027F));
            this.TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.61462F));
            this.TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.57143F));
            this.TableLayoutPanel.Controls.Add(this.ConditionValueTextBox1, 2, 0);
            this.TableLayoutPanel.Controls.Add(this.ConditionFieldComboBox1, 0, 0);
            this.TableLayoutPanel.Controls.Add(this.ConditionOperatorComboBox1, 1, 0);
            this.TableLayoutPanel.Controls.Add(this.FiltersLabel, 0, 1);
            this.TableLayoutPanel.Location = new System.Drawing.Point(12, 215);
            this.TableLayoutPanel.Name = "TableLayoutPanel";
            this.TableLayoutPanel.RowCount = 2;
            this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TableLayoutPanel.Size = new System.Drawing.Size(281, 31);
            this.TableLayoutPanel.TabIndex = 13;
            // 
            // ConditionValueTextBox1
            // 
            this.ConditionValueTextBox1.Location = new System.Drawing.Point(203, 3);
            this.ConditionValueTextBox1.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.ConditionValueTextBox1.Name = "ConditionValueTextBox1";
            this.ConditionValueTextBox1.Size = new System.Drawing.Size(69, 22);
            this.ConditionValueTextBox1.TabIndex = 1;
            // 
            // ConditionFieldComboBox1
            // 
            this.ConditionFieldComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ConditionFieldComboBox1.FormattingEnabled = true;
            this.ConditionFieldComboBox1.Items.AddRange(new object[] {
            "Цена за результат",
            "Результаты",
            "Расходы",
            "Цена за установку",
            "Показы",
            "Охват",
            "CPM",
            "CPC",
            "CTR"});
            this.ConditionFieldComboBox1.Location = new System.Drawing.Point(3, 3);
            this.ConditionFieldComboBox1.Name = "ConditionFieldComboBox1";
            this.ConditionFieldComboBox1.Size = new System.Drawing.Size(151, 24);
            this.ConditionFieldComboBox1.TabIndex = 2;
            this.ConditionFieldComboBox1.Leave += new System.EventHandler(this.ConditionFieldComboBoxLeave);
            // 
            // ConditionOperatorComboBox1
            // 
            this.ConditionOperatorComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ConditionOperatorComboBox1.FormattingEnabled = true;
            this.ConditionOperatorComboBox1.Items.AddRange(new object[] {
            ">",
            "<"});
            this.ConditionOperatorComboBox1.Location = new System.Drawing.Point(160, 3);
            this.ConditionOperatorComboBox1.Name = "ConditionOperatorComboBox1";
            this.ConditionOperatorComboBox1.Size = new System.Drawing.Size(37, 24);
            this.ConditionOperatorComboBox1.TabIndex = 3;
            // 
            // FiltersLabel
            // 
            this.FiltersLabel.AutoSize = true;
            this.FiltersLabel.Location = new System.Drawing.Point(3, 30);
            this.FiltersLabel.Name = "FiltersLabel";
            this.FiltersLabel.Size = new System.Drawing.Size(69, 17);
            this.FiltersLabel.TabIndex = 16;
            this.FiltersLabel.Text = "Фильтры";
            // 
            // ConditionGroupBox
            // 
            this.ConditionGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ConditionGroupBox.Controls.Add(this.AddCondition);
            this.ConditionGroupBox.Controls.Add(this.DeleteCondition);
            this.ConditionGroupBox.Location = new System.Drawing.Point(12, 248);
            this.ConditionGroupBox.Name = "ConditionGroupBox";
            this.ConditionGroupBox.Size = new System.Drawing.Size(288, 52);
            this.ConditionGroupBox.TabIndex = 7;
            this.ConditionGroupBox.TabStop = false;
            this.ConditionGroupBox.Text = "Добавление/удаление фильтров";
            // 
            // DeleteCondition
            // 
            this.DeleteCondition.Location = new System.Drawing.Point(150, 21);
            this.DeleteCondition.Name = "DeleteCondition";
            this.DeleteCondition.Size = new System.Drawing.Size(122, 22);
            this.DeleteCondition.TabIndex = 14;
            this.DeleteCondition.Text = "-";
            this.DeleteCondition.UseVisualStyleBackColor = true;
            this.DeleteCondition.Click += new System.EventHandler(this.DeleteConditionClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TimeRangeComboBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 138);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(281, 54);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Диапазон времени";
            // 
            // TimeRangeComboBox
            // 
            this.TimeRangeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TimeRangeComboBox.FormattingEnabled = true;
            this.TimeRangeComboBox.Items.AddRange(new object[] {
            "Весь срок действия",
            "Сегодня"});
            this.TimeRangeComboBox.Location = new System.Drawing.Point(6, 21);
            this.TimeRangeComboBox.Name = "TimeRangeComboBox";
            this.TimeRangeComboBox.Size = new System.Drawing.Size(264, 24);
            this.TimeRangeComboBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 195);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 17);
            this.label1.TabIndex = 16;
            this.label1.Text = "Условие триггера";
            // 
            // CreateRuleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 345);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.TableLayoutPanel);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.CreateButton);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.ConditionGroupBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(323, 531);
            this.MinimumSize = new System.Drawing.Size(323, 392);
            this.Name = "CreateRuleForm";
            this.Text = "Добавление авто-правила";
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.TableLayoutPanel.ResumeLayout(false);
            this.TableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ConditionValueTextBox1)).EndInit();
            this.ConditionGroupBox.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.Button CreateButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox EntityTypeComboBox;
        private System.Windows.Forms.Button AddCondition;
        private System.Windows.Forms.TableLayoutPanel TableLayoutPanel;
        private System.Windows.Forms.GroupBox ConditionGroupBox;
        private System.Windows.Forms.Button DeleteCondition;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox TimeRangeComboBox;
        private System.Windows.Forms.Label FiltersLabel;
        private System.Windows.Forms.NumericUpDown ConditionValueTextBox1;
        private System.Windows.Forms.ComboBox ConditionFieldComboBox1;
        private System.Windows.Forms.ComboBox ConditionOperatorComboBox1;
        private System.Windows.Forms.Label label1;
    }
}