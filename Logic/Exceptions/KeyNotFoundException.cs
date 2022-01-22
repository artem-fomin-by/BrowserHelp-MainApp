namespace Logic.Exceptions{
    public class RegKeyNotFoundException : Exception{
        public readonly string KeyName;
        public readonly string KeyFullName;

        public RegKeyNotFoundException(string keyName, string keyFullName) : base(){
            KeyName = keyName;
            KeyFullName = keyFullName;
        }

        public RegKeyNotFoundException(string keyName, string keyFullName, string message) : base(message){
            KeyName = keyName;
            KeyFullName = keyFullName;
        }

        public RegKeyNotFoundException(string keyName, string keyFullName, string message, Exception innerException) :
            base(message, innerException){
            KeyName = keyName;
            KeyFullName = keyFullName;
        }
    }
}
