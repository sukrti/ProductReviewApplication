using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;
using Review.API.Models;
using Review.API.Contracts;
using Newtonsoft.Json.Linq;

namespace Review.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : Controller
    {
        private readonly IReviewService _service;
        public ReviewController(IReviewService service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets the Review data: Id,Score,Title,Comment,Recommendation
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {
            var result = await _service.GetAllReviews();
            if (result.Count == 0)
            {
                return NoContent();
            }
            return Ok(result);
                         
        }

        /// <summary>
        /// Gets the Review Summary data: AverageScore,Percentage of Recommendation
        /// </summary>

        [HttpGet("summary")]
        public async Task<IActionResult> GetReviewSummary()
        {
            var localhostvalue = HttpContext.Request.Host.Value;
           var result = await _service.GetSummary(localhostvalue);
            if (result.Recomendation == null || result.AverageScore==0)
            {
                
                return NoContent();
            }
            return Ok(result);

        }

        /// <summary>
        /// Add a New Review to a product
        /// </summary>

        [HttpPost]
        public async Task<IActionResult> AddReviews(ReviewData newReviewData)
        {
            await _service.Add(newReviewData);
            return Ok();
         
        }

    }
}