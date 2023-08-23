namespace CreateCover.Models
{
    /// <summary>Interface for all supported SVG elements.</summary>
    public interface ISVGElement
    {
        string GetSVG(bool debugInfo);
    }
}
