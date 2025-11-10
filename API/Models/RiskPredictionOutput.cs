using Microsoft.ML.Data;

namespace MotoVision.Domain.Models
{
    public class RiskPredictionOutput
    {
        [ColumnName("PredictedLabel")]
        public bool Prediction { get; set; }

        public float Probability { get; set; }
        public float Score { get; set; }
    }
}

