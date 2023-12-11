using OfficeOpenXml;
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
        DataSet tablas = Session["tablas"] as DataSet;
        
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        ExcelPackage pck = new ExcelPackage();
        for (int n = 0; n <= tablas.Tables.Count - 1; n++)
        {
            DataTable tabla = tablas.Tables[n];
            var ws = pck.Workbook.Worksheets.Add("Destinos");

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
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("content-disposition", "attachment;  filename=Destinos.xlsx");
        Response.End();
    }
}