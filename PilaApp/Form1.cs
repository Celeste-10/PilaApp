using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PilaApp
{
    public partial class Form1 : Form
    {
        public class Empleado
        {
            //Se declara la clase Empleado para almacenar los datos
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public string Cargo { get; set; }
            public decimal Salario { get; set; }

            public string NombreCompleto => $"{Nombre} {Apellidos}";

            public override string ToString()
            {
                return $"{NombreCompleto}, {Cargo}, C${Salario:N2}";
                //Formateamos el salario en cordobas, con dos decimales 
            }
        }
        private Stack<Empleado> pila = new Stack<Empleado>(); //Declaramos la pila usando Stack
        public Form1()
        {
            InitializeComponent();

            btnAgregar.Click += btnAgregar_Click;
            btnEliminar.Click += btnEliminar_Click;
            btnVerTope.Click += btnVerTope_Click;
            //No me mostraba los empleados en la lista. Asi que agregamos esto.
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //Validamos que los campos no esten vacios al ingresar los datos
            if (string.IsNullOrWhiteSpace(tbNombre.Text) ||
                string.IsNullOrWhiteSpace(tbApellido.Text) ||
                string.IsNullOrWhiteSpace(tbCargo.Text) ||
                string.IsNullOrWhiteSpace(tbSalario.Text))
            {
                MessageBox.Show("Advertencia: Todos los campos son obligatorios.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Validamos que el salario sea un numero valido y enviamos una advertencia si no lo es
            if (!decimal.TryParse(tbSalario.Text, out decimal salario))
            {
                MessageBox.Show("Advertencia: El salario debe ser un número válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Creamos un nuevo empleado para seguir ingresando datos en la pila
            Empleado nuevoEmpleado = new Empleado
            {
                Nombre = tbNombre.Text.Trim(),
                Apellidos = tbApellido.Text.Trim(),
                Cargo = tbCargo.Text.Trim(),
                Salario = salario
            };

            pila.Push(nuevoEmpleado);
            tbNombre.Clear();
            tbApellido.Clear();
            tbCargo.Clear();
            tbSalario.Clear();
            //Agregamos a la pila usando Push y limpiamos los TxtBox despues de agregar

            ActualizarListBox();

            //Para cada procedimiento realizado se actualiza la lista y mostramos un mensaje emergente de confirmacion
            MessageBox.Show("Empleado agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Verificamos la pila usando Count para evitar errores
            if (pila.Count == 0)
            {
                MessageBox.Show("Advertencia: La pila está vacía.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Eliminamos el empleado usando Pop, se muestra el nombre completo en un mensaje emergente
            Empleado eliminado = pila.Pop();

            ActualizarListBox();

            MessageBox.Show($"Empleado eliminado: {eliminado.NombreCompleto}", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnVerTope_Click(object sender, EventArgs e)
        {
            if (pila.Count == 0)
            {
                MessageBox.Show("Advertencia: La pila está vacía.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Empleado tope = pila.Peek();

            MessageBox.Show($"Tope de la pila: {tope.NombreCompleto}", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //Utilizamos un metodo auxiliar para poder actualizar la lista
        private void ActualizarListBox()
        {
            lbPila.Items.Clear();
            Empleado[] array = pila.ToArray();
            for (int i = array.Length - 1; i >= 0; i--)
            {
                lbPila.Items.Add(array[i].ToString());
            }
        }
        //El metodo convierte la pila a un arreglo con "array" y usando "for" que lo recorre en orden inverso
    }

}

