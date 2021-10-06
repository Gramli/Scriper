namespace ScriperLib.Clone
{
    public interface IDeepCloneAdapter
    {
        public T DeepClone<T>(T objectToClone) where T : class;
    }
}
