using Game.Base;
using Game.Managers;
using Game.Units;
using TMPro;
using UnityEngine;

namespace Game.Data
{
    public class EndBonus : InteractableBase<Player>
    {
        [SerializeField] private TMP_Text bonusText;
        [SerializeField] private MeshRenderer bonusMeshMat;
        private int _bonus;

        public void SetBonus(int bonus, Material meshMat)
        {
            _bonus = bonus;
            bonusText.text = $"{bonus}x";
            bonusMeshMat.sharedMaterials = new [] {meshMat};
        }
        
        protected override void Interact(Player _)
        {
            base.Interact(_);
            GameManager.Instance.EndBonusManager.SetEndBonus(_bonus);
        }
    }
}

