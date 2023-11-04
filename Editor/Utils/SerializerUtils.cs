using System.IO;
using System.Runtime.Serialization;

namespace Editor.Utils
{
    public static class SerializerUtils
    {
        public static void ToFile<T>(this T instance, string path)
        {
            using (var fs = new FileStream(path, FileMode.Create))
            {
                DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(T));
                dataContractSerializer.WriteObject(fs, instance);
            }
        }
        public static T FromFile<T>(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open))
            {
                DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(T));
                T instance = (T) dataContractSerializer.ReadObject(fs);
                return instance;

            }
        }
    }
}
