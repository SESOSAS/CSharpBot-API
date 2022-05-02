using DSharpPlus;
using DSharpPlus.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpBot_API
{
    public class Commands
    {

        //How to create commands
        public static void help(MessageCreateEventArgs e, string[] args)
        {
            e.Message.RespondAsync("No Help!");
        }

        public static void test(MessageCreateEventArgs e, string[] args)
        {
            e.Message.RespondAsync("No Test!");
        }
    }
}
