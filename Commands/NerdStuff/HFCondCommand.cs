using System;
using System.Net;
using System.Threading.Tasks;

using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Tofu.Commands.Attributes;
using Tofu.Util;

namespace Tofu.Commands.NerdStuff
{
    public class HFCondCommand : BaseCommandModule
    {
        [Command("hfcond")]
        [Description("Sends current HF conditions")]
        [Category(Category.NerdStuff)]
        public async Task HFCond(CommandContext Context)
        {
            string gifFile = TempManager.GetTempFile("hfcond.gif", true);
            new WebClient().DownloadFile("https://www.hamqsl.com/solar101pic.php", gifFile);
            await Context.Channel.SendFileAsync(gifFile);
        }
    }
}
