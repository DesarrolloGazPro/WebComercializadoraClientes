using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Destinos : System.Web.UI.Page
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
            if (u == null)
            {
                Response.Redirect("login.aspx");
            }

            cVar.cnnComercializadora = System.Configuration.ConfigurationManager.ConnectionStrings["ComercializadoraConnectionString"].ToString();
            Cargardatos();
        }
    }

    private void Cargardatos()
    {
        cUsuario u = (cUsuario)Session["cUsuario"];
        try
        {
            string msg = "";
            cVar.cnnComercializadora = System.Configuration.ConfigurationManager.ConnectionStrings["ComercializadoraConnectionString"].ToString();
            cSql sql = new cSql();
            sql.conectar(cVar.cnnComercializadora);
            DataSet ds = new DataSet();

            string query = "";

            query = "select * from destinos";


            ds = sql.consultaDataSetLibre(query, out msg);
            //ConvertDataTableToHTML(ds.Tables[0]);

            if (ds.Tables.Count > 0)
            {
                Session["tablas"] = ds;
                RadDestinos.DataSource = ds.Tables[0];
                RadDestinos.Rebind();
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void exportExcel_Click(object sender, EventArgs e)
    {

    }
}