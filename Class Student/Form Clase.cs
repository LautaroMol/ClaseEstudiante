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
using CapaNegocios;
using System.Drawing.Printing;
using System.Drawing;



namespace Class_Student
{
    public partial class Form_Clase : Form
    {
        List<Estudiante> listaest = new List<Estudiante>();
        NegEstudiantes objNegEstudiantes = new NegEstudiantes();
        
        public Form_Clase()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Add("DNI", "DNI");
            dataGridView1.Columns.Add("Nombre", "Nombre");
            dataGridView1.Columns.Add("Apellido", "Apellido");
            dataGridView1.Columns.Add("Edad", "Edad");
            dataGridView1.Columns.Add("Asistencia", "Asistencia");
			//Llenamos el data grid
			

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
                        dataGridView1.Rows.Add(txtdni.Text,txtnom.Text,txtap.Text,checkBox1.Checked,estudiante.getEd());
                        MessageBox.Show("estudiante guardado");
                    }
                    
                    
                }
            }  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtbusc.Text == "") { MessageBox.Show("ingrese primero un dni a buscar"); }
            else {
                lblmost.Text = "Datos del Estudiante: \n";
				DataSet ds = new DataSet();
                ds = objNegEstudiantes.listadoEst("todos");
                DataRow estud = null;
				if (ds.Tables[0].Rows.Count > 0)
				{
					foreach (DataRow dr in ds.Tables[0].Rows)
					{
						if (dr[0].ToString() == txtbusc.Text)
						{
							estud = dr;
							break;
						}
					}
                    if (estud != null)
                    {
						lblmost.Text += "DNI: " +  estud[0].ToString() + "\n Nombre: " + estud[1].ToString() + "\n Apellido: " +
                            estud[2].ToString() + "\n Edad: " + estud[3].ToString() + "\n aistio?: " + estud[4].ToString();
					}
					else
					{
						MessageBox.Show("Estudiante no encontrado");
					}
				}
            }
        }
        private void LlenarDGV()
        {
            dataGridView1.Rows.Clear();
            DataSet ds = new DataSet();
            ds = objNegEstudiantes.listadoEst("todos");
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dataGridView1.Rows.Add(dr[0].ToString(), dr[1], dr[2].ToString(), dr[3].ToString(), dr[4].ToString());
                }
            }
            else MessageBox.Show("no hay alumnos cargados");
		}

        private void btnCargar_Click(object sender, EventArgs e)
        {
			lblmost.Text = "Datos del Estudiante: \n";
			DateTime Fecnac = dateTimePicker1.Value;
            DateTime Fecha = DateTime.Now;
            if (Fecnac > Fecha)
            {
                MessageBox.Show("la fecha de nacimiento es mayor a la actual");
            }
            else
            {
                if (txtdni.Text != "" && txtap.Text != "" && txtnom.Text != "")
                {
                    DataSet ds = new DataSet();
                    ds = objNegEstudiantes.listadoEst("todos");
                    DataRow estudianteEncontrado = null;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            if (dr[0].ToString() == txtdni.Text)
                            {
                                estudianteEncontrado = dr;
                                break;
                            }
                        }
                    }
                    if (estudianteEncontrado != null)
                        {
                            MessageBox.Show("el estudiante ya existe con ese dni");
                        }
                    else
                        {
                            int nGrabados = -1;
                            Estudiante NuevoEst = new Estudiante(txtdni.Text, txtnom.Text, txtap.Text, dateTimePicker1.Value, checkBox1.Checked);
                            nGrabados = objNegEstudiantes.abmEstudiantes("Alta", NuevoEst);
                            if (nGrabados == -1)
                            {
                                MessageBox.Show("No se pudo grabar el prod en el sistema");
                            }
                            else
                            {
                                lblmost.Text += NuevoEst.DarDatos();
                            }
                            MessageBox.Show("estudiante guardado");
                        }
                    }
                }
            }

		private void button3_Click(object sender, EventArgs e)
		{
            LlenarDGV();
		}

		private void btnborrar_Click(object sender, EventArgs e)
		{
            DataRow estudiant = null;
			DataSet ds = objNegEstudiantes.listadoEst("todos");
			foreach (DataRow dr in ds.Tables[0].Rows) 
            {
                if (dr[0].ToString() == txtbusc.Text) 
                {
                    estudiant = dr;
                    break;
				}
            }
            if (estudiant != null)
            {
                Estudiante nuevoestud = new Estudiante(estudiant[0].ToString(), estudiant[1].ToString(), estudiant[2].ToString(), int.Parse(estudiant[3].ToString()), bool.Parse(estudiant[4].ToString()));
                int est = objNegEstudiantes.abmEstudiantes("Borrar", nuevoestud);
                MessageBox.Show("estudiante borrado");
            }
            else { MessageBox.Show("estudiante borrado"); }
		}

		private void btnAct_Click(object sender, EventArgs e)
		{
			bool band = false;
			DataSet ds = objNegEstudiantes.listadoEst("todos");
			Estudiante estudMod = new Estudiante();
            if (txtbusc.Text != "" && txtap.Text != "" && txtnom.Text != "" && txtdni.Text =="") 
            {
                estudMod = new Estudiante(txtbusc.Text, txtnom.Text, txtap.Text, dateTimePicker1.Value, checkBox1.Checked);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr[0].ToString() == txtbusc.Text)
                    {
                        int est = objNegEstudiantes.abmEstudiantes("Modificar", estudMod);
                        MessageBox.Show("Estudiante modificado");
                        band = true;
                    }
                }
				if (band == false) { MessageBox.Show("no se encontro el estudiante"); }
			}
            else { MessageBox.Show("llene todos los campos excepto el de dni"); }
		}

		private void btnImprimir_Click(object sender, EventArgs e)
		{
            PrintDocument doc = new PrintDocument();
            doc.DefaultPageSettings.Landscape = true;
            doc.PrinterSettings.PrinterName = "Microsoft Print to PDF";

            PrintPreviewDialog ppd = new PrintPreviewDialog { Document = doc};
            ((Form)ppd).WindowState = FormWindowState.Maximized;
            doc.PrintPage += delegate (object ev, PrintPageEventArgs ep)
            {
                const int DGV_ALTO = 35;
                int left = ep.MarginBounds.Left, top = ep.MarginBounds.Top;

                foreach (DataGridViewColumn col in dataGridView1.Columns) 
                {
                    ep.Graphics.DrawString(col.HeaderText, new Font("Sego UI",16 ,FontStyle.Bold),
                    Brushes.DeepSkyBlue, left,top);
                    left += col.Width;
                }
                left = ep.MarginBounds.Left;
                ep.Graphics.FillRectangle(Brushes.Black, left, top+40, ep.MarginBounds.Right-left,3);
                top += 43;
                foreach(DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Index == dataGridView1.RowCount - 1) break;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        ep.Graphics.DrawString(Convert.ToString(cell.Value), new Font("Segoe UI", 13),
                        Brushes.Black, left, top + 4);
                        left += cell.OwningColumn.Width;
                    }
                    top += DGV_ALTO;
                }
            };
            ppd.ShowDialog();
		}
	}
}
