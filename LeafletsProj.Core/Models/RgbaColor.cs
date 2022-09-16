using System.Globalization;
using Windows.UI;

namespace LeafletsProj.Core.Models
{
    /// <summary>
    /// Represents simple model of RGBA color entity
    /// </summary>
    public readonly struct RgbaColor
    {
        #region Public Properties

        /// <summary>
        /// Alpha channel value
        /// </summary>
        public int A { get; }

        /// <summary>
        /// Blue channel value
        /// </summary>
        public int B { get; }

        /// <summary>
        /// Gets new <see cref="Windows.UI.Color"/> instance from ARGB properties
        /// </summary>
        public Color Color => Color.FromArgb((byte)A, (byte)R, (byte)G, (byte)B);

        /// <summary>
        /// Green channel value
        /// </summary>
        public int G { get; }

        /// <summary>
        /// Gets hex representation of color for code like 0xFF99CC
        /// </summary>
        public string HexString => "0x" + IntHex.ToString("X6", CultureInfo.InvariantCulture);

        /// <summary>
        /// Returns integer representation of color without alpha channel
        /// </summary>
        public int IntHex => 0 | (R << 16) | (G << 8) | B;

        /// <summary>
        /// Gets hex representation of color for markup like #FF99CC
        /// </summary>
        public string MarkupColorString => "#" + IntHex.ToString("X6", CultureInfo.InvariantCulture);

        /// <summary>
        /// Red channel value
        /// </summary>
        public int R { get; }

        #endregion Public Properties

        #region Public Constructors

        /// <summary>
        /// Explicit constructor of RGBA color values
        /// </summary>
        /// <param name="a">Alpha channel value</param>
        /// <param name="r">Red channel value</param>
        /// <param name="g">Green channel value</param>
        /// <param name="b">Blue channel value</param>
        public RgbaColor(int a, int r, int g, int b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }

        /// <summary>
        /// Default ctor with defaults for fields
        /// </summary>
/*        public RgbaColor()
        {
            A = 0xFF;
            R = default;
            G = default;
            B = default;
        }*/

        /// <summary>
        /// Constructor for initializaing from hex value with RGB
        /// </summary>
        /// <param name="hexValue"></param>
        public RgbaColor(int hexValue)
        {
            A = 255;
            R = (hexValue & 0xff0000) >> 16;
            G = (hexValue & 0xff00) >> 8;
            B = (hexValue & 0xff);
        }

        #endregion Public Constructors

        #region Public Methods

        public static bool operator !=(RgbaColor lhs, RgbaColor rhs) => !(lhs == rhs);

        public static bool operator ==(RgbaColor lhs, RgbaColor rhs) => lhs.Equals(rhs);

        public override bool Equals(object obj) => obj is RgbaColor other && this.Equals(other);

        public bool Equals(RgbaColor c) => A == c.A && R == c.R && G == c.G && B == c.B;

        public override int GetHashCode() => (A, R, G, B).GetHashCode();

        #endregion Public Methods
    }
}