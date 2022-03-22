using JavaScriptEngineSwitcher.ChakraCore;
using JavaScriptEngineSwitcher.Core;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApiTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChakraCoreController : ControllerBase
    {
        public ChakraCoreController()
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string jsText = @"function say(name){
                                return 'Hello '+name;    
                            }";
            IJsEngineSwitcher engineSwitcher = JsEngineSwitcher.Current;
            engineSwitcher.EngineFactories.Add(new ChakraCoreJsEngineFactory());
            engineSwitcher.DefaultEngineName = ChakraCoreJsEngine.EngineName;
            using (IJsEngine engine = JsEngineSwitcher.Current.CreateDefaultEngine())
            {
                engine.Execute(jsText);
                string result = engine.CallFunction<string>("say", "chakraCoreJs");
                return Ok(result);
            }

        }
    }
}
