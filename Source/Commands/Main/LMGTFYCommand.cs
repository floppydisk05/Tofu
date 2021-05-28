using System.Threading.Tasks;

using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

using WinBot.Commands.Attributes;

namespace WinBot.Commands.Main
{
    public class LMGTFYCommand : BaseCommandModule
    {
        [Command("lmgtfy")]
        [Description("Lemme Google that for you...")]
        [Usage("[query]")]
        [Category(Category.Main)]
        public async Task LMGTFY(CommandContext Context, [RemainingText]string query)
        {
            await Context.RespondAsync($"https://www.google.com/search?q={System.Web.HttpUtility.UrlEncode(query)}");
        }
    }
}
