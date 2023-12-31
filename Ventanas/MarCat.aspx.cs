﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;
using Servicios;

namespace Ventanas
{
    public partial class AdminMarCat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar que el usuario es admin
            if (Helper.esAdmin(Session["user"]))
            {
                MarcaNegocio mNegocio = new MarcaNegocio();
                CategoriaNegocio cNegocio = new CategoriaNegocio();
                // Cargar las grillas
                dgvMarcas.DataSource = mNegocio.listarMarcas();
                dgvMarcas.DataBind();
                dgvCategorias.DataSource = cNegocio.listarCategorias();
                dgvCategorias.DataBind();
            }
            else
            {
                Session.Add("error", "No tiene permiso para acceder a esta página");
                Response.Redirect("Error.aspx?code=02", false);
            }
        }

        protected void btnNuevaCategoria_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar que el campo esté cargado
                if (tbxNuevaCategoria.Text != "")
                {
                    CategoriaNegocio cNegocio = new CategoriaNegocio();
                    Categoria nueva = new Categoria();
                    // Lógica para cargar nueva categiróa
                    nueva.Descripcion = tbxNuevaCategoria.Text;
                    cNegocio.agregar(nueva);
                    tbxNuevaCategoria.Text = null;
                    tbxNuevaMarca.Text = null;
                    // Recargar página
                    Page_Load(sender, e);
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", Helper.mensajeError(ex));
                Response.Redirect("Error.aspx?code=00");
            }
        }

        protected void btnNuevaMarca_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar que el campo está cargado
                if (tbxNuevaMarca.Text != "")
                {
                    MarcaNegocio mNegocio = new MarcaNegocio();
                    Marca nueva = new Marca();
                    // Lógica para cargar marca
                    nueva.Descripcion = tbxNuevaMarca.Text;
                    mNegocio.agregar(nueva);
                    tbxNuevaCategoria.Text = null;
                    tbxNuevaMarca.Text = null;
                    // Recargar página
                    Page_Load(sender, e);
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", Helper.mensajeError(ex));
                Response.Redirect("Error.aspx?code=00");
            }
        }

        protected void dgvCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = int.Parse(dgvCategorias.SelectedDataKey.Value.ToString());
            Response.Redirect("AdminMarCat.aspx?id=" + id + "&tipo=categoria");
        }

        protected void dgvMarcas_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = int.Parse(dgvMarcas.SelectedDataKey.Value.ToString());
            Response.Redirect("AdminMarCat.aspx?id=" + id + "&tipo=marca");
        }

        protected void dgvCategorias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvCategorias.PageIndex = e.NewPageIndex;
            dgvCategorias.DataBind();
        }

        protected void dgvMarcas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvMarcas.PageIndex = e.NewPageIndex;
            dgvMarcas.DataBind();
        }
    }
}