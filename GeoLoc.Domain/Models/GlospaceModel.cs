namespace GeoLoc.Domain.Models;

public class GlospaceModel
{
    public int? DeviceId { get; set; } 
    
    public DateTime? Date { get; set; } 

    public DateTime? Time { get; set; }

    public string? Latitude { get; set; } = string.Empty;
    
    public char? NS { get; set; }

    public string? Longitude { get; set; } = string.Empty;
    
    public char? EW { get; set; }
    
    public int? Speed { get; set; }
    
    public int? Course { get; set; } 
    
    public int? Altitude { get; set; }
    
    public int? Odometer { get; set; } 
    
    public int? IOStatus { get; set; } 
    
    public int? EventId { get; set; }
    
    public double? AIN1 { get; set; } 
    
    public double? AIN2 { get; set; }
    
    public int? FixMode { get; set; }
    
    public int? GlonassSatNo { get; set; }

    public int? GPSSatNo { get; set; }

    public double? HDOP { get; set; }

}