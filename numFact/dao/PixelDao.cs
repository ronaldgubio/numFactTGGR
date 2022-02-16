using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using numFact.conexion;
using numFact.dto;
using numFact.service;

namespace numFact.dao
{
    class PixelDao
    {
        private Conexiones conexiones;


        private const String SQL_POSHEADER_SELECT = @"
select  
TRANSACT, 
TABLENUM, 
TIMESTART, 
TIMEEND, 
NUMCUST, 
TAX1, 
TAX2, 
TAX3, 
TAX4, 
TAX5, 
TAX1ABLE, 
TAX2ABLE, 
TAX3ABLE, 
TAX4ABLE, 
TAX5ABLE, 
NETTOTAL, 
WHOSTART, 
WHOCLOSE, 
ISSPLIT, 
SALETYPEINDEX, 
EXP, 
WAITINGAUTH, 
STATNUM, 
STATUS, 
FINALTOTAL, 
STORENUM, 
PUNCHINDEX, 
GRATUITY, 
OPENDATE, 
MEMCODE, 
TOTALPOINTS, 
POINTSAPPLIED, 
UPDATESTATUS, 
ISDELIVERY, 
SCHEDULEDATE, 
TAX1EXEMPT, 
TAX2EXEMPT, 
TAX3EXEMPT, 
TAX4EXEMPT, 
TAX5EXEMPT, 
MEMRATE, 
MEALTIME, 
ISINTERNET, 
REVCENTER, 
PUNCHIDXSTART, 
STATNUMSTART, 
SECNUM, 
GRATAMOUNT, 
SHIPTO, 
ENFORCEDGRAT, 
NUMPRINTEDFINAL, 
REFID, 
RSTORDNUM, 
SNUM
from DBA.POSHEADER aa where aa.opendate=?";

        private const String SQL_POSHEADER_INSERT = @"
INSERT INTO PIXEL_POSHEADER (
TRANSACT, 
TABLENUM, 
TIMESTART, 
TIMEEND, 
NUMCUST, 
TAX1, 
TAX2, 
TAX3, 
TAX4, 
TAX5, 
TAX1ABLE, 
TAX2ABLE, 
TAX3ABLE, 
TAX4ABLE, 
TAX5ABLE, 
NETTOTAL, 
WHOSTART, 
WHOCLOSE, 
ISSPLIT, 
SALETYPEINDEX, 
EXP, 
WAITINGAUTH, 
STATNUM, 
STATUS, 
FINALTOTAL, 
STORENUM, 
PUNCHINDEX, 
GRATUITY, 
OPENDATE, 
MEMCODE, 
TOTALPOINTS, 
POINTSAPPLIED, 
UPDATESTATUS, 
ISDELIVERY, 
SCHEDULEDATE, 
TAX1EXEMPT, 
TAX2EXEMPT, 
TAX3EXEMPT, 
TAX4EXEMPT, 
TAX5EXEMPT, 
MEMRATE, 
MEALTIME, 
ISINTERNET, 
REVCENTER, 
PUNCHIDXSTART, 
STATNUMSTART, 
SECNUM, 
GRATAMOUNT, 
SHIPTO, 
ENFORCEDGRAT, 
NUMPRINTEDFINAL, 
REFID, 
RSTORDNUM, 
SNUM) 
VALUES ( 
:0,:1,:2,:3,:4,
:5,:6,:7,:8,:9,
:10,:11,:12,:13,:14,
:15,:16,:17,:18,:19,
:20,:21,:22,:23,:24,
:25,:26,:27,:28,:29,
:30,:31,:32,:33,:34,
:35,:36,:37,:38,:39,
:40,:41,:42,:43,:44,
:45,:46,:47,:48,:49,
:50,:51,:52,:53
)";

        private const String SQL_POSDETAIL_SELECT = @"
SELECT
UNIQUEID, 
TRANSACT, 
PRODNUM, 
WHOORDER, 
WHOAUTH, 
COSTEACH, 
QUAN, 
TIMEORD, 
PRINTLOC, 
SEATNUM, 
MINUTES, 
NOTAX, 
HOWORDERED, 
STATUS, 
NEXTPOS, 
PRIORPOS, 
RECPOS, 
PRODTYPE, 
APPLYTAX1, 
APPLYTAX2, 
APPLYTAX3, 
APPLYTAX4, 
APPLYTAX5, 
REDUCEINVENTORY, 
STORENUM, 
STATNUM, 
RECIPECOSTEACH, 
OPENDATE, 
MEALTIME, 
LINEDES, 
REVCENTER, 
MASTERITEM, 
QUESTIONID, 
ORIGCOSTEACH, 
NETCOSTEACH, 
DISCOUNT, 
UPDATESTATUS, 
GRATEXEMPT, 
AUTHCODE, 
SNUM FROM DBA.POSDETAIL WHERE OPENDATE=?";

        private const String SQL_POSDETAIL_INSERT = @"
INSERT INTO PIXEL_POSDETAIL (
UNIQUEID, 
TRANSACT, 
PRODNUM, 
WHOORDER, 
WHOAUTH, 
COSTEACH, 
QUAN, 
TIMEORD, 
PRINTLOC, 
SEATNUM, 
MINUTES, 
NOTAX, 
HOWORDERED, 
STATUS, 
NEXTPOS, 
PRIORPOS, 
RECPOS, 
PRODTYPE, 
APPLYTAX1, 
APPLYTAX2, 
APPLYTAX3, 
APPLYTAX4, 
APPLYTAX5, 
REDUCEINVENTORY, 
STORENUM, 
STATNUM, 
RECIPECOSTEACH, 
OPENDATE, 
MEALTIME, 
LINEDES, 
REVCENTER, 
MASTERITEM, 
QUESTIONID, 
ORIGCOSTEACH, 
NETCOSTEACH, 
DISCOUNT, 
UPDATESTATUS, 
GRATEXEMPT, 
AUTHCODE, 
SNUM) 
VALUES (
:0,:1,:2,:3,:4,
:5,:6,:7,:8,:9,
:10,:11,:12,:13,:14,
:15,:16,:17,:18,:19,
:20,:21,:22,:23,:24,
:25,:26,:27,:28,:29,
:30,:31,:32,:33,:34,
:35,:36,:37,:38,:39
)";


        private const String SQL_HOWPAID_INSERT = @"
INSERT INTO PIXEL_HOWPAID (
HOWPAIDLINK, 
TRANSDATE, 
EMPNUM, 
TENDER, 
METHODNUM, 
CHANGE, 
AUTHORIZED, 
AUTHCODE, 
MEMCODE, 
EXCHANGERATE, 
TRANSACT, 
PAYTYPE, 
OPENDATE, 
PUNCHINDEX, 
UPDATESTATUS, 
SETTLED, 
STATUS, 
APPROVED, 
SNUM, 
STATNUM, 
ISPAYINOUT, 
PAYREASON, 
MEALTIME, 
REVCENTER, 
VOIDED, 
VOIDEDLINK, 
LCUDIFF, 
ENFORCEDGRAT, 
GRATAMOUNT) 
VALUES (
:0,:1,:2,:3,:4,
:5,:6,:7,:8,:9,
:10,:11,:12,:13,:14,
:15,:16,:17,:18,:19,
:20,:21,:22,:23,:24,
:25,:26,:27,:28
)";

        private const String SQL_HOWPAID_SELECT = @"
SELECT 
HOWPAIDLINK, 
TRANSDATE, 
EMPNUM, 
TENDER, 
METHODNUM, 
CHANGE, 
AUTHORIZED, 
AUTHCODE, 
MEMCODE, 
EXCHANGERATE, 
TRANSACT, 
PAYTYPE, 
OPENDATE, 
PUNCHINDEX, 
UPDATESTATUS, 
SETTLED, 
STATUS, 
APPROVED, 
SNUM, 
STATNUM, 
ISPAYINOUT, 
PAYREASON, 
MEALTIME, 
REVCENTER, 
VOIDED, 
VOIDEDLINK, 
LCUDIFF, 
ENFORCEDGRAT, 
GRATAMOUNT
FROM DBA.HOWPAID WHERE OPENDATE=?";


        private const String SQL_POSBANK_INSERT = @"INSERT INTO PIXEL_POSBANK (
UNIQUEID, 
PUNCHINDEX, 
METHODNUM, 
CALCTENDERED, 
EXCHANGERATE, 
DATEENTERED, 
ENTRYTYPE, 
UPDATESTATUS, 
SNUM, 
REFCODE, 
OPENDATE, 
WHOAUTH, 
PLINK, 
REASONID, 
SAFENUM, 
FLOATNUM, 
DENOMNUM, 
WASPROCESSED, 
ISACTIVE, 
STATNUM, 
MEMCODE) 
VALUES ( 
:0,:1,:2,:3,:4,
:5,:6,:7,:8,:9,
:10,:11,:12,:13,:14,
:15,:16,:17,:18,:19,
:20
)";

        private const String SQL_POSBANK_SELECT = @"select
UNIQUEID, 
PUNCHINDEX, 
METHODNUM, 
CALCTENDERED, 
EXCHANGERATE, 
DATEENTERED, 
ENTRYTYPE, 
UPDATESTATUS, 
SNUM, 
REFCODE, 
OPENDATE, 
WHOAUTH, 
PLINK, 
REASONID, 
SAFENUM, 
FLOATNUM, 
DENOMNUM, 
WASPROCESSED, 
ISACTIVE, 
STATNUM, 
MEMCODE
from DBA.POSBANK where opendate=?";

        private const String SQL_CASHOUT_INSERT = @"
INSERT INTO PIXEL_CASHOUT (
CASHOUTNUM, 
EMPNUM, 
PUNCHINDEX, 
OPENDATE, 
""UId"", 
WHOENTERED, 
DATEENTERED, 
WHOPROCESSED, 
DATEPROCESSED, 
TOTALAMOUNT, 
TOTALTIP, 
TOTALSURCHG, 
TOTALCASHDUE, 
TOTALPOSTENDER, 
TOTALFLOATS, 
TOTALPAYINS, 
TOTALPAYOUTS, 
TOTALPICKUPS, 
ISTIPPAIDOUT, 
OVERSHORT, 
REASON, 
ISACTIVE, 
UPDATESTATUS, 
SNUM) 
VALUES(
:0,:1,:2,:3,:4,
:5,:6,:7,:8,:9,
:10,:11,:12,:13,:14,
:15,:16,:17,:18,:19,
:20,:21,:22,:23
)";

        private const String SQL_CASHOUT_SELECT = @"select
CASHOUTNUM, 
EMPNUM, 
PUNCHINDEX, 
OPENDATE, 
UId, 
WHOENTERED, 
DATEENTERED, 
WHOPROCESSED, 
DATEPROCESSED, 
TOTALAMOUNT, 
TOTALTIP, 
TOTALSURCHG, 
TOTALCASHDUE, 
TOTALPOSTENDER, 
TOTALFLOATS, 
TOTALPAYINS, 
TOTALPAYOUTS, 
TOTALPICKUPS, 
ISTIPPAIDOUT, 
OVERSHORT, 
REASON, 
ISACTIVE, 
UPDATESTATUS, 
SNUM
from DBA.CASHOUT where opendate=?";

        private const String SQL_CASHOUTDETAIL_INSERT = @"
INSERT INTO PIXEL_CASHOUTDETAIL (
CASHOUTDETAILNUM, 
CASHOUTNUM, 
METHODNUM, 
NOOFITEMS, 
EXCHANGE, 
AMOUNT, 
CALCTENDER, 
OVERSHORT, 
WHOPROCESSED, 
DATEENTERED, 
DATEPROCESSED, 
ISCURRENCY, 
SHOWCALCTENDER, 
TENDERSETTLEMENT, 
HASDENOMS, 
ISEFT, 
ADJUSTCASHNUM, 
ISACTIVE, 
UPDATESTATUS, 
SNUM) 
VALUES ( 
:0,:1,:2,:3,:4,
:5,:6,:7,:8,:9,
:10,:11,:12,:13,:14,
:15,:16,:17,:18,:19
)";


        private const String SQL_CASHOUTDETAIL_SELECT = @"select
CASHOUTDETAILNUM, 
CASHOUTNUM, 
METHODNUM, 
NOOFITEMS, 
EXCHANGE, 
AMOUNT, 
CALCTENDER, 
OVERSHORT, 
WHOPROCESSED, 
DATEENTERED, 
DATEPROCESSED, 
ISCURRENCY, 
SHOWCALCTENDER, 
TENDERSETTLEMENT, 
HASDENOMS, 
ISEFT, 
ADJUSTCASHNUM, 
ISACTIVE, 
UPDATESTATUS, 
SNUM
from DBA.CASHOUTDETAIL where cashoutnum in(select aa.cashoutnum from DBA.cashout aa where aa.opendate=?)";

        public PixelDao()
        {
            conexiones = new Conexiones();
        }
        public void EjecutarProcesoInicial()
        {
            OdbcConnection conPixel = null;
            OdbcTransaction tranPixel = null;
            try
            {
                //OdbcConnection conBI = conexiones.getOdbcConnectionBI();

                //Abro la conexión a Pixel
                conPixel = conexiones.GetOdbcConnectionPixel();

                //Inicio la transacción
                tranPixel = conPixel.BeginTransaction();

                //llamo el procedimiento 1
                OdbcCommand comandoPixel1 = new OdbcCommand("{call dba.ACK_CARGA_ACK_OCRC()}", conPixel);
                comandoPixel1.CommandType = CommandType.StoredProcedure;
                comandoPixel1.Transaction = tranPixel;
                comandoPixel1.ExecuteNonQuery();

                //llamo el procedimiento 2
                OdbcCommand comandoPixel2 = new OdbcCommand("{call dba.ACK_CARGA_ACK_OWHS()}", conPixel);
                comandoPixel2.CommandType = CommandType.StoredProcedure;
                comandoPixel2.Transaction = tranPixel;
                comandoPixel2.ExecuteNonQuery();

                //llamo el procedimiento 3
                OdbcCommand comandoPixel3 = new OdbcCommand("{call dba.ACK_CARGA_ACK_OITM()}", conPixel);
                comandoPixel3.CommandType = CommandType.StoredProcedure;
                comandoPixel3.Transaction = tranPixel;
                comandoPixel3.ExecuteNonQuery();

                //confirmo la transacción

                conexiones.CommitAndCloseTransaction(tranPixel);
                //ODBCCommand.Parameters.AddWithValue("@KundenEmail", KundenEmail);
            }
            catch (Exception ex)
            {
                conexiones.RollbackAndCloseTransaction(tranPixel);
                throw ex;
            }
        }

