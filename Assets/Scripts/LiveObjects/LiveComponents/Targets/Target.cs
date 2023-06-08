namespace AIBattle.LiveObjects.LiveComponents.Targets
{
    /// <summary>
    /// Component mark liveObject, as interactable for other components and liveObjects
    /// </summary>
    public class Target : LiveComponent
    {
        public TargetId Id { get; private set; }

        public Target(TargetParameters parameters)
        {
            Id = parameters.Id;
        }

        public override void OnDestroy()
        {
            Id = new(null);
        }

        public static implicit operator TargetId(Target value) => value.Id;
    }
}
