namespace SynoDs.Core.Exceptions
{
    public class SynologyException : System.Exception
    {
        public SynologyException()
        {
        }

        public SynologyException(string message)
            : base(message)
        {
        }

        public SynologyException(int errorCode, string message)
            : base(string.Format("Error code: {0}: {1}", errorCode, message))
        {
            
        }
        
        public SynologyException(string error, System.Exception innerException) : base(error, innerException) { }
    }
}
