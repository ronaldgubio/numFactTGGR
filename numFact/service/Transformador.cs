using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using numFact.dto;

namespace numFact.service
{
    class Transformador
    {

        public void ProcesarArchivos(List<Factura> facturasObject)
        {
            try
            {
                ELiminarArchivosTemporales();
                GenerarArchivosXMLDesdeLista(facturasObject);
                //CopiarArchivosTemporalesLocalmente();
                CopiarArchivosTemporalesRemotamente();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GenerarArchivosXMLDesdeLista(List<Factura> facturasObject)
        {
            try
            {
                ELiminarArchivosTemporales();
                foreach (Factura facturaObject in facturasObject)
                {
                    GenerarXML(facturaObject);
                }
                CopiarArchivosTemporalesLocalmente();
                CopiarArchivosTemporalesRemotamente();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GenerarXML(Factura facturaObject)
        {
            TextWriter writer = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Factura));
                String rutaLocal = ConfigurationManager.AppSettings["DireccionTemporal"].ToString() + facturaObject.infoTributaria.claveAcceso + ".xml";
                writer = new StreamWriter(rutaLocal);

                serializer.Serialize(writer, facturaObject);
                writer.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CerrarWriter(writer);
            }
        }


        private void CerrarWriter(TextWriter writer)
        {
            if (writer != null)
            {
                writer.Close();
            }
        }

        private void ELiminarArchivosTemporales()
        {
            try
            {
                string[] sourcefiles = Directory.GetFiles(ConfigurationManager.AppSettings["DireccionTemporal"].ToString());

                foreach (string sourcefile in sourcefiles)
                {
                    File.Delete(sourcefile);
                }

                string[] sourcefiles1 = Directory.GetFiles(ConfigurationManager.AppSettings["DireccionLocal"].ToString());

                foreach (string sourcefile in sourcefiles1)
                {
                    File.Delete(sourcefile);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CopiarArchivosTemporalesLocalmente()
        {
            try
            {
                string[] sourcefiles = Directory.GetFiles(ConfigurationManager.AppSettings["DireccionTemporal"].ToString());

                foreach (string sourcefile in sourcefiles)
                {
                    string fileName = Path.GetFileName(sourcefile);
                    String targetPath1 = ConfigurationManager.AppSettings["DireccionLocal"].ToString();
                    string destFile1 = Path.Combine(targetPath1, fileName);

                    File.Copy(sourcefile, destFile1, true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CopiarArchivosTemporalesRemotamente()
        {
            try
            {
                string[] sourcefiles = Directory.GetFiles(ConfigurationManager.AppSettings["DireccionTemporal"].ToString());

                foreach (string sourcefile in sourcefiles)
                {
                    string fileName = Path.GetFileName(sourcefile);
                    String targetPath2 = ConfigurationManager.AppSettings["DireccionRemota"].ToString();
                    string destFile2 = Path.Combine(targetPath2, fileName);

                    File.Copy(sourcefile, destFile2, true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
