using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.ClearScript.V8;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClearScriptController : ControllerBase
    {
        public ClearScriptController()
        {
        }

        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    string jsText = @"function say(name){
        //                        var c=add(10,10);
        //                        return 'Hello '+name+c;    

        //                        function add(a,b){
        //                            return a+b;}
        //                    }";
        //    using (var engine = new V8ScriptEngine())
        //    {
        //        engine.DocumentSettings.AccessFlags = Microsoft.ClearScript.DocumentAccessFlags.EnableFileLoading;
        //        engine.DefaultAccess = Microsoft.ClearScript.ScriptAccess.Full;
        //        //V8Script script = engine.CompileDocument("./jsfiles/hellojs.js");   // 载入并编译js文件。
        //        engine.Execute(jsText);  //直接执行js字符串
        //        var result = engine.Script.say("clearScript");  //执行方法
        //        return Ok(result);
        //    }
        //}

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            string mobile = "13812345678";
            string jsTextPrefix = "function dojs(input){";
            string jsTextPostfix = "return output;}";

            var jsContent = @"var tel = input.Mobile;           //获取入参对象
                                var reg=/(\d{3})\d{4}(\d{4})/;
                                var telnew = tel.replace(reg, '$1****$2');  //业务逻辑，如手机号中间四位加密
                                output={'Mobile':telnew};       // 定义返回值格式
                              ";
            string jsText = jsTextPrefix + jsContent + jsTextPostfix;
            using (var engine = new V8ScriptEngine())
            {
                engine.DocumentSettings.AccessFlags = Microsoft.ClearScript.DocumentAccessFlags.EnableFileLoading;
                engine.DefaultAccess = Microsoft.ClearScript.ScriptAccess.Full;
                engine.Execute(jsText);  //直接执行js字符串
                var input = new
                {
                    Mobile = mobile
                };
                var result = engine.Script.dojs(input);  //执行方法
                return Ok(JsonConvert.SerializeObject(result));
            }
        }
    }
}
