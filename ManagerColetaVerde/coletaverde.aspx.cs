using ManagerColetaVerde.Classes.DbUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ManagerColetaVerde
{
    public partial class coletaverde : System.Web.UI.Page
    {
       DbConn conexao = new DbConn();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //protected void bntUpdateInfo_Click(object sender, EventArgs e)
        //{

        //    int codImovelPK = 0;
        //    int codEndImovelPK = 0;

        //    try
        //    {
               

        //        //codImovelPK = Convert.ToInt32(hfImovelPK.Value);
               


              



        //        string numeroEndImovel = "NULL";
        //        if (!string.IsNullOrEmpty(numeroTxt.Text))
        //            numeroEndImovel = numeroTxt.Text;

        //        string shapeEndereco = "NULL";
        //        if (!string.IsNullOrEmpty(hfLat.Value) && !string.IsNullOrEmpty(hfLng.Value))
        //            shapeEndereco = "geometry::STGeomFromText('POINT('+convert(varchar(20),'" + hfLng.Value + "')+' '+convert(varchar(20),'" + hfLat.Value + "')+')',4674)";
              
               

        //       // conexao.commandExec("UPDATE IMOVEL SET TIPO_IMOVEL = '" + tipoImovel + "', DENOMINACAO = '" + txtDenominacao.Text + "', NUM_PATRIMONIO = " + numPatrimonio + ", SITUACAO_IMOVEL = '" + txtSituacao.Text + "', VALOR_TERRENO = " + valorTerreno + ", VALOR_AREA_CONSTRUIDA = " + valorAreaConstruida + ", VALOR_AVALIACAO = " + valorAvaliado + ", AREA_IMOVEL = " + area + ", OBSERVACAO = '" + txtObservacao.Text + "', UNIDADE_AREA = '" + ddlTipoMedida.SelectedValue + "', LINK_DOC_IMOVEL=" + link + " where COD_IMOVEL_PK = " + codImovelPK);

        //        //conexao.commandExec("UPDATE ENDERECO_IMOVEL SET [NOME_LOGRADOURO]='" + txtNRua.Text + "',[CEP]='" + txtCEP.Text + "',[SHAPE] = " + shapeEndereco + ", [NUMERO]=" + numeroEndImovel + ", [PRECISAO] = '" + hfPrecisao.Value + "', [TYPES] = '" + hfTypes.Value + "' ,[TIPO_LOGRADOURO]='" + ddlTipo.SelectedValue + "',[COMPLEMENTO]='" + txtComplemento.Text + "',[BAIRRO]='" + txtBairro.Text + "',[CIDADE]='" + SearchCidade.Text + "',[ESTADO]='" + ddlEstados.SelectedValue.ToString() + "' WHERE COD_IMOVEL_FK =" + codImovelPK);

        //    }

        //    catch (Exception exception)
        //    {
        //        //e.Message;
        //        lblCadastradoInfo.ForeColor = System.Drawing.Color.Red;
        //        lblCadastradoInfo.Text = exception.Message;
        //        lblCadastradoInfo.Visible = true;

        //        return;
        //    }

        //    if (codImovelPK == 0 || codEndImovelPK == 0)
        //    {
        //        lblCadastradoInfo.ForeColor = System.Drawing.Color.Red;
        //        lblCadastradoInfo.Text = "Erro";
        //        lblCadastradoInfo.Visible = true;

        //        return;
        //    }

        //    lblCadastradoInfo.ForeColor = System.Drawing.Color.Green;
        //    lblCadastradoInfo.Text = "Atualizado";
        //    lblCadastradoInfo.Visible = true;


        //}      


        //protected void bntCadastrarInfo_Click(object sender, EventArgs e)
        //{


        //    int codImovelPK = 0;
           

           

        //    try
        //    {
        //       /* codImovelPK = conexao.insertReturn(
        //            "Declare @count int " +
        //            "SELECT @count = COUNT(*) FROM IMOVEL " +
        //            "IF(@count > 0) " +
        //            "BEGIN " +
        //            "INSERT INTO [dbo].[IMOVEL] ([TIPO_IMOVEL], [DENOMINACAO], [NUM_PATRIMONIO], [SITUACAO_IMOVEL], [VALOR_TERRENO], [VALOR_AREA_CONSTRUIDA], [VALOR_AVALIACAO], [AREA_IMOVEL], [OBSERVACAO], [UNIDADE_AREA], [DATA_AVALIACAO], [COD_IMOVEL_PK],[LINK_DOC_IMOVEL]) OUTPUT Inserted.COD_IMOVEL_PK " +
        //            "VALUES ('" + tipoImovel + "', '" + txtDenominacao.Text + "', " + numPatrimonio + ", '" + txtSituacao.Text + "', " + valorTerreno + "," + valorAreaConstruida + ", " + valorAvaliado + ", " + area + ", '" + txtObservacao.Text + "', '" + ddlTipoMedida.SelectedValue + "'," + dataAvaliacao + ", ((SELECT TOP 1 COD_IMOVEL_PK FROM IMOVEL ORDER BY COD_IMOVEL_PK DESC) + 1)," + link + ") " +
        //            "END " +
        //            "ELSE " +
        //            "BEGIN " +
        //            "INSERT INTO [dbo].[IMOVEL] ([TIPO_IMOVEL], [DENOMINACAO], [NUM_PATRIMONIO], [SITUACAO_IMOVEL], [VALOR_TERRENO], [VALOR_AREA_CONSTRUIDA], [VALOR_AVALIACAO], [AREA_IMOVEL], [OBSERVACAO], [UNIDADE_AREA],[DATA_AVALIACAO], [COD_IMOVEL_PK],[LINK_DOC_IMOVEL]) OUTPUT Inserted.COD_IMOVEL_PK " +
        //            "VALUES ('" + tipoImovel + "', '" + txtDenominacao.Text + "', " + numPatrimonio + ", '" + txtSituacao.Text + "', " + valorTerreno + "," + valorAreaConstruida + ", " + valorAvaliado + ", " + area + ", '" + txtObservacao.Text + "','" + ddlTipoMedida.SelectedValue + "'," + dataAvaliacao + ", 1," + link + ") " +
        //            "END", "COD_IMOVEL_PK"
        //        );*/

        //        if (codImovelPK == 0)
        //        {
        //            //e.Message;
        //            lblCadastradoInfo.ForeColor = System.Drawing.Color.Red;
        //            lblCadastradoInfo.Text = "Erro inesperado ao tentar cadastrar imovel, tente novamente.";
        //            lblCadastradoInfo.Visible = true;

        //            return;
        //        }


        //        string numeroEndImovel = "NULL";
        //        if (!string.IsNullOrEmpty(numeroTxt.Text))
        //            numeroEndImovel = numeroTxt.Text;

        //        string shapeEndereco = "NULL";
        //        if (!string.IsNullOrEmpty(hfLat.Value) && !string.IsNullOrEmpty(hfLng.Value))
        //            shapeEndereco = "geometry::STGeomFromText('POINT('+convert(varchar(20),'" + hfLng.Value + "')+' '+convert(varchar(20),'" + hfLat.Value + "')+')',4674)";

        //        /*codEndImovelPK = conexao.insertReturn(
        //            "Declare @count int " +
        //            "SELECT @count = COUNT(*) FROM ENDERECO_IMOVEL " +
        //            "IF(@count > 0) " +
        //            "BEGIN " +
        //            "INSERT INTO ENDERECO_IMOVEL([COD_END_IMOVEL_PK],[COD_IMOVEL_FK],[TIPO_LOGRADOURO],[NOME_LOGRADOURO],[CEP],[SHAPE],[BAIRRO],[CIDADE],[ESTADO],[COMPLEMENTO],[NUMERO],[PRECISAO],[TYPES]) OUTPUT Inserted.COD_END_IMOVEL_PK " +
        //            "VALUES(((SELECT TOP 1 COD_END_IMOVEL_PK FROM ENDERECO_IMOVEL ORDER BY COD_END_IMOVEL_PK DESC) + 1)," + codImovelPK + ",'" + ddlTipo.SelectedValue + "','" + txtNRua.Text + "','" + txtCEP.Text + "', " + shapeEndereco + " ,'" + txtBairro.Text + "','" + SearchCidade.Text + "','" + ddlEstados.SelectedValue + "','" + txtComplemento.Text + "'," + numeroEndImovel + ",'" + hfPrecisao.Value + "','" + hfTypes.Value + "')" +
        //            "END " +
        //            "ELSE " +
        //            "BEGIN " +
        //            "INSERT INTO ENDERECO_IMOVEL([COD_END_IMOVEL_PK],[COD_IMOVEL_FK],[TIPO_LOGRADOURO],[NOME_LOGRADOURO],[CEP],[SHAPE],[BAIRRO],[CIDADE],[ESTADO],[COMPLEMENTO],[NUMERO],[PRECISAO],[TYPES]) OUTPUT Inserted.COD_END_IMOVEL_PK " +
        //            "VALUES(1," + codImovelPK + ",'" + ddlTipo.SelectedValue + "','" + txtNRua.Text + "','" + txtCEP.Text + "', " + shapeEndereco + " ,'" + txtBairro.Text + "','" + SearchCidade.Text + "','" + ddlEstados.SelectedValue + "','" + txtComplemento.Text + "'," + numeroEndImovel + ",'" + hfPrecisao.Value + "','" + hfTypes.Value + "')" +
        //            "END", "COD_END_IMOVEL_PK"
        //        );*/
        //       // if (codEndImovelPK == 0)
        //       // {
        //            //e.Message;
        //        //    conexao.commandExec("DELETE FROM IMOVEL WHERE COD_IMOVEL_PK=" + codImovelPK);
        //        //    lblCadastradoInfo.ForeColor = System.Drawing.Color.Red;
        //         //   lblCadastradoInfo.Text = "Erro inesperado ao tentar cadastrar endereço, tente novamente.";
        //         //   lblCadastradoInfo.Visible = true;

        //         //   return;
        //       // }



        //    }

        //    catch (Exception exception)
        //    {
        //        //e.Message;
        //        lblCadastradoInfo.ForeColor = System.Drawing.Color.Red;
        //        lblCadastradoInfo.Text = exception.Message;
        //        lblCadastradoInfo.Visible = true;

        //        return;
        //    }

        //  /*  if (codImovelPK == 0 || codEndImovelPK == 0)
        //    {
        //        lblCadastradoInfo.ForeColor = System.Drawing.Color.Red;
        //         lblCadastradoInfo.Text = "Erro ao cadastrar imovel";
        //        lblCadastradoInfo.Visible = true;

        //        return;
        //    }
        //    else
        //    {



              
        //        lblCadastradoInfo.ForeColor = System.Drawing.Color.Green;
        //        lblCadastradoInfo.Text = "Cadastrado";
        //        lblCadastradoInfo.Visible = true;
             
        //        //ddlMuni.Items.FindByValue(SearchCidade.Text).Selected = true;

        //      //  ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", "alerta(" + codImovelPK + ");", true);

        //    }*/

        //}
    }
}