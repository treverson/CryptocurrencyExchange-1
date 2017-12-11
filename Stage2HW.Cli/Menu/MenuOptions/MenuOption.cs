namespace Stage2HW.Cli.Menu.MenuOptions
{
    internal class MenuOption
    {
        public delegate void CommandCallBack();

        public CommandCallBack CallOption { get; }
        public string Name { get; }
        public int OptionNumber { get; set; }

        public MenuOption(/*int optionNumber,*/ string name, CommandCallBack callback)
        {
            //OptionNumber = optionNumber;
            CallOption = callback;
            Name = name;
        }

        public MenuOption(/*int optionNumber,*/ string name)
        {
          //  OptionNumber = optionNumber;
            Name = name;
        }
    }
}
