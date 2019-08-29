namespace ToyGraf.Engine.Utility
{
    using OpenTK;
    using ToyGraf.Engine.Types;

    public static class Maths
    {
        public static Matrix4 CreateCameraView(Camera camera) =>
            CreateCameraView(camera.Position.ToPoint3F(), camera.Rotation.ToEuler3F());

        public static Matrix4 CreateCameraView(Point3F position, Euler3F rotation) =>
            Matrix4.CreateTranslation(-position.ToVector3()) *
            Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation.Roll)) *
            Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotation.Yaw)) *
            Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotation.Pitch));

        public static Matrix4 CreateOrthographicProjection(
            float width, float height, float zNear, float zFar) =>
            Matrix4.CreateOrthographic(width, height, zNear, zFar);

        public static Matrix4 CreateOrthographicProjection(
            float left, float right, float bottom, float top, float zNear, float zFar) =>
            Matrix4.CreateOrthographicOffCenter(left, right, bottom, top, zNear, zFar);

        public static Matrix4 CreatePerspectiveProjection(
            float fovy, float aspect, float zNear, float zFar) =>
            Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(fovy), aspect, zNear, zFar);

        public static Matrix4 CreatePerspectiveProjection(
            float left, float right, float bottom, float top, float zNear, float zFar) =>
            Matrix4.CreatePerspectiveOffCenter(left, right, bottom, top, zNear, zFar);

        public static Matrix4 CreateTransformation(
            Point3F location, Euler3F orientation, Point3F scale) =>
            Matrix4.CreateScale(scale.X, scale.Y, scale.Z) *
            Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(orientation.Roll)) *
            Matrix4.CreateRotationY(MathHelper.DegreesToRadians(orientation.Yaw)) *
            Matrix4.CreateRotationX(MathHelper.DegreesToRadians(orientation.Pitch)) *
            Matrix4.CreateTranslation(location.ToVector3());

        public static Euler3F ToEuler3F(this Vector3 v) => new Euler3F(v.X, v.Y, v.Z);
        public static Euler3F ToEuler3F(this Quaternion v) => new Euler3F(v.X, v.Y, v.Z);
        public static Point3F ToPoint3F(this Vector3 v) => new Point3F(v.X, v.Y, v.Z);
        public static Vector3 ToVector3(this Euler3F e) => new Vector3(e.Pitch, e.Yaw, e.Roll);
        public static Vector3 ToVector3(this Point3F p) => new Vector3(p.X, p.Y, p.Z);
    }
}
