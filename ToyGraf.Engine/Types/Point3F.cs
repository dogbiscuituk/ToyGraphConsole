namespace ToyGraf.Engine.Types
{
    using System;
    using System.ComponentModel;
    using ToyGraf.Engine.TypeConverters;

    [TypeConverter(typeof(Point3FTypeConverter))]
    public class Point3F
    {
        public Point3F() : this(0, 0, 0) { }

        public Point3F(Point3F p) : this(p.X, p.Y, p.Z) { }

        public Point3F(Point3F p, string fieldName, float value) : this(p)
        {
            switch (fieldName)
            {
                case "X": X = value; break;
                case "Y": Y = value; break;
                case "Z": Z = value; break;
            }
        }

        public Point3F(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        [DefaultValue(0f)] public float X { get; set; }
        [DefaultValue(0f)] public float Y { get; set; }
        [DefaultValue(0f)] public float Z { get; set; }

        public override bool Equals(object obj) => obj is Point3F p && p.X == X && p.Y == Y && p.Z == Z;
        public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
        public override string ToString() => $"{X}, {Y}, {Z}";

        public static Point3F Parse(string s)
        {
            var t = s.Split(',');
            return new Point3F(float.Parse(t[0]), float.Parse(t[1]), float.Parse(t[2]));
        }
    }
}
