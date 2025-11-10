namespace MotoVision.Domain.Models
{
	public class RiskPredictionInput
	{
		public float DaysInOperation { get; set; }
		public float TotalMileageKm { get; set; }
		public string YardType { get; set; } = string.Empty;
	}
}

