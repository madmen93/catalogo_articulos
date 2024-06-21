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

namespace presentacion
{
    public partial class frmArticulos : Form
    {
        private List<Articulo> listaArticulo;
        public frmArticulos()
        {
            InitializeComponent();
        }

        private void frmArticulos_Load(object sender, EventArgs e)
        {
            
            cargar();
            cbCampo.Items.Add("Código");
            cbCampo.Items.Add("Nombre");
            cbCampo.Items.Add("Descripción");
            cbCampo.Items.Add("Precio");
            cbTipo.Items.Add("Marca");
            cbTipo.Items.Add("Categoría");
            
        }
        private void cargar()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                listaArticulo = negocio.listar();
                dgvArticulos.DataSource = listaArticulo;
                ocultarColumnas();
                cargarImagen(listaArticulo[0].UrlImagen);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private void ocultarColumnas()
        {
            dgvArticulos.Columns["UrlImagen"].Visible = false;
            dgvArticulos.Columns["Id"].Visible = false;
        }
        private void cargarImagen(string imagen)
        {
            try
            {
                pbxUrlImagen.Load(imagen);
            }
            catch (Exception)
            {

                pbxUrlImagen.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR8TrfhVXyRtTKud_EqvLkVVInC76ZdnCz4OwTxOiyXyA&s");
            }
        }
        private void dgvArticulos_SelectionChanged_1(object sender, EventArgs e)
        {
            if(dgvArticulos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.UrlImagen);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaArticulo alta = new frmAltaArticulo();
            alta.ShowDialog();
            cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            frmAltaArticulo modificar = new frmAltaArticulo(seleccionado);
            modificar.ShowDialog();
            cargar();
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("¿Está seguro de eliminar este artículo?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(respuesta == DialogResult.Yes)
                {
                    seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    negocio.eliminar(seleccionado.Id);
                    cargar();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnDetalles_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            frmDetalleArticulo detalle = new frmDetalleArticulo(seleccionado);
            detalle.ShowDialog();
            cargar();
        }

        private void tbxBusquedaRapida_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada;
            string filtro = tbxBusquedaRapida.Text;
            if(filtro.Length >= 3)
            {
                listaFiltrada = listaArticulo.FindAll(x => x.Nombre.ToLower().Contains(filtro.ToLower()) || x.Marca.Descripcion.ToLower().Contains(filtro.ToLower()));
            }
            else
            {
                listaFiltrada = listaArticulo;
            }
            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = listaFiltrada;
            ocultarColumnas();
        }

        private void cbCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cbCampo.SelectedItem.ToString();
            if(opcion == "Precio")
            {
                cbCriterio.Items.Clear();
                cbCriterio.Items.Add("Mayor a");
                cbCriterio.Items.Add("Menor a");
                cbCriterio.Items.Add("Igual a");
            }
            else
            {
                cbCriterio.Items.Clear();
                cbCriterio.Items.Add("Comienza con");
                cbCriterio.Items.Add("Termina con");
                cbCriterio.Items.Add("Contiene");
            }
        }
        private void cbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            string opcion = cbTipo.SelectedItem.ToString();
            try
            {
                if (opcion == "Marca")
                {
                    cbSubtipo.DataSource = marcaNegocio.listar();
                    cbSubtipo.ValueMember = "Id";
                    cbSubtipo.DisplayMember = "Descripcion";
                    cbSubtipo.SelectedItem = null;
                }else
                {
                    cbSubtipo.DataSource = categoriaNegocio.listar();
                    cbSubtipo.ValueMember = "Id";
                    cbSubtipo.DisplayMember = "Descripcion";
                    cbSubtipo.SelectedItem = null;
                }
                
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private bool validarFiltro()
        {
            if(cbTipo.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, escoja un tipo para filtrar.");
                return true;
            }
            if(cbSubtipo.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, escoja un subtipo para filtrar.");
                return true;
            }
            if(cbCampo.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, escoja un campo para filtrar");
                return true;
            }
            if(cbCriterio.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, escoja un criterio para filtrar");
                return true;
            }
            if(cbCampo.SelectedItem.ToString() == "Precio")
            {
                if (string.IsNullOrEmpty(tbxFiltroAvanzado.Text))
                {
                    MessageBox.Show("Debe completar el criterio del precio para filtrar.");
                    return true;
                }
                if (!(soloNumeros(tbxFiltroAvanzado.Text)))
                {
                    MessageBox.Show("Solo se admiten números para este criterio.");
                    return true;
                }
            }
            return false;
        }
        private bool soloNumeros(string cadena)
        {
            decimal numero = 0;
            bool canConvert = decimal.TryParse(cadena, out numero);
            if (canConvert) return true;
            else return false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                if (validarFiltro())
                    return;

                string tipo = cbTipo.SelectedItem.ToString();
                string subtipo = cbSubtipo.SelectedItem.ToString();
                string campo = cbCampo.SelectedItem.ToString();
                string criterio = cbCriterio.SelectedItem.ToString();
                string filtro = tbxFiltroAvanzado.Text;
                dgvArticulos.DataSource = negocio.filtrar(campo, criterio, filtro, tipo, subtipo);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            cargar();
        }
    }
}
