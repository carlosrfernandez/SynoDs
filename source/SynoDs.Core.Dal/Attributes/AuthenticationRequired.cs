// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthenticationRequired.cs" company="">
//   
// </copyright>
// <summary>
//   The authentication required.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynoDs.Core.Dal.Attributes
{
    using System;

    /// <summary>
    /// The authentication required.
    /// </summary>
    public class AuthenticationRequired : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationRequired"/> class.
        /// </summary>
        /// <param name="requiresAuthentication">
        /// The requires authentication.
        /// </param>
        public AuthenticationRequired(bool requiresAuthentication)
        {
            this.RequiresAuthentication = requiresAuthentication;
        }

        /// <summary>
        /// Gets a value indicating whether requires authentication.
        /// </summary>
        private bool RequiresAuthentication { get; }

        /// <summary>
        /// The get authentication requirements.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool GetAuthenticationRequirements()
        {
            return this.RequiresAuthentication;
        }
    }
}