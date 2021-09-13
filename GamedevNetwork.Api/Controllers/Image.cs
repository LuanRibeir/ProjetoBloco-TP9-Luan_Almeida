using GamedevNetwork.Infra.Services.Blob;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamedevNetwork.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Image : ControllerBase
    {
        [HttpPost]
        public async Task<IEnumerable<string>> Upload([FromServices] IBlobService blobService)
        {
            if (!Request.HasFormContentType)
                BadRequest();
        

            var tasks = Request.Form.Files.Select(file =>
            {
                return blobService.UploadAsync(file.OpenReadStream());
            });

            return await Task.WhenAll(tasks);
        }
    }
}
