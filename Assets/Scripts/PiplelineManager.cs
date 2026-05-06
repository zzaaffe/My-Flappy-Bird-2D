using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PiplelineManager : MonoBehaviour
{
    public static PiplelineManager Instance;
    public GameObject piple;
    public ObjectPool<GameObject> pipePool;
    private void Awake()
    {
        Instance = this;
        pipePool = new ObjectPool<GameObject>(createPiple, GetPiple, ReleasePiple, DestroyPiple, false, 10, 100);
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    Coroutine coroutine = null;

    private GameObject createPiple()
    {
        GameObject obj = Instantiate(piple,this.transform,true);
        return obj;
    }

    void GetPiple(GameObject piple)
    {
        piple.gameObject.SetActive(true);
        piple.gameObject.transform.position = this.piple.transform.localPosition;
        piple.transform.rotation = Quaternion.identity;
    }

    void ReleasePiple(GameObject piple)
    {
        piple.gameObject.SetActive(false);
    }

    void DestroyPiple(GameObject piple)
    {
        Destroy(piple);
    }
    public void StartRun()
    {
        coroutine = StartCoroutine(CreatePiples());
    }

    public void StopRun()
    {
        StopCoroutine(coroutine);
    }

    IEnumerator CreatePiples()
    {
        while(true)
        {
            pipePool.Get();
            yield return new WaitForSeconds(1.5f);
        }
    }

}
