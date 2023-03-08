using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using api.datecsa.UnityAPI;
using api.datecsa.controlador;

namespace api.datecsa.UI
{
    public partial class FrmAppDatecsa : Form
    {
        private enum ActiveFiel 
        {
            Enable,
            Disable
        }

        public FrmAppDatecsa()
        {
            InitializeComponent();
        }

        #region ObtenerDatos
        private void BtnCargarDatos_Click(object sender, EventArgs e)
        {
            try 
            {
                bool radAgregar = RadAgregar.Checked;
                string TipoCarga = string.Empty;
                if (radAgregar) 
                {
                    TipoCarga = "Agregar";
                }
                else
                {
                    TipoCarga = "Eliminar";
                }

                controladorAdminSK Cn = new controladorAdminSK(TxtUsuario.Text,TxtPasswordUsu.Text,TxtUrlAppServer.Text,TxtDataSource.Text);
                MessageBox.Show("Conexion Exitosa");

                bool confirmacion = Cn.Ingresar(radAgregar);

                if (confirmacion) 
                {
                    MessageBox.Show("Proceso de " + TipoCarga + " SK Exitoso");
                }
                else 
                {
                    MessageBox.Show("Error en el Proceso de " + TipoCarga + " SK");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Conexion Fallida " + ex);
            }

            
        }
        #endregion
    }
}
