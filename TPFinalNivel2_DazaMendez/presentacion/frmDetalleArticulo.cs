using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using accesoDatos;
using negocio;

namespace presentacion
{
    public partial class frmDetalleArticulo : Form
    {
        private Articulo articulo = null;
        public frmDetalleArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDetalleArticulo_Load(object sender, EventArgs e)
        {
            try
            {
                if(articulo != null)
                {
                    lbResultCodigo.Text = articulo.Codigo;
                    lbResultNombre.Text = articulo.Nombre;
                    lbResultDescripción.Text = articulo.Descripcion;
                    lbResultMarca.Text = articulo.Marca.Descripcion;
                    lbResultCategoria.Text = articulo.Categoria.Descripcion;
                    lbResultImagen.Text = articulo.UrlImagen;
                    cargarImagen(articulo.UrlImagen);
                    lbResultPrecio.Text = articulo.Precio.ToString();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private void cargarImagen(string imagen)
        {
            try
            {
                pbImagen.Load(imagen);
            }
            catch (Exception)
            {

                pbImagen.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR8TrfhVXyRtTKud_EqvLkVVInC76ZdnCz4OwTxOiyXyA&s");
            }
        }

        private void lbPrecio_Click(object sender, EventArgs e)
        {

        }

        private void lbResultPrecio_Click(object sender, EventArgs e)
        {

        }
    }
}
