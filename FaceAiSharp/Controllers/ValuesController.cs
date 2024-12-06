using FaceAiSharpApi.Extentions;
using FaceAiSharpApi.Services;
using FaceAiSharpApi.Services.IServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FaceAiSharpApi.Controllers
{
    [Route("api/file")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IFaceAiService _faceAiService;

        public ValuesController(IFaceAiService faceAiService)
        {
            _faceAiService = faceAiService;
        }

        // GET: api/<ValuesController>
        [HttpPost]
        public async Task<IActionResult> Register(IFormFile file)
        {
            var result =  await _faceAiService.FaceAiRegisterAsync(file);

            return Ok(result);
        }

        // POST api/<ValuesController>
        [HttpPost("{id}")]
        public async Task<IActionResult> Compare(IFormFile file, [FromRoute] int id)
        {
            var result = await _faceAiService.FaceAiCompareAsync(file, id);

            return Ok(result);
        }
    }
}
