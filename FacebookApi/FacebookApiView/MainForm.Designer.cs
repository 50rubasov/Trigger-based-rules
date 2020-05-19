namespace FacebookApiView
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.DataGridView = new System.Windows.Forms.DataGridView();
            this.CreateRuleButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RkComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.BmComboBox = new System.Windows.Forms.ComboBox();
            this.TokenTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.filtersResultBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Targer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.filtersResultBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // DataGridView
            // 
            this.DataGridView.AllowUserToAddRows = false;
            this.DataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Targer,
            this.Column3,
            this.Column2});
            this.DataGridView.Location = new System.Drawing.Point(12, 75);
            this.DataGridView.Name = "DataGridView";
            this.DataGridView.ReadOnly = true;
            this.DataGridView.RowHeadersWidth = 30;
            this.DataGridView.RowTemplate.Height = 24;
            this.DataGridView.Size = new System.Drawing.Size(714, 263);
            this.DataGridView.StandardTab = true;
            this.DataGridView.TabIndex = 6;
            
            // 
            // CreateRuleButton
            // 
            this.CreateRuleButton.Location = new System.Drawing.Point(12, 344);
            this.CreateRuleButton.Name = "CreateRuleButton";
            this.CreateRuleButton.Size = new System.Drawing.Size(270, 33);
            this.CreateRuleButton.TabIndex = 7;
            this.CreateRuleButton.Text = "Добавить новое правило";
            this.CreateRuleButton.UseVisualStyleBackColor = true;
            this.CreateRuleButton.Click += new System.EventHandler(this.CreateRuleButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(456, 344);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(270, 33);
            this.DeleteButton.TabIndex = 8;
            this.DeleteButton.Text = "Удалить правило";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RkComboBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.BmComboBox);
            this.groupBox1.Controls.Add(this.TokenTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(713, 56);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Выбор аккаунта";
            // 
            // RkComboBox
            // 
            this.RkComboBox.FormattingEnabled = true;
            this.RkComboBox.Location = new System.Drawing.Point(553, 22);
            this.RkComboBox.Name = "RkComboBox";
            this.RkComboBox.Size = new System.Drawing.Size(154, 24);
            this.RkComboBox.TabIndex = 6;
            this.RkComboBox.SelectedIndexChanged += new System.EventHandler(this.RkComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(517, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "РК:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(352, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "БМ:";
            // 
            // BmComboBox
            // 
            this.BmComboBox.FormattingEnabled = true;
            this.BmComboBox.Location = new System.Drawing.Point(390, 22);
            this.BmComboBox.Name = "BmComboBox";
            this.BmComboBox.Size = new System.Drawing.Size(121, 24);
            this.BmComboBox.TabIndex = 3;
            this.BmComboBox.SelectedIndexChanged += new System.EventHandler(this.BmComboBox_SelectedIndexChanged);
            // 
            // TokenTextBox
            // 
            this.TokenTextBox.Location = new System.Drawing.Point(64, 22);
            this.TokenTextBox.Name = "TokenTextBox";
            this.TokenTextBox.Size = new System.Drawing.Size(282, 22);
            this.TokenTextBox.TabIndex = 1;
            this.TokenTextBox.Leave += new System.EventHandler(this.TokenTextBox_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Токен:";
            // 
            // filtersResultBindingSource
            // 
            this.filtersResultBindingSource.DataSource = typeof(FacebookApiModel.FiltersResult);
            // 
            // Column1
            // 
            this.Column1.FillWeight = 75F;
            this.Column1.HeaderText = "Название";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Targer
            // 
            this.Targer.HeaderText = "Цель Правила";
            this.Targer.MinimumWidth = 6;
            this.Targer.Name = "Targer";
            this.Targer.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Действие";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Условие";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 389);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.CreateRuleButton);
            this.Controls.Add(this.DataGridView);
            this.Name = "MainForm";
            this.Text = "Правила на основе триггеров";
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.filtersResultBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView DataGridView;
        private System.Windows.Forms.Button CreateRuleButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox BmComboBox;
        private System.Windows.Forms.TextBox TokenTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox RkComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.BindingSource filtersResultBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Targer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}

