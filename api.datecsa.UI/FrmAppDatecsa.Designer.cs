
namespace api.datecsa.UI
{
    partial class FrmAppDatecsa
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        /// paramtros
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtUsuario = new System.Windows.Forms.TextBox();
            this.TxtPasswordUsu = new System.Windows.Forms.TextBox();
            this.TxtUrlAppServer = new System.Windows.Forms.TextBox();
            this.TxtDataSource = new System.Windows.Forms.TextBox();
            this.BtnCargarDatos = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.RadAgregar = new System.Windows.Forms.RadioButton();
            this.RadEliminar = new System.Windows.Forms.RadioButton();
            this.bndCargaDatos = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bndCargaDatos)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(177, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(233, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ingresar los siguientes datos";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(32, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Usuario Onbase";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(32, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Contraseña Usuario";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(32, 219);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Url AppServer";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(32, 273);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "DataSource DB";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TxtUsuario
            // 
            this.TxtUsuario.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bndCargaDatos, "UsuarioOnbase", true));
            this.TxtUsuario.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtUsuario.Location = new System.Drawing.Point(323, 118);
            this.TxtUsuario.Name = "TxtUsuario";
            this.TxtUsuario.Size = new System.Drawing.Size(189, 25);
            this.TxtUsuario.TabIndex = 4;
            // 
            // TxtPasswordUsu
            // 
            this.TxtPasswordUsu.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bndCargaDatos, "PasswordUsuario", true));
            this.TxtPasswordUsu.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPasswordUsu.Location = new System.Drawing.Point(323, 166);
            this.TxtPasswordUsu.Name = "TxtPasswordUsu";
            this.TxtPasswordUsu.PasswordChar = '*';
            this.TxtPasswordUsu.Size = new System.Drawing.Size(189, 25);
            this.TxtPasswordUsu.TabIndex = 6;
            // 
            // TxtUrlAppServer
            // 
            this.TxtUrlAppServer.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bndCargaDatos, "UrlAppServer", true));
            this.TxtUrlAppServer.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtUrlAppServer.Location = new System.Drawing.Point(323, 219);
            this.TxtUrlAppServer.Name = "TxtUrlAppServer";
            this.TxtUrlAppServer.Size = new System.Drawing.Size(370, 25);
            this.TxtUrlAppServer.TabIndex = 8;
            // 
            // TxtDataSource
            // 
            this.TxtDataSource.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bndCargaDatos, "DataSourceDB", true));
            this.TxtDataSource.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDataSource.Location = new System.Drawing.Point(323, 273);
            this.TxtDataSource.Name = "TxtDataSource";
            this.TxtDataSource.Size = new System.Drawing.Size(189, 25);
            this.TxtDataSource.TabIndex = 10;
            // 
            // BtnCargarDatos
            // 
            this.BtnCargarDatos.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCargarDatos.Location = new System.Drawing.Point(269, 320);
            this.BtnCargarDatos.Name = "BtnCargarDatos";
            this.BtnCargarDatos.Size = new System.Drawing.Size(125, 41);
            this.BtnCargarDatos.TabIndex = 11;
            this.BtnCargarDatos.Text = "Cargar Datos";
            this.BtnCargarDatos.UseVisualStyleBackColor = true;
            this.BtnCargarDatos.Click += new System.EventHandler(this.BtnCargarDatos_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(32, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(253, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "Definir si decea Cargar o Eliminar las SK";
            // 
            // RadAgregar
            // 
            this.RadAgregar.AutoSize = true;
            this.RadAgregar.Checked = true;
            this.RadAgregar.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RadAgregar.Location = new System.Drawing.Point(323, 75);
            this.RadAgregar.Name = "RadAgregar";
            this.RadAgregar.Size = new System.Drawing.Size(92, 24);
            this.RadAgregar.TabIndex = 1;
            this.RadAgregar.TabStop = true;
            this.RadAgregar.Text = "Agregar SK";
            this.RadAgregar.UseVisualStyleBackColor = true;
            // 
            // RadEliminar
            // 
            this.RadEliminar.AutoSize = true;
            this.RadEliminar.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RadEliminar.Location = new System.Drawing.Point(454, 75);
            this.RadEliminar.Name = "RadEliminar";
            this.RadEliminar.Size = new System.Drawing.Size(95, 24);
            this.RadEliminar.TabIndex = 2;
            this.RadEliminar.TabStop = true;
            this.RadEliminar.Text = "Eliminar SK";
            this.RadEliminar.UseVisualStyleBackColor = true;
            // 
            // bndCargaDatos
            // 
            this.bndCargaDatos.DataSource = typeof(api.datecsa.modelo.DatosUsuarioSK);
            // 
            // FrmAppDatecsa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(712, 378);
            this.Controls.Add(this.RadEliminar);
            this.Controls.Add(this.RadAgregar);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.BtnCargarDatos);
            this.Controls.Add(this.TxtDataSource);
            this.Controls.Add(this.TxtUrlAppServer);
            this.Controls.Add(this.TxtPasswordUsu);
            this.Controls.Add(this.TxtUsuario);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAppDatecsa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aplicativo Interno";
            ((System.ComponentModel.ISupportInitialize)(this.bndCargaDatos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TxtUsuario;
        private System.Windows.Forms.TextBox TxtPasswordUsu;
        private System.Windows.Forms.TextBox TxtUrlAppServer;
        private System.Windows.Forms.TextBox TxtDataSource;
        private System.Windows.Forms.BindingSource bndCargaDatos;
        private System.Windows.Forms.Button BtnCargarDatos;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton RadAgregar;
        private System.Windows.Forms.RadioButton RadEliminar;
    }
}