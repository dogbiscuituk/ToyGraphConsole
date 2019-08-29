namespace ToyGraf.Engine
{
    using OpenTK;
    using OpenTK.Graphics.OpenGL;
    using System.Text;
    using ToyGraf.Engine.Utility;

    public abstract class Shader
    {
        #region Public Interface

        public Shader() { }

        public virtual void Cleanup() => DeleteProgram();

        public virtual string CreateProgram()
        {
            ShaderLog = new StringBuilder();
            ProgramID = GL.CreateProgram();
            CreateShaders();
            BindAttributes();
            GL.LinkProgram(ProgramID);
            GL.ValidateProgram(ProgramID);
            GetAllUniformLocations();
            var log = ShaderLog.ToString();
            ShaderLog = null;
#if DEBUG
            System.Diagnostics.Debug.WriteLine(log);
#endif
            DeleteShaders();
            return log;
        }

        public void LoadProjectionMatrix(Matrix4 matrix) => LoadMatrix(location_projectionMatrix, matrix);
        public void LoadTransformationMatrix(Matrix4 matrix) => LoadMatrix(location_transformationMatrix, matrix);
        public void LoadViewMatrix(Camera camera) => LoadMatrix(location_viewMatrix, Maths.CreateCameraView(camera));

        public void Start() => GL.UseProgram(ProgramID);
        public void Stop() => GL.UseProgram(0);

        #endregion

        #region Protected Methods

        protected void BindAttribute(int attributeIndex, string variableName) =>
            GL.BindAttribLocation(ProgramID, attributeIndex, variableName);

        protected abstract void BindAttributes();

        protected int GetUniformLocation(string uniformName) => GL.GetUniformLocation(ProgramID, uniformName);
        protected void LoadFloat(int location, float value) => GL.Uniform1(location, value);
        protected void LoadVector(int location, Vector3 vector) => GL.Uniform3(location, vector);
        protected void LoadBoolean(int location, bool value) => GL.Uniform1(location, value ? 1f : 0f);
        protected void LoadMatrix(int location, Matrix4 value) => GL.UniformMatrix4(location, false, ref value);

        #endregion

        #region Private Properties

        private int
            ProgramID,
            VertexShaderID,
            TessControlShaderID,
            TessEvaluationShaderID,
            GeometryShaderID,
            FragmentShaderID,
            ComputeShaderID;

        private int
            location_projectionMatrix,
            location_transformationMatrix,
            location_viewMatrix;

        private StringBuilder ShaderLog;

        #endregion

        #region Private Methods

        private void AppendLog(string log)
        {
            if (!string.IsNullOrWhiteSpace(log))
                ShaderLog.AppendLine(log);
        }

        private int CreateShader(ShaderType shaderType, bool mandatory = false)
        {
            var shaderSource = GetScript(shaderType);
            if (string.IsNullOrWhiteSpace(shaderSource))
                return 0;
            var shaderID = GL.CreateShader(shaderType);
            GL.ShaderSource(shaderID, shaderSource);
            GL.CompileShader(shaderID);
            AppendLog(GL.GetShaderInfoLog(shaderID));
            GL.AttachShader(ProgramID, shaderID);
            return shaderID;
        }

        private void CreateShaders()
        {
            VertexShaderID = CreateShader(ShaderType.VertexShader, true);
            TessControlShaderID = CreateShader(ShaderType.TessControlShader);
            TessEvaluationShaderID = CreateShader(ShaderType.TessEvaluationShader);
            GeometryShaderID = CreateShader(ShaderType.GeometryShader);
            FragmentShaderID = CreateShader(ShaderType.FragmentShader, true);
            ComputeShaderID = CreateShader(ShaderType.ComputeShader);
        }

        private void DeleteProgram()
        {
            if (ProgramID == 0)
                return;
            DeleteShaders();
            GL.DeleteProgram(ProgramID);
            ProgramID = 0;
        }

        private void DeleteShader(ref int shaderID)
        {
            if (shaderID == 0)
                return;
            GL.DetachShader(ProgramID, shaderID);
            GL.DeleteShader(shaderID);
            shaderID = 0;
        }

        private void DeleteShaders()
        {
            DeleteShader(ref VertexShaderID);
            DeleteShader(ref TessControlShaderID);
            DeleteShader(ref TessEvaluationShaderID);
            DeleteShader(ref GeometryShaderID);
            DeleteShader(ref FragmentShaderID);
            DeleteShader(ref ComputeShaderID);
        }

        protected virtual void GetAllUniformLocations()
        {
            location_projectionMatrix = GetUniformLocation("projectionMatrix");
            location_transformationMatrix = GetUniformLocation("transformationMatrix");
            location_viewMatrix = GetUniformLocation("viewMatrix");
        }

        protected abstract string GetScript(ShaderType shaderType);

        #endregion
    }
}
