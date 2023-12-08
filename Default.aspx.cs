using System;
using System.Data;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;
using OfficeOpenXml;

public partial class Default2 : System.Web.UI.Page
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

            cVar.cnnComercializadora = System.Configuration.ConfigurationManager.ConnectionStrings["ComercializadoraConnectionString"].ToString();
            txtFecha.SelectedDate = DateTime.Now;
            //exampleFormControlInput1.Text = DateTime.Now.ToString("dd/MM/yyyy");
            CargarCentros();
            CargarDestinos();
            CargarDatos();

        }
    }

    private void CargarMenus(int EsAdmin)
    {
        divActualizacion.Style.Add("display", "none");
        if (EsAdmin == 1)
        {
            divActualizacion.Style.Add("display", "block");
        }
    }

    private void CargarDatos()
    {
        cUsuario u = (cUsuario)Session["cUsuario"];
        try
        {
            string msg = "";
            cVar.cnnComercializadora = System.Configuration.ConfigurationManager.ConnectionStrings["ComercializadoraConnectionString"].ToString();
            cSql sql = new cSql();
            sql.conectar(cVar.cnnComercializadora);
            DataSet ds = new DataSet();
            //lblRegistros.Text = "0";
            lblActualizacion.Text = "";

            //string[] afecha = exampleFormControlInput1.Text.Split('-');

            string sfecha = DateTime.Parse(txtFecha.SelectedDate.ToString().Split(' ')[0]).ToString("dd/MM/yyyy");

            string condicionDestino = "";

            if (ddDestino.SelectedIndex > 0)
            {
                condicionDestino = "and Destino='" + ddDestino.SelectedItem.Text.Trim() + "'";
            }
            else
            {
                condicionDestino = "";
            }

            if (u.allDestinos == 1)
            {
                if (dropCentro.SelectedIndex > 0)
                {
                    ds = sql.consultaDataSet("datosPemex", "centro='" + dropCentro.SelectedItem.Text +
                "' and SUBSTRING(fechahoraestimada,1,10)='" + sfecha + "' " + condicionDestino + " order by destino", out msg);
                    //ConvertDataTableToHTML(ds.Tables[0]);
                }
                else
                {
                    ds = sql.consultaDataSet("datosPemex", "SUBSTRING(fechahoraestimada,1,10)='" + sfecha + "' " + condicionDestino + " order by destino", out msg);
                    //ConvertDataTableToHTML(ds.Tables[0]);
                }
            }
            if (u.allDestinos == 0)
            {
                if (dropCentro.SelectedIndex > 0)
                {
                    ds = sql.consultaDataSet("datosPemex p", "centro='" + dropCentro.SelectedItem.Text +
                         "' and SUBSTRING(fechahoraestimada,1,10)='" + sfecha + "' and " +
                         "p.destino in (select destino from destinos d join usuariosDestinos u on u.idDestino=d.id where u.idusuario='"
                        + Session["idUsuario"].ToString().Trim() + "') order by destino", out msg);
                }
                else
                {
                    ds = sql.consultaDataSet("datosPemex p", "p.destino in (select destino from destinos d join usuariosDestinos u on u.idDestino=d.id where u.idusuario='"
                        + Session["idUsuario"].ToString().Trim() + "') and SUBSTRING(fechahoraestimada,1,10)='" + sfecha + "' "
                        + condicionDestino
                        + " order by destino", out msg);
                }
            }
            if (ds.Tables.Count > 0)
            {
                Session["tablas"] = ds;
                RadEmbarques.DataSource = ds.Tables[0];

            }
            RadEmbarques.Rebind();

            lblRegistros.Text = RadEmbarques.Items.Count.ToString();

            if (lblRegistros.Text != "0")
            {
                lblTotal.Visible = true;
                lblupdate.Visible = true;
                lblActualizacion.Visible = true;
                lblRegistros.Visible = true;
                lblActualizacion.Text = sql.consultaDato("top 1 convert(nvarchar(25),FechaActualizacion)", "datosPemex", "0=0 order by fechaActualizacion desc", out msg);
            }
            else
            {
                lblActualizacion.Visible = false;
                lblRegistros.Visible = false;
                lblTotal.Visible = false;
                lblupdate.Visible = false;
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void CargarCentros()
    {
        cUsuario u = (cUsuario)Session["cUsuario"];
        string msg = "";
        cVar.cnnComercializadora = System.Configuration.ConfigurationManager.ConnectionStrings["ComercializadoraConnectionString"].ToString();
        cSql sql = new cSql();
        sql.conectar(cVar.cnnComercializadora);
        DataSet ds = new DataSet();
        //lblRegistros.Text = "0";
        lblActualizacion.Text = "";
        try
        {

            if (u.esAdmin == 1)
                ds = sql.consultaDataSetLibre("select 0 as Id , 'Todos' as Centro  union SELECT[Id], [Centro] FROM[Centros] c ", out msg);
            else
                ds = sql.consultaDataSetLibre("select 0 as Id , 'Todos' as Centro  union SELECT[Id], [Centro] FROM[Centros] c " +
                                        "where c.Id in (select idCentro from UsuarioCentros u where u.idUsuario ="
                                        + Session["idUsuario"].ToString().Trim() + ")", out msg);
            dropCentro.DataSource = ds;
            dropCentro.DataTextField = "Centro";
            dropCentro.DataValueField = "id";
            dropCentro.DataBind();
        }
        catch
        {

        }
    }

    private void CargarDestinos()
    {
        cUsuario u = (cUsuario)Session["cUsuario"];
        string msg = "";
        cVar.cnnComercializadora = System.Configuration.ConfigurationManager.ConnectionStrings["ComercializadoraConnectionString"].ToString();
        cSql sql = new cSql();
        sql.conectar(cVar.cnnComercializadora);
        DataSet ds = new DataSet();
        try
        {
            if (u.esAdmin == 1)
                ds = sql.consultaDataSetLibre("select 0 as idDestino , 'Todos' as Destino  union SELECT idDestino,d.Destino " +
                "FROM [Comercializadora].[dbo].[UsuariosDestinos] u join[Comercializadora].[dbo].[Destinos] d on u.idDestino = d.id ", out msg);
            else

                ds = sql.consultaDataSetLibre("select 0 as idDestino , 'Todos' as Destino  union SELECT idDestino,d.Destino " +
                "FROM [Comercializadora].[dbo].[UsuariosDestinos] u join[Comercializadora].[dbo].[Destinos] d on u.idDestino = d.id " +
                "where u.idUsuario=" + Session["idUsuario"].ToString().Trim(), out msg);
            ddDestino.DataSource = ds;
            ddDestino.DataTextField = "Destino";
            ddDestino.DataValueField = "idDestino";
            ddDestino.DataBind();
        }
        catch (Exception ex)
        {

        }
    }

    protected void BtnExportarXls_Click(object sender, EventArgs e)
    {
        DataSet tablas = Session["tablas"] as DataSet;

        if (tablas.Tables.Count > 0)
        {
            exportaraExcel(tablas, "xls");
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
        Response.AddHeader("content-disposition", "attachment;  filename=Inventario." + extension);
        Response.End();
    }

    protected void dropCentro_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarDestinos();
        CargarDatos();
    }

    protected void ddDestino_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarDatos();
    }

    protected void exampleFormControlInput1_TextChanged(object sender, EventArgs e)
    {
        CargarDatos();
    }

    protected void RadDateTimePicker1_SelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
    {
        CargarDatos();
    }

    protected void RadEmbarques_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        cUsuario u = (cUsuario)Session["cUsuario"];
        cVar.cnnComercializadora = System.Configuration.ConfigurationManager.ConnectionStrings["ComercializadoraConnectionString"].ToString();
        cSql sql = new cSql();
        sql.conectar(cVar.cnnComercializadora);
        string msg = "";


        if (e.CommandName == "Ver")
        {
            string estado = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Estado"].ToString();
            Session["Folio"] = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Folio"].ToString();
            Session["Centro"] = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Centro"].ToString();
            Session["Destino"] = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Destino"].ToString();
            if (estado == "FACTURADO")
                Response.Redirect("viewFactura.aspx");
            else
            {
                string javaScript = "confirmacionError('El pedido no se encuentra facturado');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", javaScript, true);
            }
        }
        else if (e.CommandName == "litros")
        {
            Session["remision"] = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Remision"].ToString().Split('-')
                                 [e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Remision"].ToString().Split('-').Length - 1];
            Session["Destino"] = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Destino"].ToString().Split('-')[0];
            int existe = 0;
            //638-66750-20/12/2022-852267
            existe = sql.consultaEntero("count(*)", "PemexDevolucion", "destino like '%" + Session["Destino"].ToString().Trim()
                                        + "%' and remision like '%" + Session["remision"].ToString().Trim() + "%'", out msg);
            if (existe == 1)
                Response.Redirect("viewDevolucion.aspx");
            else
            {
                string javaScript = "confirmacionError('El pedido no cuenta con devolucion');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", javaScript, true);
            }
        }
    }

    protected void RadEmbarques_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = e.Item as GridDataItem;
            foreach (GridColumn col in RadEmbarques.MasterTableView.AutoGeneratedColumns)
            {
                if (col.DataType == typeof(int))
                {
                    item[col.UniqueName].Font.Bold = true;
                }
            }
        }
    }

    private void alimentarTabla()
    {

        //Uso futuro
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        StringBuilder sb = new StringBuilder();

        if (ds.Tables.Count > 0)
        {
            dt = ds.Tables[0];
            Session["tablas"] = ds;
        }

        int contador = 1;


        //Table start.
        sb.Append("<table class='table'>");

        //Adding HeaderRow.
        sb.Append("<thead><tr>");
        sb.Append("<th>Centro</th>");
        sb.Append("<th>Destino</th>");
        sb.Append("<th>Producto</th>");
        sb.Append("<th>Turno</th>");
        sb.Append("<th>Clave</th>");
        sb.Append("<th>Transportista</th>");
        sb.Append("<th>Tonel</th>");
        sb.Append("<th>Litros</th>");
        sb.Append("<th>FechEstimada</th>");
        sb.Append("<th>FechaFactura</th>");
        sb.Append("<th>Estado</th>");
        sb.Append("<th>Ver</th>");
        sb.Append("</tr></thead>");
        sb.Append("<tbody>");

        //Adding DataRow.
        foreach (DataRow row in dt.Rows)
        {

            sb.Append("<tr>");
            sb.Append("<td>" + row["Centro"].ToString().Trim() + "</td>");
            sb.Append("<td>" + row["Destino"].ToString().Trim() + "</td>");
            sb.Append("<td>" + row["Producto"].ToString().Trim().Substring(0, 10) + "</td>");
            sb.Append("<td>" + row["Turno"].ToString().Trim() + "</td>");
            sb.Append("<td>" + row["Clave"].ToString().Trim() + "</td>");
            sb.Append("<td>" + row["Transportista"].ToString().Trim() + "</td>");
            sb.Append("<td>" + row["Tonel"].ToString().Trim() + "</td>");
            sb.Append("<td>" + row["Litros"].ToString().Trim() + "</td>");
            sb.Append("<td>" + row["FechaHoraEstimada"].ToString().Trim() + "</td>");
            sb.Append("<td>" + row["FechaFactura"].ToString().Trim() + "</td>");
            sb.Append("<td>" + row["Estado"].ToString().Trim() + "</td>");
            sb.Append("<td><a class='icon icon-pdf' href='viewfactura.aspx?" + row["Centro"].ToString().Trim()
                + "?" + row["Estado"].ToString().Trim()
                + "?" + row["Folio"].ToString().Trim()
                + "?" + row["Destino"].ToString().Trim()
                + "'></a></td>");
            sb.Append("</tr>");
            contador++;
        }
        sb.Append("</tbody>");
        //Table end.
        sb.Append("</table>");
        //divlista.InnerHtml = sb.ToString();
    }
}