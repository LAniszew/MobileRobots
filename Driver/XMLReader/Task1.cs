using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Driver.XMLReader
{
    [XmlRoot(ElementName = "TargetPosition")]
        public class TargetPosition1
        {
            [XmlAttribute(AttributeName = "ThetaDeg")]
            public string ThetaDeg { get; set; }

            [XmlAttribute(AttributeName = "X")]
            public string X { get; set; }

            [XmlAttribute(AttributeName = "Y")]
            public string Y { get; set; }
        }

        [XmlRoot(ElementName = "TargetPositions")]
        public class TargetPositions1
        {
            [XmlElement(ElementName = "TargetPosition")]
            public List<TargetPosition> TargetPosition { get; set; }
        }
    }

