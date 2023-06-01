using Entidades;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Class_Student
{
    public partial class Form_Clase : Form
    {
        List<Estudiante> listaest = new List<Estudiante>();
        
        public Form_Clase()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Add("DNI", "DNI");
            dataGridView1.Columns.Add("Nombre", "Nombre");
            dataGridView1.Columns.Add("Apellido", "Apellido");
            dataGridView1.Columns.Add("Edad", "Edad");
            dataGridView1.Columns.Add("Asistencia", "Asistencia");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime Fecnac = dateTimePicker1.Value;
            DateTime Fecha = DateTime.Now;
            if (Fecnac > Fecha)
            {
                MessageBox.Show("la fecha de nacimiento es mayor a la actual");
            }
            else {
                if (txtdni.Text != "" && txtap.Text != "" && txtnom.Text != "")
                {
                    
                    Estudiante estudianteEncontrado = null;
                    foreach (Estudiante estudiante in listaest)
                    {
                        if (estudiante.getDni() == txtdni.Text)
                        {
                            estudianteEncontrado = estudiante;
                            break;
                        }
                    }
                    if (estudianteEncontrado != null)
                    {
                        MessageBox.Show("el estudiante ya existe con ese dni");
                    }
                    else
                    {
                        Estudiante estudiante = new Estudiante(txtdni.Text.ToString(), txtnom.Text.ToString(), txtap.Text.ToString(), dateTimePicker1.Value, checkBox1.Checked);
                        listaest.Add(estudiante);
                        dataGridView1.Rows.Add(txtdni.Text,txtnom.Text,txtap.Text,checkBox1.Checked,estudiante.getEdad());
                        MessageBox.Show("estudiante guardado");
                    }
                    
                    
                }
            }  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtbusc.Text == "") { MessageBox.Show("ingrese primero un dni a buscar"); }
            else {
                lblmost.Text = "Datos del estudiante: \n";
                Estudiante estudianteEncontrado = null;
                foreach (Estudiante estudiante in listaest)
                {
                    if (estudiante.getDni() == txtbusc.Text)
                    {
                        estudianteEncontrado = estudiante;
                        break;
                    }
                }
                if (estudianteEncontrado != null)
                {
                    lblmost.Text += estudianteEncontrado.DarDatos();
                }
                else
                {
                    MessageBox.Show("Estudiante no encontrado");
                }
            }
        }

    }
}
