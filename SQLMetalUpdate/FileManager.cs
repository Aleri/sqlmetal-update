using System.IO;
using System.Xml.Serialization;

namespace SQLMetalUpdate
{
    /// <summary>
    ///   Handles file related tasks : creation, reading from, writing to files.
    /// </summary>
    public class FileManager
    {
        private const string FileName = "Settings.xml";
        private static readonly string _path = Directory.GetCurrentDirectory();
        private readonly string _newPath = System.IO.Path.Combine(_path, FileName);

        public FileManager()
        {
            Settings = new Settings();
        }

        public Settings Settings { get; set; }

        /// <summary>
        ///   Loads settings from the disk.
        /// </summary>
        public void Load()
        {
            if (!File.Exists(_newPath))
            {
                Create();
            }
            else
            {
                var info = new FileInfo(_newPath);
                if (info.Length != 0)
                {
                    var serializer = new XmlSerializer(typeof (Settings));
                    var reader = new StreamReader(_newPath);
                    Settings = (Settings) serializer.Deserialize(reader);
                    reader.Close();
                }
            }
        }

        /// <summary>
        ///   Saves the settings to disk.
        /// </summary>
        /// <param name="settings"> Settings to be written to disk </param>
        public void Save(Settings settings)
        {
            var serializer = new XmlSerializer(typeof (Settings));
            var writer = new StreamWriter(_newPath);
            serializer.Serialize(writer, settings);
            writer.Close();
        }

        /// <summary>
        ///   Create an empty file.
        /// </summary>
        private void Create()
        {
            System.IO.FileStream fs = System.IO.File.Create(_newPath);
            fs.Close();
        }

        public void InsertLineInFile(string path, string line, int position)
        {
            string[] lines = File.ReadAllLines(path);
            using (var writer = new StreamWriter(path))
            {
                for (int i = 0; i < position; i++)
                    writer.WriteLine(lines[i]);
                writer.WriteLine(line);
                for (int i = position; i < lines.Length; i++)
                    writer.WriteLine(lines[i]);
            }
        }
    }
}