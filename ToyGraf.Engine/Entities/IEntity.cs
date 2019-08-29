namespace ToyGraf.Engine.Entities
{
    using OpenTK;

    public interface IEntity
    {
        Prototype Prototype { get; set; }
        Vector3 Location { get; set; }
        Vector3 Rotation { get; set; }
        Vector3 Scale { get; set; }

        void MoveBy(float dx, float dy, float dz);
        void MoveTo(float x, float y, float z);
        void RotateBy(float dx, float dy, float dz);
        void RotateTo(float x, float y, float z);
        //void ScaleBy(float scale);
        //void ScaleTo(float scale);
    }
}
