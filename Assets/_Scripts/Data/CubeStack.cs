using DG.Tweening;
using Game.Base;
using UnityEngine;
using Game.Units;

namespace Game.Data
{
    public class CubeStack : CollectionBase<Cube>
    {
        [SerializeField] private Transform stackParent;
        [SerializeField] private Transform stackStartingPoint;
        
        private Transform _playerVisual;
        private float _cubeY = .045f;
        private float _playerY = .025f;

        private void Awake()
        {
            _playerVisual = stackParent.GetChild(0);
            var firstCube = stackParent.GetChild(1).GetComponent<Cube>();
            Add(firstCube);
        }

        public override bool Add(Cube cube)
        {
            if (!base.Add(cube)) return false;

            cube.transform.SetParent(stackParent, true);
            
            ReorderCubes(Ease.InQuad, .15f);

            return true;
        }

        public override bool Remove(Cube cube)
        {
            if (!base.Remove(cube)) return false;

            cube.transform.DOKill(false);
            cube.transform.SetParent(transform.parent, true);

            ReorderCubes(Ease.OutQuad, .25f);

            return true;
        }

        private void ReorderCubes(Ease easeType, float tweenTime)
        {
            var cubePos = stackStartingPoint.localPosition;

            for (var i = Collection.Count - 1; i >= 0; i--)
            {
                var cube = Collection[i];
                cube.transform.DOLocalMove(cubePos, tweenTime).SetEase(easeType);
                cubePos += new Vector3(0, _cubeY, 0);
            }

            Vector3 playerVisualPos;
            if (Collection.Count > 0)
                playerVisualPos = cubePos + new Vector3(0, _playerY - _cubeY , 0);
            else
                playerVisualPos = stackStartingPoint.localPosition - new Vector3(0, -.025f, 0);

            _playerVisual.DOLocalMove(playerVisualPos, tweenTime).SetEase(easeType);
        }
    }
}
