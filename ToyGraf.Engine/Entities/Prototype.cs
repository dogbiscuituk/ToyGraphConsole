namespace ToyGraf.Engine.Entities
{
    using OpenTK.Graphics.OpenGL;
    using System;
    using System.Collections.Generic;

    public class Prototype : IDisposable
    {
        #region Public Interface

        public Prototype(float[] positions, int[] indices)
        {
            VaoID = CreateVao();
            VertexCount = indices.Length;
            GL.BindVertexArray(VaoID);
            BindIndicesBuffer(indices);
            StoreDataInAttributeList(0, positions);
            UnbindVao();
        }

        public int VaoID { get; }
        public int VertexCount { get; }

        #endregion

        #region Private Methods

        private void BindIndicesBuffer(int[] indices)
        {
            var vboID = CreateVbo();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, vboID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(int), indices, BufferUsageHint.StaticDraw);
        }

        private int CreateVao()
        {
            GL.GenVertexArrays(1, out int vaoID);
            VaoIDs.Add(VaoID);
            return vaoID;
        }

        private int CreateVbo()
        {
            GL.GenBuffers(1, out int vboID);
            VboIDs.Add(vboID);
            return vboID;
        }

        private void StoreDataInAttributeList(int attributeNumber, float[] data)
        {
            var vboID = CreateVbo();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboID);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attributeNumber, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        private void UnbindVao() => GL.BindVertexArray(0);

        #endregion

        #region IDisposable Support

        ~Prototype() => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // Dispose managed state (managed objects).

                }
                // Free unmanaged resources (unmanaged objects) & set large fields to null.
                Cleanup();
                disposedValue = true;
            }
        }

        private readonly List<int> TextureIDs = new List<int>();
        private readonly List<int> VaoIDs = new List<int>();
        private readonly List<int> VboIDs = new List<int>();

        private void Cleanup()
        {
            //foreach (var vboID in VboIDs)
            //    GL.DeleteBuffer(vboID);
            //VboIDs.Clear();
            //foreach (var vaoID in VaoIDs)
            //    GL.DeleteVertexArray(vaoID);
            //VaoIDs.Clear();
            //foreach (var textureID in TextureIDs)
            //    GL.DeleteTexture(textureID);
            //TextureIDs.Clear();
        }

        private bool disposedValue = false;

        #endregion
    }
}
