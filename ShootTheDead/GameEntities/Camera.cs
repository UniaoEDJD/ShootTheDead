namespace ShootTheDead

{
public class Camera
{
    public Matrix Transform { get; private set; }

    public void Follow(Sprite target)
    {
        var position = Matrix.CreateTranslation
        (
            -target.sPosition.X - ( target.sRectangles.Width/2),
            -target.sPosition.Y - (target.sRectangles.Height/2),
            0 
        ); 
        var offset = Matrix.CreateTranslation
        (
            Game1.ScreenWidth/2,
            Game1.ScreenHeight/2,
            0
        );

        Transform = position * offset;
    }
}

}