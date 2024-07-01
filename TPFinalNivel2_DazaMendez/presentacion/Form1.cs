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
                cbTipo.SelectedItem = null;
                cbSubtipo.SelectedItem = null;
                cbCampo.SelectedItem = null;
                cbCriterio.SelectedItem = null;
                tbxFiltroAvanzado.Text = "";

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
            dgvArticulos.Columns["Descripcion"].Visible = false;
            dgvArticulos.Columns["Codigo"].Visible = false;
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
        private bool validarSeleccion()
        {
            if(dgvArticulos.CurrentRow == null)
            {
                MessageBox.Show("No ha seleccionado ningún artículo.");
                return true;
            }
            else
            {
                return false;
            }
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
                Articulo seleccionado;
                if(validarSeleccion())
                    return;
                seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    frmAltaArticulo modificar = new frmAltaArticulo(seleccionado);
                    modificar.ShowDialog();
                    cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            if (validarSeleccion())
                return;
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
            if (validarSeleccion())
                return;
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
            string opcion = null;
            if(cbCampo.SelectedItem != null)
                opcion = cbCampo.SelectedItem.ToString();
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
            string opcion = null;
            if(cbTipo.SelectedItem != null)
                opcion = cbTipo.SelectedItem.ToString();
            try
            {
                if(opcion != null)
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
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private bool validarFiltroTipo()
        {
            if(cbTipo.SelectedItem != null && cbSubtipo.SelectedItem != null && cbCampo.SelectedItem == null && cbCriterio.SelectedItem == null && tbxFiltroAvanzado.Text == "")
            {
                return false;
            }else if(cbTipo.SelectedItem != null && cbSubtipo.SelectedItem == null && cbCampo.SelectedItem == null && cbCriterio.SelectedItem == null && tbxFiltroAvanzado.Text == "")
            {
                MessageBox.Show("Debe escoger al menos un Subtipo para realizar una búsqueda.");
                return true;
            }
            else
            {
                return true;
            }
        }
        private bool validarFiltroCampo()
        {
            if(cbTipo.SelectedItem == null && cbSubtipo.SelectedItem == null && cbCampo.SelectedItem != null && cbCriterio.SelectedItem != null /*&& tbxFiltroAvanzado.Text != ""*/)
            {
                if (cbCampo.SelectedItem.ToString() == "Precio")
                {
                    if (validarPrecio())
                    {
                        return true;
                    }
                }
                return false;
            }else if(cbTipo.SelectedItem == null && cbSubtipo.SelectedItem == null && cbCampo.SelectedItem != null && cbCriterio.SelectedItem == null)
            {
                MessageBox.Show("Debe escoger al menos el Criterio para realizar la búsqueda.");
                return true;
            }
            else
            {
                return true;
            }
        }
        private bool validarFiltroCompleto()
        {
            if(cbTipo.SelectedItem != null && cbSubtipo.SelectedItem != null && cbCampo.SelectedItem != null && cbCriterio.SelectedItem != null)
            {
                if (cbCampo.SelectedItem.ToString() == "Precio")
                {
                    if (validarPrecio())
                    {
                        return true;
                    }
                }
                return false;
            }else if (cbTipo.SelectedItem == null && cbSubtipo.SelectedItem == null && cbCampo.SelectedItem == null && cbCriterio.SelectedItem == null)
            {
                MessageBox.Show("Asegúrese de completar correctamente el filtro para continuar con la búsqueda.");
                return true;
            }
            return true;
        }
        private bool validarPrecio()
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
            else { return false; }
                
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
                string tipo = null;
                string subtipo = null;
                string campo = null;
                string criterio = null;
                string filtro = null;

                if (!validarFiltroTipo())
                {
                    tipo = cbTipo.SelectedItem.ToString();
                    subtipo = cbSubtipo.SelectedItem.ToString();
                    dgvArticulos.DataSource = negocio.filtrarTipoSubtipo(tipo, subtipo);
                    return;
                }else if (!validarFiltroCampo())
                {
                    campo = cbCampo.SelectedItem.ToString();
                    criterio = cbCriterio.SelectedItem.ToString();
                    filtro = tbxFiltroAvanzado.Text;
                    dgvArticulos.DataSource = negocio.filtrarCampoCriterio(campo, criterio, filtro);
                    return;
                }else if (!validarFiltroCompleto())
                {
                    tipo = cbTipo.SelectedItem.ToString();
                    subtipo = cbSubtipo.SelectedItem.ToString();
                    campo = cbCampo.SelectedItem.ToString();
                    criterio = cbCriterio.SelectedItem.ToString();
                    filtro = tbxFiltroAvanzado.Text;
                    dgvArticulos.DataSource = negocio.filtrar(campo, criterio, filtro, tipo, subtipo);
                    return;
                }
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
