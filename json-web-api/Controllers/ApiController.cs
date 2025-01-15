using json_web_api.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Yitter.IdGenerator;

namespace json_web_api.Controllers
{
    public class ApiController(IMemoryCache _memoryCache) : ControllerBase
    {
        [HttpPost("AddJson")]
        public IActionResult AddJsonAsync([FromBody] AddJsonRequest param)
        {
            var guid = YitIdHelper.NextId();
            _memoryCache.Set(guid.ToString(), param, TimeSpan.FromHours(param.Hour));
            return Ok(new { Key = guid });
        }

        [HttpGet("GetJson")]
        public IActionResult GetJsonAsync([FromQuery] string key)
        {
            var res = _memoryCache.Get<AddJsonRequest>(key);
            return Ok(res);
        }
    }
}
