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
            if (codigoValido() == false || nombreValido() == false || marcaValida() == false || categoriaValida() == false || precioValido() == false || soloNumeros(tbxPrecio.Text) == false || codigoExtension() == false ||nombreExtension() == false || descripcionExtension() == false)
            {
                MessageBox.Show("Asegúrese de completar correctamente los campos obligatorios.");
                return true;
            }
            return false;
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                if (articulo == null)
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
                codigoEP.SetIconAlignment(tbxCodigo, ErrorIconAlignment.MiddleRight);
                codigoEP.SetIconPadding(tbxCodigo, 2);
                nombreEP.SetIconAlignment(tbxNombre, ErrorIconAlignment.MiddleRight);
                nombreEP.SetIconPadding(tbxNombre, 2);
                descripcionEP.SetIconAlignment(tbxDescripcion, ErrorIconAlignment.TopRight);
                descripcionEP.SetIconPadding(tbxDescripcion, 2);
                marcaEP.SetIconAlignment(cbMarca, ErrorIconAlignment.MiddleRight);
                marcaEP.SetIconPadding(cbMarca, 2);
                categoriaEP.SetIconAlignment(cbCategoria, ErrorIconAlignment.MiddleRight);
                categoriaEP.SetIconPadding (cbCategoria, 2);
                precioEP.SetIconAlignment(tbxPrecio, ErrorIconAlignment.MiddleRight);
                precioEP.SetIconPadding(tbxPrecio, 2);

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
        //Eventos para la validación en el form:
        private void tbxCodigo_Validated(object sender, EventArgs e)
        {
            if (codigoValido())
            {
                codigoEP.SetError(this.tbxCodigo, String.Empty);
                if (!codigoExtension())
                    codigoEP.SetError(this.tbxCodigo, "El código solo acepta hasta 50 caracteres.");
            }
            else
                codigoEP.SetError(this.tbxCodigo, "El campo Código es obligatorio.");
        }

        private void tbxNombre_Validated(object sender, EventArgs e)
        {
            if (nombreValido())
            {
                nombreEP.SetError(this.tbxNombre, String.Empty);
                if (!nombreExtension())
                    nombreEP.SetError(this.tbxNombre, "El nombre solo acepta hasta 50 caracteres.");
            }
            else
                nombreEP.SetError(this.tbxNombre, "El campo Nombre es obligatorio.");
        }

        private void cbMarca_Validated(object sender, EventArgs e)
        {
            if (!marcaValida())
                marcaEP.SetError(this.cbMarca, "Debe seleccionar una marca.");
            else
                marcaEP.SetError(this.cbMarca, string.Empty);
        }

        private void cbCategoria_Validated(object sender, EventArgs e)
        {
            if (!categoriaValida())
                categoriaEP.SetError(this.cbCategoria, "Debe seleccionar una categoría.");
            else
                categoriaEP.SetError(this.cbCategoria, string.Empty);
        }

        private void tbxPrecio_Validated(object sender, EventArgs e)
        {
            if (precioValido())
            {
                precioEP.SetError(this.tbxPrecio, string.Empty);
                if (!soloNumeros(tbxPrecio.Text))
                    precioEP.SetError(this.tbxPrecio, "Este cammpo solo admite números.");
            }
            else
                precioEP.SetError(this.tbxPrecio, "El campo Precio es obligatorio.");
        }
        private void tbxDescripcion_Validated(object sender, EventArgs e)
        {
            if (!descripcionExtension())
                descripcionEP.SetError(this.tbxDescripcion, "El campo Descripción solo admite hasta 150 caracteres");
            else
                descripcionEP.SetError(this.tbxDescripcion, string.Empty);
        }
        //Métodos para la validación del alta:
        private bool codigoValido()
        {
            if (tbxCodigo.Text.Length > 0) { return true; }
            return false;
        }
        private bool codigoExtension()
        {
            if (tbxCodigo.Text.Length < 51) { return true; }
            return false;
        }
        private bool nombreExtension()
        {
            if (tbxNombre.Text.Length < 51) { return true; }
            return false;
        }
        private bool nombreValido()
        {
            if (tbxNombre.Text.Length > 0) { return true; }
            return false;
        }
        private bool descripcionExtension()
        {
            if(tbxDescripcion.Text.Length < 151) { return true; }
            return false;
        }
        private bool marcaValida()
        {
            if (cbMarca.SelectedItem != null) { return true; }
            return false;
        }
        private bool categoriaValida()
        {
            if (cbCategoria.SelectedItem != null) { return true; }
            return false;
        }
        private bool precioValido()
        {
            if (tbxPrecio.Text.Length > 0) { return true; }
            return false;
        }
        private bool soloNumeros(string cadena)
        {
            decimal numero = 0;
            bool canConvert = decimal.TryParse(cadena, out numero);
            if (canConvert) return true;
            else return false;
        }

        
    }
}
