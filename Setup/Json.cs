using System.Runtime.Serialization;

namespace Setup
{
    [DataContract]
    internal class JsonFilePathsSet
    {
        [DataMember]
        public string[] FilePaths { get; set; }
    }


    [DataContract]
    internal class JsonRegKeySet
    {
        [DataMember]
        public JsonRegKey[] RegistryKey_ValuesSets { get; set; }
    }

    [DataContract]
    internal class JsonRegKey
    {
        [DataMember]
        public string MainKey { get; set; }

        [DataMember]
        public string Path { get; set; }
        
        [DataMember]
        public JsonRegValue[] Values { get; set; }
    }

    [DataContract]
    internal class JsonRegValue
    {
        [DataMember]
        public string Name { get; set; }
        
        [DataMember]
        public string Value { get; set; }
    }
}
