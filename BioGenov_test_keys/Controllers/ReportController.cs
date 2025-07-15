using BioGenom_test_keys.DTO;
using BioGenom_test_keys.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace BioGenov_test_keys.Controllers
{

    [ApiController]
    [Route("api/reports")]
    public class ReportController : ControllerBase
    {
        private readonly DbAppContext _context;

        public ReportController(DbAppContext context) => _context = context;

        [HttpGet("latest", Name = "GetLatestNutritionReport")]
        public async Task<ActionResult<NutritionReport>> Get()
        {
            var report = await _context.Reports
                .Include(r => r.CurrentMetrics)
                .Include(r => r.PersonalizedSets)
                    .ThenInclude(ps => ps.SupplementNutrients)
                .AsNoTracking()
                .OrderByDescending(r => r.CreatedAt)
                .FirstOrDefaultAsync();

            if (report == null)
                return NotFound();

            // Ручной маппинг
            var reportDto = new ReportDto
            {
                Id = report.Id,
                ReducedCount = report.ReducedCount,
                SufficientCount = report.SufficientCount,
                Status = report.Status,
                CurrentMetrics = report.CurrentMetrics.Select(m => new MetricDto
                {
                    Name = m.Name,
                    CurrentValue = (double)m.CurrentValue,
                    NormValue = m.NormValue,
                    Unit = m.Unit
                }).ToList(),
                PersonalizedSet = report.PersonalizedSets != null && report.PersonalizedSets.Any() ? new PersonalizedSetDto
                {
                    Name = report.PersonalizedSets.First().Name,
                    Note = report.PersonalizedSets.First().Note,
                    Description = report.PersonalizedSets.First().Description,
                    AlternativesCount = report.PersonalizedSets.First().AlternativesCount,
                    Nutrients = report.PersonalizedSets.First().SupplementNutrients.Select(n => new SupplementNutrientDto
                    {
                        Name = n.Name,
                        FromSet = (double)n.FromSet,
                        FromNutrition = (double)n.FromNutrition,
                        Unit = n.Unit
                    }).ToList()
                } : null
            };

            return Ok(reportDto);
        }
        
        [HttpPost]
        public async Task<IActionResult> UpdateReport([FromBody] NutritionReport report)
        {
            // Удаляем существующий отчет
            var existing = await _context.Reports.FirstOrDefaultAsync();
            if (existing != null) _context.Remove(existing);

            // Добавляем новый
            await _context.AddAsync(report);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
