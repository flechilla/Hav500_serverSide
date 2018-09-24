namespace Havana500.Models.SectionViewModel
{
    public class SectionIndexViewModel : SectionBaseViewModel
    {
        /// <summary>
        ///     Gets or sets the value that indicates the 
        ///     amount of times that the user has entered
        ///     to this section.
        /// </summary>
        public int Views { get; set; }

        /// <summary>
        ///     Gets or sets the value that indicates the 
        ///     amount of comments that the section has.
        /// </summary>
        public int AmountOfComments { get; set; }

        /// <summary>
        ///     Gets or sets the amount of Articles related to the current Section.
        /// </summary>
        public int AmountOfArticles { get; set; }
    }
}
