using UnityEngine;

namespace AIBattle.Animations
{
    public class AnimationEventTracker : MonoBehaviour
    {
        public Events.Event<string> OnMessageReceived { get; private set; } = new();

        public void ReceiveMessage(string message) => OnMessageReceived?.Invoke(message);
    }
}
