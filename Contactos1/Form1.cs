using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Contactos1
{
    public partial class Form1 : Form

    {
        private List<Contact> contacts;
        public Form1()
        {
            InitializeComponent();
            contacts = new List<Contact>();
            btnInsertar.Click -= btnInsertar_Click;
            btnInsertar.Click += btnInsertar_Click;
            btnBuscar.Click -= btnBuscar_Click;
            btnBuscar.Click += btnBuscar_Click;
            btnActualizar.Click-= btnActualizar_Click;
            btnActualizar.Click += btnActualizar_Click;
            btnEliminar.Click -= btnEliminar_Click;
            btnEliminar.Click += btnEliminar_Click;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTelefono_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string phone = txtTelefono.Text;
            string name = txtNombre.Text;

            // Si el teléfono no está vacío, intenta buscar por teléfono
            if (!string.IsNullOrEmpty(phone))
            {
                if (!checkPhone(phone))
                {
                    MessageBox.Show("Introduce un número de teléfono válido antes de buscar.");
                    return;
                }

                var contact = contacts.FirstOrDefault(c => c.Phone == phone);

                if (contact != null)
                {
                    txtNombre.Text = contact.Name;
                    MessageBox.Show($"Se encontró el siguiente contacto:\nNombre: {contact.Name}\nTeléfono: {contact.Phone}");
                }
                else
                {
                    MessageBox.Show($"No se encontró ningún contacto con el número de teléfono {phone}.");
                }
            }
            // Si el teléfono está vacío, intenta buscar por nombre
            else if (!string.IsNullOrEmpty(name))
            {
                var contact = contacts.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

                if (contact != null)
                {
                    txtNombre.Text = contact.Name;
                    txtTelefono.Text = contact.Phone;
                    MessageBox.Show($"Se encontró el siguiente contacto:\nNombre: {contact.Name}\nTeléfono: {contact.Phone}");
                }
                else
                {
                    MessageBox.Show($"No se encontró ningún contacto con el nombre {name}.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, introduce un número de teléfono o un nombre para buscar.");
            }
        }


        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (listViewContactos.SelectedItems.Count == 0)
            {
                MessageBox.Show("Seleccione un contacto para actualizar");
                return;
            }

            ListViewItem selectedItem = listViewContactos.SelectedItems[0];
            string currentName = selectedItem.Text;
            string currentPhone = selectedItem.SubItems[1].Text;
            string newName = txtNombre.Text;
            string newPhone = txtTelefono.Text;

            if (!checkPhone(newPhone))
            {
                return;
            }

            int index = contacts.FindIndex(c => c.Name == currentName && c.Phone == currentPhone);

            if (index != -1)
            {
                contacts[index].Name = newName;
                contacts[index].Phone = newPhone;

                selectedItem.Text = newName;
                selectedItem.SubItems[1].Text = newPhone;

                MessageBox.Show("Contacto actualizado correctamente");
            }
            else
            {
                MessageBox.Show("No se encontró el contacto");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string phone = txtTelefono.Text;
            if (!checkPhone(phone))
            {
                return;
            }

            var contact = contacts.Find(c => c.Phone == phone);
            if (contact != null)
            {
                contacts.Remove(contact);
                foreach (ListViewItem item in listViewContactos.Items)
                {
                    if (item.SubItems[1].Text == phone)
                    {
                        listViewContactos.Items.Remove(item);
                        break;
                    }
                }
                MessageBox.Show("Contacto eliminado correctamente");
            }
            else
            {
                MessageBox.Show("No se encontró ningún contacto con esos datos");
            }
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            string name = txtNombre.Text;
            string phone = txtTelefono.Text;

            if (string.IsNullOrEmpty(name) || !checkPhone(phone))
            {
                return;
            }

            // Validar si el contacto ya existe
            if (contacts.Any(c => c.Phone == phone))
            {
                MessageBox.Show("El contacto con este número de teléfono ya existe.");
                return;
            }

            Contact contact = new Contact(name, phone);
            contacts.Add(contact);

            ListViewItem item = new ListViewItem(contact.Name);
            item.SubItems.Add(contact.Phone);
            listViewContactos.Items.Add(item);

            MessageBox.Show("Contacto añadido correctamente");
        }

        private void listViewContactos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private bool checkPhone(string phone)
        {
            if (phone == null || phone.Equals(""))
            {
                MessageBox.Show("ERROR: el número de teléfono no puede estar vacío.");
                return false;
            }
            if (phone.Length != 11)
            {
                MessageBox.Show("ERROR: el número de teléfono debe tener 11 dígitos.");
                return false;
            }
            for (int i = 0; i < phone.Length; i++)
            {
                if (phone[i] < '0' || phone[i] > '9')
                {
                    MessageBox.Show("ERROR: el número de teléfono debe tener solo números.");
                    return false;
                }
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtNombre.Text = string.Empty;
            txtTelefono.Text = string.Empty;

            // Limpiar la selección del ListView
            listViewContactos.SelectedItems.Clear();

            MessageBox.Show("Los campos han sido limpiados.");
        }
    }
}
