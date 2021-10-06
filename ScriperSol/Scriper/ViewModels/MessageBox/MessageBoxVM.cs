using ReactiveUI;
using Scriper.Closing;
using System;
using System.Reactive;

namespace Scriper.ViewModels.MessageBox
{
    public class MessageBoxVM : IClose<EventArgs>
    {
        public string Title { get; init; }
        public string Content { get; init; }
        public ReactiveCommand<Unit, Unit> OkCmd { get; }

        public event CloseEventHandler<EventArgs> Close;

        public MessageBoxVM(string title, string content)
        {
            Title = title;
            Content = content;
            OkCmd = ReactiveCommand.Create(() => Close(this, new CloseEventArgs<EventArgs>()));
        }
    }
}
