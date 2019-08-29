namespace ToyGraf.Engine.Utility
{
    public static class Processes
    {
        public static void Launch(this string url) => System.Diagnostics.Process.Start(url);
    }
}
