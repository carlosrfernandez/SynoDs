namespace SynoDs.Core.Exception
{
    public class SynologyException : System.Exception
    {
        public SynologyException()
            : base("Unknown error occurred.")
        {
        }

        public SynologyException(string message)
            : base(message)
        {
        }

        public SynologyException(int errorCode, string message)
        {
            throw new SynologyException(string.Format("Error code: {0}: {1}", errorCode, message));
        }
    }
}
