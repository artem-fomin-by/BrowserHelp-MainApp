using System.Runtime.InteropServices;

namespace Logic.Exceptions
{
    public class NotSupportedOSException : NotSupportedException{
        public const string Windows = "Microsoft Windows";
        public const string Linux = "Linux";
        public const string OSX = "OSX";
        public const string FreeBSD = "FreeBSD";

        public static void CheckOS(string osName){
            OSPlatform os;
            switch(osName){
                case Windows:
                    os = OSPlatform.Windows;
                    break;
                case Linux:
                    os = OSPlatform.Linux;
                    break;
                case OSX:
                    os = OSPlatform.OSX;
                    break;
                case FreeBSD:
                    os = OSPlatform.FreeBSD;
                    break;
                default: throw new NotSupportedOSException("OS: " + osName + " is not supported");
            }

            if (!RuntimeInformation.IsOSPlatform(os))
                throw new NotSupportedException("The GetKey function does not support any OS but Microsoft Windows");
        }

        #region Constructors
        
        public NotSupportedOSException() : base(){}

        public NotSupportedOSException(string message) : base(message){}

        public NotSupportedOSException(string message, Exception innerException) : base(message, innerException) {}
        
        #endregion
    }
}
