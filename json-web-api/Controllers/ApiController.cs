using json_web_api.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Yitter.IdGenerator;

namespace json_web_api.Controllers
{
    public class ApiController(IMemoryCache memoryCache) : ControllerBase
    {
        [HttpPost("AddJson")]
        public IActionResult AddJsonAsync([FromBody] AddJsonRequest param)
        {
            var key = YitIdHelper.NextId().ToString();
            param.Key = key;
            memoryCache.Set(key, param, TimeSpan.FromHours(param.Hour));
            return Ok(new { Key = key });
        }

        [HttpGet("GetJson")]
        public IActionResult GetJsonAsync([FromQuery] string key)
        {
            var res = memoryCache.Get<AddJsonRequest>(key);
            return Ok(res ?? new AddJsonRequest());
        }
    }
}
