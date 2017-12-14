namespace Stage2HW.Cli.Menu.MenuOptions
{
    internal class MenuOption
    {
        public delegate void CommandCallBack();

        public CommandCallBack CallOption { get; }
        public string Name { get; }
        public int OptionNumber { get; set; }

        public MenuOption(string name, CommandCallBack callback)
        {
            CallOption = callback;
            Name = name;
        }

        public MenuOption(string name)
        {
            Name = name;
        }
    }
}
