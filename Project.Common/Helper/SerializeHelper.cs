using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Project.Common.Helper
{
    public class XSerializer<T>
    {
        public string FilePath { get; set; }

        public XSerializer(string filePath)
        {
            this.FilePath = filePath;
        }

        public void Serialize(T banners)
        {
            using (FileStream fs = new FileStream(this.FilePath, FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                try
                {
                    serializer.Serialize(fs, banners);
                }
                catch (SerializationException e)
                {
                    Console.WriteLine("序列化出錯，原因: " + e.Message);
                    throw;
                }
                finally
                {
                    fs.Close();
                }
            }
        }

        public T Deserialize()
        {
            T _data;

            using (FileStream fs = new FileStream(this.FilePath, FileMode.OpenOrCreate))
            {
                //null
                if (fs.Length < 1)
                {
                    return default(T);
                }

                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    _data = (T)serializer.Deserialize(fs);
                }
                catch (SerializationException e)
                {
                    Console.WriteLine("反序列化出錯，原因: " + e.Message);
                    throw;
                }
                finally
                {
                    fs.Close();
                }
            }
            return _data;
        }
    }
}
