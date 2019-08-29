namespace ToyGraf.Console
{
    using OpenTK;
    using ToyGraf.Engine.Entities;

    public class Entity : IEntity
    {
        public Entity(Prototype prototype, Vector3 location, Vector3 rotation, Vector3 scale)
        {
            Prototype = prototype;
            Location = location;
            Rotation = rotation;
            Scale = scale;
        }

        public void MoveBy(float dx, float dy, float dz) => Location += new Vector3(dx, dy, dz);
        public void MoveTo(float x, float y, float z) => Location = new Vector3(x, y, z);
        public void RotateBy(float dx, float dy, float dz) => Rotation += new Vector3(dx, dy, dz);
        public void RotateTo(float x, float y, float z) => Rotation = new Vector3(x, y, z);
        //public void ScaleBy(float scale) => Scale *= scale;
        //public void ScaleTo(float scale) => Scale = scale;

        public Prototype Prototype { get; set; }
        public Vector3 Location { get; set; }
        public Vector3 Rotation { get; set; }
        public Vector3 Scale { get; set; }
    }
}
