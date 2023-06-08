using System;
using System.Linq;
using UnityEngine;

namespace AIBattle.UnityAlternatives
{
    /// <summary>
    /// Alternative for UnityEngine.Input. Contain events on different inputs (as facade)
    /// </summary>
    public class Input
    {
        /// <summary>
        /// Alternative for Unity Update method
        /// </summary>
        public static event Action OnUpdate;
        public static event Action OnLmbDown;
        public static event Action OnRmbDown;
        public static event Action OnEscDown;
        public static event Action On1Down;
        public static event Action On2Down;

        public static float Horizontal { get; private set; }
        public static float Vertical { get; private set; }

        public static Vector3 MouseInWorld => Camera.main.ScreenToWorldPoint(MouseInScreen);
        public static Vector3 MouseInScreen => UnityEngine.Input.mousePosition;

        public Input()
        {
            OnUpdate = null;
            OnLmbDown = null;
            OnRmbDown = null;
            OnEscDown = null;
            On1Down = null;
            On2Down = null;
        }

        public void Update()
        {
            Horizontal = UnityEngine.Input.GetAxis("Horizontal");
            Vertical = UnityEngine.Input.GetAxis("Vertical");

            InvokeKeys();

            OnUpdate?.Invoke();
        }

        private void InvokeKeys()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
                OnLmbDown?.Invoke();

            if (UnityEngine.Input.GetMouseButtonDown(1))
                OnRmbDown?.Invoke();

            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
                OnEscDown?.Invoke();

            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha1))
                On1Down?.Invoke();

            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha2))
                On2Down?.Invoke();
        }

        public static RaycastHit GetMouseHit()
        {
            Ray ray = GetMouseRay();
            Physics.Raycast(ray, out RaycastHit raycastHit);
            return raycastHit;
        }

        public static RaycastHit GetMouseHitWith(Func<RaycastHit, bool> condition)
        {
            Ray ray = GetMouseRay();

            RaycastHit[] hits = new RaycastHit[5];
            Physics.RaycastNonAlloc(ray, hits);
            return hits.Where((h) => h.transform != null).Where(condition).FirstOrDefault();
        }

        private static Ray GetMouseRay() => Camera.main.ScreenPointToRay(MouseInScreen);
    }
}
