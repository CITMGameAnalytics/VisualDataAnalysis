using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gamekit3D
{
    public class TransformFollow : MonoBehaviour
    {
        public Transform target;
        public UnityEvent OnHoldingKey;

        private float nextKeyEvent = 0.0f;
        public float period = 2.0f;

        private void LateUpdate()
        {
            transform.position = target.position;
            transform.rotation = target.rotation;

            if(Time.time > nextKeyEvent)
            {
                nextKeyEvent = Time.time + period;
                OnHoldingKey.Invoke();
            }
        }
    } 
}
