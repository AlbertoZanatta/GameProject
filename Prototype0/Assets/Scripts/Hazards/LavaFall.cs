using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaFall : MonoBehaviour {

    public float timeToDeactivate = 2.5f;
    public float timeStayDeactive = 1.5f;
    private float elapsedTime = 0;
    private bool isDeactivating = false;
	
	// Update is called once per frame
	void Update () {
        if(!isDeactivating)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= timeToDeactivate)
            {
                elapsedTime = 0;
                StartCoroutine(Deactivate());
            }
        }
        
	}

    IEnumerator Deactivate()
    {
        isDeactivating = true;
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child != null)
                child.SetActive(false);
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child != null)
                child.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
        isDeactivating = false;

    }
}
