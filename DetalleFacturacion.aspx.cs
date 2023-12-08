using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DetalleFacturacion2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        cUsuario u = (cUsuario)Session["cUsuario"];
        if (u == null)
        {
            Response.Redirect("login.aspx");
        }

        if (!Page.IsPostBack)
        {
            CargarDatos();
        }
    }

    private void CargarDatos()
    {
        StringBuilder sb = new StringBuilder();
        string msg = "";
        cVar.cnnComercializadora = System.Configuration.ConfigurationManager.ConnectionStrings["ComercializadoraConnectionString"].ToString();
        cSql sql = new cSql();
        sql.conectar(cVar.cnnComercializadora);
        string tabla = sql.consultaDato("htmlremision", "datosPemex", "folio='" + Session["Folio"].ToString().Trim() + "' and destino='" + Session["Destino"].ToString().Trim() + "' and centro='" + Session["Centro"].ToString().Trim() + "'", out msg);
        string remision = sql.consultaDato("remision", "datosPemex", "folio='" + Session["Folio"].ToString().Trim() + "' and destino='" + Session["Destino"].ToString().Trim() + "' and centro='" + Session["Centro"].ToString().Trim() + "'", out msg);
        tabla = tabla.Replace("Fecha de elaboraci?n", "Fecha de elaboración");
        tabla = tabla.Replace("N?mero de remisi?n", "Número de remisión");
        tabla = tabla.Replace("N?mero de factura", "Número de factura");
        tabla = tabla.Replace("Clave de veh?culo", "Clave de vehículo");
        sb.Append(tabla);
        sb.Append("<br/>");
        sb.Append("<br/>");
        sb.Append("<a class='breadcrumb-item' target='blank' href = 'https://www.comercialrefinacion.pemex.com/portal/scfai001/controlador?Destino=GeneraCompRepFactPDF&icto=" +
            remision.Split('-')[0] + "&fprueba_lab=" + remision.Split('-')[2] + "&iprueba_lab=1'>Informe de Calidad de Producto</a>");
        divLista.InnerHtml = sb.ToString();
    }
}