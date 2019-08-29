namespace ToyGraf.Engine.TypeConverters
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using ToyGraf.Engine.Types;

    public class Point3FTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) =>
            sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string s)
                return Point3F.Parse(s);
            return base.ConvertFrom(context, culture, value);
        }

        public override PropertyDescriptorCollection GetProperties(
            ITypeDescriptorContext context, object value, Attribute[] attributes) =>
            TypeDescriptor.GetProperties(typeof(Point3F), attributes)
                .Sort(new string[] { "X", "Y", "Z" });

        public override bool GetPropertiesSupported(ITypeDescriptorContext context) => true;
    }
}
