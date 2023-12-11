using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
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
            txtIDUsuarioModal.Text = "";
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
        txtIDUsuarioModal.Text ="0";
        string javaScript = "showCompose();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", javaScript, true);
    }

    protected void RadUsuarios_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        cUsuario u = (cUsuario)Session["cUsuario"];
        string id = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString();

        if (e.CommandName == "Editar")
        {
            txtIDUsuarioModal.Text = id;
            CargarDatos();
            string javaScript = "showCompose();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", javaScript, true);
        }
        if (e.CommandName == "Delete")
        {
            cSql sql = new cSql();
            sql.conectar(cVar.cnnComercializadora);
            string msg = "";
            sql.eliminar("Usuarios", "id=" + id, out msg);
            if (msg == "")
            {
                Cargardatos();
                string javaScript = "confirmacionCorrecto('El usuario fue eliminado exitosamente');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", javaScript, true);
            }
        }
    }

    protected void btnCerrar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Usuarios.aspx");
    }

    protected void RadDestinos_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        string idDestino = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["idDestino"].ToString();

        if (e.CommandName == "Delete")
        {
            cSql sql = new cSql();
            sql.conectar(cVar.cnnComercializadora);
            string msg = "";
            sql.eliminar("UsuariosDestinos", "idUsuario=" + txtIDUsuarioModal.Text + " and idDestino=" + idDestino, out msg);
            if (msg == "")
            {
                string javaScript = "confirmacionCorrecto('El destino fue eliminado exitosamente');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", javaScript, true);

                divItem.Visible = false;
                divListaItems.Visible = true;
                CargarDatos();
            }
        }
    }

    protected void btnAgregarDestino_Click(object sender, EventArgs e)
    {
        //Guardar();
        divItem.Visible = true;
        divListaItems.Visible = false;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        string msg = Guardar();
        if (msg == "")
        {
            string javaScript = "confirmacionCorrecto('El usuario se guardo exitosamente');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", javaScript, true);
        }
    }

    public static bool esEmailValido(string strMailAddress)
    {
        return Regex.IsMatch(strMailAddress, @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
    }

    private void insertarUsuario()
    {
        cVar.cnnComercializadora = System.Configuration.ConfigurationManager.ConnectionStrings["ComercializadoraConnectionString"].ToString();
        cSql sql = new cSql();
        sql.conectar(cVar.cnnComercializadora);
        DataSet ds = new DataSet();
        string msg = "";


        int id = sql.consultaEntero("max(id)", "usuarios", "id>0", out msg);
        id++;
        sql.insertar("insert into usuarios (id, nombre) values('" + id + "','')", out msg);
        if (msg == "")
        {
            //Session["idUsuario"] = id;
            ds = sql.consultaDataSet("usuariosDestinos d join destinos d on d.id=u.iddestino", "d.idUsuario=" + txtIDUsuarioModal.Text, out msg);
            RadDestinos.DataSource = ds;
            RadDestinos.DataBind();
        }

    }

    private void CargarDatos()
    {
        cVar.cnnComercializadora = System.Configuration.ConfigurationManager.ConnectionStrings["ComercializadoraConnectionString"].ToString();
        cSql sql = new cSql();
        sql.conectar(cVar.cnnComercializadora);
        DataSet ds = new DataSet();
        string msg = "";

        ds = sql.consultaDataSet("usuarios", "id=" + txtIDUsuarioModal.Text, out msg);
        if (ds.Tables.Count > 0)
        {
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                txtNombre.Text = row["Nombre"].ToString().Trim();
                txtUsuario.Text = row["Usuario"].ToString().Trim();
                txtPassword.Text = row["Password"].ToString().Trim();
                txtCorreo.Text = row["Correo"].ToString().Trim();
                dropAllDestinos.SelectedIndex = int.Parse(row["allDestinos"].ToString().Trim());
                dropEsadmin.SelectedIndex = int.Parse(row["esAdmin"].ToString().Trim());
            }
        }

        ds = sql.consultaDataSet("usuariosDestinos u join destinos d on d.id=u.idDestino", "u.idUsuario=" + txtIDUsuarioModal.Text, out msg);
        RadDestinos.DataSource = ds;
        RadDestinos.DataBind();
    }

    protected void btnGuardarDestino_Click(object sender, EventArgs e)
    {
        cVar.cnnComercializadora = System.Configuration.ConfigurationManager.ConnectionStrings["ComercializadoraConnectionString"].ToString();
        cSql sql = new cSql();
        sql.conectar(cVar.cnnComercializadora);
        DataSet ds = new DataSet();
        string msg = "";

        Guardar();

        if (Guardar() != "error")
            sql.insertar("insert usuariosDestinos (idUsuario,IdDestino)  values ('" + txtIDUsuarioModal.Text + "','" + dropDestino.SelectedValue + "')", out msg);

        divItem.Visible = false;
        divListaItems.Visible = true;

        ds = sql.consultaDataSet("usuariosDestinos u join destinos d on d.id=u.iddestino", "u.idUsuario=" + txtIDUsuarioModal.Text, out msg);
        RadDestinos.DataSource = ds;
        RadDestinos.DataBind();
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        divItem.Visible = false;
        divListaItems.Visible = true;
    }

    private string Guardar()
    {
        cVar.cnnComercializadora = System.Configuration.ConfigurationManager.ConnectionStrings["ComercializadoraConnectionString"].ToString();
        cSql sql = new cSql();
        sql.conectar(cVar.cnnComercializadora);
        DataSet ds = new DataSet();
        string msg = "";
        int error = 0;

        if (txtNombre.Text.Trim() == "")
        {
            lblErrorNombre.Text = "El nombre es obligatorio";
            error++;
        }
        else
        {
            lblErrorNombre.Text = "";
        }

        if (txtUsuario.Text.Trim() == "")
        {
            lblErrorUsuario.Text = "El usuario es obligatorio";
            error++;
        }
        else
        {
            lblErrorUsuario.Text = "";
        }
        if (txtPassword.Text.Trim() == "")
        {
            lblErrorPassword.Text = "La contraseña es obligatoria";
            error++;
        }
        else
        {
            lblErrorPassword.Text = "";
        }

        if (!esEmailValido(txtCorreo.Text))
        {
            lblErrorEmail.Text = "El correo es invalido";
            error++;
        }
        else
        {
            lblErrorEmail.Text = "";
        }

        if (error > 0)
            return "error";

        sql.insertar("update usuarios set nombre='" + txtNombre.Text + "',usuario='" + txtUsuario.Text.Trim() + "',password='" + txtPassword.Text.Trim() + "',correo='" + txtCorreo.Text.Trim() + "'," +
            "allDestinos=" + dropAllDestinos.SelectedIndex + ",esadmin=" + dropEsadmin.SelectedIndex + " where id=" + txtIDUsuarioModal.Text, out msg);

        return msg;
    }
}