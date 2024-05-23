namespace presentacion
{
    partial class frmDetalleArticulo
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
            this.btnCerrar = new System.Windows.Forms.Button();
            this.lbnombre = new System.Windows.Forms.Label();
            this.lbCodigo = new System.Windows.Forms.Label();
            this.lbDescripcion = new System.Windows.Forms.Label();
            this.lbMarca = new System.Windows.Forms.Label();
            this.lcCategoria = new System.Windows.Forms.Label();
            this.lbImagen = new System.Windows.Forms.Label();
            this.lbPrecio = new System.Windows.Forms.Label();
            this.pbImagen = new System.Windows.Forms.PictureBox();
            this.lbResultCodigo = new System.Windows.Forms.Label();
            this.lbResultNombre = new System.Windows.Forms.Label();
            this.lbResultDescripción = new System.Windows.Forms.Label();
            this.lbResultMarca = new System.Windows.Forms.Label();
            this.lbResultCategoria = new System.Windows.Forms.Label();
            this.lbResultImagen = new System.Windows.Forms.Label();
            this.lbResultPrecio = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbImagen)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCerrar
            // 
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.Location = new System.Drawing.Point(621, 481);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(103, 32);
            this.btnCerrar.TabIndex = 0;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // lbnombre
            // 
            this.lbnombre.AutoSize = true;
            this.lbnombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbnombre.Location = new System.Drawing.Point(73, 85);
            this.lbnombre.Name = "lbnombre";
            this.lbnombre.Size = new System.Drawing.Size(59, 16);
            this.lbnombre.TabIndex = 1;
            this.lbnombre.Text = "Nombre:";
            // 
            // lbCodigo
            // 
            this.lbCodigo.AutoSize = true;
            this.lbCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCodigo.Location = new System.Drawing.Point(78, 42);
            this.lbCodigo.Name = "lbCodigo";
            this.lbCodigo.Size = new System.Drawing.Size(54, 16);
            this.lbCodigo.TabIndex = 2;
            this.lbCodigo.Text = "Código:";
            // 
            // lbDescripcion
            // 
            this.lbDescripcion.AutoSize = true;
            this.lbDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDescripcion.Location = new System.Drawing.Point(50, 128);
            this.lbDescripcion.Name = "lbDescripcion";
            this.lbDescripcion.Size = new System.Drawing.Size(82, 16);
            this.lbDescripcion.TabIndex = 3;
            this.lbDescripcion.Text = "Descripción:";
            // 
            // lbMarca
            // 
            this.lbMarca.AutoSize = true;
            this.lbMarca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMarca.Location = new System.Drawing.Point(84, 226);
            this.lbMarca.Name = "lbMarca";
            this.lbMarca.Size = new System.Drawing.Size(48, 16);
            this.lbMarca.TabIndex = 4;
            this.lbMarca.Text = "Marca:";
            // 
            // lcCategoria
            // 
            this.lcCategoria.AutoSize = true;
            this.lcCategoria.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lcCategoria.Location = new System.Drawing.Point(63, 269);
            this.lcCategoria.Name = "lcCategoria";
            this.lcCategoria.Size = new System.Drawing.Size(69, 16);
            this.lcCategoria.TabIndex = 5;
            this.lcCategoria.Text = "Categoría:";
            // 
            // lbImagen
            // 
            this.lbImagen.AutoSize = true;
            this.lbImagen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbImagen.Location = new System.Drawing.Point(57, 312);
            this.lbImagen.Name = "lbImagen";
            this.lbImagen.Size = new System.Drawing.Size(75, 16);
            this.lbImagen.TabIndex = 6;
            this.lbImagen.Text = "Url Imagen:";
            // 
            // lbPrecio
            // 
            this.lbPrecio.AutoSize = true;
            this.lbPrecio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPrecio.Location = new System.Drawing.Point(83, 371);
            this.lbPrecio.Name = "lbPrecio";
            this.lbPrecio.Size = new System.Drawing.Size(49, 16);
            this.lbPrecio.TabIndex = 7;
            this.lbPrecio.Text = "Precio:";
            this.lbPrecio.Click += new System.EventHandler(this.lbPrecio_Click);
            // 
            // pbImagen
            // 
            this.pbImagen.Location = new System.Drawing.Point(411, 42);
            this.pbImagen.Name = "pbImagen";
            this.pbImagen.Size = new System.Drawing.Size(313, 402);
            this.pbImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbImagen.TabIndex = 8;
            this.pbImagen.TabStop = false;
            // 
            // lbResultCodigo
            // 
            this.lbResultCodigo.AutoSize = true;
            this.lbResultCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbResultCodigo.Location = new System.Drawing.Point(150, 42);
            this.lbResultCodigo.Name = "lbResultCodigo";
            this.lbResultCodigo.Size = new System.Drawing.Size(0, 16);
            this.lbResultCodigo.TabIndex = 9;
            // 
            // lbResultNombre
            // 
            this.lbResultNombre.AutoSize = true;
            this.lbResultNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbResultNombre.Location = new System.Drawing.Point(150, 85);
            this.lbResultNombre.Name = "lbResultNombre";
            this.lbResultNombre.Size = new System.Drawing.Size(0, 16);
            this.lbResultNombre.TabIndex = 10;
            // 
            // lbResultDescripción
            // 
            this.lbResultDescripción.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbResultDescripción.Location = new System.Drawing.Point(150, 128);
            this.lbResultDescripción.Name = "lbResultDescripción";
            this.lbResultDescripción.Size = new System.Drawing.Size(255, 86);
            this.lbResultDescripción.TabIndex = 11;
            // 
            // lbResultMarca
            // 
            this.lbResultMarca.AutoSize = true;
            this.lbResultMarca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbResultMarca.Location = new System.Drawing.Point(150, 226);
            this.lbResultMarca.Name = "lbResultMarca";
            this.lbResultMarca.Size = new System.Drawing.Size(0, 16);
            this.lbResultMarca.TabIndex = 12;
            // 
            // lbResultCategoria
            // 
            this.lbResultCategoria.AutoSize = true;
            this.lbResultCategoria.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbResultCategoria.Location = new System.Drawing.Point(150, 269);
            this.lbResultCategoria.Name = "lbResultCategoria";
            this.lbResultCategoria.Size = new System.Drawing.Size(0, 16);
            this.lbResultCategoria.TabIndex = 13;
            // 
            // lbResultImagen
            // 
            this.lbResultImagen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbResultImagen.Location = new System.Drawing.Point(150, 312);
            this.lbResultImagen.Name = "lbResultImagen";
            this.lbResultImagen.Size = new System.Drawing.Size(255, 50);
            this.lbResultImagen.TabIndex = 14;
            // 
            // lbResultPrecio
            // 
            this.lbResultPrecio.AutoSize = true;
            this.lbResultPrecio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbResultPrecio.Location = new System.Drawing.Point(150, 371);
            this.lbResultPrecio.Name = "lbResultPrecio";
            this.lbResultPrecio.Size = new System.Drawing.Size(0, 16);
            this.lbResultPrecio.TabIndex = 15;
            this.lbResultPrecio.Click += new System.EventHandler(this.lbResultPrecio_Click);
            // 
            // frmDetalleArticulo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(777, 550);
            this.Controls.Add(this.lbResultPrecio);
            this.Controls.Add(this.lbResultImagen);
            this.Controls.Add(this.lbResultCategoria);
            this.Controls.Add(this.lbResultMarca);
            this.Controls.Add(this.lbResultDescripción);
            this.Controls.Add(this.lbResultNombre);
            this.Controls.Add(this.lbResultCodigo);
            this.Controls.Add(this.pbImagen);
            this.Controls.Add(this.lbPrecio);
            this.Controls.Add(this.lbImagen);
            this.Controls.Add(this.lcCategoria);
            this.Controls.Add(this.lbMarca);
            this.Controls.Add(this.lbDescripcion);
            this.Controls.Add(this.lbCodigo);
            this.Controls.Add(this.lbnombre);
            this.Controls.Add(this.btnCerrar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDetalleArticulo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Detalle artículo";
            this.Load += new System.EventHandler(this.frmDetalleArticulo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbImagen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label lbnombre;
        private System.Windows.Forms.Label lbCodigo;
        private System.Windows.Forms.Label lbDescripcion;
        private System.Windows.Forms.Label lbMarca;
        private System.Windows.Forms.Label lcCategoria;
        private System.Windows.Forms.Label lbImagen;
        private System.Windows.Forms.Label lbPrecio;
        private System.Windows.Forms.PictureBox pbImagen;
        private System.Windows.Forms.Label lbResultCodigo;
        private System.Windows.Forms.Label lbResultNombre;
        private System.Windows.Forms.Label lbResultDescripción;
        private System.Windows.Forms.Label lbResultMarca;
        private System.Windows.Forms.Label lbResultCategoria;
        private System.Windows.Forms.Label lbResultImagen;
        private System.Windows.Forms.Label lbResultPrecio;
    }
}