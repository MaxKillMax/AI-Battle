namespace AIBattle.UnityAlternatives
{
    /// <summary>
    /// Alternative for UnityEngine.Time
    /// </summary>
    public class Time
    {
        public static float Delta { get; private set; }

        public void Update() => Delta = UnityEngine.Time.deltaTime;

        public void Stop() => Delta = 0;
    }
}
