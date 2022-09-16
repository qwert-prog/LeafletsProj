namespace LeafletsProj.Core.Models
{
    /// <summary>
    /// The class that stores information about the paper format
    /// </summary>
    public class PaperFormat
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets height of format to millimeters
        /// </summary>
        public double Height { get; }

        /// <summary>
        /// Gets or sets name of format
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets or sets width of format to millimeters
        /// </summary>
        public double Width { get; }

        #endregion Public Properties

        #region Public Constructors

        /// <summary>
        /// Initialize an instance of a class <see cref="PaperFormat"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public PaperFormat(string name, double width, double height)
        {
            Name = name;
            Width = width;
            Height = height;
        }

        #endregion Public Constructors
    }
}