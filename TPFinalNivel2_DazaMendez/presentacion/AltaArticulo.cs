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
using negocio;
using accesoDatos;
using System.IO;
using System.Configuration;

namespace presentacion
{
    public partial class frmAltaArticulo : Form
    {
        private Articulo articulo = null;
        private OpenFileDialog archivo = null;
        public frmAltaArticulo()
        {
            InitializeComponent();
        }
        public frmAltaArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar artículo";
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                if(articulo == null)
                {
                    articulo = new Articulo();

                }
                articulo.Codigo = tbxCodigo.Text;
                articulo.Nombre = tbxNombre.Text;
                articulo.Descripcion = tbxDescripcion.Text;
                articulo.Marca = (Marca)cbMarca.SelectedItem;
                articulo.Categoria = (Categoria)cbCategoria.SelectedItem;
                articulo.UrlImagen = tbxImagen.Text;
                articulo.Precio = decimal.Parse(tbxPrecio.Text);

                if(articulo.Id != 0)
                {
                    negocio.modificar(articulo);
                    MessageBox.Show("Modificado exitosamente");
                }
                else
                {
                    negocio.agregar(articulo);
                    MessageBox.Show("Agregado exitosamente");
                }
                if (archivo != null && !(tbxImagen.Text.ToUpper().Contains("HTTP")))
                     File.Copy(archivo.FileName, ConfigurationManager.AppSettings["images-articulos"] + archivo.SafeFileName);

                Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void frmAltaArticulo_Load(object sender, EventArgs e)
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio cateNegocio = new CategoriaNegocio();
            try
            {
                cbMarca.DataSource = marcaNegocio.listar();
                cbMarca.ValueMember = "Id";
                cbMarca.DisplayMember = "Descripcion";
                cbCategoria.DataSource = cateNegocio.listar();
                cbCategoria.ValueMember = "Id";
                cbCategoria.DisplayMember = "Descripcion";

                if(articulo != null)
                {
                    tbxCodigo.Text = articulo.Codigo;
                    tbxNombre.Text = articulo.Nombre;
                    tbxDescripcion.Text = articulo.Descripcion;
                    cbMarca.SelectedValue = articulo.Marca.Id;
                    cbCategoria.SelectedValue = articulo.Categoria.Id;
                    tbxImagen.Text = articulo.UrlImagen;
                    cargarImagen(articulo.UrlImagen);
                    tbxPrecio.Text = articulo.Precio.ToString();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void tbxImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(tbxImagen.Text);
        }
        private void cargarImagen(string imagen)
        {
            try
            {
                pbImg.Load(imagen);
            }
            catch (Exception)
            {

                pbImg.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR8TrfhVXyRtTKud_EqvLkVVInC76ZdnCz4OwTxOiyXyA&s");
            }
        }

        private void btnImagenLocal_Click(object sender, EventArgs e)
        {
            archivo = new OpenFileDialog();
            archivo.Filter = "jpg|*.jpg;|png|*.png";
            if (archivo.ShowDialog() == DialogResult.OK)
            {
                tbxImagen.Text = archivo.FileName;
                cargarImagen(archivo.FileName);
            }
        }
    }
}
