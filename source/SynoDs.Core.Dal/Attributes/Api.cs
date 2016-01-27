// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Api.cs" company="">
//   
// </copyright>
// <summary>
//   The api.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynoDs.Core.Dal.Attributes
{
    using System;

    using SynoDs.Core.Dal.Enums;

    /// <summary>
    /// The api.
    /// </summary>
    public sealed class Api : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Api"/> class.
        /// </summary>
        /// <param name="rootApi">
        /// The root api.
        /// </param>
        /// <param name="chidApi">
        /// The chid api.
        /// </param>
        public Api(RootApi rootApi, ChildApi chidApi)
        {
            this.RootApi = rootApi;
            this.ChildApi = chidApi;
        }

        /// <summary>
        /// Gets the root api.
        /// </summary>
        private RootApi RootApi { get; }

        /// <summary>
        /// Gets the child api.
        /// </summary>
        private ChildApi ChildApi { get; }

        /// <summary>
        /// The get api.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetApi()
        {
            return string.Format("SYNO.{0}.{1}", this.RootApi, this.ChildApi);
        }

        /// <summary>
        /// The get root api.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetRootApi()
        {
            return this.RootApi.ToString();
        }

        /// <summary>
        /// The get child api.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetChildApi()
        {
            return this.ChildApi.ToString();
        }
    }
}