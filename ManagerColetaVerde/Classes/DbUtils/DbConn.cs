using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using GoogleTranslatorAPI;
namespace ManagerColetaVerde.Classes.DbUtils
{
    public class DbConn
    {
        SqlConnection SqlConn = new SqlConnection();
        public DataTable SqlTable = new DataTable();
        GoogleTransAPI google = new GoogleTransAPI("en", "pt");
        public DbConn()
        {
            SqlConn.ConnectionString = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
        }
        public DataTable SELECT(String command)
        {
            try
            {
                SqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(command, SqlConn);
                da.SelectCommand.CommandTimeout = 360;
         
                da.Fill(SqlTable);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("<script>alert('Ops, Algo deu errado! " + ex.Message + "' )</script>");
            }

            SqlConn.Close();
            return SqlTable;
        }
        public string Translate(string value)
        {
            string result = google.TranslateText(value);
           
            return result;
        }
        public DataTable limpaData(DataTable dt)
        {
            for (int counter = dt.Columns.Count - 1; counter >= 0; counter--)
            {
                dt.Columns.RemoveAt(counter);
            }
            return dt;
        }

        public int insertReturn(String command, String chave)
        {
            DataTable dt = new DataTable();
            try
            {
               // dt.Clear();
                //dt = limpaData(dt);
                SqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(command, SqlConn);
                da.Fill(dt);
                SqlConn.Close();
                if (dt.Rows.Count > 0)
                {
                    return (int)dt.Rows[dt.Rows.Count - 1][chave];
                }else{
                    return 0;
                }
            }
          
            catch (FormatException fe)
            {
                Message("Erro ao Cadastrar. Messagem do sistema: "+fe.Message);
                return 0;
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
                //HttpContext.Current.Response.Write("<script>alert('Ops, Algo deu errado! " + ex.Message + "' )</script>");
                
            }
        }


        public void retrieveData(String command)
        {
            try
            {
                SqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(command, SqlConn);
                da.Fill(SqlTable);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("<script>alert('Ops, Algo deu errado! " + ex.Message + "' )</script>");
            }
            finally
            {
                SqlConn.Close();
            }
        }

        public int commandExec(String command)
        {
            try
            {
                this.SqlConn.Open();
                SqlCommand SqlConn = new SqlCommand(command, this.SqlConn);

                int rowInfected = SqlConn.ExecuteNonQuery();
           
                if (rowInfected > 0)
                {
                    return rowInfected;
                   
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return -1;
                throw new System.Exception(ex.Message);
            }
            finally
            {
                SqlConn.Close();
            }
        }
        public void Message(string mensagem)
        {
            HttpContext.Current.Response.Write("<script>alert('" + mensagem + "' )</script>");

        }
    }
}