using json_web_api.Request;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StackExchange.Redis;
using Yitter.IdGenerator;

namespace json_web_api.Controllers;

public class ApiController(ConnectionMultiplexer connectionMultiplexer) : ControllerBase
{
    [HttpPost("AddJson")]
    public async Task<IActionResult> AddJsonAsync([FromBody] AddJsonRequest param)
    {
        var db = connectionMultiplexer.GetDatabase(15);
        var key = YitIdHelper.NextId().ToString();
        param.Key = key;
        await db.StringSetAsync(
            key,
            JsonConvert.SerializeObject(param),
            TimeSpan.FromHours(param.Hour)
        );
        return Ok(new { Key = key });
    }

    [HttpGet("GetJson")]
    public async Task<IActionResult> GetJsonAsync([FromQuery] string key)
    {
        var db = connectionMultiplexer.GetDatabase(15);
        var res = await db.StringGetAsync(key);
        return Ok(JsonConvert.DeserializeObject<AddJsonRequest>(res.ToString()));
    }
}
