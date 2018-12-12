using extOSC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public OSCTransmitter transmitter;
    public static SoundManager instance;

    public float firstWaterfall;
    public float secondWaterfall;

    public void Fall(string mode)
    {
        var message = new OSCMessage("/fall");
        message.AddValue(OSCValue.String(mode));
        transmitter.Send(message);
    }

    public float maxWaterfalls = 8;

    public float caveDrums;

    private bool drumsActivated = false; //to avoid playing drums again

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }
        else if (instance != this)
        {
            // Then destroy this.This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

    }

    public void Drums(float distance)
    {
       if(distance <= caveDrums)
        {
            if(!drumsActivated)
            {
               
                drumsActivated = true;
                var messageAct = new OSCMessage("/drums");
                messageAct.AddValue(OSCValue.Int(1));
                transmitter.Send(messageAct);
            }

            float volume = ((distance - caveDrums) * 1f) / -caveDrums;
            var message = new OSCMessage("/drumsvolume");
            message.AddValue(OSCValue.Float(volume));
            transmitter.Send(message);
        }
       else
        {
           
                drumsActivated = false;
                var messageAct = new OSCMessage("/drums");
                messageAct.AddValue(OSCValue.Int(0));
                transmitter.Send(messageAct);
          
        }
    }

    public void Hit(string mode)
    {
        var message = new OSCMessage("/hit");
        message.AddValue(OSCValue.String(mode));
        transmitter.Send(message);
    }

    public void Swing()
    { 
        var message = new OSCMessage("/swing");
        message.AddValue(OSCValue.Int(1));
        transmitter.Send(message);
    }

    public void StartWalking()
    {
        var message = new OSCMessage("/walk");
        message.AddValue(OSCValue.Int(300));
        transmitter.Send(message);
    }

    public void StopWalking()
    {
        var message = new OSCMessage("/walk");
        message.AddValue(OSCValue.Int(0));
        transmitter.Send(message);
    }


    public void Waterfall(float playerDistance, int numWaterfall)
    {
        if(numWaterfall <= maxWaterfalls)
        {
            var message = new OSCMessage("/waterfall");
            float fWaterfall = ((playerDistance - firstWaterfall) * 0.5f) / -firstWaterfall;
            float sWaterfall = ((playerDistance - secondWaterfall) * 0.3f) / -secondWaterfall;
            float firstParam = fWaterfall >= 0 ? fWaterfall : 0;
            float secondParam = sWaterfall >= 0 ? sWaterfall : 0;
            string routeParam = "waterfall" + numWaterfall;

            message.AddValue(OSCValue.String(routeParam));
            message.AddValue(OSCValue.Float(firstParam));
            message.AddValue(OSCValue.Float(secondParam));
            transmitter.Send(message);
        } 
    }

    public void levelComplete(string mode)
    {
        var message = new OSCMessage( "/levelComplete" );
        message.AddValue( OSCValue.String(mode) );
        transmitter.Send(message);
    }

    public void GameOver(string mode)
    {
        var message = new OSCMessage( "/gameover" );
        message.AddValue( OSCValue.String(mode) );
        transmitter.Send(message);
    }
}
