namespace SynoDs.Core.CrossCutting
{
    public class Bootstrapper
    {
        public IoCFactory Factory { get; set; }

        public Bootstrapper(IoCFactory factory)
        {
            Factory = factory;
        }

        public void Startup()
        {
            // Register dependencies.

        }

        public void ShutDown()
        {
            
        }
    }
}
