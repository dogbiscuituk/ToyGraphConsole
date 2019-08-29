namespace ToyGraf.Engine.Types
{
    using System.ComponentModel;
    using ToyGraf.Engine.TypeConverters;

    [TypeConverter(typeof(ColourFormatTypeConverter))]
    public class ColourFormat
    {
        public ColourFormat() : this(0) { }

        public ColourFormat(int bpp) : this(bpp, bpp, bpp, bpp) { }

        public ColourFormat(ColourFormat colourFormat) :
            this(colourFormat.Red, colourFormat.Green, colourFormat.Blue, colourFormat.Alpha)
        { }

        public ColourFormat(ColourFormat colourFormat, string fieldName, int value) :
            this(colourFormat)
        {
            switch (fieldName)
            {
                case "Red": Red = value; break;
                case "Blue": Blue = value; break;
                case "Green": Green = value; break;
                case "Alpha": Alpha = value; break;
            }
        }

        public ColourFormat(int red, int green, int blue, int alpha)
        {
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
        }

        [DefaultValue(0)] public int Red { get; set; }
        [DefaultValue(0)] public int Green { get; set; }
        [DefaultValue(0)] public int Blue { get; set; }
        [DefaultValue(0)] public int Alpha { get; set; }

        public override bool Equals(object obj) => obj is ColourFormat f &&
            f.Red == Red && f.Green == Green && f.Blue == Blue && f.Alpha == Alpha;
        public override int GetHashCode() => Red ^ Green ^ Blue ^ Alpha;
        public override string ToString() => $"{Red}, {Green}, {Blue}, {Alpha}";

        public static ColourFormat Parse(string s)
        {
            var t = s.Split(',');
            return new ColourFormat(int.Parse(t[0]), int.Parse(t[1]), int.Parse(t[2]), int.Parse(t[3]));
        }
    }
}