        public List<Venta> VentasFindAllPendientes()
        {
            List<Venta> retorno = new List<Venta>();
            OdbcConnection conPixel = null;
            OdbcDataReader readerDatos = null;
            try
            {
                //Abro la conexión a Pixel
                conPixel = conexiones.GetOdbcConnectionPixel();

                //llamo el procedimiento 2
                OdbcCommand comandoPixel1 = new OdbcCommand("SELECT DBA.Dayinfo.UId,DBA.Dayinfo.OPENDATE,DBA.Dayinfo.TIMEEND FROM DBA.Dayinfo WHERE DBA.Dayinfo.TIMEEND IS NOT NULL AND DBA.Dayinfo.UId NOT IN (SELECT DBA.ACK_ESTADOS.ID_TABLA FROM DBA.ACK_ESTADOS WHERE DBA.ACK_ESTADOS.TIPO='DIA DE VENTA') ORDER BY 2 DESC", conPixel);
                //OdbcCommand comandoPixel1 = new OdbcCommand("SELECT DBA.Dayinfo.UId,DBA.Dayinfo.OPENDATE,DBA.Dayinfo.TIMEEND FROM DBA.Dayinfo WHERE DBA.Dayinfo.TIMEEND IS NOT NULL ORDER BY 2 DESC", conPixel);
                readerDatos = comandoPixel1.ExecuteReader();
                if (readerDatos.HasRows)
                {
                    while (readerDatos.Read())
                    {
                        Venta venta = new Venta();
                        venta.UId = readerDatos.GetString(0);
                        venta.OPENDATE = readerDatos.GetString(1);
                        retorno.Add(venta);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexiones.CloseConnection(conPixel);
            }
            return retorno;
        }

        public void EjecutarCorreccionCCDomicilio()
        {
            OdbcConnection conPixel = null;
            OdbcTransaction tranPixel = null;
            try
            {
                //OdbcConnection conBI = conexiones.getOdbcConnectionBI();

                //Abro la conexión a Pixel
                conPixel = conexiones.GetOdbcConnectionPixel();

                //Inicio la transacción
                tranPixel = conPixel.BeginTransaction();

                //llamo el procedimiento 1
                OdbcCommand comandoPixel1 = new OdbcCommand("{call dba.inf_corregircc_prc()}", conPixel);
                comandoPixel1.CommandType = CommandType.StoredProcedure;
                comandoPixel1.Transaction = tranPixel;
                comandoPixel1.ExecuteNonQuery();

                conexiones.CommitAndCloseTransaction(tranPixel);
            }
            catch (Exception ex)
            {
                conexiones.RollbackAndCloseTransaction(tranPixel);
                throw ex;
            }
        }

        public void EjecutarProcesoVentas(String idTabla)
        {
            OdbcConnection conPixel = null;
            OdbcTransaction tranPixel = null;
            //OdbcConnection conOracle = null;
            //OdbcTransaction tranOracle = null;
            try
            {
                //OdbcConnection conBI = conexiones.getOdbcConnectionBI();

                //Abro la conexión a Pixel
                //conOracle = conexiones.GetOdbcConnectionORA();

                //Inicio la transacción
                //tranOracle = conOracle.BeginTransaction();

                //Abro la conexión a Pixel
                conPixel = conexiones.GetOdbcConnectionPixel();

                //Inicio la transacción
                tranPixel = conPixel.BeginTransaction();

                //llamo al proceso de copia de información entre bases de datos Pixel y Oracle
                //CopiarInformacionParaQLikSense(idTabla, conPixel, tranPixel, conOracle, tranOracle);

                //llamo el procedimiento 1
                OdbcCommand comandoPixel1 = new OdbcCommand("{call dba.ACK_LTG_VENTAS_PAGOS(?)}", conPixel);
                comandoPixel1.CommandType = CommandType.StoredProcedure;
                comandoPixel1.Transaction = tranPixel;
                comandoPixel1.Parameters.AddWithValue("@idTabla", idTabla);
                comandoPixel1.ExecuteNonQuery();

                //llamo el procedimiento 2
                OdbcCommand comandoPixel2 = new OdbcCommand("INSERT INTO DBA.ACK_ESTADOS(ID_EST,ID_TABLA,TIPO)VALUES((select coalesce(max(aa1.ID_EST),0)+1 from dba.ACK_ESTADOS aa1), ?, 'DIA DE VENTA')", conPixel);
                comandoPixel2.Transaction = tranPixel;
                comandoPixel2.Parameters.AddWithValue("@idTabla", idTabla);
                comandoPixel2.ExecuteNonQuery();

                //confirmo la transacción
                //confirmo la transacción

                conexiones.CommitAndCloseTransaction(tranPixel);
                //conexiones.CommitAndCloseTransaction(tranOracle);
            }
            catch (Exception ex)
            {
                conexiones.RollbackAndCloseTransaction(tranPixel);
                //conexiones.RollbackAndCloseTransaction(tranOracle);
                throw ex;
            }
        }



        public void Prueba()
        {
            String idTabla = "20190902";
            OdbcConnection conPixel = null;
            OdbcTransaction tranPixel = null;
            //OdbcConnection conOracle = null;
            //OdbcTransaction tranOracle = null;
            try
            {
                //OdbcConnection conBI = conexiones.getOdbcConnectionBI();

                //Abro la conexión a Pixel
                //conOracle = conexiones.GetOdbcConnectionORA();

                //Inicio la transacción
                //tranOracle = conOracle.BeginTransaction();

                //Abro la conexión a Pixel
                conPixel = conexiones.GetOdbcConnectionPixel();

                //Inicio la transacción
                tranPixel = conPixel.BeginTransaction();

                //llamo al proceso de copia de información entre bases de datos Pixel y Oracle
                //CopiarInformacionParaQLikSense(idTabla, conPixel, tranPixel, conOracle, tranOracle);

                conexiones.CommitAndCloseTransaction(tranPixel);
                //conexiones.CommitAndCloseTransaction(tranOracle);
            }
            catch (Exception ex)
            {
                conexiones.RollbackAndCloseTransaction(tranPixel);
                //conexiones.RollbackAndCloseTransaction(tranOracle);
                throw ex;
            }
        }

        private void CopiarInformacionParaQLikSense(String idTabla, OdbcConnection conPixel, OdbcTransaction tranPixel, OdbcConnection conOracle, OdbcTransaction tranOracle)
        {
            /********************************POSHEADER****************************************/
            //lectura de tablas e inserción entre bases de datos
            OdbcCommand comandoPixel1 = new OdbcCommand(SQL_POSHEADER_SELECT, conPixel);
            comandoPixel1.Transaction = tranPixel;
            comandoPixel1.Parameters.AddWithValue("@idTabla", idTabla);
            OdbcDataReader dataReaderPixel1 = comandoPixel1.ExecuteReader();

            //lectura en Pixel, inserción oracle
            OdbcCommand comandoOracle1 = new OdbcCommand(SQL_POSHEADER_INSERT, conOracle);
            comandoOracle1.Transaction = tranOracle;
            if (dataReaderPixel1.HasRows)
            {
                while (dataReaderPixel1.Read())
                {
                    comandoOracle1.Parameters.Clear();
                    Console.WriteLine(dataReaderPixel1.GetValue(0));
                    comandoOracle1.Parameters.AddWithValue(":0", dataReaderPixel1.GetValue(0));
                    comandoOracle1.Parameters.AddWithValue(":1", dataReaderPixel1.GetValue(1));
                    comandoOracle1.Parameters.AddWithValue(":2", dataReaderPixel1.GetValue(2));
                    comandoOracle1.Parameters.AddWithValue(":3", dataReaderPixel1.GetValue(3));
                    comandoOracle1.Parameters.AddWithValue(":4", dataReaderPixel1.GetValue(4));
                    comandoOracle1.Parameters.AddWithValue(":5", dataReaderPixel1.GetValue(5));
                    comandoOracle1.Parameters.AddWithValue(":6", dataReaderPixel1.GetValue(6));
                    comandoOracle1.Parameters.AddWithValue(":7", dataReaderPixel1.GetValue(7));
                    comandoOracle1.Parameters.AddWithValue(":8", dataReaderPixel1.GetValue(8));
                    comandoOracle1.Parameters.AddWithValue(":9", dataReaderPixel1.GetValue(9));
                    comandoOracle1.Parameters.AddWithValue(":10", dataReaderPixel1.GetValue(10));
                    comandoOracle1.Parameters.AddWithValue(":11", dataReaderPixel1.GetValue(11));
                    comandoOracle1.Parameters.AddWithValue(":12", dataReaderPixel1.GetValue(12));
                    comandoOracle1.Parameters.AddWithValue(":13", dataReaderPixel1.GetValue(13));
                    comandoOracle1.Parameters.AddWithValue(":14", dataReaderPixel1.GetValue(14));
                    comandoOracle1.Parameters.AddWithValue(":15", dataReaderPixel1.GetValue(15));
                    comandoOracle1.Parameters.AddWithValue(":16", dataReaderPixel1.GetValue(16));
                    comandoOracle1.Parameters.AddWithValue(":17", dataReaderPixel1.GetValue(17));
                    comandoOracle1.Parameters.AddWithValue(":18", dataReaderPixel1.GetValue(18));
                    comandoOracle1.Parameters.AddWithValue(":19", dataReaderPixel1.GetValue(19));
                    comandoOracle1.Parameters.AddWithValue(":20", dataReaderPixel1.GetValue(20));
                    comandoOracle1.Parameters.AddWithValue(":21", dataReaderPixel1.GetValue(21));
                    comandoOracle1.Parameters.AddWithValue(":22", dataReaderPixel1.GetValue(22));
                    comandoOracle1.Parameters.AddWithValue(":23", dataReaderPixel1.GetValue(23));
                    comandoOracle1.Parameters.AddWithValue(":24", dataReaderPixel1.GetValue(24));
                    comandoOracle1.Parameters.AddWithValue(":25", dataReaderPixel1.GetValue(25));
                    comandoOracle1.Parameters.AddWithValue(":26", dataReaderPixel1.GetValue(26));
                    comandoOracle1.Parameters.AddWithValue(":27", dataReaderPixel1.GetValue(27));
                    comandoOracle1.Parameters.AddWithValue(":28", dataReaderPixel1.GetValue(28));
                    comandoOracle1.Parameters.AddWithValue(":29", dataReaderPixel1.GetValue(29));
                    comandoOracle1.Parameters.AddWithValue(":30", dataReaderPixel1.GetValue(30));
                    comandoOracle1.Parameters.AddWithValue(":31", dataReaderPixel1.GetValue(31));
                    comandoOracle1.Parameters.AddWithValue(":32", dataReaderPixel1.GetValue(32));
                    comandoOracle1.Parameters.AddWithValue(":33", dataReaderPixel1.GetValue(33));
                    comandoOracle1.Parameters.AddWithValue(":34", dataReaderPixel1.GetValue(34));
                    comandoOracle1.Parameters.AddWithValue(":35", dataReaderPixel1.GetValue(35));
                    comandoOracle1.Parameters.AddWithValue(":36", dataReaderPixel1.GetValue(36));
                    comandoOracle1.Parameters.AddWithValue(":37", dataReaderPixel1.GetValue(37));
                    comandoOracle1.Parameters.AddWithValue(":38", dataReaderPixel1.GetValue(38));
                    comandoOracle1.Parameters.AddWithValue(":39", dataReaderPixel1.GetValue(39));
                    comandoOracle1.Parameters.AddWithValue(":40", dataReaderPixel1.GetValue(40));
                    comandoOracle1.Parameters.AddWithValue(":41", dataReaderPixel1.GetValue(41));
                    comandoOracle1.Parameters.AddWithValue(":42", dataReaderPixel1.GetValue(42));
                    comandoOracle1.Parameters.AddWithValue(":43", dataReaderPixel1.GetValue(43));
                    comandoOracle1.Parameters.AddWithValue(":44", dataReaderPixel1.GetValue(44));
                    comandoOracle1.Parameters.AddWithValue(":45", dataReaderPixel1.GetValue(45));
                    comandoOracle1.Parameters.AddWithValue(":46", dataReaderPixel1.GetValue(46));
                    comandoOracle1.Parameters.AddWithValue(":47", dataReaderPixel1.GetValue(47));
                    comandoOracle1.Parameters.AddWithValue(":48", dataReaderPixel1.GetValue(48));
                    comandoOracle1.Parameters.AddWithValue(":49", dataReaderPixel1.GetValue(49));
                    comandoOracle1.Parameters.AddWithValue(":50", dataReaderPixel1.GetValue(50));
                    comandoOracle1.Parameters.AddWithValue(":51", dataReaderPixel1.GetValue(51));
                    comandoOracle1.Parameters.AddWithValue(":52", dataReaderPixel1.GetValue(52));
                    comandoOracle1.Parameters.AddWithValue(":53", dataReaderPixel1.GetValue(53));
                    comandoOracle1.ExecuteNonQuery();
                }
            }
            dataReaderPixel1.Close();

            /********************************POSDETAIL****************************************/
            //lectura de tablas e inserción entre bases de datos
            OdbcCommand comandoPixel2 = new OdbcCommand(SQL_POSDETAIL_SELECT, conPixel);
            comandoPixel2.Transaction = tranPixel;
            comandoPixel2.Parameters.AddWithValue("@idTabla", idTabla);
            OdbcDataReader dataReaderPixel2 = comandoPixel2.ExecuteReader();

            //lectura en Pixel, inserción oracle
            OdbcCommand comandoOracle2 = new OdbcCommand(SQL_POSDETAIL_INSERT, conOracle);
            comandoOracle2.Transaction = tranOracle;
            if (dataReaderPixel2.HasRows)
            {
                while (dataReaderPixel2.Read())
                {
                    comandoOracle2.Parameters.Clear();
                    Console.WriteLine(dataReaderPixel2.GetValue(0));
                    comandoOracle2.Parameters.AddWithValue(":0", dataReaderPixel2.GetValue(0));
                    comandoOracle2.Parameters.AddWithValue(":1", dataReaderPixel2.GetValue(1));
                    comandoOracle2.Parameters.AddWithValue(":2", dataReaderPixel2.GetValue(2));
                    comandoOracle2.Parameters.AddWithValue(":3", dataReaderPixel2.GetValue(3));
                    comandoOracle2.Parameters.AddWithValue(":4", dataReaderPixel2.GetValue(4));
                    comandoOracle2.Parameters.AddWithValue(":5", dataReaderPixel2.GetValue(5));
                    comandoOracle2.Parameters.AddWithValue(":6", dataReaderPixel2.GetValue(6));
                    comandoOracle2.Parameters.AddWithValue(":7", dataReaderPixel2.GetValue(7));
                    comandoOracle2.Parameters.AddWithValue(":8", dataReaderPixel2.GetValue(8));
                    comandoOracle2.Parameters.AddWithValue(":9", dataReaderPixel2.GetValue(9));
                    comandoOracle2.Parameters.AddWithValue(":10", dataReaderPixel2.GetValue(10));
                    comandoOracle2.Parameters.AddWithValue(":11", dataReaderPixel2.GetValue(11));
                    comandoOracle2.Parameters.AddWithValue(":12", dataReaderPixel2.GetValue(12));
                    comandoOracle2.Parameters.AddWithValue(":13", dataReaderPixel2.GetValue(13));
                    comandoOracle2.Parameters.AddWithValue(":14", dataReaderPixel2.GetValue(14));
                    comandoOracle2.Parameters.AddWithValue(":15", dataReaderPixel2.GetValue(15));
                    comandoOracle2.Parameters.AddWithValue(":16", dataReaderPixel2.GetValue(16));
                    comandoOracle2.Parameters.AddWithValue(":17", dataReaderPixel2.GetValue(17));
                    comandoOracle2.Parameters.AddWithValue(":18", dataReaderPixel2.GetValue(18));
                    comandoOracle2.Parameters.AddWithValue(":19", dataReaderPixel2.GetValue(19));
                    comandoOracle2.Parameters.AddWithValue(":20", dataReaderPixel2.GetValue(20));
                    comandoOracle2.Parameters.AddWithValue(":21", dataReaderPixel2.GetValue(21));
                    comandoOracle2.Parameters.AddWithValue(":22", dataReaderPixel2.GetValue(22));
                    comandoOracle2.Parameters.AddWithValue(":23", dataReaderPixel2.GetValue(23));
                    comandoOracle2.Parameters.AddWithValue(":24", dataReaderPixel2.GetValue(24));
                    comandoOracle2.Parameters.AddWithValue(":25", dataReaderPixel2.GetValue(25));
                    comandoOracle2.Parameters.AddWithValue(":26", dataReaderPixel2.GetValue(26));
                    comandoOracle2.Parameters.AddWithValue(":27", dataReaderPixel2.GetValue(27));
                    comandoOracle2.Parameters.AddWithValue(":28", dataReaderPixel2.GetValue(28));
                    comandoOracle2.Parameters.AddWithValue(":29", dataReaderPixel2.GetValue(29));
                    comandoOracle2.Parameters.AddWithValue(":30", dataReaderPixel2.GetValue(30));
                    comandoOracle2.Parameters.AddWithValue(":31", dataReaderPixel2.GetValue(31));
                    comandoOracle2.Parameters.AddWithValue(":32", dataReaderPixel2.GetValue(32));
                    comandoOracle2.Parameters.AddWithValue(":33", dataReaderPixel2.GetValue(33));
                    comandoOracle2.Parameters.AddWithValue(":34", dataReaderPixel2.GetValue(34));
                    comandoOracle2.Parameters.AddWithValue(":35", dataReaderPixel2.GetValue(35));
                    comandoOracle2.Parameters.AddWithValue(":36", dataReaderPixel2.GetValue(36));
                    comandoOracle2.Parameters.AddWithValue(":37", dataReaderPixel2.GetValue(37));
                    comandoOracle2.Parameters.AddWithValue(":38", dataReaderPixel2.GetValue(38));
                    comandoOracle2.Parameters.AddWithValue(":39", dataReaderPixel2.GetValue(39));
                    comandoOracle2.ExecuteNonQuery();
                }
            }
            dataReaderPixel2.Close();

            /********************************HOWPAID****************************************/
            //lectura de tablas e inserción entre bases de datos
            OdbcCommand comandoPixel3 = new OdbcCommand(SQL_HOWPAID_SELECT, conPixel);
            comandoPixel3.Transaction = tranPixel;
            comandoPixel3.Parameters.AddWithValue("@idTabla", idTabla);
            OdbcDataReader dataReaderPixel3 = comandoPixel3.ExecuteReader();

            //lectura en Pixel, inserción oracle
            OdbcCommand comandoOracle3 = new OdbcCommand(SQL_HOWPAID_INSERT, conOracle);
            comandoOracle3.Transaction = tranOracle;
            if (dataReaderPixel3.HasRows)
            {
                while (dataReaderPixel3.Read())
                {
                    comandoOracle3.Parameters.Clear();
                    Console.WriteLine(dataReaderPixel3.GetValue(0));
                    comandoOracle3.Parameters.AddWithValue(":0", dataReaderPixel3.GetValue(0));
                    comandoOracle3.Parameters.AddWithValue(":1", dataReaderPixel3.GetValue(1));
                    comandoOracle3.Parameters.AddWithValue(":2", dataReaderPixel3.GetValue(2));
                    comandoOracle3.Parameters.AddWithValue(":3", dataReaderPixel3.GetValue(3));
                    comandoOracle3.Parameters.AddWithValue(":4", dataReaderPixel3.GetValue(4));
                    comandoOracle3.Parameters.AddWithValue(":5", dataReaderPixel3.GetValue(5));
                    comandoOracle3.Parameters.AddWithValue(":6", dataReaderPixel3.GetValue(6));
                    comandoOracle3.Parameters.AddWithValue(":7", dataReaderPixel3.GetValue(7));
                    comandoOracle3.Parameters.AddWithValue(":8", dataReaderPixel3.GetValue(8));
                    comandoOracle3.Parameters.AddWithValue(":9", dataReaderPixel3.GetValue(9));
                    comandoOracle3.Parameters.AddWithValue(":10", dataReaderPixel3.GetValue(10));
                    comandoOracle3.Parameters.AddWithValue(":11", dataReaderPixel3.GetValue(11));
                    comandoOracle3.Parameters.AddWithValue(":12", dataReaderPixel3.GetValue(12));
                    comandoOracle3.Parameters.AddWithValue(":13", dataReaderPixel3.GetValue(13));
                    comandoOracle3.Parameters.AddWithValue(":14", dataReaderPixel3.GetValue(14));
                    comandoOracle3.Parameters.AddWithValue(":15", dataReaderPixel3.GetValue(15));
                    comandoOracle3.Parameters.AddWithValue(":16", dataReaderPixel3.GetValue(16));
                    comandoOracle3.Parameters.AddWithValue(":17", dataReaderPixel3.GetValue(17));
                    comandoOracle3.Parameters.AddWithValue(":18", dataReaderPixel3.GetValue(18));
                    comandoOracle3.Parameters.AddWithValue(":19", dataReaderPixel3.GetValue(19));
                    comandoOracle3.Parameters.AddWithValue(":20", dataReaderPixel3.GetValue(20));
                    comandoOracle3.Parameters.AddWithValue(":21", dataReaderPixel3.GetValue(21));
                    comandoOracle3.Parameters.AddWithValue(":22", dataReaderPixel3.GetValue(22));
                    comandoOracle3.Parameters.AddWithValue(":23", dataReaderPixel3.GetValue(23));
                    comandoOracle3.Parameters.AddWithValue(":24", dataReaderPixel3.GetValue(24));
                    comandoOracle3.Parameters.AddWithValue(":25", dataReaderPixel3.GetValue(25));
                    comandoOracle3.Parameters.AddWithValue(":26", dataReaderPixel3.GetValue(26));
                    comandoOracle3.Parameters.AddWithValue(":27", dataReaderPixel3.GetValue(27));
                    comandoOracle3.Parameters.AddWithValue(":28", dataReaderPixel3.GetValue(28));
                    comandoOracle3.ExecuteNonQuery();
                }
            }
            dataReaderPixel3.Close();

            /********************************POSBANK****************************************/
            //lectura de tablas e inserción entre bases de datos
            OdbcCommand comandoPixel4 = new OdbcCommand(SQL_POSBANK_SELECT, conPixel);
            comandoPixel4.Transaction = tranPixel;
            comandoPixel4.Parameters.AddWithValue("@idTabla", idTabla);
            OdbcDataReader dataReaderPixel4 = comandoPixel4.ExecuteReader();

            //lectura en Pixel, inserción oracle
            OdbcCommand comandoOracle4 = new OdbcCommand(SQL_POSBANK_INSERT, conOracle);
            comandoOracle4.Transaction = tranOracle;
            if (dataReaderPixel4.HasRows)
            {
                while (dataReaderPixel4.Read())
                {
                    comandoOracle4.Parameters.Clear();
                    Console.WriteLine(dataReaderPixel4.GetValue(0));
                    comandoOracle4.Parameters.AddWithValue(":0", dataReaderPixel4.GetValue(0));
                    comandoOracle4.Parameters.AddWithValue(":1", dataReaderPixel4.GetValue(1));
                    comandoOracle4.Parameters.AddWithValue(":2", dataReaderPixel4.GetValue(2));
                    comandoOracle4.Parameters.AddWithValue(":3", dataReaderPixel4.GetValue(3));
                    comandoOracle4.Parameters.AddWithValue(":4", dataReaderPixel4.GetValue(4));
                    comandoOracle4.Parameters.AddWithValue(":5", dataReaderPixel4.GetValue(5));
                    comandoOracle4.Parameters.AddWithValue(":6", dataReaderPixel4.GetValue(6));
                    comandoOracle4.Parameters.AddWithValue(":7", dataReaderPixel4.GetValue(7));
                    comandoOracle4.Parameters.AddWithValue(":8", dataReaderPixel4.GetValue(8));
                    comandoOracle4.Parameters.AddWithValue(":9", dataReaderPixel4.GetValue(9));
                    comandoOracle4.Parameters.AddWithValue(":10", dataReaderPixel4.GetValue(10));
                    comandoOracle4.Parameters.AddWithValue(":11", dataReaderPixel4.GetValue(11));
                    comandoOracle4.Parameters.AddWithValue(":12", dataReaderPixel4.GetValue(12));
                    comandoOracle4.Parameters.AddWithValue(":13", dataReaderPixel4.GetValue(13));
                    comandoOracle4.Parameters.AddWithValue(":14", dataReaderPixel4.GetValue(14));
                    comandoOracle4.Parameters.AddWithValue(":15", dataReaderPixel4.GetValue(15));
                    comandoOracle4.Parameters.AddWithValue(":16", dataReaderPixel4.GetValue(16));
                    comandoOracle4.Parameters.AddWithValue(":17", dataReaderPixel4.GetValue(17));
                    comandoOracle4.Parameters.AddWithValue(":18", dataReaderPixel4.GetValue(18));
                    comandoOracle4.Parameters.AddWithValue(":19", dataReaderPixel4.GetValue(19));
                    comandoOracle4.Parameters.AddWithValue(":20", dataReaderPixel4.GetValue(20));
                    comandoOracle4.ExecuteNonQuery();
                }
            }
            dataReaderPixel4.Close();

            /********************************CASHOUT****************************************/
            //lectura de tablas e inserción entre bases de datos
            OdbcCommand comandoPixel5 = new OdbcCommand(SQL_CASHOUT_SELECT, conPixel);
            comandoPixel5.Transaction = tranPixel;
            comandoPixel5.Parameters.AddWithValue("@idTabla", idTabla);
            OdbcDataReader dataReaderPixel5 = comandoPixel5.ExecuteReader();

            //lectura en Pixel, inserción oracle
            OdbcCommand comandoOracle5 = new OdbcCommand(SQL_CASHOUT_INSERT, conOracle);
            comandoOracle5.Transaction = tranOracle;
            if (dataReaderPixel5.HasRows)
            {
                while (dataReaderPixel5.Read())
                {
                    comandoOracle5.Parameters.Clear();
                    Console.WriteLine(dataReaderPixel5.GetValue(0));
                    comandoOracle5.Parameters.AddWithValue(":0", dataReaderPixel5.GetValue(0));
                    comandoOracle5.Parameters.AddWithValue(":1", dataReaderPixel5.GetValue(1));
                    comandoOracle5.Parameters.AddWithValue(":2", dataReaderPixel5.GetValue(2));
                    comandoOracle5.Parameters.AddWithValue(":3", dataReaderPixel5.GetValue(3));
                    comandoOracle5.Parameters.AddWithValue(":4", dataReaderPixel5.GetValue(4));
                    comandoOracle5.Parameters.AddWithValue(":5", dataReaderPixel5.GetValue(5));
                    comandoOracle5.Parameters.AddWithValue(":6", dataReaderPixel5.GetValue(6));
                    comandoOracle5.Parameters.AddWithValue(":7", dataReaderPixel5.GetValue(7));
                    comandoOracle5.Parameters.AddWithValue(":8", dataReaderPixel5.GetValue(8));
                    comandoOracle5.Parameters.AddWithValue(":9", dataReaderPixel5.GetValue(9));
                    comandoOracle5.Parameters.AddWithValue(":10", dataReaderPixel5.GetValue(10));
                    comandoOracle5.Parameters.AddWithValue(":11", dataReaderPixel5.GetValue(11));
                    comandoOracle5.Parameters.AddWithValue(":12", dataReaderPixel5.GetValue(12));
                    comandoOracle5.Parameters.AddWithValue(":13", dataReaderPixel5.GetValue(13));
                    comandoOracle5.Parameters.AddWithValue(":14", dataReaderPixel5.GetValue(14));
                    comandoOracle5.Parameters.AddWithValue(":15", dataReaderPixel5.GetValue(15));
                    comandoOracle5.Parameters.AddWithValue(":16", dataReaderPixel5.GetValue(16));
                    comandoOracle5.Parameters.AddWithValue(":17", dataReaderPixel5.GetValue(17));
                    comandoOracle5.Parameters.AddWithValue(":18", dataReaderPixel5.GetValue(18));
                    comandoOracle5.Parameters.AddWithValue(":19", dataReaderPixel5.GetValue(19));
                    comandoOracle5.Parameters.AddWithValue(":20", dataReaderPixel5.GetValue(20));
                    comandoOracle5.Parameters.AddWithValue(":21", dataReaderPixel5.GetValue(21));
                    comandoOracle5.Parameters.AddWithValue(":22", dataReaderPixel5.GetValue(22));
                    comandoOracle5.Parameters.AddWithValue(":23", dataReaderPixel5.GetValue(23));
                    comandoOracle5.ExecuteNonQuery();
                }
            }
            dataReaderPixel5.Close();

            /********************************CASHOUTDETAIL****************************************/
            //lectura de tablas e inserción entre bases de datos
            OdbcCommand comandoPixel6 = new OdbcCommand(SQL_CASHOUTDETAIL_SELECT, conPixel);
            comandoPixel6.Transaction = tranPixel;
            comandoPixel6.Parameters.AddWithValue("@idTabla", idTabla);
            OdbcDataReader dataReaderPixel6 = comandoPixel6.ExecuteReader();

            //lectura en Pixel, inserción oracle
            OdbcCommand comandoOracle6 = new OdbcCommand(SQL_CASHOUTDETAIL_INSERT, conOracle);
            comandoOracle6.Transaction = tranOracle;
            if (dataReaderPixel6.HasRows)
            {
                while (dataReaderPixel6.Read())
                {
                    comandoOracle6.Parameters.Clear();
                    Console.WriteLine(dataReaderPixel6.GetValue(0));
                    comandoOracle6.Parameters.AddWithValue(":0", dataReaderPixel6.GetValue(0));
                    comandoOracle6.Parameters.AddWithValue(":1", dataReaderPixel6.GetValue(1));
                    comandoOracle6.Parameters.AddWithValue(":2", dataReaderPixel6.GetValue(2));
                    comandoOracle6.Parameters.AddWithValue(":3", dataReaderPixel6.GetValue(3));
                    comandoOracle6.Parameters.AddWithValue(":4", dataReaderPixel6.GetValue(4));
                    comandoOracle6.Parameters.AddWithValue(":5", dataReaderPixel6.GetValue(5));
                    comandoOracle6.Parameters.AddWithValue(":6", dataReaderPixel6.GetValue(6));
                    comandoOracle6.Parameters.AddWithValue(":7", dataReaderPixel6.GetValue(7));
                    comandoOracle6.Parameters.AddWithValue(":8", dataReaderPixel6.GetValue(8));
                    comandoOracle6.Parameters.AddWithValue(":9", dataReaderPixel6.GetValue(9));
                    comandoOracle6.Parameters.AddWithValue(":10", dataReaderPixel6.GetValue(10));
                    comandoOracle6.Parameters.AddWithValue(":11", dataReaderPixel6.GetValue(11));
                    comandoOracle6.Parameters.AddWithValue(":12", dataReaderPixel6.GetValue(12));
                    comandoOracle6.Parameters.AddWithValue(":13", dataReaderPixel6.GetValue(13));
                    comandoOracle6.Parameters.AddWithValue(":14", dataReaderPixel6.GetValue(14));
                    comandoOracle6.Parameters.AddWithValue(":15", dataReaderPixel6.GetValue(15));
                    comandoOracle6.Parameters.AddWithValue(":16", dataReaderPixel6.GetValue(16));
                    comandoOracle6.Parameters.AddWithValue(":17", dataReaderPixel6.GetValue(17));
                    comandoOracle6.Parameters.AddWithValue(":18", dataReaderPixel6.GetValue(18));
                    comandoOracle6.Parameters.AddWithValue(":19", dataReaderPixel6.GetValue(19));
                    comandoOracle6.ExecuteNonQuery();
                }
            }
            dataReaderPixel6.Close();
        }

        public decimal DescuentoFaltanteGetTotal(string openDate)
        {
            decimal retorno = 0;
            OdbcConnection conPixel = null;
            try
            {
                //Abro la conexión a Pixel
                conPixel = conexiones.GetOdbcConnectionPixel();

                //llamo el procedimiento 2
                OdbcCommand comandoPixel1 = new OdbcCommand("SELECT CreditSum FROM (SELECT(ISNULL(SUM(CO.OverShort), 0)) * -1 AS CreditSum FROM DBA.CashOut CO WHERE CO.OpenDate = ? ) XX WHERE XX.CreditSum >= 0.01", conPixel);
                comandoPixel1.Parameters.AddWithValue("openDate", openDate);

                if (comandoPixel1.ExecuteScalar() != null)
                {
                    retorno = Decimal.Parse(comandoPixel1.ExecuteScalar().ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexiones.CloseConnection(conPixel);
            }
            return retorno;
        }

        public List<Documento> DocumentosFindAllPendientes()
        {
            List<Documento> retorno = new List<Documento>();
            OdbcConnection conPixel = null;
            OdbcDataReader readerDatos = null;
            try
            {
                //Abro la conexión a Pixel
                conPixel = conexiones.GetOdbcConnectionPixel();

                //llamo el procedimiento 2
                OdbcCommand comandoPixel1 = new OdbcCommand("SELECT DI.UId,DI.OPENDATE,(SELECT COUNT(*) FROM DBA.POSHEADER PH WHERE PH.OPENDATE=DI.OPENDATE AND PH.TRANSACT IN (SELECT PD.TRANSACT FROM DBA.POSDETAIL PD WHERE PD.PRODTYPE NOT IN(100,101) AND PD.COSTEACH>0) and PH.REFID <> 0) FROM DBA.Dayinfo DI WHERE DI.UID NOT IN(SELECT DBA.ACK_ESTADOS.ID_TABLA FROM DBA.ACK_ESTADOS WHERE DBA.ACK_ESTADOS.TIPO='FACTURACION ELECTRONICA') AND DI.TIMEEND IS NOT NULL ORDER BY 2 DESC", conPixel);
                //OdbcCommand comandoPixel1 = new OdbcCommand("SELECT DI.UId,DI.OPENDATE,(SELECT COUNT(*) FROM DBA.POSHEADER PH WHERE PH.OPENDATE=DI.OPENDATE AND PH.TRANSACT IN (SELECT PD.TRANSACT FROM DBA.POSDETAIL PD WHERE PD.PRODTYPE NOT IN(100,101) AND PD.COSTEACH>0) and PH.REFID <> 0) TOTAL FROM DBA.Dayinfo DI WHERE DI.TIMEEND IS NOT NULL ORDER BY 2 DESC", conPixel);
                readerDatos = comandoPixel1.ExecuteReader();
                if (readerDatos.HasRows)
                {
                    while (readerDatos.Read())
                    {
                        Documento documento = new Documento();
                        documento.UId = readerDatos.GetString(0);
                        documento.OPENDATE = readerDatos.GetString(1);
                        documento.TOTAL = readerDatos.GetInt32(2);
                        retorno.Add(documento);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexiones.CloseConnection(conPixel);
            }
            return retorno;
        }


        public void EjecutarProcesoFE(String idTabla, String transaccionCodigo)
        {
            try
            {
                //saco un listado de facturas con las que generaré los XML
                List<Factura> listaFacturas = GenerarListaFacturas(idTabla, transaccionCodigo);
                //se elimina, genera, y mueve los archivos tanto a directorios locales como remotos
                new Transformador().ProcesarArchivos(listaFacturas);
                //genero estado si todo va bien
                CrearEstadoFE(idTabla, transaccionCodigo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CrearEstadoFE(String idTabla, String transaccionCodigo)
        {
            if (transaccionCodigo == null)
            {
                OdbcConnection conPixel = null;
                OdbcTransaction tranPixel = null;
                try
                {
                    //OdbcConnection conBI = conexiones.getOdbcConnectionBI();

                    //Abro la conexión a Pixel
                    conPixel = conexiones.GetOdbcConnectionPixel();

                    //Inicio la transacción
                    tranPixel = conPixel.BeginTransaction();
                    //llamo el procedimiento 2
                    OdbcCommand comandoPixel2 = new OdbcCommand("INSERT INTO DBA.ACK_ESTADOS(ID_EST,ID_TABLA,TIPO)VALUES((select coalesce(max(aa1.ID_EST),0)+1 from dba.ACK_ESTADOS aa1), ?, 'FACTURACION ELECTRONICA')", conPixel);
                    comandoPixel2.Transaction = tranPixel;
                    comandoPixel2.Parameters.AddWithValue("@idTabla", idTabla);
                    comandoPixel2.ExecuteNonQuery();

                    //confirmo la transacción
                    conexiones.CommitAndCloseTransaction(tranPixel);
                }
                catch (Exception ex)
                {
                    conexiones.RollbackAndCloseTransaction(tranPixel);
                    throw ex;
                }
            }
        }

        private String GetConfigProp(String key)
        {
            return ConfigurationManager.AppSettings[key].ToString();
        }

        private List<Factura> GenerarListaFacturas(String idTabla, String transaccionCodigo)
        {
            List<Factura> retorno = new List<Factura>();
            OdbcConnection conPixel = null;
            OdbcDataReader readerDatos = null;
            try
            {
                //Abro la conexión a Pixel
                conPixel = conexiones.GetOdbcConnectionPixel();

                //listo las facturas
                OdbcCommand comandoPixel = new OdbcCommand("SELECT SI.Fax AS RUC,SI.Owner AS NOM_COMERCIAL,SI.Contact AS NOM_LOCAL,SI.Email AS DIR_MATRIZ,SI.Website AS DIR_LOCAL,SI.City AS OBL_CONTA,SI.Country AS PUNTO_EMI,SI.Postal AS CONT_ESPECIAL,SI.Prov AS NUM_LOCAL,SI.Phone AS GENERA_XML,SI.Address1 AS PATH_LOCAL,SI.Address2 AS PATH_SERVER,SI.RptComment AS PATH_RUTAS,(SELECT DBA.PARAMETROS_DLL.AMBIENTE_FE FROM DBA.PARAMETROS_DLL),SI.ColaborationURL FROM DBA.Sysinfo SI", conPixel);
                comandoPixel.ExecuteNonQuery();
                readerDatos = comandoPixel.ExecuteReader();


                Factura facturaDatosGenerales = new Factura();
                facturaDatosGenerales.infoTributaria = new Factura.InfoTributaria();
                facturaDatosGenerales.infoFactura = new Factura.InfoFactura();
                if (readerDatos.HasRows)
                {
                    while (readerDatos.Read())
                    {
                        facturaDatosGenerales.infoTributaria.ruc = readerDatos.GetString(0);
                        facturaDatosGenerales.infoTributaria.nombreComercial = readerDatos.GetString(2);
                        facturaDatosGenerales.infoTributaria.razonSocial = readerDatos.GetString(1);
                        facturaDatosGenerales.infoTributaria.dirMatriz = readerDatos.GetString(3);
                        facturaDatosGenerales.infoFactura.dirEstablecimiento = readerDatos.GetString(4);
                        facturaDatosGenerales.infoFactura.obligadoContabilidad = readerDatos.GetString(5);
                        facturaDatosGenerales.infoTributaria.ptoEmi = readerDatos.GetString(6);
                        facturaDatosGenerales.infoFactura.contribuyenteEspecial = readerDatos.GetString(7);
                        facturaDatosGenerales.infoTributaria.estab = readerDatos.GetString(8);
                        facturaDatosGenerales.infoTributaria.ambiente = readerDatos.GetString(13);
                        facturaDatosGenerales.infoTributaria.codDoc = GetConfigProp("FacturaCodDoc");
                        facturaDatosGenerales.infoTributaria.tipoEmision = GetConfigProp("FacturaTipoEmision");
                    }
                }
                conexiones.CloseDataReader(readerDatos);

                //listo las facturas
                if (transaccionCodigo == null)
                {
                    comandoPixel = new OdbcCommand("SELECT CA.TaxNumber, SUBSTRING(PH.REFID, 7, 9), convert(char(10),PH.TIMEEND,103),(CASE WHEN trim(cast(M.POSTAL as varchar)) IS NULL OR trim(cast(M.POSTAL as varchar))='0' THEN '06' WHEN LENGTH(trim(cast(M.POSTAL as varchar)))<=1 THEN '0' || trim(cast(M.POSTAL as varchar)) END) AS T_DOC,M.FIRSTNAME || ' ' || M.LASTNAME,M.ADRESS2,trim(str(round(PH.NETTOTAL,2),16,2)),trim(str(round((case when (truncnum(ph.NETTOTAL,2)-truncnum(ph.TAX1ABLE,2)) not between -0.02 and 0.02 then round(ph.NETTOTAL,2)-round(ph.TAX1ABLE,2) else 0 end), 2), 16, 2)) AS IVA_0,trim(str(round(PH.TAX1ABLE,2),16,2)),trim(str(round(PH.TAX1,2),16,2)),trim(str(round((CASE WHEN (SELECT DBA.Sysinfo.TAXRATE3 FROM DBA.Sysinfo) = 10 THEN PH.TAX3ABLE ELSE PH.TAX2ABLE END),2),16,2)) AS SERVICIO,trim(str(round(PH.FINALTOTAL,2),16,2)),(CASE WHEN M.EMAIL IS NULL OR LENGTH(M.EMAIL)=0 THEN (SELECT dba.Sysinfo.Address2 FROM DBA.Sysinfo)ELSE M.EMAIL END) AS CORREO,EM.POSNAME,cast(PH.TRANSACT as varchar) codigo,CONVERT(CHAR(10),PH.TIMEEND,111),(CASE WHEN LENGTH(M.ADRESS1)= 0 OR M.ADRESS1 IS NULL THEN 'NINGUNA' ELSE M.ADRESS1 END) direccion FROM DBA.POSHEADER PH INNER JOIN DBA.Member M ON  PH.MemCode=M.MEMCODE INNER JOIN DBA.employee EM ON PH.WHOCLOSE=EM.EMPNUM INNER JOIN DBA.TaxExemptNumbers CA ON PH.TRANSACT= CA.TRANSACT WHERE (PH.OPENDATE = ? And PH.REFID <> 0) AND PH.TRANSACT IN(SELECT PD.TRANSACT FROM DBA.POSDETAIL PD WHERE PD.PRODTYPE NOT IN(101) AND PD.COSTEACH>0) ORDER BY PH.REFID", conPixel);
                    comandoPixel.Parameters.AddWithValue("@idTabla", idTabla);
                }
                else
                {
                    comandoPixel = new OdbcCommand("SELECT CA.TaxNumber, SUBSTRING(PH.REFID, 7, 9), convert(char(10),PH.TIMEEND,103),(CASE WHEN trim(cast(M.POSTAL as varchar)) IS NULL OR trim(cast(M.POSTAL as varchar))='0' THEN '06' WHEN LENGTH(trim(cast(M.POSTAL as varchar)))<=1 THEN '0' || trim(cast(M.POSTAL as varchar)) END) AS T_DOC,M.FIRSTNAME || ' ' || M.LASTNAME,M.ADRESS2,trim(str(round(PH.NETTOTAL,2),16,2)),trim(str(round((case when (truncnum(ph.NETTOTAL,2)-truncnum(ph.TAX1ABLE,2)) not between -0.02 and 0.02 then round(ph.NETTOTAL,2)-round(ph.TAX1ABLE,2) else 0 end), 2), 16, 2)) AS IVA_0,trim(str(round(PH.TAX1ABLE,2),16,2)),trim(str(round(PH.TAX1,2),16,2)),trim(str(round((CASE WHEN (SELECT DBA.Sysinfo.TAXRATE3 FROM DBA.Sysinfo) = 10 THEN PH.TAX3ABLE ELSE PH.TAX2ABLE END),2),16,2)) AS SERVICIO,trim(str(round(PH.FINALTOTAL,2),16,2)),(CASE WHEN M.EMAIL IS NULL OR LENGTH(M.EMAIL)=0 THEN (SELECT dba.Sysinfo.Address2 FROM DBA.Sysinfo)ELSE M.EMAIL END) AS CORREO,EM.POSNAME,cast(PH.TRANSACT as varchar) codigo,CONVERT(CHAR(10),PH.TIMEEND,111),(CASE WHEN LENGTH(M.ADRESS1)= 0 OR M.ADRESS1 IS NULL THEN 'NINGUNA' ELSE M.ADRESS1 END) direccion FROM DBA.POSHEADER PH INNER JOIN DBA.Member M ON  PH.MemCode=M.MEMCODE INNER JOIN DBA.employee EM ON PH.WHOCLOSE=EM.EMPNUM INNER JOIN DBA.TaxExemptNumbers CA ON PH.TRANSACT= CA.TRANSACT WHERE (PH.Transact = ? And PH.REFID <> 0) AND PH.TRANSACT IN(SELECT PD.TRANSACT FROM DBA.POSDETAIL PD WHERE PD.PRODTYPE NOT IN(101) AND PD.COSTEACH>0) ORDER BY PH.REFID", conPixel);
                    comandoPixel.Parameters.AddWithValue("@transaccionCodigo", transaccionCodigo);
                }
                comandoPixel.ExecuteNonQuery();

                readerDatos = comandoPixel.ExecuteReader();
                if (readerDatos.HasRows)
                {
                    while (readerDatos.Read())
                    {
                        Console.WriteLine(readerDatos.GetString(1));
                        Console.WriteLine(readerDatos.GetString(14));
                        //Defino los datos generales
                        Factura factura = new Factura();
                        factura.id = GetConfigProp("FacturaId");
                        factura.version = GetConfigProp("FacturaVersion");
                        factura.infoTributaria = new Factura.InfoTributaria();
                        factura.infoFactura = new Factura.InfoFactura();
                        factura.detalles = new Factura.Detalles();
                        factura.infoAdicional = new Factura.InfoAdicional();

                        factura.infoTributaria.ruc = facturaDatosGenerales.infoTributaria.ruc;
                        factura.infoTributaria.nombreComercial = facturaDatosGenerales.infoTributaria.nombreComercial;
                        factura.infoTributaria.razonSocial = facturaDatosGenerales.infoTributaria.razonSocial;
                        factura.infoTributaria.dirMatriz = facturaDatosGenerales.infoTributaria.dirMatriz;
                        factura.infoFactura.dirEstablecimiento = facturaDatosGenerales.infoFactura.dirEstablecimiento;
                        factura.infoFactura.obligadoContabilidad = facturaDatosGenerales.infoFactura.obligadoContabilidad;
                        factura.infoTributaria.ptoEmi = facturaDatosGenerales.infoTributaria.ptoEmi;
                        factura.infoFactura.contribuyenteEspecial = facturaDatosGenerales.infoFactura.contribuyenteEspecial;
                        factura.infoTributaria.estab = facturaDatosGenerales.infoTributaria.estab;
                        factura.infoTributaria.ambiente = facturaDatosGenerales.infoTributaria.ambiente;
                        factura.infoTributaria.codDoc = facturaDatosGenerales.infoTributaria.codDoc;
                        factura.infoTributaria.tipoEmision = facturaDatosGenerales.infoTributaria.tipoEmision;

                        factura.infoTributaria.claveAcceso = readerDatos.GetString(0);
                        factura.infoTributaria.secuencial = readerDatos.GetString(1);
                        factura.infoFactura.fechaEmision = readerDatos.GetString(2);
                        factura.infoFactura.tipoIdentificacionComprador = readerDatos.GetString(3);
                        factura.infoFactura.razonSocialComprador = readerDatos.GetString(4);
                        factura.infoFactura.identificacionComprador = readerDatos.GetString(5);
                        factura.infoFactura.totalSinImpuestos = readerDatos.GetString(6);

                        //total con impuestos 0
                        Factura.InfoFactura.TotalConImpuestos.TotalImpuesto totalImpuesto0 = new Factura.InfoFactura.TotalConImpuestos.TotalImpuesto();
                        totalImpuesto0.codigo = GetConfigProp("FacturaImpuestos0Codigo");
                        totalImpuesto0.codigoPorcentaje = GetConfigProp("FacturaImpuestos0CodigoPorcentaje");
                        totalImpuesto0.descuentoAdicional = GetConfigProp("FacturaImpuestos0DescuentoAdicional");
                        totalImpuesto0.baseImponible = readerDatos.GetString(7);
                        totalImpuesto0.valor = GetConfigProp("FacturaImpuestos0Valor");

                        //total con impuestos 12
                        Factura.InfoFactura.TotalConImpuestos.TotalImpuesto totalImpuesto12 = new Factura.InfoFactura.TotalConImpuestos.TotalImpuesto();
                        totalImpuesto12.codigo = GetConfigProp("FacturaImpuestos12Codigo");
                        totalImpuesto12.codigoPorcentaje = GetConfigProp("FacturaImpuestos12CodigoPorcentaje");
                        totalImpuesto12.descuentoAdicional = GetConfigProp("FacturaImpuestos12DescuentoAdicional");
                        totalImpuesto12.baseImponible = readerDatos.GetString(8);
                        totalImpuesto12.valor = readerDatos.GetString(9);

                        //inicializo objetos internos relacionados a impuestos, para evitar obtener error de valores nulos
                        factura.infoFactura.totalConImpuestos = new Factura.InfoFactura.TotalConImpuestos();
                        factura.infoFactura.totalConImpuestos.totalImpuesto = new List<Factura.InfoFactura.TotalConImpuestos.TotalImpuesto>();

                        factura.infoFactura.totalConImpuestos.totalImpuesto.Add(totalImpuesto0);
                        factura.infoFactura.totalConImpuestos.totalImpuesto.Add(totalImpuesto12);

                        factura.infoFactura.propina = readerDatos.GetString(10);
                        factura.infoFactura.importeTotal = readerDatos.GetString(11);
                        factura.infoFactura.moneda = GetConfigProp("FacturaMoneda");
                        factura.infoFactura.direccionComprador = readerDatos.GetString(16);

                        //agrego formas de pago
                        List<Factura.InfoFactura.Pagos.Pago> pagos = new List<Factura.InfoFactura.Pagos.Pago>();


                        //inicializo objetos de pagos para evitar errores de nulos
                        factura.infoFactura.pagos = new Factura.InfoFactura.Pagos();
                        factura.infoFactura.pagos.pago = new List<Factura.InfoFactura.Pagos.Pago>();

                        //listo las formas de pago

                        OdbcCommand comandoPixelFP = new OdbcCommand("SELECT trim(str(round(SUM(HP.TENDER),2),16,2)),(replicate('0', (2 - char_length(MP.AccountCode))) || MP.AccountCode) FROM DBA.Howpaid HP INNER JOIN DBA.MethodPay MP ON  HP.METHODNUM=MP.METHODNUM WHERE HP.TRANSACT=? GROUP BY (replicate('0', (2 - char_length(MP.AccountCode))) || MP.AccountCode)", conPixel);
                        OdbcDataReader readerDatosFP = null;
                        comandoPixelFP.Parameters.AddWithValue("@idTransaccion", readerDatos.GetString(14));
                        //comandoPixelFP.ExecuteNonQuery();
                        readerDatosFP = comandoPixelFP.ExecuteReader();
                        if (readerDatosFP.HasRows)
                        {
                            while (readerDatosFP.Read())
                            {
                                Factura.InfoFactura.Pagos.Pago pago = new Factura.InfoFactura.Pagos.Pago();
                                pago.formaPago = readerDatosFP.GetString(1);
                                pago.total = readerDatosFP.GetString(0);
                                pago.plazo = GetConfigProp("FacturaPagoPlazo");
                                pago.unidadTiempo = GetConfigProp("FacturaPagoUnidadTiempo");
                                factura.infoFactura.pagos.pago.Add(pago);
                            }
                        }
                        readerDatosFP.Close();

                        //listo los detalles de la factura
                        OdbcCommand comandoPixelDF = new OdbcCommand("SELECT PD.PRODNUM AS ID_PROD,(CASE WHEN PD.LINEDES IS NOT NULL OR LENGTH(PD.LINEDES)>0 THEN PD.LINEDES ELSE PR.PRINTDES END) AS PRODUCTO,trim(str(round(PD.QUAN,2),16,2)) AS CANTIDAD,trim(str(round(PD.COSTEACH,2),16,2)) AS P_UNITARIO,trim(str(round((CASE WHEN (ISNULL(PD.DISCOUNT,0))<0 THEN (ISNULL(PD.DISCOUNT,0))*-1 ELSE (ISNULL(PD.DISCOUNT,0)) END),2),16,2)) AS DESCUENTO,trim(str(round((PD.QUAN * PD.COSTEACH) - DESCUENTO,2),16,2)) AS  P_TOTAL_SIN_IMPUESTOS,(CASE WHEN PD.APPLYTAX1=1 THEN 2 ELSE 0 END) AS CODIGO_PORCENTAJE,(CASE WHEN CODIGO_PORCENTAJE=2 THEN 12 ELSE 0 END) AS TARIFA,trim(str(round(P_TOTAL_SIN_IMPUESTOS,2),16,2)) AS BASE_IMPONIBLE,trim(str(round((P_TOTAL_SIN_IMPUESTOS * TARIFA)/100,2),16,2)) AS VALOR FROM DBA.POSDETAIL PD INNER JOIN DBA.PRODUCT PR ON PD.PRODNUM=PR.PRODNUM WHERE PD.PRODTYPE NOT IN (100,101) and pd.COSTEACH<>0 AND PD.TRANSACT=?", conPixel);
                        OdbcDataReader readerDatosDF = null;
                        comandoPixelDF.Parameters.AddWithValue("@idTransaccion", readerDatos.GetString(14));
                        comandoPixelDF.ExecuteNonQuery();
                        readerDatosDF = comandoPixelDF.ExecuteReader();
                        factura.detalles = new Factura.Detalles();
                        Decimal totalDescuentoPorFactura = 0;
                        factura.detalles = new Factura.Detalles();
                        factura.detalles.detalle = new List<Factura.Detalles.Detalle>();
                        if (readerDatosDF.HasRows)
                        {
                            while (readerDatosDF.Read())
                            {
                                Factura.Detalles.Detalle detalle = new Factura.Detalles.Detalle();
                                detalle.codigoPrincipal = readerDatosDF.GetString(0);
                                detalle.codigoAuxiliar = readerDatosDF.GetString(0);
                                detalle.descripcion = readerDatosDF.GetString(1);
                                detalle.cantidad = readerDatosDF.GetString(2);
                                detalle.precioUnitario = readerDatosDF.GetString(3);
                                detalle.descuento = readerDatosDF.GetString(4);
                                //sumatoria de descuentos por factura
                                totalDescuentoPorFactura = totalDescuentoPorFactura + Convert.ToDecimal(detalle.descuento);
                                detalle.precioTotalSinImpuesto = readerDatosDF.GetString(5);
                                detalle.impuestos = new Factura.Detalles.Detalle.Impuestos();
                                detalle.impuestos.impuesto = new List<Factura.Detalles.Detalle.Impuestos.Impuesto>();
                                Factura.Detalles.Detalle.Impuestos.Impuesto impuesto = new Factura.Detalles.Detalle.Impuestos.Impuesto();
                                impuesto.codigo = GetConfigProp("FacturaImpuestos12Codigo");
                                impuesto.codigoPorcentaje = readerDatosDF.GetString(6);
                                impuesto.tarifa = readerDatosDF.GetString(7);
                                impuesto.baseImponible = readerDatosDF.GetString(8);
                                impuesto.valor = readerDatosDF.GetString(9);
                                detalle.impuestos.impuesto.Add(impuesto);
                                factura.detalles.detalle.Add(detalle);
                            }
                        }
                        readerDatosDF.Close();
                        factura.infoFactura.totalDescuento = Convert.ToString(totalDescuentoPorFactura);

                        //agrego información adicional
                        factura.infoAdicional.campoAdicional = new List<Factura.InfoAdicional.CampoAdicional>();
                        Factura.InfoAdicional.CampoAdicional campoAdicional1 = new Factura.InfoAdicional.CampoAdicional();
                        campoAdicional1.nombre = "CorreoCliente";
                        //campoAdicional1.valor = readerDatos.GetString(12);
                        campoAdicional1.texto = readerDatos.GetString(12);
                        factura.infoAdicional.campoAdicional.Add(campoAdicional1);

                        Factura.InfoAdicional.CampoAdicional campoAdicional2 = new Factura.InfoAdicional.CampoAdicional();
                        campoAdicional2.nombre = "CAJERO";
                        //campoAdicional2.valor = readerDatos.GetString(13);
                        campoAdicional2.texto = readerDatos.GetString(13);
                        factura.infoAdicional.campoAdicional.Add(campoAdicional2);

                        Factura.InfoAdicional.CampoAdicional campoAdicional3 = new Factura.InfoAdicional.CampoAdicional();
                        campoAdicional3.nombre = "TRANSACCION";
                        //campoAdicional3.valor = readerDatos.GetString(14);
                        campoAdicional3.texto = readerDatos.GetString(14);
                        factura.infoAdicional.campoAdicional.Add(campoAdicional3);

                        //listo las formas de pago con sus descripciones
                        OdbcCommand comandoPixelFPA = new OdbcCommand("SELECT top 1 trim(str(round(HP.TENDER,2),16,2)),MP.AccountCode,MP.DESCRIPT FROM DBA.Howpaid HP INNER JOIN DBA.MethodPay MP ON HP.METHODNUM = MP.METHODNUM WHERE HP.TRANSACT =? order by hp.TRANSDATE desc", conPixel);
                        OdbcDataReader readerDatosFPA = null;
                        comandoPixelFPA.Parameters.AddWithValue("@idTransaccion", readerDatos.GetString(14));
                        //comandoPixelFPA.ExecuteNonQuery();
                        readerDatosFPA = comandoPixelFPA.ExecuteReader();
                        if (readerDatosFPA.HasRows)
                        {
                            while (readerDatosFPA.Read())
                            {
                                Factura.InfoAdicional.CampoAdicional pagoA = new Factura.InfoAdicional.CampoAdicional();
                                pagoA.nombre = "FORMA DE PAGO";
                                pagoA.texto = readerDatosFPA.GetString(2) + ": " + readerDatosFPA.GetString(0);
                                //pagoA.valor = readerDatosFPA.GetString(2) + ": " + readerDatosFPA.GetString(0);
                                factura.infoAdicional.campoAdicional.Add(pagoA);
                            }
                        }
                        readerDatosFPA.Close();

                        Factura.InfoAdicional.CampoAdicional campoAdicional4 = new Factura.InfoAdicional.CampoAdicional();
                        campoAdicional4.nombre = "FECHA CIERRE";
                        //campoAdicional4.valor = readerDatos.GetString(15);
                        campoAdicional4.texto = readerDatos.GetString(15);
                        factura.infoAdicional.campoAdicional.Add(campoAdicional4);

                        retorno.Add(factura);
                    }
                }
                readerDatos.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexiones.CloseConnection(conPixel);
            }
            return retorno;
        }



        //*******************************************************************
        //procedimientos provisionales para generacion total de comprobantes por local
        public void ProvisionalEjecutarProcesoFE()
        {
            try
            {
                //conexion pixel
                OdbcConnection conProvisional = conexiones.GetOdbcConnectionPixel();
                OdbcCommand comandoProvisional = new OdbcCommand("SELECT DI.UId FROM DBA.Dayinfo DI WHERE DI.TIMEEND IS NOT NULL and di.opendate>='2018-12-01' ORDER BY 1", conProvisional);
                OdbcDataReader readerDatosProvisional = null;
                readerDatosProvisional = comandoProvisional.ExecuteReader();
                List<Factura> listaFacturas = new List<Factura>();

                if (readerDatosProvisional.HasRows)
                {
                    while (readerDatosProvisional.Read())
                    {
                        //saco un listado de facturas con las que generaré los XML
                        String idTabla = readerDatosProvisional.GetString(0);
                        Console.WriteLine("Procesando día de venta: " + idTabla);
                        listaFacturas = GenerarListaFacturasProvisional(idTabla);

                        //se elimina, genera, y mueve los archivos tanto a directorios locales como remo POR CADA DIA DE VENTA
                        new Transformador().ProcesarArchivos(listaFacturas);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private Boolean FacturaExisteEnOracle(String facturaNumero)
        {
            Boolean retorno = false;
            OdbcConnection conORA = null;
            OdbcDataReader readerDatos = null;
            try
            {
                //Abro la conexión a Pixel
                conORA = conexiones.GetOdbcConnectionORA();

                //llamo el procedimiento 2
                OdbcCommand comandoORA = new OdbcCommand("SELECT count(*) from inf_comprobante_offline where coof_comprobantenumero=?", conORA);
                comandoORA.Parameters.AddWithValue(":facturaNumero", facturaNumero);
                //OdbcCommand comandoPixel1 = new OdbcCommand("SELECT DI.UId,DI.OPENDATE,(SELECT COUNT(*) FROM DBA.POSHEADER PH WHERE PH.OPENDATE=DI.OPENDATE AND PH.TRANSACT IN (SELECT PD.TRANSACT FROM DBA.POSDETAIL PD WHERE PD.PRODTYPE NOT IN(100,101) AND PD.COSTEACH>0) and PH.REFID <> 0) TOTAL FROM DBA.Dayinfo DI WHERE DI.TIMEEND IS NOT NULL ORDER BY 2 DESC", conPixel);
                readerDatos = comandoORA.ExecuteReader();
                if (readerDatos.HasRows)
                {
                    while (readerDatos.Read())
                    {
                        Int32 conteo = readerDatos.GetInt32(0);
                        if (conteo > 0)
                        {
                            retorno = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexiones.CloseConnection(conORA);
            }
            return retorno;
        }

        public List<Factura> GenerarListaFacturasProvisional(String idTabla)
        {
            List<Factura> retorno = new List<Factura>();
            OdbcConnection conPixel = null;
            OdbcDataReader readerDatos = null;
            try
            {
                //Abro la conexión a Pixel
                conPixel = conexiones.GetOdbcConnectionPixel();

                //listo las facturas
                OdbcCommand comandoPixel = new OdbcCommand("SELECT SI.Fax AS RUC,SI.Owner AS NOM_COMERCIAL,SI.Contact AS NOM_LOCAL,SI.Email AS DIR_MATRIZ,SI.Website AS DIR_LOCAL,SI.City AS OBL_CONTA,SI.Country AS PUNTO_EMI,SI.Postal AS CONT_ESPECIAL,SI.Prov AS NUM_LOCAL,SI.Phone AS GENERA_XML,SI.Address1 AS PATH_LOCAL,SI.Address2 AS PATH_SERVER,SI.RptComment AS PATH_RUTAS,(SELECT DBA.PARAMETROS_DLL.AMBIENTE_FE FROM DBA.PARAMETROS_DLL),SI.ColaborationURL FROM DBA.Sysinfo SI", conPixel);
                comandoPixel.ExecuteNonQuery();
                readerDatos = comandoPixel.ExecuteReader();


                Factura facturaDatosGenerales = new Factura();
                facturaDatosGenerales.infoTributaria = new Factura.InfoTributaria();
                facturaDatosGenerales.infoFactura = new Factura.InfoFactura();
                if (readerDatos.HasRows)
                {
                    while (readerDatos.Read())
                    {
                        facturaDatosGenerales.infoTributaria.ruc = readerDatos.GetString(0);
                        facturaDatosGenerales.infoTributaria.nombreComercial = readerDatos.GetString(2);
                        facturaDatosGenerales.infoTributaria.razonSocial = readerDatos.GetString(1);
                        facturaDatosGenerales.infoTributaria.dirMatriz = readerDatos.GetString(3);
                        facturaDatosGenerales.infoFactura.dirEstablecimiento = readerDatos.GetString(4);
                        facturaDatosGenerales.infoFactura.obligadoContabilidad = readerDatos.GetString(5);
                        facturaDatosGenerales.infoTributaria.ptoEmi = readerDatos.GetString(6);
                        facturaDatosGenerales.infoFactura.contribuyenteEspecial = readerDatos.GetString(7);
                        facturaDatosGenerales.infoTributaria.estab = readerDatos.GetString(8);
                        facturaDatosGenerales.infoTributaria.ambiente = readerDatos.GetString(13);
                        facturaDatosGenerales.infoTributaria.codDoc = GetConfigProp("FacturaCodDoc");
                        facturaDatosGenerales.infoTributaria.tipoEmision = GetConfigProp("FacturaTipoEmision");
                    }
                }
                conexiones.CloseDataReader(readerDatos);

                //listo las facturas
                comandoPixel = new OdbcCommand("SELECT CA.TaxNumber, SUBSTRING(PH.REFID, 7, 9), convert(char(10),PH.TIMEEND,103),(CASE WHEN trim(cast(M.POSTAL as varchar)) IS NULL OR trim(cast(M.POSTAL as varchar))='0' THEN '06' WHEN LENGTH(trim(cast(M.POSTAL as varchar)))<=1 THEN '0' || trim(cast(M.POSTAL as varchar)) END) AS T_DOC,M.FIRSTNAME || ' ' || M.LASTNAME,M.ADRESS2,trim(str(round(PH.NETTOTAL,2),16,2)),trim(str(round((CASE WHEN (SELECT DBA.Sysinfo.TAXRATE2 FROM DBA.Sysinfo) = 0 THEN PH.TAX2ABLE ELSE PH.TAX3ABLE END),2),16,2)) AS IVA_0,trim(str(round(PH.TAX1ABLE,2),16,2)),trim(str(round(PH.TAX1,2),16,2)),trim(str(round((CASE WHEN (SELECT DBA.Sysinfo.TAXRATE3 FROM DBA.Sysinfo) = 10 THEN PH.TAX3ABLE ELSE PH.TAX2ABLE END),2),16,2)) AS SERVICIO,trim(str(round(PH.FINALTOTAL,2),16,2)),(CASE WHEN M.EMAIL IS NULL OR LENGTH(M.EMAIL)=0 THEN (SELECT dba.Sysinfo.Address2 FROM DBA.Sysinfo)ELSE M.EMAIL END) AS CORREO,EM.POSNAME,cast(PH.TRANSACT as varchar) codigo,CONVERT(CHAR(10),PH.TIMEEND,111),(CASE WHEN LENGTH(M.ADRESS1)= 0 OR M.ADRESS1 IS NULL THEN 'NINGUNA' ELSE M.ADRESS1 END) direccion FROM DBA.POSHEADER PH INNER JOIN DBA.Member M ON  PH.MemCode=M.MEMCODE INNER JOIN DBA.employee EM ON PH.WHOCLOSE=EM.EMPNUM INNER JOIN DBA.TaxExemptNumbers CA ON PH.TRANSACT= CA.TRANSACT WHERE (PH.OPENDATE = ? And PH.REFID <> 0) AND PH.TRANSACT IN(SELECT PD.TRANSACT FROM DBA.POSDETAIL PD WHERE PD.PRODTYPE NOT IN(101) AND PD.COSTEACH>0) ORDER BY PH.REFID", conPixel);
                comandoPixel.Parameters.AddWithValue("@idTabla", idTabla);
                comandoPixel.ExecuteNonQuery();

                readerDatos = comandoPixel.ExecuteReader();
                if (readerDatos.HasRows)
                {
                    while (readerDatos.Read())
                    {
                        //Defino los datos generales
                        Factura factura = new Factura();
                        factura.id = GetConfigProp("FacturaId");
                        factura.version = GetConfigProp("FacturaVersion");
                        factura.infoTributaria = new Factura.InfoTributaria();
                        factura.infoFactura = new Factura.InfoFactura();
                        factura.detalles = new Factura.Detalles();
                        factura.infoAdicional = new Factura.InfoAdicional();

                        factura.infoTributaria.ruc = facturaDatosGenerales.infoTributaria.ruc;
                        factura.infoTributaria.nombreComercial = facturaDatosGenerales.infoTributaria.nombreComercial;
                        factura.infoTributaria.razonSocial = facturaDatosGenerales.infoTributaria.razonSocial;
                        factura.infoTributaria.dirMatriz = facturaDatosGenerales.infoTributaria.dirMatriz;
                        factura.infoFactura.dirEstablecimiento = facturaDatosGenerales.infoFactura.dirEstablecimiento;
                        factura.infoFactura.obligadoContabilidad = facturaDatosGenerales.infoFactura.obligadoContabilidad;
                        factura.infoTributaria.ptoEmi = facturaDatosGenerales.infoTributaria.ptoEmi;
                        factura.infoFactura.contribuyenteEspecial = facturaDatosGenerales.infoFactura.contribuyenteEspecial;
                        factura.infoTributaria.estab = facturaDatosGenerales.infoTributaria.estab;
                        factura.infoTributaria.ambiente = facturaDatosGenerales.infoTributaria.ambiente;
                        factura.infoTributaria.codDoc = facturaDatosGenerales.infoTributaria.codDoc;
                        factura.infoTributaria.tipoEmision = facturaDatosGenerales.infoTributaria.tipoEmision;

                        factura.infoTributaria.claveAcceso = readerDatos.GetString(0);
                        factura.infoTributaria.secuencial = readerDatos.GetString(1);
                        factura.infoFactura.fechaEmision = readerDatos.GetString(2);
                        factura.infoFactura.tipoIdentificacionComprador = readerDatos.GetString(3);
                        factura.infoFactura.razonSocialComprador = readerDatos.GetString(4);
                        factura.infoFactura.identificacionComprador = readerDatos.GetString(5);
                        factura.infoFactura.totalSinImpuestos = readerDatos.GetString(6);

                        //total con impuestos 0
                        Factura.InfoFactura.TotalConImpuestos.TotalImpuesto totalImpuesto0 = new Factura.InfoFactura.TotalConImpuestos.TotalImpuesto();
                        totalImpuesto0.codigo = GetConfigProp("FacturaImpuestos0Codigo");
                        totalImpuesto0.codigoPorcentaje = GetConfigProp("FacturaImpuestos0CodigoPorcentaje");
                        totalImpuesto0.descuentoAdicional = GetConfigProp("FacturaImpuestos0DescuentoAdicional");
                        totalImpuesto0.baseImponible = readerDatos.GetString(7);
                        totalImpuesto0.valor = GetConfigProp("FacturaImpuestos0Valor");

                        //total con impuestos 12
                        Factura.InfoFactura.TotalConImpuestos.TotalImpuesto totalImpuesto12 = new Factura.InfoFactura.TotalConImpuestos.TotalImpuesto();
                        totalImpuesto12.codigo = GetConfigProp("FacturaImpuestos12Codigo");
                        totalImpuesto12.codigoPorcentaje = GetConfigProp("FacturaImpuestos12CodigoPorcentaje");
                        totalImpuesto12.descuentoAdicional = GetConfigProp("FacturaImpuestos12DescuentoAdicional");
                        totalImpuesto12.baseImponible = readerDatos.GetString(8);
                        totalImpuesto12.valor = readerDatos.GetString(9);

                        //inicializo objetos internos relacionados a impuestos, para evitar obtener error de valores nulos
                        factura.infoFactura.totalConImpuestos = new Factura.InfoFactura.TotalConImpuestos();
                        factura.infoFactura.totalConImpuestos.totalImpuesto = new List<Factura.InfoFactura.TotalConImpuestos.TotalImpuesto>();

                        factura.infoFactura.totalConImpuestos.totalImpuesto.Add(totalImpuesto0);
                        factura.infoFactura.totalConImpuestos.totalImpuesto.Add(totalImpuesto12);

                        factura.infoFactura.propina = readerDatos.GetString(10);
                        factura.infoFactura.importeTotal = readerDatos.GetString(11);
                        factura.infoFactura.moneda = GetConfigProp("FacturaMoneda");
                        factura.infoFactura.direccionComprador = readerDatos.GetString(16);

                        //agrego formas de pago
                        List<Factura.InfoFactura.Pagos.Pago> pagos = new List<Factura.InfoFactura.Pagos.Pago>();


                        //inicializo objetos de pagos para evitar errores de nulos
                        factura.infoFactura.pagos = new Factura.InfoFactura.Pagos();
                        factura.infoFactura.pagos.pago = new List<Factura.InfoFactura.Pagos.Pago>();

                        //listo las formas de pago

                        OdbcCommand comandoPixelFP = new OdbcCommand("SELECT trim(str(round(SUM(HP.TENDER),2),16,2)),(replicate('0', (2 - char_length(MP.AccountCode))) || MP.AccountCode) FROM DBA.Howpaid HP INNER JOIN DBA.MethodPay MP ON  HP.METHODNUM=MP.METHODNUM WHERE HP.TRANSACT=? GROUP BY (replicate('0', (2 - char_length(MP.AccountCode))) || MP.AccountCode)", conPixel);
                        OdbcDataReader readerDatosFP = null;
                        comandoPixelFP.Parameters.AddWithValue("@idTransaccion", readerDatos.GetString(14));
                        //comandoPixelFP.ExecuteNonQuery();
                        readerDatosFP = comandoPixelFP.ExecuteReader();
                        if (readerDatosFP.HasRows)
                        {
                            while (readerDatosFP.Read())
                            {
                                Factura.InfoFactura.Pagos.Pago pago = new Factura.InfoFactura.Pagos.Pago();
                                pago.formaPago = readerDatosFP.GetString(1);
                                pago.total = readerDatosFP.GetString(0);
                                pago.plazo = GetConfigProp("FacturaPagoPlazo");
                                pago.unidadTiempo = GetConfigProp("FacturaPagoUnidadTiempo");
                                factura.infoFactura.pagos.pago.Add(pago);
                            }
                        }
                        readerDatosFP.Close();

                        //listo los detalles de la factura
                        OdbcCommand comandoPixelDF = new OdbcCommand("SELECT PD.PRODNUM AS ID_PROD,(CASE WHEN PD.LINEDES IS NOT NULL OR LENGTH(PD.LINEDES)>0 THEN PD.LINEDES ELSE PR.PRINTDES END) AS PRODUCTO,trim(str(round(PD.QUAN,2),16,2)) AS CANTIDAD,trim(str(round(PD.COSTEACH,2),16,2)) AS P_UNITARIO,trim(str(round((CASE WHEN (ISNULL(PD.DISCOUNT,0))<0 THEN (ISNULL(PD.DISCOUNT,0))*-1 ELSE (ISNULL(PD.DISCOUNT,0)) END),2),16,2)) AS DESCUENTO,trim(str(round((PD.QUAN * PD.COSTEACH) - DESCUENTO,2),16,2)) AS  P_TOTAL_SIN_IMPUESTOS,(CASE WHEN PD.APPLYTAX1=1 THEN 2 ELSE 0 END) AS CODIGO_PORCENTAJE,(CASE WHEN CODIGO_PORCENTAJE=2 THEN 12 ELSE 0 END) AS TARIFA,trim(str(round(P_TOTAL_SIN_IMPUESTOS,2),16,2)) AS BASE_IMPONIBLE,trim(str(round((P_TOTAL_SIN_IMPUESTOS * TARIFA)/100,2),16,2)) AS VALOR FROM DBA.POSDETAIL PD INNER JOIN DBA.PRODUCT PR ON PD.PRODNUM=PR.PRODNUM WHERE PD.PRODTYPE NOT IN (100,101,1) AND PD.TRANSACT=?", conPixel);
                        OdbcDataReader readerDatosDF = null;
                        comandoPixelDF.Parameters.AddWithValue("@idTransaccion", readerDatos.GetString(14));
                        comandoPixelDF.ExecuteNonQuery();
                        readerDatosDF = comandoPixelDF.ExecuteReader();
                        factura.detalles = new Factura.Detalles();
                        Decimal totalDescuentoPorFactura = 0;
                        factura.detalles = new Factura.Detalles();
                        factura.detalles.detalle = new List<Factura.Detalles.Detalle>();
                        if (readerDatosDF.HasRows)
                        {
                            while (readerDatosDF.Read())
                            {
                                Factura.Detalles.Detalle detalle = new Factura.Detalles.Detalle();
                                detalle.codigoPrincipal = readerDatosDF.GetString(0);
                                detalle.codigoAuxiliar = readerDatosDF.GetString(0);
                                detalle.descripcion = readerDatosDF.GetString(1);
                                detalle.cantidad = readerDatosDF.GetString(2);
                                detalle.precioUnitario = readerDatosDF.GetString(3);
                                detalle.descuento = readerDatosDF.GetString(4);
                                //sumatoria de descuentos por factura
                                totalDescuentoPorFactura = totalDescuentoPorFactura + Convert.ToDecimal(detalle.descuento);
                                detalle.precioTotalSinImpuesto = readerDatosDF.GetString(5);
                                detalle.impuestos = new Factura.Detalles.Detalle.Impuestos();
                                detalle.impuestos.impuesto = new List<Factura.Detalles.Detalle.Impuestos.Impuesto>();
                                Factura.Detalles.Detalle.Impuestos.Impuesto impuesto = new Factura.Detalles.Detalle.Impuestos.Impuesto();
                                impuesto.codigo = GetConfigProp("FacturaImpuestos12Codigo");
                                impuesto.codigoPorcentaje = readerDatosDF.GetString(6);
                                impuesto.tarifa = readerDatosDF.GetString(7);
                                impuesto.baseImponible = readerDatosDF.GetString(8);
                                impuesto.valor = readerDatosDF.GetString(9);
                                detalle.impuestos.impuesto.Add(impuesto);
                                factura.detalles.detalle.Add(detalle);
                            }
                        }
                        readerDatosDF.Close();
                        factura.infoFactura.totalDescuento = Convert.ToString(totalDescuentoPorFactura);

                        //agrego información adicional
                        factura.infoAdicional.campoAdicional = new List<Factura.InfoAdicional.CampoAdicional>();
                        Factura.InfoAdicional.CampoAdicional campoAdicional1 = new Factura.InfoAdicional.CampoAdicional();
                        campoAdicional1.nombre = "CorreoCliente";
                        //campoAdicional1.valor = readerDatos.GetString(12);
                        campoAdicional1.texto = readerDatos.GetString(12);
                        factura.infoAdicional.campoAdicional.Add(campoAdicional1);

                        Factura.InfoAdicional.CampoAdicional campoAdicional2 = new Factura.InfoAdicional.CampoAdicional();
                        campoAdicional2.nombre = "CAJERO";
                        //campoAdicional2.valor = readerDatos.GetString(13);
                        campoAdicional2.texto = readerDatos.GetString(13);
                        factura.infoAdicional.campoAdicional.Add(campoAdicional2);

                        Factura.InfoAdicional.CampoAdicional campoAdicional3 = new Factura.InfoAdicional.CampoAdicional();
                        campoAdicional3.nombre = "TRANSACCION";
                        //campoAdicional3.valor = readerDatos.GetString(14);
                        campoAdicional3.texto = readerDatos.GetString(14);
                        factura.infoAdicional.campoAdicional.Add(campoAdicional3);

                        //listo las formas de pago con sus descripciones
                        OdbcCommand comandoPixelFPA = new OdbcCommand("SELECT trim(str(round(HP.TENDER,2),16,2)),MP.AccountCode,MP.DESCRIPT FROM DBA.Howpaid HP INNER JOIN DBA.MethodPay MP ON HP.METHODNUM = MP.METHODNUM WHERE HP.TRANSACT =?", conPixel);
                        OdbcDataReader readerDatosFPA = null;
                        comandoPixelFPA.Parameters.AddWithValue("@idTransaccion", readerDatos.GetString(14));
                        //comandoPixelFPA.ExecuteNonQuery();
                        readerDatosFPA = comandoPixelFPA.ExecuteReader();
                        if (readerDatosFPA.HasRows)
                        {
                            while (readerDatosFPA.Read())
                            {
                                Factura.InfoAdicional.CampoAdicional pagoA = new Factura.InfoAdicional.CampoAdicional();
                                pagoA.nombre = "FORMA DE PAGO";
                                pagoA.texto = readerDatosFPA.GetString(2) + ": " + readerDatosFPA.GetString(0);
                                //pagoA.valor = readerDatosFPA.GetString(2) + ": " + readerDatosFPA.GetString(0);
                                factura.infoAdicional.campoAdicional.Add(pagoA);
                            }
                        }
                        readerDatosFPA.Close();

                        Factura.InfoAdicional.CampoAdicional campoAdicional4 = new Factura.InfoAdicional.CampoAdicional();
                        campoAdicional4.nombre = "FECHA CIERRE";
                        //campoAdicional4.valor = readerDatos.GetString(15);
                        campoAdicional4.texto = readerDatos.GetString(15);
                        factura.infoAdicional.campoAdicional.Add(campoAdicional4);

                        String numeroCompleto = factura.infoTributaria.estab + factura.infoTributaria.ptoEmi + factura.infoTributaria.secuencial;
                        if (!FacturaExisteEnOracle(numeroCompleto))
                        {
                            Console.WriteLine("no encontrada en oracle, procesando: " + numeroCompleto);
                            retorno.Add(factura);
                        }
                    }
                }
                readerDatos.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexiones.CloseConnection(conPixel);
            }
            return retorno;
        }

        public Boolean ComprobarSiTransaccionExiste(String transaccionCodigo)
        {
            Boolean retorno = false;
            OdbcConnection conPixel = null;
            try
            {
                //Abro la conexión a Pixel
                conPixel = conexiones.GetOdbcConnectionPixel();

                //llamo el procedimiento 2
                OdbcCommand comandoPixel1 = new OdbcCommand("SELECT count(*) from dba.POSHEADER aa where aa.transact=?", conPixel);
                //OdbcCommand comandoPixel1 = new OdbcCommand("SELECT DBA.Dayinfo.UId,DBA.Dayinfo.OPENDATE,DBA.Dayinfo.TIMEEND FROM DBA.Dayinfo WHERE DBA.Dayinfo.TIMEEND IS NOT NULL ORDER BY 2 DESC", conPixel);
                comandoPixel1.Parameters.AddWithValue("transaccionCodigo", transaccionCodigo);

                if (Int16.Parse(comandoPixel1.ExecuteScalar().ToString()) > 0)
                {
                    retorno = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexiones.CloseConnection(conPixel);
            }
            return retorno;
        }


        //saco el personal que está disponible en la nómina de SquareNet
        public List<RhPersona> GetPersonaFromNominaByFiltros(String personaFiltroBusqueda)
        {
            OdbcConnection conPixel = null;
            List<RhPersona> retorno = new List<RhPersona>();
            try
            {
                //Abro la conexión a Pixel
                conPixel = conexiones.getOdbcConnectionSquarenet();

                //llamo el procedimiento 2
                OdbcCommand comandoPixel1 = new OdbcCommand("SELECT persona_nombre, persona_id, persona_tipo from RH_PERSONA_ACTIVO_LTG_VIEW aa where upper(aa.persona_nombre) like '%'+upper(?)+'%' or upper(aa.persona_id) like '%'+upper(?)+'%' order by 1,2", conPixel);
                //OdbcCommand comandoPixel1 = new OdbcCommand("SELECT DBA.Dayinfo.UId,DBA.Dayinfo.OPENDATE,DBA.Dayinfo.TIMEEND FROM DBA.Dayinfo WHERE DBA.Dayinfo.TIMEEND IS NOT NULL ORDER BY 2 DESC", conPixel);
                comandoPixel1.Parameters.AddWithValue("nombre", personaFiltroBusqueda);
                comandoPixel1.Parameters.AddWithValue("cedula", personaFiltroBusqueda);
                OdbcDataReader dataReader = null;
                dataReader = comandoPixel1.ExecuteReader();
                while (dataReader.Read())
                {
                    RhPersona persona = new RhPersona();
                    persona.PersonaNombre = dataReader.GetString(0);
                    persona.PersonaId = dataReader.GetString(1);
                    persona.PersonaTipo = dataReader.GetString(2);
                    retorno.Add(persona);
                }
                dataReader.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexiones.CloseConnection(conPixel);
            }
            return retorno;
        }


        //ejecutar proceso de ventas, asignación de descuentos en base intermedia y carga de CXC a Nómina
        public void EjecutarProcesoVentasMasDescuentosMasNomina(String idTabla, List<Descuento> descuentos)
        {
            //objetos odbc pixel
            OdbcConnection conPixel = null;
            OdbcTransaction tranPixel = null;

            //objetos odbc sql server intermedia sap
            OdbcConnection conBI = null;
            OdbcTransaction tranBI = null;

            //objetos odbc sql server nómina
            OdbcConnection conNomina = null;
            OdbcTransaction tranNomina = null;
            try
            {
                //********************************************inicio del paso 1*********************************************
                //envío de información Pixel aislado de otros procesos y con bloque temporal
                //Abro la conexión a Pixel SQL anywhere
                conPixel = conexiones.GetOdbcConnectionPixel();

                //Inicio la transacción
                tranPixel = conPixel.BeginTransaction();



                //abro conexión a BI
                conBI = conexiones.getOdbcConnectionBI();
                tranBI = conBI.BeginTransaction();


                //saco datos del almacen en Pixel
                OdbcCommand comandoPixel3 = new OdbcCommand("select valor from dba.PARAMETROS where id_par = ?", conPixel);
                comandoPixel3.Transaction = tranPixel;
                comandoPixel3.Parameters.AddWithValue("@idParametro", GetConfigProp("ParametroIdAlmacen"));
                OdbcDataReader dataReaderAlmacen = comandoPixel3.ExecuteReader();
                String almacen = null;
                while (dataReaderAlmacen.Read())
                {
                    almacen = dataReaderAlmacen.GetString(0);
                }

                //consulto la tabla de SQL server BI
                OdbcCommand comandoBI0 = new OdbcCommand("SELECT COUNT(*) FROM oinv WHERE Identificador=?", conBI);
                comandoBI0.Transaction = tranBI;
                comandoBI0.Parameters.AddWithValue("@idTabla", almacen + "-" + idTabla);

                int conteo = ((int)comandoBI0.ExecuteScalar());
                Boolean ejecutarPrimerPaso = conteo == 0 ? true : false;

                if (ejecutarPrimerPaso)
                {
                    //llamo el procedimiento 1
                    OdbcCommand comandoPixel1 = new OdbcCommand("{call dba.ACK_LTG_VENTAS_PAGOS(?)}", conPixel);
                    comandoPixel1.CommandType = CommandType.StoredProcedure;
                    comandoPixel1.Transaction = tranPixel;
                    comandoPixel1.Parameters.AddWithValue("@idTabla", idTabla);
                    comandoPixel1.ExecuteNonQuery();
                    //proceso intermedio y cierre de la transacción primera
                    conexiones.CommitTransaction(tranPixel);

                    //Inicio la transacción por segunda vez y paso el control a la app
                    tranPixel = conPixel.BeginTransaction();
                }

                //********************************************inicio del paso 2*********************************************
                //actualización de estados en BI 
                OdbcCommand comandoBI01 = new OdbcCommand("UPDATE oinv SET Sincronizado='D' WHERE Identificador=?", conBI);
                comandoBI01.Transaction = tranBI;
                comandoBI01.Parameters.AddWithValue("@idTabla", almacen + "-" + idTabla);
                comandoBI01.ExecuteNonQuery();

                OdbcCommand comandoBI02 = new OdbcCommand("UPDATE orct SET Sincronizado='D' WHERE Identificador=?", conBI);
                comandoBI02.Transaction = tranBI;
                comandoBI02.Parameters.AddWithValue("@idTabla", almacen + "-" + idTabla);
                comandoBI02.ExecuteNonQuery();

                //confirmo transacciones a la BI y abro una nueva transacción
                conexiones.CommitTransaction(tranBI);
                tranBI = conBI.BeginTransaction();


                //********************************************inicio del paso 3*********************************************
                //trabajo en procesos de asignación de descuentos sobre la BI


                foreach (Descuento tmpDes in descuentos)
                {
                    OdbcCommand comandoBi1 = new OdbcCommand("INSERT INTO pixel_cxc_ltg(cxc_id, cxc_cedula, cxc_nombre,cxc_almacen,cxc_opendate,cxc_valor, cxc_tipo) VALUES((select coalesce(max(aa1.cxc_id),0)+1 from pixel_cxc_ltg aa1), ?, ?, ?,?,?,?)", conBI);
                    comandoBi1.Transaction = tranBI;

                    comandoBi1.Parameters.AddWithValue("@cedula", tmpDes.Cedula);
                    comandoBi1.Parameters.AddWithValue("@nombre", tmpDes.Nombre);
                    comandoBi1.Parameters.AddWithValue("@almacen", almacen);
                    comandoBi1.Parameters.AddWithValue("@idTabla", idTabla);
                    comandoBi1.Parameters.AddWithValue("@valor", tmpDes.Valor);
                    comandoBi1.Parameters.AddWithValue("@tipo", tmpDes.Tipo);
                    comandoBi1.ExecuteNonQuery();
                }

                //llamo y guardo datos de la linea de descuento desde la tabla de la BI
                OdbcCommand comandoBi2 = new OdbcCommand("select DocNum,CreditCard,CreditAcct,Line_ID,CrTypeCode,CrCardNum,CardValid,VoucherNum,NumOfPmnts,CreditSum,CreateDatePOS,CreateDateSap,Origen,PaymentCode,Sincronizado,Accion,Identificador,U_HBT_Depositado,U_LTG_ID_COLAB,UpdateStatus from rct3 where Identificador = ? and CreditCard=?", conBI);
                comandoBi2.Transaction = tranBI;
                comandoBi2.Parameters.AddWithValue("@identificador", almacen + "-" + idTabla);
                comandoBi2.Parameters.AddWithValue("@codigo_faltante", GetConfigProp("CreditCardSapFaltante"));
                OdbcDataReader readerLineaDescuento = comandoBi2.ExecuteReader();

                Rct3 rct3 = new Rct3();
                while (readerLineaDescuento.Read())
                {
                    rct3.DocNum = GetStringNull(readerLineaDescuento, 0);
                    rct3.CreditCard = GetStringNull(readerLineaDescuento, 1);
                    rct3.CreditAcct = GetStringNull(readerLineaDescuento, 2);
                    rct3.Line_ID = readerLineaDescuento.GetInt32(3);
                    rct3.CrTypeCode = GetStringNull(readerLineaDescuento, 4);
                    rct3.CrCardNum = GetStringNull(readerLineaDescuento, 5);
                    rct3.CardValid = readerLineaDescuento.GetDate(6);
                    rct3.VoucherNum = GetStringNull(readerLineaDescuento, 7);
                    rct3.NumOfPmnts = readerLineaDescuento.GetInt16(8);
                    rct3.CreditSum = readerLineaDescuento.GetDecimal(9);
                    rct3.CreateDatePOS = readerLineaDescuento.GetDate(10);
                    rct3.CreateDateSap = GetDateNull(readerLineaDescuento, 11);
                    rct3.Origen = GetStringNull(readerLineaDescuento, 12);
                    rct3.PaymentCode = GetStringNull(readerLineaDescuento, 13);
                    rct3.Sincronizado = GetStringNull(readerLineaDescuento, 14);
                    rct3.Accion = GetStringNull(readerLineaDescuento, 15);
                    rct3.Identificador = GetStringNull(readerLineaDescuento, 16);
                    rct3.U_HBT_Depositado = GetStringNull(readerLineaDescuento, 17);
                    rct3.U_LTG_ID_COLAB = GetStringNull(readerLineaDescuento, 18);
                    rct3.UpdateStatus = readerLineaDescuento.GetInt32(19);
                }

                //elimino la linea de descuento de la desde la tabla de la BI
                OdbcCommand comandoBi3 = new OdbcCommand("delete from rct3 where Identificador = ? and CreditCard=?", conBI);
                comandoBi3.Transaction = tranBI;
                comandoBi3.Parameters.AddWithValue("@identificador", almacen + "-" + idTabla);
                comandoBi3.Parameters.AddWithValue("@codigo_faltante", GetConfigProp("CreditCardSapFaltante"));
                comandoBi3.ExecuteNonQuery();

                //recorro cada uno de los descuentos y creao las líneas
                foreach (Descuento descTmp in descuentos)
                {

                    OdbcCommand comandoBi4 = new OdbcCommand("insert into rct3(DocNum,CreditCard,CreditAcct,Line_ID,CrTypeCode,CrCardNum,CardValid,VoucherNum,NumOfPmnts,CreditSum,CreateDatePOS,CreateDateSap,Origen,PaymentCode,Sincronizado,Accion,Identificador,U_HBT_Depositado,U_LTG_ID_COLAB,UpdateStatus) values(?,?,?,(select coalesce(max(coalesce(aa1.Line_id,0))+1,0) from rct3 aa1 where aa1.Identificador=?),?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)", conBI);
                    //OdbcCommand comandoBi4 = new OdbcCommand("insert into rct3(DocNum,CreditCard,CreditAcct,Line_ID,CrTypeCode,CrCardNum,CardValid,VoucherNum,NumOfPmnts,CreditSum,CreateDatePOS,CreateDateSap,Origen,PaymentCode,Sincronizado,Accion,Identificador,U_HBT_Depositado,U_LTG_ID_COLAB,UpdateStatus) values('','','',(select max(coalesce(aa1.Line_id,0))+1 from rct3 aa1 where aa1.Identificador='BD102-20181127'),'','','','',0,0,getdate(),getdate(),'','','','','BD102-20181127','','','')", conBI);
                    comandoBi4.Transaction = tranBI;


                    comandoBi4.Parameters.AddWithValue("@a1", getParametroONull(rct3.DocNum));

                    if (descTmp.Tipo.Equals("PERSONA"))
                    {
                        comandoBi4.Parameters.AddWithValue("@a2", getParametroONull(rct3.CreditCard));
                        comandoBi4.Parameters.AddWithValue("@a3", getParametroONull(rct3.CreditAcct));
                    }
                    else if (descTmp.Tipo.Equals("COMPRA"))
                    {
                        comandoBi4.Parameters.AddWithValue("@a2", getParametroONull(GetConfigProp("CreditCardSapCompra")));
                        comandoBi4.Parameters.AddWithValue("@a3", getParametroONull(GetConfigProp("AccountSapCompra")));
                    }
                    else
                    {
                        comandoBi4.Parameters.AddWithValue("@a2", getParametroONull(GetConfigProp("CreditCardSapRetencion")));
                        comandoBi4.Parameters.AddWithValue("@a3", getParametroONull(GetConfigProp("AccountSapRetencion")));
                    }
                    comandoBi4.Parameters.AddWithValue("@a4", getParametroONull(rct3.Identificador));
                    comandoBi4.Parameters.AddWithValue("@a5", getParametroONull(rct3.CrTypeCode));
                    comandoBi4.Parameters.AddWithValue("@a6", getParametroONull(descTmp.Cedula));
                    comandoBi4.Parameters.AddWithValue("@a7", getParametroONull(rct3.CardValid));
                    comandoBi4.Parameters.AddWithValue("@a8", getParametroONull(descTmp.Cedula));
                    comandoBi4.Parameters.AddWithValue("@a9", getParametroONull(rct3.NumOfPmnts));
                    comandoBi4.Parameters.AddWithValue("@a10", getParametroONull(descTmp.Valor));
                    comandoBi4.Parameters.AddWithValue("@a11", getParametroONull(rct3.CreateDatePOS));
                    comandoBi4.Parameters.AddWithValue("@a12", getParametroONull(null));
                    comandoBi4.Parameters.AddWithValue("@a13", getParametroONull(rct3.Origen));
                    comandoBi4.Parameters.AddWithValue("@a14", getParametroONull(rct3.PaymentCode));
                    comandoBi4.Parameters.AddWithValue("@a15", getParametroONull(rct3.Sincronizado));
                    comandoBi4.Parameters.AddWithValue("@a16", getParametroONull(rct3.Accion));
                    comandoBi4.Parameters.AddWithValue("@a17", getParametroONull(rct3.Identificador));
                    comandoBi4.Parameters.AddWithValue("@a18", getParametroONull(rct3.U_HBT_Depositado));
                    comandoBi4.Parameters.AddWithValue("@a19", getParametroONull(descTmp.Cedula));
                    comandoBi4.Parameters.AddWithValue("@a20", getParametroONull(rct3.UpdateStatus));
                    comandoBi4.ExecuteNonQuery();
                }

                //actualización de descuento en sistema de Nómina Squarenet
                //Abro la conexión a SQL Server Nómina Squarenet
                conNomina = conexiones.getOdbcConnectionSquarenet();
                //Inicio la transacción
                tranNomina = conNomina.BeginTransaction();

                OdbcCommand comandoNomina0 = new OdbcCommand("SELECT cod_centro_costo FROM co_centro_costo where whs_code=? and cod_empresa=?", conNomina);
                comandoNomina0.Transaction = tranNomina;
                comandoNomina0.Parameters.AddWithValue("@almacen", almacen);
                comandoNomina0.Parameters.AddWithValue("@empresa", GetConfigProp("CodigoEmpresaNomina"));
                String centroCosto = ((string)comandoNomina0.ExecuteScalar());

                if (centroCosto == null)
                {
                    throw new Exception("Asigne la relación entre el alacén Sap y centro de costo Squarenet CO_CENTRO_COSTO(whs_code), Base SquareNet Nómina");
                }

                //realizo la inserción de los descuentos en Sistema de Nómina
                foreach (Descuento descTmp in descuentos)
                {
                    OdbcCommand comandoNomina1 = new OdbcCommand("insert into ltg_cxc(CXCINDEX, USROCDGO_CXC, USRONMBR_CXC, USROFCIN, LOCAL_CXC, VALOR_CXC, MOTIVO_CXC, COD_EMPRESA) values((select 'CXC-'+cast(max(cast(substring(aa.CXCINDEX, 5,len(aa.CXCINDEX)) as bigint)+1) as varchar) from ltg_cxc aa where aa.CXCINDEX like 'CXC-%'),?,?,?,?,?,?,?)", conNomina);
                    comandoNomina1.Transaction = tranNomina;

                    if (descTmp.Tipo.Equals("PERSONA"))
                    {
                        comandoNomina1.Parameters.AddWithValue("@1", getParametroONull(descTmp.Cedula));
                        comandoNomina1.Parameters.AddWithValue("@2", getParametroONull(descTmp.Nombre));
                        comandoNomina1.Parameters.AddWithValue("@3", getParametroONull(rct3.CreateDatePOS));
                        comandoNomina1.Parameters.AddWithValue("@4", getParametroONull(centroCosto));
                        comandoNomina1.Parameters.AddWithValue("@5", getParametroONull(descTmp.Valor));
                        comandoNomina1.Parameters.AddWithValue("@6", getParametroONull(GetConfigProp("CodigoRubroFaltanteCaja")));
                        comandoNomina1.Parameters.AddWithValue("@7", getParametroONull(GetConfigProp("CodigoEmpresaNomina")));
                        comandoNomina1.ExecuteNonQuery();
                    }
                }

                //actualización de estados en BI 
                OdbcCommand comandoBI5 = new OdbcCommand("UPDATE oinv SET Sincronizado='P' WHERE Identificador=?", conBI);
                comandoBI5.Transaction = tranBI;
                comandoBI5.Parameters.AddWithValue("@idTabla", almacen + "-" + idTabla);
                comandoBI5.ExecuteNonQuery();

                OdbcCommand comandoBI6 = new OdbcCommand("UPDATE orct SET Sincronizado='P' WHERE Identificador=?", conBI);
                comandoBI6.Transaction = tranBI;
                comandoBI6.Parameters.AddWithValue("@idTabla", almacen + "-" + idTabla);
                comandoBI6.ExecuteNonQuery();

                //cambio el estado del envío de información si y solo sí todo el proceso se cumple y finaliza correctamente
                OdbcCommand comandoPixel2 = new OdbcCommand("INSERT INTO DBA.ACK_ESTADOS(ID_EST,ID_TABLA,TIPO)VALUES((select coalesce(max(aa1.ID_EST),0)+1 from dba.ACK_ESTADOS aa1), ?, 'DIA DE VENTA')", conPixel);
                comandoPixel2.Transaction = tranPixel;
                comandoPixel2.Parameters.AddWithValue("@idTabla", idTabla);
                comandoPixel2.ExecuteNonQuery();

                //confirmo la transacción
                conexiones.CommitAndCloseTransaction(tranPixel);
                conexiones.CommitAndCloseTransaction(tranBI);
                conexiones.CommitAndCloseTransaction(tranNomina);
            }
            catch (Exception ex)
            {
                conexiones.RollbackAndCloseTransaction(tranNomina);
                conexiones.RollbackAndCloseTransaction(tranBI);
                conexiones.RollbackAndCloseTransaction(tranPixel);
                throw ex;
            }
        }


        private String GetStringNull(OdbcDataReader dataReader, int posicion)
        {
            if (!dataReader.IsDBNull(posicion))
            {
                return dataReader.GetString(posicion);
            }
            else
            {
                return null;
            }
        }


        private DateTime GetDateNull(OdbcDataReader dataReader, int posicion)
        {
            if (!dataReader.IsDBNull(posicion))
            {
                return dataReader.GetDate(posicion);
            }
            else
            {
                return new DateTime();
            }
        }


        public Object getParametroONull(Object parametro)
        {
            if (parametro == null)
            {
                return DBNull.Value;
            }
            else
            {
                return parametro;
            }
        }
    }

}
