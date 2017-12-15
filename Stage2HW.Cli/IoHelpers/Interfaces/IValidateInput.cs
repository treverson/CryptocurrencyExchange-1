using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace Stage2HW.Cli.IoHelpers.Interfaces
{
    internal interface IValidateInput
    {
        string ValidateName();
        string ValidatePassword();
        string ValidateNickName();
        void PauseLoop();
        double ValidateAmount();
    }
}