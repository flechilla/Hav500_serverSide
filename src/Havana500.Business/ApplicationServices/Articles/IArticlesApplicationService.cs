﻿using Havana500.Business.Base;
using Havana500.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Havana500.Business.ApplicationServices.Articles
{
    /// <summary>
    ///     <para>
    ///         Contains the declaration of the  necessary functionalities
    ///         to handle the operations on the <see cref="Havana500.Domain.Article" /> entity.
    ///     </para>
    ///     <remarks>
    ///         This object handle the data of the <see cref="Havana500.Domain.Article" /> entity
    ///         through the <see cref="IArticlesRepository" /> but when necessary
    ///         adds some data or apply operations on the data before pass it to the DataAcces layer
    ///         or to the Presentation layer
    ///     </remarks>
    /// </summary>
    public interface IArticlesApplicationService : IBaseApplicationService<Article, int>
    {
        /// <summary>
        ///     Increment the amount of views in the Article
        ///     with Id equal to <paramref name="articleId"/>.
        /// </summary>
        /// <param name="articleId">The Id of the Article.</param>
        /// <returns>Returns the total amount of views for the given article.</returns>
        int AddView(int articleId);

        /// <summary>
        ///     Increment the amount of views in the Article
        ///     with Id equal to <paramref name="articleId"/> asynchronously.
        /// </summary>
        /// <param name="articleId">The Id of the Article.</param>
        /// <returns>Returns the total amount of views for the given article.</returns>
        Task<int> AddViewAsync(int articleId);
    }
}
