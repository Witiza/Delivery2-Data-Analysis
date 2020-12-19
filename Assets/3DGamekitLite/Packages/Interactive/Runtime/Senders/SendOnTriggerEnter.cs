using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamekit3D.GameCommands
{
    public class SendOnTriggerEnter : TriggerCommand
    {
        public delegate void MyDataEvent(Vector3 position);
        public static MyDataEvent buttonDelegateEvent;

        public LayerMask layers;

        void OnTriggerEnter(Collider other)
        {
            if (0 != (layers.value & 1 << other.gameObject.layer))
            {
                Send();
                buttonDelegateEvent?.Invoke(gameObject.transform.position);
            }
        }
    }
}
