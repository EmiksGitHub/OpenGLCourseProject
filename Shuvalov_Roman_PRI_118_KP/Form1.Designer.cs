
namespace Shuvalov_Roman_PRI_118_KP
{
    partial class Form1
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
            this.AnT = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.RenderTimer = new System.Windows.Forms.Timer(this.components);
            this.numericX = new System.Windows.Forms.NumericUpDown();
            this.numericAngle = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonAnimation = new System.Windows.Forms.Button();
            this.buttonAddKolobok = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAngle)).BeginInit();
            this.SuspendLayout();
            // 
            // AnT
            // 
            this.AnT.AccumBits = ((byte)(0));
            this.AnT.AutoCheckErrors = false;
            this.AnT.AutoFinish = false;
            this.AnT.AutoMakeCurrent = true;
            this.AnT.AutoSwapBuffers = true;
            this.AnT.BackColor = System.Drawing.Color.Black;
            this.AnT.ColorBits = ((byte)(32));
            this.AnT.DepthBits = ((byte)(16));
            this.AnT.Location = new System.Drawing.Point(13, 13);
            this.AnT.Margin = new System.Windows.Forms.Padding(4);
            this.AnT.Name = "AnT";
            this.AnT.Size = new System.Drawing.Size(1104, 712);
            this.AnT.StencilBits = ((byte)(0));
            this.AnT.TabIndex = 1;
            this.AnT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AnT_KeyDown);
            // 
            // RenderTimer
            // 
            this.RenderTimer.Interval = 30;
            this.RenderTimer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // numericX
            // 
            this.numericX.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericX.Location = new System.Drawing.Point(1171, 692);
            this.numericX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericX.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericX.Name = "numericX";
            this.numericX.Size = new System.Drawing.Size(66, 22);
            this.numericX.TabIndex = 3;
            this.numericX.ValueChanged += new System.EventHandler(this.numericX_ValueChanged);
            // 
            // numericAngle
            // 
            this.numericAngle.Location = new System.Drawing.Point(1264, 692);
            this.numericAngle.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericAngle.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericAngle.Name = "numericAngle";
            this.numericAngle.Size = new System.Drawing.Size(66, 22);
            this.numericAngle.TabIndex = 15;
            this.numericAngle.ValueChanged += new System.EventHandler(this.numericAngle_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1261, 655);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 34);
            this.label7.TabIndex = 16;
            this.label7.Text = "Скорость\r\nповорота";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1168, 655);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 34);
            this.label8.TabIndex = 17;
            this.label8.Text = "Скорость\r\nкамеры";
            // 
            // buttonAnimation
            // 
            this.buttonAnimation.Location = new System.Drawing.Point(1264, 12);
            this.buttonAnimation.Name = "buttonAnimation";
            this.buttonAnimation.Size = new System.Drawing.Size(106, 47);
            this.buttonAnimation.TabIndex = 22;
            this.buttonAnimation.Text = "Испытание горки";
            this.buttonAnimation.UseVisualStyleBackColor = true;
            this.buttonAnimation.Click += new System.EventHandler(this.buttonAnimation_Click);
            // 
            // buttonAddKolobok
            // 
            this.buttonAddKolobok.Location = new System.Drawing.Point(1136, 12);
            this.buttonAddKolobok.Name = "buttonAddKolobok";
            this.buttonAddKolobok.Size = new System.Drawing.Size(101, 47);
            this.buttonAddKolobok.TabIndex = 24;
            this.buttonAddKolobok.Text = "Добавить колобка";
            this.buttonAddKolobok.UseVisualStyleBackColor = true;
            this.buttonAddKolobok.Click += new System.EventHandler(this.buttonAddKolobok_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1125, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 238);
            this.label1.TabIndex = 25;
            this.label1.Text = "Управление:\r\n\r\n  w\r\na s d - перемещение камеры\r\n\r\nq, e - поворот камеры влево/впр" +
    "аво\r\n\r\nr, f - поворот камеры вверх/вниз\r\n\r\n   y\r\ng h j - управление колобком\r\n\r\n" +
    "  o\r\nk l ; - управление порталом";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Главная камера",
            "Камеры рождения колобка",
            "Камера испытания горки",
            "Закулисье"});
            this.comboBox1.Location = new System.Drawing.Point(1128, 325);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(242, 24);
            this.comboBox1.TabIndex = 27;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1387, 738);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonAddKolobok);
            this.Controls.Add(this.buttonAnimation);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.numericAngle);
            this.Controls.Add(this.numericX);
            this.Controls.Add(this.AnT);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAngle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Tao.Platform.Windows.SimpleOpenGlControl AnT;
        private System.Windows.Forms.Timer RenderTimer;
        private System.Windows.Forms.NumericUpDown numericX;
        private System.Windows.Forms.NumericUpDown numericAngle;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonAnimation;
        private System.Windows.Forms.Button buttonAddKolobok;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

