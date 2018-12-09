using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {

	// Use this for initialization
	public class MinMax
    {
        float _min;
        float _max;

        public MinMax(float min, float max)
        {
            this._min = min;
            this._max = max;

        }

        public float min
        {
            get { return _min; }
        }

        public float max
        {
            get { return _max; }
        }

        public float GetRand()
        {
            return Random.Range(_min, _max);
        }
    }
	
	// Update is called once per frame
	
}
