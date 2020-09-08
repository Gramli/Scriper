namespace Scriper.Closing
{
    public interface IClose<T>
    {
        event CloseEventHandler<T> Close;
    }
}
