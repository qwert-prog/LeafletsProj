using System.Collections.Generic;

namespace LeafletsProj.Models
{
    /// <summary>
    /// Represents model of card
    /// </summary>
    public sealed class Card : Paper
    {
        /// <summary>
        /// Gets or sets path to the back image
        /// </summary>
        public string BackImagePath { get; set; }

        /// <summary>
        /// Gets or sets list texts in back image
        /// </summary>
        public List<Text> BackTextTemplates { get; set; }

        /// <summary>
        /// Gets or sets list images in back image
        /// </summary>
        public List<ImageModel> BackImageTemplates { get; set; } = new List<ImageModel>();
    }
}