namespace IngameScript
{
    partial class Program
    {
        private void Help()
        {
            Echo($"[CommandLineActions] use '/help [<SubCommand>]'."
                + $"\n"
                + $"\n(sub)Command's:"
                + $"\n----------------------"
                + $"\n/help		\t=> Get help for each command or in general."
                + $"\n/lcd		\t\t=> Change LCD State's, Using pre-programmed sprites and text / info."
                + $"\n/light	\t=> Change Light State's, Using pre-programmed states."
                + $"\n/door		\t\t=> Change Door State's, Basically ON or OFF."
                + $"\n/block	\t=> Makes every other command useless or redundant / depricated"
                + $"\n----------------------"
                + $"\nDon't bother me about my creations please. im happy with thankyou's"
                + $"\n\nPage: 1 of 1 | use '/help [<Index>]'"
            );
        }
    }
}
