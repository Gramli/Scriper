namespace ScriperLib
{
    public interface IScriperLibContainer
    {
        T GetInstance<T>() where T : class;
    }
}
