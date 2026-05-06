using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Piple : MonoBehaviour
{
    public int speed = 5;
    private Coroutine coroutine = null;
    public void StopRun()
    {
        coroutine = StartCoroutine(ReleasePiple());
    }

    IEnumerator ReleasePiple()
    {
        yield return new WaitForSeconds(4f);
        PiplelineManager.Instance.pipePool.Release(this.gameObject);                                                          
        
    }
    void Start()
    {
        float y = Random.Range(-2.5f, 2.5f);
        this.transform.localPosition += new Vector3(0, y, 0);
        StopRun();
    }
    void Update()
    {
        if(GameManager.Instance.status == GameManager.GAME_STATUS.InGame)
        {
            this.transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * speed;
        }
        
        if(GameManager.Instance.status == GameManager.GAME_STATUS.GameOver)
        {
            PiplelineManager.Instance.pipePool.Release(this.gameObject);
        }
    }

}
