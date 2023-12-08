using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        cUsuario u = (cUsuario)Session["cUsuario"];
        if (u == null)
        {
            Response.Redirect("login.aspx");
        }
        if (u.primerLogin == "1")
        {
            Response.Redirect("NuevoPassword.aspx");
        }
        if (!Page.IsPostBack)
        {
            if (u != null)
            {
                CargarMenus(u.esAdmin);
            }
            else
            {
                menuConfiguracion.Visible = false;
            }
        }
    }

    private void CargarMenus(int EsAdmin)
    {
        menuConfiguracion.Visible = false;
        menuReporte.Visible = false;
        if (EsAdmin == 1)
        {
            menuConfiguracion.Visible = true;
            menuReporte.Visible = true;
        }
    }

    protected void CerrarSesion_Click(object sender, EventArgs e)
    {
        Session["cUsuario"] = "";
        Response.Redirect("Login.aspx");
    }
}
