namespace ScriperLib.Clone
{
    internal class DeepCloneAdapter : IDeepCloneAdapter
    {
        public T DeepClone<T>(T objectToClone) where T : class
        {
            return FastDeepCloner.DeepCloner.Clone(objectToClone);
        }
    }
}
