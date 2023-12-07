using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for oSql
/// </summary>
public class cSql
{
    //private string strcnn = cVar.cnnA24;
    public SqlConnection cnn;
    //private SqlCommandBuilder cmb;
    public DataSet ds = new DataSet();
    public SqlDataAdapter da;
    public SqlCommand cmd;




    // Conexion
    public void conectar(string strcnn)
    {
        cnn = new SqlConnection(strcnn);
    }

    //Consultar
    public string consultaDato(string campo, string tabla, string condicion, out string msg)
    {
        msg = "";
        string resp = "";
        string sqlcmd = "select " + campo + " from " + tabla + " where " + condicion;
        cmd = new SqlCommand(sqlcmd, cnn);
        try
        {
            cnn.Open();
            resp = (string)cmd.ExecuteScalar();
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        finally { cnn.Close(); }
        return resp;
    }

    public Int32 consultaEntero(string campo, string tabla, string condicion, out string msg)
    {
        msg = "";
        int resp = 0;
        string sqlcmd = "select " + campo + " from " + tabla + " where " + condicion;
        cmd = new SqlCommand(sqlcmd, cnn);
        try
        {
            cnn.Open();
            resp = (int)cmd.ExecuteScalar();
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        finally { cnn.Close(); }
        return resp;
    }

    public decimal consultaDecimal(string campo, string tabla, string condicion, out string msg)
    {
        msg = "";
        decimal resp = 0;
        string sqlcmd = "select " + campo + " from " + tabla + " where " + condicion;
        cmd = new SqlCommand(sqlcmd, cnn);
        try
        {
            cnn.Open();
            resp = (decimal)cmd.ExecuteScalar();
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        finally { cnn.Close(); }
        return resp;
    }

    public Int32 consultaMaxId(string campo, string tabla, out string msg)
    {
        msg = "";
        int resp = 0;
        string sqlcmd = "select max(" + campo + ") from " + tabla;
        cmd = new SqlCommand(sqlcmd, cnn);
        try
        {
            cnn.Open();
            resp = (int)cmd.ExecuteScalar();
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        finally { cnn.Close(); }
        return resp;
    }

    public DataSet consultaDataSet(string tabla, string condicion, out string msg)
    {
        msg = "";
        DataSet ds = new DataSet();
        string sqlcmd = "select * from " + tabla + " where " + condicion;
        da = new SqlDataAdapter(sqlcmd, cnn);
        try
        {
            cnn.Open();
            da.SelectCommand.CommandTimeout = 0;
            da.Fill(ds, tabla);



        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        finally { cnn.Close(); }
        return ds;
    }

    public DataTable consultaTabla(string tabla, string condicion, out string msg)
    {
        msg = "";
        DataTable dt = new DataTable();
        string sqlcmd = "select * from " + tabla + " where " + condicion;

        try
        {

            cnn.Open();
            SqlCommand cmd = new SqlCommand(sqlcmd, cnn);
            dt.Load(cmd.ExecuteReader());
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        finally { cnn.Close(); }
        return dt;
    }

    public DataSet consultaDataSet2(string campos, string tabla, string condicion, out string msg)
    {
        msg = "";
        DataSet ds = new DataSet();
        string sqlcmd = "select " + campos + " from " + tabla + " where " + condicion;
        da = new SqlDataAdapter(sqlcmd, cnn);
        try
        {
            cnn.Open();
            da.SelectCommand.CommandTimeout = 0;
            da.Fill(ds, tabla);
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        finally { cnn.Close(); }
        return ds;
    }

    public DataTable consulCampostabla(string campos, string tabla, string condicion, out string msg)
    {

        msg = "";
        DataTable dt = new DataTable();
        string sqlcmd = "select " + campos + " from " + tabla + " where " + condicion;

        try
        {

            cnn.Open();
            SqlCommand cmd = new SqlCommand(sqlcmd, cnn);
            dt.Load(cmd.ExecuteReader());
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        finally { cnn.Close(); }
        return dt;


    }

    public DataSet consultaDataSetLibre(string query, out string msg)
    {
        msg = "";
        DataSet ds = new DataSet();
        string sqlcmd = query;
        da = new SqlDataAdapter(sqlcmd, cnn);
        try
        {
            cnn.Open();
            da.SelectCommand.CommandTimeout = 0;
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        finally { cnn.Close(); }
        return ds;
    }


    //Eliminar
    public bool eliminar(string tabla, string condicion, out string msg)
    {
        msg = "";
        string sqlcmd = "delete from " + tabla + " where " + condicion;
        cmd = new SqlCommand(sqlcmd, cnn);
        try
        {
            cnn.Open();
            int r = cmd.ExecuteNonQuery();
            if (r > 0)
                return true;
            else
                return false;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
            return false;
        }
        finally { cnn.Close(); }
    }

    //Actualizar
    public bool actualizar(string tabla, string campos, string condicion, out string msg)
    {
        msg = "";
        string sqlcmd = "update " + tabla + " set " + campos + " where " + condicion;
        cmd = new SqlCommand(sqlcmd, cnn);
        try
        {
            cnn.Open();
            int r = cmd.ExecuteNonQuery();
            if (r > 0)
                return true;
            else
                return false;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
            return false;
        }
        finally { cnn.Close(); }
    }

    //Insertar
    public bool insertar(string sqlcmd, out string msg)
    {
        msg = "";
        cmd = new SqlCommand(sqlcmd, cnn);
        try
        {
            cnn.Open();
            int r = cmd.ExecuteNonQuery();
            if (r > 0)
                return true;
            else
                return false;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
            return false;
        }
        finally { cnn.Close(); }
    }

    /// <summary>
    /// Insert Bytes o datos
    /// </summary>
    /// <param name="tabla"></param>
    /// <param name="NombreCampoDato"></param>
    /// <param name="condicion"></param>
    /// <param name="Datos"></param>
    /// <param name="msg"></param>
    /// <returns></returns>
    public bool insertarDatos(string tabla, string NombreCampoDato, string condicion, byte[] Datos, out string msg)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        bool result = false;
        msg = "";
        try
        {
            Byte[] bytes = Datos;
            msg = "";

            string sqlcmd = "Update " + tabla + " set " + NombreCampoDato + "= @datos where " + condicion;
            SqlDataAdapter da = new SqlDataAdapter();

            cnn.Open();
            da.UpdateCommand = new SqlCommand(sqlcmd, cnn);
            da.UpdateCommand.Parameters.AddWithValue("@datos", bytes);
            da.UpdateCommand.ExecuteNonQuery();
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        finally
        {
            cnn.Close();
        }
        return result;
    }

    public Int32 verificaAcceso(string Acceso, string Accion, string NivelUsuario)
    {
        int resp = 0;
        string sqlcmd = "select " + Accion + " from gUsuariosSeg where acceso='" + Acceso + "' and nivel='" + NivelUsuario + "'";
        cmd = new SqlCommand(sqlcmd, cnn);
        try
        {
            cnn.Open();
            resp = (int)cmd.ExecuteScalar();
        }
        catch { }
        finally { cnn.Close(); }
        return resp;
    }
}