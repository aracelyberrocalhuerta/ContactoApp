using ContactoApp.model;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ContactoApp
{


    public partial class Pantalla1 : Form
    {
        /* Data variables */

        string nombre;
        string apellido;
        int telefono;
        string correo;

        /* Data model */
        contacto modelo = new contacto();
        public Pantalla1()
        {
            InitializeComponent();

        }

        private void Pantalla1_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'contactsAppDataSet.contacto' Puede moverla o quitarla según sea necesario.
            this.contactoTableAdapter.Fill(this.contactsAppDataSet.contacto);

        }

        public void btnAdd_Click(object sender, EventArgs e)
        {
            if(tbName.Text==""|| tbLastname.Text== "" ||tbPhone.Text=="" || tbEmail.Text=="")
            {
                MessageBox.Show("Por favor introduce todos los campos");

                
            }
            else
            {
                if (!(tbPhone.Text.All(char.IsDigit)))
                {
                    MessageBox.Show("El campo de telefono solo puede estar compuesto por números");
                }
                else 
                {
                    nombre = tbName.Text.Trim();
                    apellido = tbLastname.Text.Trim();
                    telefono = int.Parse(tbPhone.Text.Trim());
                    correo = tbEmail.Text.Trim();

                    try
                    {
                        modelo.nombre = nombre;
                        modelo.apellido = apellido;
                        modelo.telefono = telefono;
                        modelo.correo = correo;

                        using (ContactsAppEntities db = new ContactsAppEntities())
                        {
                            db.contacto.Add(modelo);
                            db.SaveChanges();
                        }

                        Clear();
                        actualizarTabla();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                
            }

        }

        private void Clear()
        {
            tbName.Text = tbLastname.Text = tbPhone.Text = tbEmail.Text = string.Empty;
        }

        void actualizarTabla()
        {
            using (ContactsAppEntities db = new ContactsAppEntities())
            {
                dataGridView1.DataSource = db.contacto.ToList<contacto>();
            }
        }

    }
}
