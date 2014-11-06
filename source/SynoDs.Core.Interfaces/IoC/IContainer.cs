namespace SynoDs.Core.Interfaces.IoC
{
    /// <summary>
    /// Provides an abstraction to an inversion of control Container. It is up to the client to use which ever one they like. 
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// Resolves a type if it was registered in the container.
        /// </summary>
        /// <typeparam name="T">Type to resolve</typeparam>
        /// <returns>The implementation of the requested type.</returns>
        T Resolve<T>();

        /// <summary>
        /// Registers an abstraction to it's implementation in the container. 
        /// </summary>
        /// <typeparam name="TAbs">Abstract class or interface</typeparam>
        /// <typeparam name="TImpl">Implementation of TAbs</typeparam>
        /// <param name="instance">the instance to register</param>
        void RegisterWithInstance<TAbs, TImpl>(TImpl instance) where TImpl : TAbs;


        /// <summary>
        /// Register an existing instance of an abstraction.
        /// </summary>
        /// <typeparam name="T">The abstract class or interface to register.</typeparam>
        /// <typeparam name="TR">The implementation of T to register</typeparam>
        void Register<T, TR>() where TR : T;
    }
}
