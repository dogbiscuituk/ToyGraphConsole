namespace ToyGraf.Engine.Controllers
{
    using OpenTK;
    using OpenTK.Graphics;
    using OpenTK.Graphics.OpenGL;
    using System.Collections.Generic;
    using System.Drawing;
    using ToyGraf.Engine;
    using ToyGraf.Engine.Entities;
    using ToyGraf.Engine.Utility;

    public abstract class Renderer
    {
        #region Public Interface

        public Camera Camera = new Camera();

        #endregion

        #region Virtuals

        protected GraphicsMode GraphicsMode => new GraphicsMode(
            color: new ColorFormat(32),
            depth: 24,
            stencil: 8,
            samples: 0);

        protected abstract IEnumerable<IEntity> GetEntities();

        protected virtual void Load()
        {
        }

        protected virtual void RenderFrame(double time)
        {
            Time += time;

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Texture2D);
            GL.ClearColor(Color.White);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            if (Shader != null)
            {
                Shader.Start();

                lock (this)
                    if (OldProjectionMatrix != NewProjectionMatrix)
                    {
                        OldProjectionMatrix = NewProjectionMatrix;
                        LoadProjectionMatrix();
                    }

                GL.VertexAttrib1(1, (float)Time);

                Shader.LoadViewMatrix(Camera);
                foreach (var entity in Entities)
                {
                    var prototype = entity.Prototype;
                    GL.BindVertexArray(prototype.VaoID);
                    GL.EnableVertexAttribArray(0);

                    var transformationMatrix = Maths.CreateTransformation(
                        entity.Location.ToPoint3F(), entity.Rotation.ToEuler3F(), entity.Scale.ToPoint3F());
                    Shader.LoadTransformationMatrix(transformationMatrix);

                    //GL.DrawArrays(PrimitiveType.LineStrip, 0, prototype.VertexCount);
                    GL.DrawElements(BeginMode.Triangles, prototype.VertexCount, DrawElementsType.UnsignedInt, 0);

                    GL.DisableVertexAttribArray(0);
                    GL.BindVertexArray(0);
                }
                Shader.Stop();
            }
            SwapBuffers();
        }

        protected virtual void Resize() => GL.Viewport(0, 0, DisplayWidth, DisplayHeight);

        protected virtual void Unload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            Shader.Cleanup();
        }

        protected virtual void UpdateFrame(double time) { }

        #endregion

        #region Abstracts

        protected abstract int DisplayWidth { get; }
        protected abstract int DisplayHeight { get; }
        protected abstract void Exit();
        protected abstract void SwapBuffers();

        #endregion

        #region Private Properties

        protected readonly List<IEntity> Entities = new List<IEntity>();
        protected Shader Shader;
        private double Time;

        #endregion

        #region Projection Matrix

        #region Public Properties

        public float FarPlane
        {
            get => _FarPlane;
            set
            {
                if (FarPlane != value)
                {
                    _FarPlane = value;
                    lock (this)
                        UpdateProjectionMatrix();
                }
            }
        }

        public float FieldOfView
        {
            get => _FieldOfView;
            set
            {
                if (FieldOfView != value)
                {
                    _FieldOfView = value;
                    lock (this)
                        UpdateProjectionMatrix();
                }
            }
        }

        public float NearPlane
        {
            get => _NearPlane;
            set
            {
                if (NearPlane != value)
                {
                    _NearPlane = value;
                    lock (this)
                        UpdateProjectionMatrix();
                }
            }
        }

        #endregion

        #region Private Properties

        private float
            _FieldOfView = 70,
            _NearPlane = 0.1f,
            _FarPlane = 1000;

        private Matrix4
            NewProjectionMatrix,
            OldProjectionMatrix;

        #endregion

        #region Private Methods

        protected void LoadProjectionMatrix() => Shader.LoadProjectionMatrix(NewProjectionMatrix);

        /// <summary>
        /// Create a Projection Matrix given values: aspect ratio = A, field of view = FOVy radians,
        /// near plane = Znear, and far plane = Zfar. Then the frustrum length is zm = Zfar - Znear.
        /// Also, let zp = Zfar + Znear. Then we have the following matrix formula:
        /// 
        /// [ x_scale     0      0       0                 ]
        /// [    0     y_scale   0       0                 ]
        /// [    0        0     -zp/zm  -(2*Zfar*Znear)/zm ]
        /// [    0        0     -1       0                 ]
        /// 
        /// where y_scale = cot(FOVy/2), and x_scale = y_scale/A.
        /// </summary>
        protected void UpdateProjectionMatrix()
        {
            NewProjectionMatrix = Maths.CreatePerspectiveProjection(
                FieldOfView,
                1920f / 1080f,
                NearPlane,
                FarPlane);
        }

        #endregion

        #endregion
    }
}
