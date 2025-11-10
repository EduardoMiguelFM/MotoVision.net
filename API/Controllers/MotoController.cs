using Microsoft.AspNetCore.Authorization; // Adicionado para [Authorize]
using Microsoft.AspNetCore.Mvc;
using MotoVision.API.Services;
using MotoVision.Domain.Models;

namespace MotoVision.API.Controllers
{
    /// <summary>
    /// Controlador responsável por expor endpoints de Machine Learning e Previsão.
    /// Versão 1.0.
    /// </summary>
    [Authorize] // Protege todos os endpoints neste controller
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/ml")]
    public class MlController : ControllerBase
    {
        private readonly MlPredictionService _mlService;

        public MlController(MlPredictionService mlService)
        {
            _mlService = mlService;
        }

        /// <summary>
        /// Realiza a previsão de risco de avaria para uma moto.
        /// O modelo usa DaysInOperation, TotalMileageKm e YardType para classificar o risco.
        /// Requer autenticação (JWT).
        /// </summary>
        /// <param name="input">Dados de entrada para a previsão.</param>
        /// <returns>Resultado da previsão, incluindo o risco e a probabilidade.</returns>
        /// <response code="200">Previsão realizada com sucesso</response>
        /// <response code="400">Dados de entrada inválidos</response>
        /// <response code="401">Não autorizado (Token JWT ausente ou inválido)</response>
        [HttpPost("predict-risk")]
        [ProducesResponseType(typeof(RiskPredictionOutput), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult PredictRisk([FromBody] RiskPredictionInput input)
        {
            // Validação simples para demonstração
            if (input.TotalMileageKm <= 0 || input.DaysInOperation <= 0 || string.IsNullOrWhiteSpace(input.YardType))
            {
                return BadRequest("Por favor, forneça DaysInOperation, TotalMileageKm e YardType válidos.");
            }

            try
            {
                var result = _mlService.Predict(input);

                return Ok(new
                {
                    RiscoAlto = result.Prediction ? "Sim" : "Não",
                    Probabilidade = $"{result.Probability:P2}", // Formata como porcentagem
                    Detalhe = result
                });
            }
            catch (Exception ex)
            {
                // Em produção, registre o erro
                return StatusCode(500, $"Ocorreu um erro interno ao realizar a previsão: {ex.Message}");
            }
        }
    }
}

