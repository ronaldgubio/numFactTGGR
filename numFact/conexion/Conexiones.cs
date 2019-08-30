using System;
using System.Collections.Generic;
using System.Data.Odbc;
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
            cn = new OdbcConnection("dsn=conexion-pixel-rplaza;UID=dba;PWD=banana1;CHARSET=UTF-8");
            cn.Open();
            return cn;
        }

        public OdbcConnection getOdbcConnectionSquarenet()
        {
            OdbcConnection cn;
            cn = new OdbcConnection("dsn=squarenet_tablita;UID=squarenet;PWD=LaTab2015+;");
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
            cn = new OdbcConnection("dsn=CONEXION-ORACLE;UID=ltgrp;PWD=ltgrp;");
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
    }
}
