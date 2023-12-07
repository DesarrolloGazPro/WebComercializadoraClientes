using System;
using System.Data;
using System.Web.UI;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["user_log"] = "";
            cVar.cnnComercializadora = System.Configuration.ConfigurationManager.ConnectionStrings["ComercializadoraConnectionString"].ToString();

        }
    }
    protected void btnIngresar_Click(object sender, EventArgs e)
    {
        ingresar();
    }
    public int VerificarAcceso(string Usuario, string Password)
    {
        cVar.cnnComercializadora = System.Configuration.ConfigurationManager.ConnectionStrings["ComercializadoraConnectionString"].ToString();

        cSql sql = new cSql();
        sql.conectar(cVar.cnnComercializadora);
        string msg;

        int id = 0;

        id = sql.consultaEntero("id", "Usuarios", "usuario='" + txtUsuario.Text.Trim() + "' and password='" + password.Text.Trim() + "'", out msg);
        Session["idUsuario"] = id;
        return id;
    }
    protected void btnRecuperar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Reestablecer.aspx");
    }
    protected void password_TextChanged(object sender, EventArgs e)
    {

    }
    public void ingresar()
    {
        try
        {
            if (txtUsuario.Text.Trim() == "")
            {
                string javaScript = "confirmacionError('El usuario es obligatorio');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", javaScript, true);
                return;
            }

            if (password.Text.Trim() == "")
            {
                string javaScript = "confirmacionError('El password es obligatorio');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", javaScript, true);
                return;
            }

            if (VerificarAcceso(txtUsuario.Text.Trim(), password.Text.Trim()) > 0)
            {
                cUsuario cDatosUsuario = new cUsuario();
                cSql sql = new cSql();
                sql.conectar(cVar.cnnComercializadora);
                string msg;
                string descripcionBitacora = "";

                //Obtener Clientes del Usuario que Inicia Sesion
                DataSet dsUsuario = sql.consultaDataSet("Usuarios", "id=" + Session["idUsuario"].ToString().Trim(), out msg);
                if (dsUsuario.Tables.Count > 0)
                {
                    if (dsUsuario.Tables[0].Rows.Count > 0)
                    {
                        cDatosUsuario.Nombre = dsUsuario.Tables[0].Rows[0]["Nombre"].ToString().Trim();
                        cDatosUsuario.primerLogin = dsUsuario.Tables[0].Rows[0]["primerLogin"].ToString().Trim();
                        cDatosUsuario.esAdmin = int.Parse(dsUsuario.Tables[0].Rows[0]["EsAdmin"].ToString().Trim());
                        cDatosUsuario.allDestinos = int.Parse(dsUsuario.Tables[0].Rows[0]["allDestinos"].ToString().Trim());
                        cDatosUsuario.usuarioId = int.Parse(Session["idUsuario"].ToString().Trim());
                        //cDatosUsuario.Modificar = int.Parse(dsUsuario.Tables[0].Rows[0]["Modificar"].ToString().Trim());
                        Session["cUsuario"] = cDatosUsuario;
                        descripcionBitacora = "El usuario " + txtUsuario.Text.Trim() + " a iniciado sesión.";
                        sql.insertar("insert into bitacora (Descripcion,Pagina,usuario,Fecha,sqlstring) values ('" + descripcionBitacora + "','Login.aspx','"
                        + txtUsuario.Text.Trim() + "','" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "','N/A')", out msg);

                        if (cDatosUsuario.primerLogin == "0")
                            Response.Redirect("Default.aspx");
                        else if (cDatosUsuario.primerLogin == "1")
                            Response.Redirect("NuevoPassword.aspx");
                    }
                }
            }
            else
            {
                string javaScript = "confirmacionError('Usuario y/o contraseña incorrecta o no existen.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", javaScript, true);
                return;
            }
        }
        catch (Exception ex)
        {
            string javaScript = "confirmacionError('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", javaScript, true);
            return;
        }
        finally
        {
            // Para uso Futuro
        }
    }


}