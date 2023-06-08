using System;
using System.Collections.Generic;
using AIBattle.Interfaces;
using AIBattle.LiveObjects;
using AIBattle.LiveObjects.LiveComponents.Attacks;
using AIBattle.LiveObjects.LiveComponents.HealDamageImproves;
using AIBattle.LiveObjects.LiveComponents.Healths;
using AIBattle.LiveObjects.LiveComponents.Movements;
using AIBattle.LiveObjects.LiveComponents.Targets;
using UnityEngine;

namespace AIBattle.Controllers.Players
{
    /// <summary>
    /// Implementing manual control
    /// </summary>
    public class Player : Controller
    {
        private const string WORLD_LAYER_NAME = "WorldObject";

        private LiveObject _selected;

        private readonly Selection _selectionPanel;

        public Player(List<LiveObject> liveObjects, TargetId id, Selection selectionPanel) : base(id, liveObjects)
        {
            _selectionPanel = selectionPanel;

            UnityAlternatives.Input.OnUpdate += TryShowLiveObjectStats;
            UnityAlternatives.Input.OnLmbDown += TrySelect;
            UnityAlternatives.Input.OnRmbDown += TryMove;
            UnityAlternatives.Input.On1Down += TryHealSelected;
            UnityAlternatives.Input.On2Down += TryDamageUpSelected;
        }

        private void TryShowLiveObjectStats()
        {
            RaycastHit hit = UnityAlternatives.Input.GetMouseHitWith((h) => h.transform.TryGetComponent(out LiveObject liveObject));

            _selectionPanel.SetActive(hit.transform != null);

            if (hit.transform == null)
                return;

            hit.transform.TryGetComponent(out LiveObject liveObject);

            TryDoAction(liveObject.TryGetLiveComponent(out Health health), _selectionPanel.SetHealthActive,
                () => _selectionPanel.SetHealthValue(health.Amount, health.MaxAmount));

            TryDoAction(liveObject.TryGetLiveComponent(out Attack attack), _selectionPanel.SetDamageActive,
                () => _selectionPanel.SetDamageValue(-attack.Action.Value));

            TryDoAction(liveObject.TryGetLiveComponent(out HealDamageImprove healDamageImprove), _selectionPanel.SetHealDamageImproveActive,
                () => _selectionPanel.SetDelayValue(healDamageImprove.Time));
        }

        private void TryHealSelected()
        {
            if (TryGetSelectedImprove(out HealDamageImprove improve))
                improve.TryHeal();
        }

        private void TryDamageUpSelected()
        {
            if (TryGetSelectedImprove(out HealDamageImprove improve))
                improve.TryDamage();
        }

        private bool TryGetSelectedImprove(out HealDamageImprove improve)
        {
            improve = default;
            return _selected != null && _selected.TryGetLiveComponent(out improve);
        }

        private void TryDoAction(bool condition, Action<bool> conditionAction, Action successAction)
        {
            conditionAction?.Invoke(condition);

            if (condition)
                successAction?.Invoke();
        }

        private void TrySelect()
        {
            RaycastHit hit = UnityAlternatives.Input.GetMouseHitWith((h) => h.transform.TryGetComponent(out LiveObject liveObject) && liveObject.TryGetLiveComponent(out Target target) && Id.Contains(target));

            if (hit.transform != null)
                _selected = hit.transform.GetComponent<LiveObject>();
            else
                TryRemoveSelection();
        }

        private void TryRemoveSelection()
        {
            if (_selected == null)
                return;

            _selected = null;
        }

        private void TryMove()
        {
            if (_selected == null || !_selected.TryGetLiveComponent(out Movement movement))
                return;

            RaycastHit hit = UnityAlternatives.Input.GetMouseHitWith((h) => h.transform.gameObject.layer == LayerMask.NameToLayer(WORLD_LAYER_NAME));

            if (hit.collider == null)
                return;

            Vector3 point = hit.point;
            point.y = 1;

            movement.MoveToPoint(point);
        }
    }
}
