using DG.Tweening;
using Game.Base;
using Game.Managers;
using UnityEngine;

namespace Game.Data
{
    public class CubeStack : CollectionBase<Cube>
    {
        [SerializeField] private Transform stackParent;
        [SerializeField] private Transform stackStartingPoint;

        [SerializeField] private AudioSource cubeCollectSound;
        
        private Transform _playerVisual;

        private bool _canPlayAudio;
        
        private const float CUBE_Y = .045f;
        private const float PLAYER_Y = .025f;

        private void Awake()
        {
            _playerVisual = stackParent.GetChild(0);
            var firstCube = stackParent.GetChild(1).GetComponent<Cube>();
            Add(firstCube);

            _canPlayAudio = true;
        }

        public override bool Add(Cube cube)
        {
            if (!base.Add(cube)) return false;

            cube.transform.SetParent(stackParent, true);
            
            ReorderCubes(Ease.InQuad, .15f);

            if (_canPlayAudio) cubeCollectSound.Play();

            return true;
        }

        public override bool Remove(Cube cube)
        {
            if (!base.Remove(cube)) return false;

            cube.transform.DOKill(false);
            cube.transform.SetParent(transform.parent, true);

            ReorderCubes(Ease.OutQuad, .25f);

            if (Count == 0)
            {
                GameManager.Instance.SetGameState(GameState.Lose);
            }
            
            return true;
        }
        
        public void Remove(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                if (Collection.Count == 0 || !Remove(Collection[Collection.Count - 1]))
                    break;
            }
        }

        private void ReorderCubes(Ease easeType, float tweenTime)
        {
            var cubePos = stackStartingPoint.localPosition;

            for (var i = Collection.Count - 1; i >= 0; i--)
            {
                var cube = Collection[i];
                cube.transform.DOLocalMove(cubePos, tweenTime).SetEase(easeType);
                cubePos += new Vector3(0, CUBE_Y, 0);
            }

            Vector3 playerVisualPos;
            if (Collection.Count > 0)
                playerVisualPos = cubePos + new Vector3(0, PLAYER_Y - CUBE_Y , 0);
            else
                playerVisualPos = new Vector3(0, -.025f, 0);

            _playerVisual.DOLocalMove(playerVisualPos, tweenTime).SetEase(easeType);
        }
    }
}
