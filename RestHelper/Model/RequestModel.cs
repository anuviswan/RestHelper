using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EcSolvo.Model
{
    [XmlRoot]
    public class RequestModel
    {
        [XmlElement]
        public string ResourceUri { get; set; }
        [XmlElement]
        public Uri BaseUri { get; set; }
        [XmlElement]
        public ParameterInfo RequestParameters { get; set; }
        [XmlElement]
        public HttpMethod MethodType { get; set; }
    }
}
