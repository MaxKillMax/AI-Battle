namespace AIBattle.Generators
{
    /// <summary>
    /// An interface that defines an object as an object generator T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenerator<T>
    {
        public T Generate();
    }
}
