namespace IP1.Samples.Models
{
    public class GeolocationQuestion : Question
    {
        public Coords StartCoords { get; set; }
        public int? StartZoom { get; set; }
        public MapType DefaultMapType { get; set; } = MapType.ROADMAP;
        public bool ShowMapTypeControl { get; set; } = true;
    }
}
