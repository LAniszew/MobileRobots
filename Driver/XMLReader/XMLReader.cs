using System.Data;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Driver.AutomaticControl.TrajectoryGenerator;


namespace Driver.XMLReader
{
    public class XMLReader
    {
        public Task readXml(string path) {
            
           // Debug.WriteLine(path);
           // var customRoot = dataObject as XmlRootAttribute;
            //make ifs for different tasks, based on file name
            XmlSerializer ser = new XmlSerializer(typeof(Task));
            Task task = new Task();
            XmlReader reader = XmlReader.Create(path); 
        {
            task  = (Task) ser.Deserialize(reader);
        }
            return task;
        }

        public TargetPositions readTask1(string path)
        {
            XmlSerializer ser = new XmlSerializer(typeof(TargetPositions));
            TargetPositions task = new TargetPositions();
            XmlReader reader = XmlReader.Create(path); 
            {
                task  = (TargetPositions) ser.Deserialize(reader);
            }
            return task;
        }
        }
    }
