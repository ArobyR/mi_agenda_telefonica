using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace my_agenda
{
    public partial class Principal : Form
    {
        private int id;
        Agenda age = new Agenda();
        DataTable dt;
        
        public Principal()
        {
            InitializeComponent();
            restablecerControles();
            Consultar();
            dgvAgenda.Columns["id"].Visible = false;
        }

        private void Consultar()
        {
            dt = age.consultar();
            dgvAgenda.DataSource = dt; 
        }

        private void obtenerId()
        {
            id = Convert.ToInt32(dgvAgenda.CurrentRow.Cells["Id"].Value);

        }

        private void obtenerDatos()
        {
            obtenerId();
            //This gives me an error and I don't know why
            try
            {
                txtNombre.Text = dgvAgenda.CurrentRow.Cells["Nombre"].Value.ToString();
                txtTelefono.Text = dgvAgenda.CurrentRow.Cells["Telefono"].Value.ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + " " + e.StackTrace);
            }

        }
        private void restablecerControles()
        {
            this.txtNombre.Clear();
            this.txtTelefono.Clear();
            this.txtFiltrar.Clear();
            this.btnEliminar.Enabled = false;
            this.btnModificar.Enabled = false;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Campo vacio");
                return;
            }
            
            bool rs = age.insertar(txtNombre.Text, txtTelefono.Text);
            if (rs)
            {
                MessageBox.Show("Registro insertado correctamente");
                restablecerControles();
                Consultar();
            }

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Campo vacio");
                return;
            }
            bool rs = age.actualizar(id, txtNombre.Text, txtTelefono.Text);
            if (rs)
            {
                MessageBox.Show("Registro actualizado correctamente");
                restablecerControles();
                Consultar();
            }
            else
            {
                MessageBox.Show("No se ha actualizado correctamente");
            }
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Eliminar", 
                "¿Esta seguro que desea eliminar este registro?", 
                MessageBoxButtons.OKCancel, 
                MessageBoxIcon.Exclamation);

            if (r == DialogResult.OK)
            {
                bool rs = age.eliminar(id);
                if (rs)
                {
                    MessageBox.Show("Registro eliminado correctamente");
                    restablecerControles();
                    Consultar();
                }
                else
                {
                    MessageBox.Show("Algo ha salido mal, no se ha podido eliminar");
                }
                
            }
        
        }

        private void txtFiltrar_TextChanged(object sender, EventArgs e)
        {
            
            // and this... Error
            dt.DefaultView.RowFilter = $"nombre LIKE '%{txtFiltrar.Text}%' OR telefono LIKE '%{txtFiltrar.Text}%'";

        }

        private void dgvAgenda_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            restablecerControles();
            obtenerId();
            this.btnEliminar.Enabled = true;
        }

        private void dgvAgenda_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            obtenerDatos();
            this.btnEliminar.Enabled = false;
            this.btnModificar.Enabled = true;
        }
    }
}
