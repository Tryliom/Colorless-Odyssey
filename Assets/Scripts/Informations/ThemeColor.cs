using UnityEngine;

public class ThemeColor
{
    public string Name { get; set; }
    public Color Color { get; set; }
}

public class YellowThemeColor : ThemeColor
{
    public YellowThemeColor()
    {
        Name = "Yellow";
        Color = new Color(255f / 255f, 248f / 255f, 4f / 255f);
    }
}