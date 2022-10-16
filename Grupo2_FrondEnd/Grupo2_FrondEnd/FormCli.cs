﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Grupo2_FrondEnd.Entidades;
using Newtonsoft.Json;

namespace Grupo2_FrondEnd
{
    public partial class FormCli : Form
    {
        public FormCli()
        {
            InitializeComponent();
            this.ttMensaje.SetToolTip(this.btnBuscar, "Buscar");
            this.ttMensaje.SetToolTip(this.btnActuliazar, "Actualizar");
            this.ttMensaje.SetToolTip(this.btnEliminar, "Eliminar");
            this.ttMensaje.SetToolTip(this.btnGuardar, "Guardar");
            this.ttMensaje.SetToolTip(this.btnLimpiar, "Limpiar los campos");
        }
        
        public static bool ValidarEmail(string comprobarImail)
        {
            string emailFormato;
            emailFormato = "\\w+([-+.']w+)*@\\w+([.']\\w+)*\\.\\w+([.']\\w+)*";
            if (Regex.IsMatch(comprobarImail, emailFormato))
            {
                if (Regex.Replace(comprobarImail, emailFormato, string.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            try
            {
                Propiedades_Clientes objcliente = new Propiedades_Clientes();

                objcliente.NIT = txtNit.Text;
                objcliente.NIT = txtNombre.Text;
                objcliente.NIT = txtDireccion.Text;
                objcliente.NIT = txtCorreo.Text;
                objcliente.NIT = txtTelefono.Text;
                string respon = objcliente.PostClientes(objcliente);
                MessageBox.Show(respon);

            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrio un error");
            }
            

                //if (ValidarEmail(txtCorreo.Text) == false)
                //{
                //    DialogResult dialogResult = MessageBox.Show("Dirreccion de correo electronico invalida", "Sistema de facturación", MessageBoxButtons.OK);
                //}

            

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtTelefono.Clear();
            txtCorreo.Clear();
            txtDireccion.Clear();
            txtNit.Clear();
            txtNombre.Clear();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Propiedades_Clientes objcliente = new Propiedades_Clientes();

            objcliente.NIT = txtNit.Text;

            string RespuestaJson = objcliente.BuscarXNIT(objcliente);
            try
            {
                if (RespuestaJson == "null")
                {
                    MessageBox.Show("ERROR: no se encontro el articulo deseado");
                    txtNit.Clear();
                }
                else
                {
                    Propiedades_Clientes clie = JsonConvert.DeserializeObject<Propiedades_Clientes>(RespuestaJson);
                    txtNit.Text = clie.NIT;
                    txtNombre.Text = clie.nombreClient;
                    txtDireccion.Text = clie.direccion;
                    txtCorreo.Text = clie.gmail;
                    txtTelefono.Text = clie.numtelefono;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnActuliazar_Click(object sender, EventArgs e)
        {
            try
            {
                Propiedades_Clientes objcliente = new Propiedades_Clientes();

                objcliente.NIT = txtNit.Text;
                objcliente.nombreClient = txtNombre.Text;
                objcliente.direccion = txtDireccion.Text;
                objcliente.gmail = txtCorreo.Text;
                objcliente.numtelefono = txtTelefono.Text;
                string respon = objcliente.Actualizar(objcliente);
                MessageBox.Show(respon);

            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrio un error");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Esta seguro que desea eliminar este registro", "Sistema de facturación", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Propiedades_Clientes objcliente = new Propiedades_Clientes();

                objcliente.NIT = txtNit.Text;
                String Respuesta = objcliente.DELETE(objcliente);
                MessageBox.Show(Respuesta);
                

            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }
    }
}
