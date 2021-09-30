using Avalonia.Collections;

namespace Scriper.ViewModels
{
    internal class ArgumentsVM : IArgumentsVM
    {
        public AvaloniaList<string> Arguments { get; private set; }

        public ArgumentsVM()
        {
            Arguments = new AvaloniaList<string>
            {
                ""
            };
        }

        public void Init(string arguments)
        {

        }

        public string GetArguments()
        {
            throw new System.NotImplementedException();
        }
    }
}
