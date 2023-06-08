using TMPro;
using UnityEngine;

namespace AIBattle.Interfaces
{
    public class Selection : MonoBehaviour
    {
        [SerializeField] private GameObject _healthGameObject;
        [SerializeField] private TMP_Text _healthValueText;
        [SerializeField] private TMP_Text _maxHealthValueText;

        [SerializeField] private GameObject _damageGameObject;
        [SerializeField] private TMP_Text _damageValueText;

        [SerializeField] private GameObject _healDamageImproveGameObject;
        [SerializeField] private TMP_Text _delayValueText;

        public void SetActive(bool state) => SetGameObjectState(gameObject, state);

        public void SetHealthActive(bool state) => SetGameObjectState(_healthGameObject, state);

        public void SetDamageActive(bool state) => SetGameObjectState(_damageGameObject, state);

        public void SetHealDamageImproveActive(bool state) => SetGameObjectState(_healDamageImproveGameObject, state);

        private void SetGameObjectState(GameObject gameObject, bool state)
        {
            if (gameObject.activeSelf != state)
                gameObject.SetActive(state);
        }

        public void SetHealthValue(float value, float maxValue)
        {
            _healthValueText.text = value.ToString("N0");
            _maxHealthValueText.text = maxValue.ToString("N0");
        }

        public void SetDamageValue(float value)
        {
            _damageValueText.text = value.ToString("N0");
        }

        public void SetDelayValue(float value)
        {
            _delayValueText.text = value.ToString("N0");
        }
    }
}
