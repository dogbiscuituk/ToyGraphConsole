namespace ToyGraf.Console
{
    using OpenTK.Graphics.OpenGL;
    using System.IO;
    using System.Text;
    using ToyGraf.Engine;

    public class StaticShader : Shader
    {
        #region Public Interface

        public StaticShader() : this(@"shader.vert", @"shader.frag") { }

        public StaticShader(string vertexPath, string fragmentPath)
        {
            VertexPath = vertexPath;
            FragmentPath = fragmentPath;
            CreateProgram();
        }

        #endregion

        #region Protected Implementation

        protected override void BindAttributes()
        {
            BindAttribute(0, "position");
            BindAttribute(1, "time");
        }

        protected override string GetScript(ShaderType shaderType)
        {
            var sourcePath = GetSourcePath(shaderType);
            if (string.IsNullOrWhiteSpace(sourcePath))
                return string.Empty;
            using (var reader = new StreamReader(sourcePath, Encoding.UTF8))
                return reader.ReadToEnd();
        }

        #endregion

        #region Private Properties

        private readonly string
            VertexPath,
            FragmentPath;

        #endregion

        #region Private Methods

        private string GetSourcePath(ShaderType shaderType)
        {
            switch (shaderType)
            {
                case ShaderType.VertexShader:
                    return VertexPath;
                case ShaderType.FragmentShader:
                    return FragmentPath;
            }
            return string.Empty;
        }

        #endregion
    }
}
