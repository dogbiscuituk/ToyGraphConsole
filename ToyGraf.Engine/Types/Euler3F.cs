namespace ToyGraf.Engine.Types
{
    using System;
    using System.ComponentModel;
    using ToyGraf.Engine.TypeConverters;

    [TypeConverter(typeof(Euler3FTypeConverter))]
    public class Euler3F
    {
        public Euler3F() : this(0, 0, 0) { }

        public Euler3F(Euler3F p) : this(p.Pitch, p.Yaw, p.Roll) { }

        public Euler3F(Euler3F p, string fieldName, float value) : this(p)
        {
            switch (fieldName)
            {
                case "Pitch": Pitch = value; break;
                case "Yaw": Yaw = value; break;
                case "Roll": Roll = value; break;
            }
        }

        public Euler3F(float pitch, float yaw, float roll)
        {
            Pitch = pitch;
            Yaw = yaw;
            Roll = roll;
        }

        [DefaultValue(0f)] public float Pitch { get; set; }
        [DefaultValue(0f)] public float Yaw { get; set; }
        [DefaultValue(0f)] public float Roll { get; set; }

        public override bool Equals(object obj) => obj is Euler3F p &&
            p.Pitch == Pitch && p.Yaw == Yaw && p.Roll == Roll;
        public override int GetHashCode() => Pitch.GetHashCode() ^ Yaw.GetHashCode() ^ Roll.GetHashCode();
        public override string ToString() => $"{Pitch}, {Yaw}, {Roll}";

        public static Euler3F Parse(string s)
        {
            var t = s.Split(',');
            return new Euler3F(float.Parse(t[0]), float.Parse(t[1]), float.Parse(t[2]));
        }
    }
}
