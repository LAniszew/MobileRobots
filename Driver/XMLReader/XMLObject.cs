using System.Collections.Generic;
using System.Xml.Serialization;

namespace Driver.XMLReader
{
	[XmlRoot(ElementName="Start")]
	public class Start {
		[XmlAttribute(AttributeName="X")]
		public string _X { get; set; }
		[XmlAttribute(AttributeName="Y")]
		public string _Y { get; set; }
	}

	[XmlRoot(ElementName="Destination")]
	public class Destination {
		[XmlAttribute(AttributeName="X")]
		public string _X { get; set; }
		[XmlAttribute(AttributeName="Y")]
		public string _Y { get; set; }
	}

	[XmlRoot(ElementName="Point")]
	public class _Point {
		[XmlAttribute(AttributeName="X")]
		public string _X { get; set; }
		[XmlAttribute(AttributeName="Y")]
		public string _Y { get; set; }
	}

	[XmlRoot(ElementName="Obstacle")]
	public class _Obstacle {
		[XmlElement(ElementName="Point")]
		public List<_Point> _Point { get; set; }
	}

	[XmlRoot(ElementName="Obstacles")]
	public class Obstacles {
		[XmlElement(ElementName="Obstacle")]
		public List<_Obstacle> _Obstacle { get; set; }
	}

	[XmlRoot(ElementName="Task3")]
     	public partial class Task {
     		[XmlElement(ElementName="Start")]
     		public Start _Start { get; set; }
     		[XmlElement(ElementName="Destination")]
     		public Destination _Destination { get; set; }
     		[XmlElement(ElementName="Obstacles")]
     		public Obstacles _Obstacles { get; set; }
     	}	
		[XmlRoot(ElementName="TargetPosition")]
		public class TargetPosition {
			[XmlAttribute(AttributeName="ThetaDeg")]
			public string ThetaDeg { get; set; }
			[XmlAttribute(AttributeName="X")]
			public string X { get; set; }
			[XmlAttribute(AttributeName="Y")]
			public string Y { get; set; }
		}

	[XmlRoot(ElementName = "TargetPositions")]
	public class TargetPositions
	{
		[XmlElement(ElementName = "TargetPosition")]
		public List<TargetPosition> TargetPosition { get; set; }
	}
}


