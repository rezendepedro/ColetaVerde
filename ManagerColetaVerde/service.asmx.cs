using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
namespace ManagerColetaVerde
{
    /// <summary>
    /// Summary description for service
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false
        )]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class service : System.Web.Services.WebService
    {
        Classes.DbUtils.DbConn conexao = new Classes.DbUtils.DbConn();
        Classes.validacao validar = new Classes.validacao();
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetPontos()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager
                    .ConnectionStrings["MyConnString"].ConnectionString;

                using (SqlCommand cmd = new SqlCommand())
                {


                    cmd.CommandText = "select distinct pc.id_posto, pc.nom_posto, pc.hr_atendimento, ps.nom_pais, " +
                    "  es.nom_estado, cd.nom_cidade, pe.nom_bairro, pe.nom_rua, pe.numero, pe.cep,foto, " +
                    "  coordenadas.Long as lng,coordenadas.Lat as lat" +
                    "  from posto_coleta pc" +
                    "  join posto_endereco pe on pe.id_posto = pc.id_posto" +
                    "  join cidade cd on cd.id_cidade = pe.id_cidade" +
                    "  join estado es on es.id_estado = cd.id_estado" +
                    "  join pais ps on ps.id_pais = es.id_pais" +
                    "  join posto_material pm on pm.id_posto = pc.id_posto"+
                    "  join posto_foto on pc.id_posto=posto_foto.id_posto";               
	              

                   
                    cmd.Connection = conn;
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    serializer.MaxJsonLength = 2147483647;

                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row;
                    foreach (DataRow dr in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, dr[col]);
                        }
                        rows.Add(row);
                    }


                    return serializer.Serialize(rows);


                }

            }

        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetMaterial(string id_posto)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager
                    .ConnectionStrings["MyConnString"].ConnectionString;

                using (SqlCommand cmd = new SqlCommand())
                {


                    cmd.CommandText = "SELECT distinct  [id_posto],material.[id_material],material.nom_material " +
                                    " FROM [dbo].[posto_material] right join material  on material.id_material=posto_material.id_material " +
                                    " where id_posto="+id_posto+" or id_posto is NULL";



                    cmd.Connection = conn;
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    serializer.MaxJsonLength = 2147483647;

                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row;
                    foreach (DataRow dr in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, dr[col]);
                        }
                        rows.Add(row);
                    }


                    return serializer.Serialize(rows);


                }

            }

        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string DeletarPonto(string lat,string lng, string id)
        {
            int rowaffects = -1;
            try
            {
               rowaffects = conexao.commandExec("begin transaction " +
               " DELETE FROM posto_endereco WHERE id_posto =" + id +
               " DELETE FROM posto_foto WHERE id_posto =" + id +
               " DELETE FROM posto_material WHERE id_posto =" + id +
               " DELETE FROM posto_coleta WHERE id_posto =" + id +
               " if @@ERROR <> 0 " +
               " rollback " +
               " else " +
               " commit ");
            


            }catch(Exception ex){
                return "Exceção, messagem original: " + ex.Message;
            }

           if(rowaffects>0)
           {
               File.Delete(Server.MapPath("~/img/" + lat + lng + ".jpg"));
               return "Posto deletado com sucesso!";
           }
           else
           {
               return "Posto não encontrado na base de dados";
           }
           
            


            

        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string InsertPonto(string lat, string lng,string imagem,string estado,string cidade,string bairro,string logradouro,string numero,string tipocoleta)
        {
            string[] coleta = tipocoleta.Split(',');
            DataTable dt = new DataTable();
            string path = "NULL";
            string caminho;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager
                    .ConnectionStrings["MyConnString"].ConnectionString;

                        if (imagem != "" && imagem!="NULL")
                        {

                            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(imagem)))
                            {
                                //Criar um novo Bitmap baseado na MemoryStream
                                using (Bitmap bmp = new Bitmap(ms))
                                {
                                    //Local onde vamos salvar a imagem
                                    path = Server.MapPath(@"~/img/" + lat + lng + ".jpg");
                                    //verificar se a imagem existe no servidor se existe para a função
                                    if (File.Exists(path))
                                    {
                                        return "Posto existente na base de dados.";
                                    }

                                    //Salvar a imagem no formato JPG na raiz do site
                                    bmp.Save(path, ImageFormat.Jpeg);
                                }
                            }
                            caminho = validar.prepDB("img/" + lat + lng + ".jpg");
                        }
                        else
                        {
                            caminho = "NULL";
                        }

                        
                       cidade = validar.prepDB(cidade);    
                       bairro = validar.prepDB(bairro);
                       logradouro = validar.prepDB(logradouro);
                       numero = validar.prepDB(numero);
                       //cep = validar.prepDB(cep);
                       string shapePonto = "geometry::STGeomFromText('POINT('+convert(varchar(20),'" + lng + "')+' '+convert(varchar(20),'" + lat + "')+')',4326)";
                       int idposto = 0;
                       idposto = conexao.insertReturn("INSERT INTO  posto_coleta(id_posto, nom_posto, hr_atendimento) " +
                       "OUTPUT Inserted.id_posto  values((select coalesce(max(id_posto),0) + 1 from posto_coleta), 'Recicla São francisco', 'Das 08:00hrs às 18:00 de segunda à sexta')", "id_posto");
                       int rowaffects = -1;
                       if (idposto != 0)
                       {
                               string material = "";
                               for (int i = 0; i < coleta.Length; i++)
                               {
                                   material += " insert into posto_material values((select coalesce(max(id_postomaterial),0) + 1 from posto_material)," + idposto + "," + coleta[i] + ") ";
                               }
                               rowaffects = conexao.commandExec("begin transaction " +
                               "insert into posto_endereco(id_postoendereco,id_posto,id_cidade,nom_bairro,nom_rua,numero,cep,coordenadas) values((select coalesce(max(id_postoendereco),0) + 1 from posto_endereco)," + idposto + "," + cidade + ",	" + bairro + ",	" + logradouro + "," + numero + ",	'NULL',	geography::STPointFromText('POINT(" + lng + " " + lat + ")', 4326)) " +
                                "insert into posto_foto(id_postofoto,id_posto,foto) values((select coalesce(max(id_postofoto),0) + 1 from posto_foto)," + idposto + "," + caminho + ") " +
                                material+
                               " if @@ERROR <> 0 " +
                                " rollback " +
                                " else " +
                                " commit ");
                       }
                       else
                       {

                           return "Falha ao tentar cadastrar posto de coleta";
                       }


                       if (rowaffects > 0)
                       {
                           // return serializer.Serialize(rows);
                           return "Cadastrado com sucesso!";
                       }
                       else if (rowaffects < 0)
                       {
                           conexao.commandExec("DELETE FROM posto_coleta WHERE id_posto =" + idposto);
                           File.Delete(Server.MapPath("~/img/" + lat + lng + ".jpg"));
                           return "Exeção do cadastro, contate o suporte para auxilialo.";

                       }
                       else
                       {
                           conexao.commandExec("DELETE FROM posto_coleta WHERE id_posto =" + idposto);
                           File.Delete(Server.MapPath("~/img/" + lat + lng + ".jpg"));
                           return "Não castrado, porfavor, tente novamente!";

                       }

                

            }

        }


    }
   
}
