﻿using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace numFact.conexion
{
    class Conexiones

    {
        public OdbcConnection GetOdbcConnectionPixel()
        {
            OdbcConnection cn;
       //  cn = new OdbcConnection("dsn=PixelSqlBase;UID=dba;PWD=banana1;CHARSET=UTF-8"); //---> RG
           cn = new OdbcConnection("dsn=PixelSqlBase;UID=dba;PWD=banana1;CHARSET=UTF-8");
            cn.Open();
            return cn;
        }
        //TABLITA 
        public SqlConnection getOdbcConnectionSquarenet()
        {
            SqlConnection cn;
        //  cn = new OdbcConnection("Data Source=tcp:squarenetweb.database.windows.net;Initial Catalog=SquareNetBD_Tablita;user id = TablitaUsuarioSoloLectura; password =Huch?trA.@i!cHEPePlst5fR1D0C*$TithON@x");
          cn = new SqlConnection("Data Source=tcp:squarenetweb.database.windows.net;Initial Catalog=SquareNetBD_Tablita;user id = TablitaUsuarioSoloLectura; password =Huch?trA.@i!cHEPePlst5fR1D0C*$TithON@x");
          

            cn.Open();
            return cn;
        }


        public OdbcConnection getOdbcConnectionBI()
        {
            OdbcConnection cn;
            cn = new OdbcConnection("dsn=ACK_LTG;UID=sa;PWD=web007;");
            cn.Open();
            return cn;
        }


        public OdbcConnection GetOdbcConnectionORA()
        {
            OdbcConnection cn;
            cn = new OdbcConnection("dsn=CONEXION-ORACLE;UID=LTGRP;PWD=LTGRP;");
            cn.Open();
            return cn;
        }
     

        public void RollbackAndCloseTransaction(OdbcTransaction tranPixel)
        {
            if (tranPixel != null)
            {
                try
                {
                    tranPixel.Rollback();
                }
                finally
                {
                    if (tranPixel.Connection != null)
                    {
                        tranPixel.Connection.Close();
                    }
                }
            }
        }

        public void CommitAndCloseTransaction(OdbcTransaction tranPixel)
        {
            if (tranPixel != null)
            {
                try
                {
                    tranPixel.Commit();
                }
                finally
                {
                    if (tranPixel.Connection != null)
                    {
                        tranPixel.Connection.Close();
                    }
                }
            }
        }

        public void CommitTransaction(OdbcTransaction tranPixel)
        {
            if (tranPixel != null)
            {
                try
                {
                    tranPixel.Commit();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        internal void CloseConnection(OdbcConnection con)
        {

            if (con != null)
            {
                con.Close();
            }
        }

        internal void CloseDataReader(OdbcDataReader dataReader)
        {

            if (dataReader != null)
            {
                dataReader.Close();
            }
        }



        // métodos adicionales específicos para SQL Server SqlClient
        public void RollbackAndCloseTransactionSql(SqlTransaction tranPixel)
        {
            if (tranPixel != null)
            {
                try
                {
                    tranPixel.Rollback();
                }
                finally
                {
                    if (tranPixel.Connection != null)
                    {
                        tranPixel.Connection.Close();
                    }
                }
            }
        }

        public void CommitAndCloseTransactionSql(SqlTransaction tranPixel)
        {
            if (tranPixel != null)
            {
                try
                {
                    tranPixel.Commit();
                }
                finally
                {
                    if (tranPixel.Connection != null)
                    {
                        tranPixel.Connection.Close();
                    }
                }
            }
        }

        public void CommitTransactionSql(SqlTransaction tranPixel)
        {
            if (tranPixel != null)
            {
                try
                {
                    tranPixel.Commit();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        internal void CloseConnectionSql(SqlConnection con)
        {

            if (con != null)
            {
                con.Close();
            }
        }

    }
}
