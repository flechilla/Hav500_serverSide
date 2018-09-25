using System;
using System.Collections.Generic;
using System.Text;

namespace Havana500.Domain.Models
{
    /// <summary>
    ///     Declares the property to identity
    ///     the language of the content.
    /// </summary>
    public interface ILanguage
    {
        /// <summary>
        ///     Gets or sets the Culture for the current
        ///     entity
        /// </summary>
        /// <remarks>
        ///     The value of this property is in neutral
        ///     culture. Ex: 'es', 'en' or 'fr'
        /// </remarks>
        string LanguageCulture { get; set; }
    }
}
