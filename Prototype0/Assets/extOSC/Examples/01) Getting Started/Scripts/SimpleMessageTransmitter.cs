/* Copyright (c) 2018 ExT (V.Sigalkin) */

using UnityEngine;
using System.Collections;

namespace extOSC.Examples
{
    public class SimpleMessageTransmitter : MonoBehaviour
    {
        #region Public Vars

        public string Address = "/start";

        [Header("OSC Settings")]
        public OSCTransmitter Transmitter;

        #endregion

        #region Unity Methods

        protected virtual void Start()
        {
            var message = new OSCMessage(Address);
            message.AddValue(OSCValue.Int(440));
            Transmitter.Send(message);
            StartCoroutine(End(3));
            StartCoroutine(Wind(3));
            
            
        }

        IEnumerator End(float delay)
        {
            var message = new OSCMessage("/end");
            message.AddValue(OSCValue.Int(0));
            yield return new WaitForSeconds(delay);
            Transmitter.Send(message);
        }

        IEnumerator Wind(float delay)
        {
            yield return new WaitForSeconds(1.5f);
            var message = new OSCMessage("/wind");
            message.AddValue(OSCValue.Int(1));
            Transmitter.Send(message);
            yield return new WaitForSeconds(delay);
            message = new OSCMessage("/wind");
            message.AddValue(OSCValue.Int(0));
            Transmitter.Send(message);

        }

        #endregion
    }
}