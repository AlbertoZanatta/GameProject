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
    public float maxWaterfalls = 8;
    public bool inCave = false;

    //Torch sounds
    public float flameDist = 5f;
    public float creakingsDist = 2f;
    public int maxTorches = 7;

    private int coinSound = 850;
    private float lastCollected;
    [SerializeField]
    private float coinTimeFrame = 2;


    public void Fall(string mode)
    {
        var message = new OSCMessage("/fall");
        message.AddValue(OSCValue.String(mode));
        transmitter.Send(message);
    }

    public void Torch(float playerDistance, int numTorch)
    {
        if(numTorch <= maxTorches)
        {
            var message = new OSCMessage("/fire");
            float flame = ((playerDistance - flameDist)) * 0.5f / -flameDist;
            float creaking = ((playerDistance - creakingsDist)) * 0.3f / -creakingsDist;
            flame = flame >= 0 ? flame : 0;
            creaking = creaking >= 0 ? creaking : 0;
            //Debug.Log("Player dist: " + playerDistance + ", flame intensity: " + flame + ", creaking intensity: " + creaking);
            string routeParam = "torch" + numTorch;

            message.AddValue(OSCValue.String(routeParam));
            message.AddValue(OSCValue.Float(flame));
            message.AddValue(OSCValue.Float(creaking));
            transmitter.Send(message);
        } 
    }
    public void ThrowKnife()
    {
        var message = new OSCMessage("/knife");
        message.AddValue(OSCValue.Int(1));
        transmitter.Send(message);
    }

    public void DrinkPotion()
    {
        var message = new OSCMessage("/potion");
        message.AddValue(OSCValue.Int(400));
        transmitter.Send(message);
    }


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

    private void Update()
    {
        lastCollected += Time.deltaTime;
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

    public void EarnCoin()
    {
        if (lastCollected <= coinTimeFrame)
        {
            if (coinSound <= 1000)
            {
                coinSound += 50;
            }
        }
        else
        {
            coinSound = 850;
        }
        lastCollected = 0;

        var message = new OSCMessage("/earnCoin");
        message.AddValue(OSCValue.Int(coinSound));
        transmitter.Send(message);
    }

    public void Hit(string mode)
    {
        var message = new OSCMessage("/hit");
        message.AddValue(OSCValue.String(mode));
        string caveMode = inCave ? "in" : "out";
        message.AddValue(OSCValue.String(caveMode));
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
