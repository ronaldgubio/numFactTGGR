using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace numFact.dto
{
    //se trabaja con clases anidadas para deserialización de cada uno de los comprobantes a objetos c#
    //documento factura
    [XmlRoot(ElementName = "factura")]
    public class Factura
    {

        [XmlAttribute(AttributeName = "id")]
        public String id { get; set; }

        [XmlAttribute(AttributeName = "version")]
        public String version { get; set; }

        [XmlElement(ElementName = "infoTributaria")]
        public InfoTributaria infoTributaria { get; set; }

        [XmlElement(ElementName = "infoFactura")]
        public InfoFactura infoFactura { get; set; }

        [XmlElement(ElementName = "detalles")]
        public Detalles detalles { get; set; }

        [XmlElement(ElementName = "infoAdicional")]
        public InfoAdicional infoAdicional { get; set; }

        //declaración de la clase InfoTributaria
        public class InfoTributaria
        {
            public String ambiente { get; set; }
            public String tipoEmision { get; set; }
            public String razonSocial { get; set; }
            public String nombreComercial { get; set; }
            public String ruc { get; set; }
            public String claveAcceso { get; set; }
            public String codDoc { get; set; }
            public String estab { get; set; }
            public String ptoEmi { get; set; }
            public String secuencial { get; set; }
            public String dirMatriz { get; set; }
        }

        //declaración de la clase InfoFactura
        public class InfoFactura
        {
            public String fechaEmision { get; set; }
            public String dirEstablecimiento { get; set; }
            public String contribuyenteEspecial { get; set; }
            public String obligadoContabilidad { get; set; }
            public String tipoIdentificacionComprador { get; set; }
            public String guiaRemision { get; set; }
            public String razonSocialComprador { get; set; }
            public String identificacionComprador { get; set; }
            public String direccionComprador { get; set; }
            public String totalSinImpuestos { get; set; }
            public String totalDescuento { get; set; }
            [XmlElement(ElementName = "totalConImpuestos")]
            public TotalConImpuestos totalConImpuestos { get; set; }
            public String propina { get; set; }
            public String importeTotal { get; set; }
            public String moneda { get; set; }
            public String valorRetIva { get; set; }
            public String valorRetRenta { get; set; }
            [XmlElement(ElementName = "pagos")]
            public Pagos pagos { get; set; }

            //declaración de la clase TotalConImpuestos
            public class TotalConImpuestos
            {
                [XmlElement(ElementName = "totalImpuesto")]
                public List<TotalImpuesto> totalImpuesto { get; set; }

                //declaración de la clase TotalImpuesto
                public class TotalImpuesto
                {
                    public String codigo { get; set; }
                    public String codigoPorcentaje { get; set; }
                    public String descuentoAdicional { get; set; }
                    public String baseImponible { get; set; }
                    public String valor { get; set; }
                }
            }

            //declaración de la clase Pagos
            public class Pagos
            {
                [XmlElement(ElementName = "pago")]
                public List<Pago> pago { get; set; }

                //declaración de la clase Pago
                public class Pago
                {
                    public String formaPago { get; set; }
                    public String total { get; set; }
                    public String plazo { get; set; }
                    public String unidadTiempo { get; set; }
                }
            }
        }

        //declaración de la clase Detalles
        public class Detalles
        {
            [XmlElement(ElementName = "detalle")]
            public List<Detalle> detalle { get; set; }

            //declaración de la clase Detalle
            public class Detalle
            {
                public String codigoPrincipal { get; set; }
                public String codigoAuxiliar { get; set; }
                public String descripcion { get; set; }
                public String cantidad { get; set; }
                public String precioUnitario { get; set; }
                public String descuento { get; set; }
                public String precioTotalSinImpuesto { get; set; }
                [XmlElement(ElementName = "detallesAdicionales")]
                public DetallesAdicionales detallesAdicionales { get; set; }
                [XmlElement(ElementName = "impuestos")]
                public Impuestos impuestos { get; set; }

                //declaración de la clase Detalles Adicionales
                public class DetallesAdicionales
                {
                    [XmlElement(ElementName = "detAdicional")]
                    public List<DetAdicional> detAdicional { get; set; }

                    //declaración de la clase DetAdicional
                    public class DetAdicional
                    {
                        [XmlAttribute]
                        public String nombre { get; set; }
                        [XmlAttribute]
                        public String valor { get; set; }
                        [XmlText]
                        public String texto { get; set; }
                    }
                }

                //declaración de la clase Impuestos
                public class Impuestos
                {
                    [XmlElement(ElementName = "impuesto")]
                    public List<Impuesto> impuesto { get; set; }

                    //declaración de la clase Impuesto
                    public class Impuesto
                    {
                        public String codigo { get; set; }
                        public String codigoPorcentaje { get; set; }
                        public String tarifa { get; set; }
                        public String baseImponible { get; set; }
                        public String valor { get; set; }
                    }
                }
            }
        }

        //declaración de la clase Detalles Adicionales
        public class InfoAdicional
        {
            [XmlElement(ElementName = "campoAdicional")]
            public List<CampoAdicional> campoAdicional { get; set; }

            //declaración de la clase DetAdicional
            public class CampoAdicional
            {
                [XmlAttribute]
                public String nombre { get; set; }
                [XmlAttribute]
                public String valor { get; set; }
                [XmlText]
                public String texto { get; set; }
            }
        }
    }
}
