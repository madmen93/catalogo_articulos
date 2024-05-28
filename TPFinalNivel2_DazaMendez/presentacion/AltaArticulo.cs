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
        private bool validarAlta()
        {
            if(tbxCodigo.Text == "")
            {
                MessageBox.Show("Debe completar el código del artículo para agregar un nuevo artículo.");
                return true;
            }
            if(tbxNombre.Text == "")
            {
                MessageBox.Show("Debe completar el nombre del artículo para agregar un nuevo artículo.");
                return true;
            }
            if(cbMarca.SelectedIndex < 0)
            {
                MessageBox.Show("Debe escoger una marca para agregar el artículo nuevo.");
                return true;
            }
            if(cbCategoria.SelectedIndex < 0)
            {
                MessageBox.Show("Debe escoger una categoría para agregar el artículo nuevo.");
                return true;
            }
            if(tbxPrecio.Text == "")
            {
                MessageBox.Show("Debe completar el precio del artículo para agregar un nuevo artículo");
                return true;
            }
            if (!(soloNumeros(tbxPrecio.Text)))
            {
                MessageBox.Show("Solo se admiten números en precio.");
                return true;
            }
            return false;
        }
        private bool soloNumeros(string cadena)
        {
            decimal numero = 0;
            bool canConvert = decimal.TryParse(cadena, out numero);
            return true;
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
                if (validarAlta())
                    return;

                articulo.Codigo = tbxCodigo.Text;
                articulo.Nombre = tbxNombre.Text;
                articulo.Descripcion = tbxDescripcion.Text;
                articulo.Marca = (Marca)cbMarca.SelectedItem;
                articulo.Categoria = (Categoria)cbCategoria.SelectedItem;
                articulo.UrlImagen = tbxImagen.Text;
                articulo.Precio = decimal.Parse(tbxPrecio.Text);

                if (archivo != null && !(tbxImagen.Text.ToUpper().Contains("HTTP")))
                     File.Copy(archivo.FileName, ConfigurationManager.AppSettings["images-articulos"] + archivo.SafeFileName);
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

                Close();
            }
            catch (System.Data.SqlClient.SqlException)
            {
                MessageBox.Show("Error en la longitud de los datos ingresados: La 'Descripción' puede contener hasta 150 caracteres y 'Código' y 'Nombre' hasta 50 caracteres.");
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("El archivo seleccionado ya existe.");
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
                cbMarca.SelectedItem = null;
                cbCategoria.DataSource = cateNegocio.listar();
                cbCategoria.ValueMember = "Id";
                cbCategoria.DisplayMember = "Descripcion";
                cbCategoria.SelectedItem = null;

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
