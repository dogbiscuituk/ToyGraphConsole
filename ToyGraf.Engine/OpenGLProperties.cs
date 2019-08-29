namespace ToyGraf.Engine
{
    using OpenTK.Graphics.OpenGL;
    using System.Collections.Generic;

    public class OpenGLProperties
    {
        public OpenGLProperties()
        {
            MajorVersionNumber = GL.GetInteger(GetPName.MajorVersion);
            MinorVersionNumber = GL.GetInteger(GetPName.MinorVersion);
            OpenGLVersionNumber = GL.GetString(StringName.Version);
            VendorString = GL.GetString(StringName.Vendor);
            RendererName = GL.GetString(StringName.Renderer);
            var extensionsCount = GL.GetInteger(GetPName.NumExtensions);
            Extensions = new List<string>();
            for (var index = 0; index < extensionsCount; index++)
                Extensions.Add(GL.GetString(StringNameIndexed.Extensions, index));
            GLSLVersionNumber = GL.GetString(StringName.ShadingLanguageVersion);
        }

        public int
            MajorVersionNumber,
            MinorVersionNumber;

        public string
            GLSLVersionNumber,
            OpenGLVersionNumber, // < 3
            RendererName,
            VendorString;

        public List<string>
            Extensions;
    }
}
