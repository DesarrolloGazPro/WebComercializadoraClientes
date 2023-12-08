using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Usuarios : System.Web.UI.Page
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
            validarAccesos();
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
            if (txtBusqueda.Text == "")
                query = "select [ID],[Nombre],[Usuario],[Password],case when [EsAdmin]=1 then 'Si' else 'No' end EsAdmin,[Correo] from Usuarios u  order by id desc";
            else
                query = "select [ID],[Nombre],[Usuario],[Password],case when [EsAdmin]=1 then 'Si' else 'No' end EsAdmin ,[Correo] from Usuarios u " +
                        "WHERE ((u.[Nombre] LIKE '%" + txtBusqueda.Text.Trim() + "%') or (u.[correo] LIKE '%" + txtBusqueda.Text.Trim()
                        + "%') or (u.[usuario] LIKE '%" + txtBusqueda.Text.Trim() + "%')) order by id desc";

            ds = sql.consultaDataSetLibre(query, out msg);
            //ConvertDataTableToHTML(ds.Tables[0]);

            if (ds.Tables.Count > 0)
            {
                Session["tablas"] = ds;
                RadUsuarios.DataSource = ds.Tables[0];
                RadUsuarios.Rebind();
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void validarAccesos()
    {
        cSql sql = new cSql();
        sql.conectar(cVar.cnnComercializadora);
        DataSet ds = new DataSet();
        cUsuario u = (cUsuario)Session["cUsuario"];
        string msg = "";
        cAccesos acceso = cSeguridad.Accesos(u, "Configuracion - Usuarios", out msg);

        if (u == null) Response.Redirect("login.aspx");

        if (u.esAdmin == 0)
            Response.Redirect("NoAcceso.html");
    }

    protected void txtBusqueda_TextChanged(object sender, EventArgs e)
    {
        Cargardatos();
    }
    protected void BtnExportarXls_Click(object sender, EventArgs e)
    {
        DataSet tablas = Session["tablas"] as DataSet;

        if (tablas.Tables.Count > 0)
        {
            exportaraExcel(tablas, "xls");
            //Session["query"] = null;
        }
        else
        {
            Response.Write("<script language='javascript' type='text/javascript'>alert('No hay resultados para criterios selecionados');</script>");
        }

    }

    protected void BtnExportarCsv_Click(object sender, EventArgs e)
    {
        DataSet tablas = Session["tablas"] as DataSet;

        if (tablas.Tables.Count > 0)
        {
            exportaraExcel(tablas, "csv");
            //Session["query"] = null;
        }
        else
        {
            Response.Write("<script language='javascript' type='text/javascript'>alert('No hay resultados para criterios selecionados');</script>");
        }
    }

    private void exportaraExcel(DataSet tablas, string extension)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        ExcelPackage pck = new ExcelPackage();
        for (int n = 0; n <= tablas.Tables.Count - 1; n++)
        {
            DataTable tabla = tablas.Tables[n];
            var ws = pck.Workbook.Worksheets.Add("Reporte");

            for (int hc = 0; hc <= tabla.Columns.Count - 1; hc++)
            {
                ws.Cells[1, hc + 1].Value = tabla.Columns[hc].ColumnName;
                ws.Cells[1, hc + 1].Style.Font.Bold = true;
            }
            int x = 2, y = 1;
            for (int row = 0; row <= tabla.Rows.Count - 1; row++)
            {
                for (int cell = 0; cell <= tabla.Columns.Count - 1; cell++)
                {
                    //if (y == 1 || y == 2 || y == 6 || y == 7)
                    //    ws.Cells[x, y].Value = int.Parse(tabla.Rows[row][cell].ToString().Trim());
                    //else
                    ws.Cells[x, y].Value = tabla.Rows[row][cell].ToString().Trim();
                    y++;
                }
                y = 1;
                x++;
            }

            for (int hc = 0; hc <= tabla.Columns.Count - 1; hc++)
            {
                ws.Column(hc + 1).AutoFit();
            }
        }

        pck.SaveAs(Response.OutputStream);
        //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("content-disposition", "attachment;  filename=Reporte." + extension);
        Response.End();
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        Response.Redirect("EditarUsuario.aspx?idUsuario=0");
    }

    protected void RadUsuarios_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        cUsuario u = (cUsuario)Session["cUsuario"];
        string id = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString();

        if (e.CommandName == "Editar")
        {

            Response.Redirect("EditarUsuario.aspx?idUsuario=" + id);
        }
        if (e.CommandName == "Delete")
        {
            cSql sql = new cSql();
            sql.conectar(cVar.cnnComercializadora);
            string msg = "";
            sql.eliminar("Usuarios", "id=" + id, out msg);
            if (msg == "")
            {
                string javaScript = "confirmacionCorrecto('El usuario fue eliminado exitosamente');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", javaScript, true);

                Cargardatos();
            }
        }

    }
}