using Havana500.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Havana500.Domain.Models.Media
{
    /// <summary>
    ///     Contains the data of a media file that is stored
    ///     in DB.
    /// </summary>
    public class MediaStorage : Entity<int>
    {
        public byte[] Data{ get; set; }
    }
}
