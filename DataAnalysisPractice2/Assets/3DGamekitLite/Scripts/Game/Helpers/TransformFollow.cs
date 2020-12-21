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

        private void LateUpdate()
        {
            transform.position = target.position;
            transform.rotation = target.rotation;
            OnHoldingKey.Invoke();
        }
    } 
}
