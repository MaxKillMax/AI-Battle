using AIBattle.LiveObjects.LiveComponents.Targets;

namespace AIBattle.LiveObjects.LiveComponents
{
    /// <summary>
    /// The package, which is needed to fully interact with a particular component
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct ComponentAction<T> where T : LiveComponent
    {
        public LiveObject LiveObject;
        public Target Target;
        public T Component;

        public bool IsCorrect() => LiveObject != null && Target != null && Component != null;
    }
}
