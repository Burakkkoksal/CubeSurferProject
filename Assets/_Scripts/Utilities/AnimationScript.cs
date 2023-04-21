using UnityEngine;

namespace Game.Utilities
{
    public class AnimationScript : MonoBehaviour 
    {
        public bool isAnimated = false;

        public bool isRotating = false;
        public bool isFloating = false;
        public bool isScaling = false;

        public Vector3 rotationAngle;
        public float rotationSpeed;

        public float floatSpeed;
        public float floatRate;
       
        public Vector3 startScale;
        public Vector3 endScale;

        public float scaleSpeed;
        public float scaleRate;
        
        private bool _scalingUp = true;
        private bool _goingUp = true;
        private float _floatTimer;
        private float _scaleTimer;

        private void Update() 
        {
            if (isAnimated)
            {
                if (isRotating)
                {
                    transform.Rotate(rotationAngle * (rotationSpeed * Time.deltaTime));
                }

                if (isFloating)
                {
                    _floatTimer += Time.deltaTime;
                    Vector3 moveDir = new Vector3(0.0f, 0.0f, floatSpeed);
                    transform.Translate(moveDir);

                    if (_goingUp && _floatTimer >= floatRate)
                    {
                        _goingUp = false;
                        _floatTimer = 0;
                        floatSpeed = -floatSpeed;
                    }

                    else if(!_goingUp && _floatTimer >= floatRate)
                    {
                        _goingUp = true;
                        _floatTimer = 0;
                        floatSpeed = +floatSpeed;
                    }
                }

                if (isScaling)
                {
                    _scaleTimer += Time.deltaTime;

                    if (_scalingUp)
                    {
                        transform.localScale = Vector3.Lerp(transform.localScale, endScale, scaleSpeed * Time.deltaTime);
                    }
                    else if (!_scalingUp)
                    {
                        transform.localScale = Vector3.Lerp(transform.localScale, startScale, scaleSpeed * Time.deltaTime);
                    }

                    if (_scaleTimer >= scaleRate)
                    {
                        if (_scalingUp) _scalingUp = false; 
                        else if (!_scalingUp) _scalingUp = true;
                        _scaleTimer = 0;
                    }
                }
            }
	    }
    }
}

