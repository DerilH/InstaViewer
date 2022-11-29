using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InstaWatcher.Extensions
{
    public static class IElementHandleExtensions
    {
        public static async Task<string> GetInnerTextAsync(this IElementHandle element) 
        {
            return await element.EvaluateFunctionAsync<string>("e => e.innerText");
        }
        public static async Task<string> GetPropAsync(this IElementHandle element, string name) 
        {
            var token = await element.GetPropertyAsync(name);
            return (string)await token.JsonValueAsync();
        }
    }
}
