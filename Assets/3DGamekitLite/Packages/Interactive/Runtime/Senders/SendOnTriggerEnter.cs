using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamekit3D.GameCommands
{
    public class SendOnTriggerEnter : TriggerCommand
    {
        public delegate void MyDataEvent(Vector3 position);
        public static MyDataEvent buttonDelegateEvent;

        bool activated = false;

        public LayerMask layers;

        void OnTriggerEnter(Collider other)
        {
            if (0 != (layers.value & 1 << other.gameObject.layer))
            {
                Send();

                if (!activated)
                {
                    buttonDelegateEvent?.Invoke(gameObject.transform.position);
                    activated = true;
                }
            }
        }
    }
}
