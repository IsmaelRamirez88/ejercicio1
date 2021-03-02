using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using controlador; 

namespace ejercicioNum1
{
    public partial class Form1 : Form
    {
        controlador.controlador Control;
        double IVA = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Control = new controlador.controlador();
            LlenarIva();
            LlenarProducto(); 
        }

        private void LlenarIva()
        {
            DataTable TempIva = new DataTable(); 
            try
            {
                TempIva = Control.ContieneIVA();
                if (TempIva.Rows.Count > 0)
                {
                    txtIva.Text = TempIva.Rows[0]["iva"].ToString();
                    IVA = Convert.ToDouble(TempIva.Rows[0]["iva"].ToString());
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void ModificarIVA()
        {
            int Resultado = 0;
            try
            {
                Resultado = Control.EditarIVA(Convert.ToDouble(txtIva.Text));
            }
            catch (Exception ex)
            {
            }
        }

        private void LlenarProducto()
        {
            DataTable TempProducto = new DataTable();
            try
            {
                dgvProductos.DataSource = null;
                cmbProducto.DataSource = null;
                TempProducto = Control.ContieneProducto();
                if (TempProducto.Rows.Count > 0)
                {
                    dgvProductos.DataSource = TempProducto;
                    cmbProducto.DataSource = TempProducto.Copy();
                    cmbProducto.ValueMember = "id";
                    cmbProducto.DisplayMember = "descripcion";
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void LimpiarCamposProducto()
        {
            lblIdProducto.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
            btnAlta.Enabled = true;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;

        }

        private void AltaProducto()
        {
            int Resultado = 0;
            try
            {
                if(!string.IsNullOrEmpty(txtDescripcion.Text) && !string.IsNullOrEmpty(txtPrecio.Text))
                {
                    Resultado = Control.AltaProducto(txtDescripcion.Text, Convert.ToDouble(txtPrecio.Text));
                    if (Resultado > 0)
                    {
                        LlenarProducto();
                        LimpiarCamposProducto();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void ModificaProducto()
        {
            int Resultado = 0;
            try
            {
                if (!string.IsNullOrEmpty(lblIdProducto.Text)  && !string.IsNullOrEmpty(txtDescripcion.Text) && !string.IsNullOrEmpty(txtPrecio.Text))
                {
                    Resultado = Control.ModificarProducto(Convert.ToInt32(lblIdProducto.Text),txtDescripcion.Text, Convert.ToDouble(txtPrecio.Text));
                    LlenarProducto();
                    LimpiarCamposProducto();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void EliminaProducto()
        {
            int Resultado = 0;
            try
            {
                if (!string.IsNullOrEmpty(lblIdProducto.Text))
                {
                    Control.EliminarProducto(Convert.ToInt32(lblIdProducto.Text));
                    LlenarProducto();
                    LimpiarCamposProducto();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void Calcular(double Precio)
        {
            try
            {
                txtOpPrecio.Text = Precio.ToString();
                txtOpIVA.Text = (Precio * (IVA / 100)).ToString();
                txtOpTotal.Text = (Precio + (Precio * (IVA / 100))).ToString();
            }
            catch (Exception)
            {

            }
        }


        #region EVENTOS
        private void btnIva_Click(object sender, EventArgs e)
        {
            ModificarIVA();
            LlenarIva();
        }

        private void txtIva_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void dgvProductos_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    lblIdProducto.Text = dgvProductos["id", e.RowIndex].Value.ToString();
                    txtDescripcion.Text = dgvProductos["descripcion", e.RowIndex].Value.ToString();
                    txtPrecio.Text = dgvProductos["precio", e.RowIndex].Value.ToString();

                    btnAlta.Enabled = false;
                    btnModificar.Enabled = true;
                    btnEliminar.Enabled = true; 
                }
            }
            catch (Exception ex)
            {
            }

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCamposProducto();
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {
            AltaProducto();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            ModificaProducto();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminaProducto();
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        #endregion

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Control.CerrarConexion();
        }

        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbProducto.SelectedValue != null && !string.IsNullOrEmpty(cmbProducto.SelectedValue.ToString()))
                {
                    int i = 0;
                    bool EsNumero = int.TryParse(cmbProducto.SelectedValue.ToString(), out i);
                    if (EsNumero)
                    {
                        var Precio = dgvProductos.Rows
                            .Cast<DataGridViewRow>().Where(b => b.Cells["id"].Value.ToString() == cmbProducto.SelectedValue.ToString()).FirstOrDefault().Cells["precio"].Value;
                        if (Precio != null && !string.IsNullOrEmpty(Precio.ToString()))
                        {
                            Calcular(Convert.ToDouble(Precio));
                        }

                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
