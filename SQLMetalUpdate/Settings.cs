using System;

namespace SQLMetalUpdate
{
    /// <summary>
    ///   Representation of the settings.
    /// </summary>
    [Serializable]
    public class Settings
    {
        [System.Xml.Serialization.XmlElement("FileToBeEdited")]
        public string FileToBeEdited { get; set; }

        [System.Xml.Serialization.XmlElement("SQLMetalFile")]
        public string SQLMetalFile { get; set; }

        [System.Xml.Serialization.XmlElement("LienToEdit")]
        public int LineToEdit { get; set; }

        [System.Xml.Serialization.XmlElement("TextToInsert")]
        public string TextToInsert { get; set; }
    }
}