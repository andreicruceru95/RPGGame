using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace GameTest
{
    /// <summary>
    /// This class will manage xml files.
    /// </summary>
    /// <typeparam name="T">type of object to mnanage.</typeparam>
    public class XmlManager<T>
    {
        public Type Type;
        public XmlManager()
        {
            Type = typeof(T);
        }
        /// <summary>
        /// Load from path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public T Load(string path)
        {
            T instance;
            using(TextReader reader = new StreamReader(path))
            {                
                XmlSerializer xml = new XmlSerializer(Type);
                instance = (T)xml.Deserialize(reader);
            }
            return instance;
        }
        /// <summary>
        /// Save to file.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="obj"></param>
        public void Save(string path, object obj)
        {
            using (TextWriter writer = new StreamWriter(path))
            {
                XmlSerializer xml = new XmlSerializer(Type);
                xml.Serialize(writer, obj);
            }
        }
    }
}
