using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace ManagerColetaVerde.Classes
{
    public class validacao
    {
        public string prepDB(string value)
        {
            value = value.Trim();
            value.Replace("'", "");
            if (value != "" && value != null && value != "NULL" && value != "null" && value != "-1")
            {
                if (IsNumeric(value))
                {
                    return value;
                }
                else
                {
                    value = "'" + value + "'";
                }
            }
            else
            {
                value = "NULL";
            }

            return value;
        }
        public string prepDBDecimal(string value)
        {
            value = value.Trim();
            value.Replace("'", "");
            if (value != "" && value != null && value != "NULL" && value != "null" && value != "-1")
            {
                if (value.Contains("."))
                {
                    if (IsNumeric(value.Split('.')[0]))
                    {
                        if (IsNumeric(value.Split('.')[1]))
                        {
                            return value;
                        }
                    }




                }
                else if (IsNumeric(value))
                {
                    return value;
                }


                value = "'" + value + "'";


            }
            else
            {
                value = "NULL";
            }

            return value;
        }

        public string validaData(string value)
        {
            string[] partDate;
            value = value.Trim();
            if (value != "" && value != null && value != "NULL" && value != "null" && value != "-1")
            {
                partDate = value.Split('/');
                value = "";
                if (partDate.Length > 0)
                {
                    for (int i = partDate.Length - 1; i >= 0; i--)
                    {
                        if (i == partDate.Length - 1)
                        {
                            value = "'" + partDate[i];
                        }
                        else
                        {
                            value = value + "-" + partDate[i];
                        }

                    }
                    value = value + "'";

                }

            }
            else
            {
                value = "NULL";
            }


            return value;
        }
        public string removeCaracter(string value)
        {
            char[] cadeia = value.ToCharArray();
            value = "";

            for (int i = 0; i < cadeia.Length; i++)
            {
                if (char.IsDigit(cadeia[i]))
                {
                    value += cadeia[i];
                }
            }



            return value;

        }
        public bool IsNumeric(string prstValor)
        {
            char[] AIM_stDatachars = prstValor.ToCharArray();

            foreach (var AIM_stDatachar in AIM_stDatachars)
            {
                if (!char.IsDigit(AIM_stDatachar))
                    return false;
            }

            return true;
        }

        private string removeSQL(string str)
        {

            //Função simples para evitar ataques de injeção SQL
            if (str == string.Empty || str == "")
                return str;

            string sValue = str;

            //Valores a serem substituidos
            sValue = sValue.Replace("'", "''");
            sValue = sValue.Replace("--", " ");
            sValue = sValue.Replace("/*", " ");
            sValue = sValue.Replace("*/", " ");
            sValue = sValue.Replace(" or ", "");
            sValue = sValue.Replace(" and ", "");
            sValue = sValue.Replace("update", "");
            sValue = sValue.Replace("-shutdown", "");
            sValue = sValue.Replace("--", "");
            sValue = sValue.Replace("'or'1'='1'", "");
            sValue = sValue.Replace("insert", "");
            sValue = sValue.Replace("drop", "");
            sValue = sValue.Replace("delete", "");
            sValue = sValue.Replace("xp_", "");
            sValue = sValue.Replace("sp_", "");
            sValue = sValue.Replace("select", "");
            sValue = sValue.Replace("1 union select", "");

            return sValue;
        }

        public int validapessoa(string mari)
        {
            int nota = 0;
            if (mari != "CHAT*")
            {
                nota++;
            }
            if (mari != "GEST*")
            {
                nota++;
            }
            if (mari == "ESTAGI*")
            {
                nota = -nota;
            }

            return nota;
        }
        public int BuscaBinaria(DataTable array, int valor, string coluna)
        {
            int min = 0;
            int max = array.Rows.Count - 1;
            while (max >= min)
            {
                /* Calcula o ponto médio (aproximado dependendo se a quantidade de posições é par ou impar*/
                int mid = (min + max) / 2;

                // Determina qual das metades será usada para continuar a busca  
                if (Convert.ToInt32(array.Rows[mid][coluna]) < valor) min = mid + 1; // troca a primeira posição do array para a posição do meio  

                else if (Convert.ToInt32(array.Rows[mid][coluna]) > valor) max = mid - 1;// troca a última posição do array para a posição do meio  

                else return mid; // Valor encontrado na posição central, para o algoritmo e retorna a posição  
            }
            // Não encontrou o valor desejado no array, retorna -1  
            return -1;
        }


        public bool existColunm(DataTable dt, string col)
        {
            bool result = false;
            //verificar fica existecia  de uma coluna
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName == col)
                {
                    result = true;
                    break;
                }

            }
            return result;
        }
        public int calcMedia(string[] numeros)
        {


            int soma = 0;//variavel para receber somatorio
            for (int i = 0; i < numeros.Length; i++)
            {
                soma += Convert.ToInt32(numeros[i].Trim());//somatoria com todos os valores do vetor
            }

            return soma / numeros.Length;//retorna o resultado da divisão da somatoria pela quantidade de numeros
        }
        public string formatUTF8(string s)
        {

            string utf8String = s;
            string propEncodeString = string.Empty;

            byte[] utf8_Bytes = new byte[utf8String.Length];
            for (int i = 0; i < utf8String.Length; ++i)
            {
                utf8_Bytes[i] = (byte)utf8String[i];
            }

            propEncodeString = Encoding.UTF8.GetString(utf8_Bytes, 0, utf8_Bytes.Length);

            return propEncodeString;
        }
        public bool existColunm(DataTable dt, string col1, string col2)
        {
            bool cola = false;
            bool colb = false;
            bool result = false;


            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName.ToString() == col1)
                {
                    cola = true;
                }
                else if (dt.Columns[i].ColumnName.ToString() == col2)
                {
                    colb = true;
                }
            }
            if (cola && colb)
                result = true;

            return result;
        }
    }
}