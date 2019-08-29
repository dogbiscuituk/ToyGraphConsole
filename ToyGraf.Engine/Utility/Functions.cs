namespace ToyGraf.Engine.Utility
{
    using System;

    public static class Functions
    {
#pragma warning disable IDE1006 // Naming Styles

        public static double abs(double x) => Math.Abs(x);
        public static double ceil(double x) => Math.Ceiling(x);
        public static double clamp(double x, double minVal, double maxVal) => Math.Min(Math.Max(x, minVal), maxVal);
        public static double floor(double x) => Math.Floor(x);
        public static double fract(double x) => x - Math.Floor(x);
        public static double inversesqrt(double x) => 1 / Math.Sqrt(x);
        public static bool isinf(double x) => double.IsInfinity(x);
        public static bool isnan(double x) => double.IsNaN(x);
        public static double max(double x, double y) => Math.Max(x, y);
        public static double min(double x, double y) => Math.Min(x, y);
        public static double mix(double x, double y, bool a) => a ? y : x;
        public static double mix(double x, double y, double a) => x * (1 - a) + y * a;
        public static double mod(double x, double y) => x - y * Math.Floor(x / y);
        public static double round(double x) => Math.Round(x);
        public static double sign(double x) => Math.Sign(x);
        public static double smoothstep(double a, double b, double x) => x <= a ? 0 : x >= b ? 1 : Hermite((x - a) / (b - a));
        public static double sqrt(double x) => Math.Sqrt(x);
        public static double step(double edge, double x) => x < edge ? 0 : 1;
        public static double trunc(double x) => (Math.Floor(Math.Abs(x)) * Math.Sign(x));

        public static float abs(float x) => Math.Abs(x);
        public static float acos(float x) => (float)Math.Acos(x);
        public static float acosh(float x) => (float)Math.Log(x + Math.Sqrt(x * x - 1));
        public static float asin(float x) => (float)Math.Asin(x);
        public static float asinh(float x) => (float)Math.Log(x + Math.Sqrt(x * x + 1));
        public static float atan(float y_over_x) => (float)Math.Atan(y_over_x);
        public static float atan(float y, float x) => (float)Math.Atan2(y, x);
        public static float atanh(float x) => (float)Math.Log((1 + x) / (1 - x)) / 2;
        public static float ceil(float x) => (float)Math.Ceiling(x);
        public static float clamp(float x, float minVal, float maxVal) => Math.Min(Math.Max(x, minVal), maxVal);
        public static float cos(float angle) => (float)Math.Cos(angle);
        public static float cosh(float x) => (float)Math.Cosh(x);
        public static float degrees(float radians) => (float)(180 * radians / Math.PI);
        public static float exp(float x) => (float)Math.Exp(x);
        public static float exp2(float x) => (float)Math.Exp(x * Math.Log(2));
        public static float floor(float x) => (float)Math.Floor(x);
        public static float fract(float x) => x - (float)Math.Floor(x);
        public static float inversesqrt(float x) => 1 / (float)Math.Sqrt(x);
        public static bool isinf(float x) => float.IsInfinity(x);
        public static bool isnan(float x) => float.IsNaN(x);
        public static float log(float x) => (float)Math.Log(x);
        public static float log2(float x) => (float)(Math.Log(x) / Math.Log(2));
        public static float max(float x, float y) => Math.Max(x, y);
        public static float min(float x, float y) => Math.Min(x, y);
        public static float mix(float x, float y, bool a) => a ? y : x;
        public static float mix(float x, float y, float a) => x * (1 - a) + y * a;
        public static float mod(float x, float y) => x - y * (float)Math.Floor(x / y);
        public static float pow(float x, float y) => (float)Math.Pow(x, y);
        public static float radians(float degrees) => (float)(Math.PI * degrees / 180);
        public static float round(float x) => (float)Math.Round(x);
        public static float sign(float x) => Math.Sign(x);
        public static float sin(float angle) => (float)Math.Sin(angle);
        public static float sinh(float x) => (float)Math.Sinh(x);
        public static float smoothstep(float a, float b, float x) => x <= a ? 0 : x >= b ? 1 : Hermite((x - a) / (b - a));
        public static float sqrt(float x) => (float)Math.Sqrt(x);
        public static float step(float edge, float x) => x < edge ? 0 : 1;
        public static float tan(float angle) => (float)Math.Tan(angle);
        public static float tanh(float x) => (float)Math.Tanh(x);
        public static float trunc(float x) => (float)(Math.Floor(Math.Abs(x)) * Math.Sign(x));

        public static int abs(int x) => Math.Abs(x);
        public static int clamp(int x, int minVal, int maxVal) => Math.Min(Math.Max(x, minVal), maxVal);
        public static int max(int x, int y) => Math.Max(x, y);
        public static int min(int x, int y) => Math.Min(x, y);
        public static int sign(int x) => Math.Sign(x);

        public static uint clamp(uint x, uint minVal, uint maxVal) => Math.Min(Math.Max(x, minVal), maxVal);
        public static uint max(uint x, uint y) => Math.Max(x, y);
        public static uint min(uint x, uint y) => Math.Min(x, y);

#pragma warning restore IDE1006 // Naming Styles

        private static double Hermite(double x) => x * x * (3 - 2 * x);
        private static float Hermite(float x) => x * x * (3 - 2 * x);
    }
}
