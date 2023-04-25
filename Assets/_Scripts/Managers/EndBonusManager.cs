using Game.Data;
using UnityEngine;

namespace Game.Managers
{
    public class EndBonusManager : MonoBehaviour
    {
        [SerializeField] private EndBonus endBonusPrefab;

        [SerializeField] private int bonusCount;
        
        [SerializeField] private Material[] bonusMaterials;

        private const float _bonusOffsetZ = .25f;
        private int _bonus = 0;

        private void OnEnable() => GameManager.OnGameStateChanged += OnGameStateChange;

        private void OnGameStateChange(GameState oldState, GameState newState)
        {
            if (newState == GameState.End && _bonus != 0)
            {
                GameManager.Instance.MultiplyScore(_bonus);
            }
        }

        private void OnDisable() => GameManager.OnGameStateChanged -= OnGameStateChange;
        
        public void CreateBonuses(Vector3 bonusStartPos)
        {
            var spawnPos = bonusStartPos + new Vector3(0, 0, _bonusOffsetZ);
            for (int i = 0; i < bonusCount; i++)
            {
                var endBonus = Instantiate(endBonusPrefab, transform);
                endBonus.transform.position = spawnPos;
                var bonus = Random.Range(0, bonusMaterials.Length);
                endBonus.SetBonus(bonus + 1, bonusMaterials[bonus]);

                spawnPos += new Vector3(0, 0, _bonusOffsetZ);
            }
        }
        
        public void SetEndBonus(int bonus)
        {
            _bonus = bonus;
        }
    }
}
