using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RestructuracionLitros : System.Web.UI.Page
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
            txtFecha.SelectedDate = DateTime.Now;
            txtFechaFin.SelectedDate = DateTime.Now;
            CargarDatos();
        }
    }

    private void CargarDatos()
    {
        cUsuario u = (cUsuario)Session["cUsuario"];
        string sfecha = DateTime.Parse(txtFecha.SelectedDate.ToString().Split(' ')[0]).ToString("dd/MM/yyyy");
        string sfechaFin = DateTime.Parse(txtFechaFin.SelectedDate.ToString().Split(' ')[0]).ToString("dd/MM/yyyy");
        try
        {
            string msg = "";
            cVar.cnnComercializadora = System.Configuration.ConfigurationManager.ConnectionStrings["ComercializadoraConnectionString"].ToString();
            cSql sql = new cSql();
            sql.conectar(cVar.cnnComercializadora);
            DataSet ds = new DataSet();

            ds = sql.consultaDataSetLibre(@"
SELECT[Fecha]
      ,[PermisoCre]
      ,[Comprobante]
      ,[Remision]
      ,[Producto]
      ,[VolumenComprobante]
      ,[VolumenRestituido]
	  ,[Destino]
  FROM[Comercializadora].[dbo].[PemexDevolucion]
  where cast(Fecha as date) between '" + sfecha + "' and '" + sfechaFin + "'", out msg);

            if (msg != "")
            {
                lblRegistros.Text = txtFecha.SelectedDate.ToString().Substring(0, 10);
                return;
            }

            if (ds.Tables.Count > 0)
            {
                Session["tablas"] = ds;
                RadDevolucion.DataSource = ds.Tables[0];

            }

            RadDevolucion.Rebind();
            lblRegistros.Text = RadDevolucion.Items.Count.ToString();
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnGenerar_Click(object sender, EventArgs e)
    {
        CargarDatos();
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

    private void exportaraExcel(DataSet tablas, string extension)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        ExcelPackage pck = new ExcelPackage();
        for (int n = 0; n <= tablas.Tables.Count - 1; n++)
        {
            DataTable tabla = tablas.Tables[n];
            var ws = pck.Workbook.Worksheets.Add("Inventario");

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
        Response.AddHeader("content-disposition", "attachment;  filename=Inventario." + extension);
        Response.End();
    }
}