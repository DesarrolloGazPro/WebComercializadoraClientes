using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for cSeguridad
/// </summary>
public class cSeguridad
{
    public static cAccesos Accesos(cUsuario User, string Modulo, out string Mensaje)
    {
        cVar.cnnComercializadora = System.Configuration.ConfigurationManager.ConnectionStrings["ComercializadoraConnectionString"].ToString();
        cAccesos acc = new cAccesos();
        acc.insertar = "0";
        acc.borrar = "0";
        acc.editar = "0";
        acc.ver = "0";
        Mensaje = "";

        try
        {
            cSql sql = new cSql();
            sql.conectar(cVar.cnnComercializadora);
            string omsg = "";

            foreach (DataRow r in User.Perfiles.Rows)
            {

                string condicion = "modulo='" + Modulo + "' and perfilID='" + r["idPerfil"].ToString().Trim() + "'";
                DataTable dtDatos = sql.consultaTabla("gPerfilesPermisos", condicion, out omsg);
                    foreach (DataRow row in dtDatos.Rows)
                    {
                        if(acc.insertar=="0") 
                            acc.insertar = row["insertar"].ToString().Trim();
                        if (acc.borrar=="0")
                            acc.borrar = row["borrar"].ToString().Trim();
                        if (acc.editar=="0")
                            acc.editar = row["editar"].ToString().Trim();
                        if (acc.ver=="0")
                            acc.ver = row["ver"].ToString().Trim();
                    }

            }

        }
        catch (Exception ex)
        {
            Mensaje = "Error al obtener informacion de Seguridad del usuario : " + ex.Message;
        }
        return acc;
    }
}
public class cAccesos
{
    public string modulo { get; set; }
    public string insertar { get; set; }
    public string borrar { get; set; }
    public string editar { get; set; }
    public string ver { get; set; }
}