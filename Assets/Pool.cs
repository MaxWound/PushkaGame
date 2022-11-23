using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField]
    private Transform DieBound;
    public static Pool instance;
    [SerializeField]
    GameObject BombObject;
    [SerializeField]
    Guy original;
    [SerializeField]
    ParticleSystem fx;
    List<Guy> Guys = new List<Guy>();
    List<GameObject> Bombs = new List<GameObject>();
    List<ParticleSystem> fxs = new List<ParticleSystem>();

    [SerializeField]
    private int StartCount;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        
        for (int i = 0; i < StartCount; i++)
        {
            GameObject newBomb = Instantiate(BombObject, transform.position, transform.rotation, transform);
            newBomb.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            Bombs.Add(newBomb);

            fxs.Add(Instantiate(fx));
        }
        for(int i = 0; i < StartCount; i++)
        {
            Guy newGuy = Instantiate(original, transform.position, transform.rotation, transform);
            Guys.Add(newGuy);
                
        }
    }
    public Vector3 DieZ()
    {
        return DieBound.position;
    }
    public void ThrowBomb(Vector3 position, Vector3 direction, float power)
    {
        Bombs[0].transform.parent = null;
        Bombs[0].transform.position = position;
        Bombs[0].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        Bombs[0].GetComponent<Rigidbody>().AddForce((direction - position) * power, ForceMode.Impulse);
        Bombs.Remove(Bombs[0]);

    }
    public void SpawnGuy(Vector3 position)
    {
        Guys[0].transform.position = position;
        Guys[0].GetComponent<Rigidbody>().isKinematic = false;
        Guys[0].RunAnim();
        Guys.Remove(Guys[0]);
         
    }
    public void ReturnGuy(Guy guy)
    {
        guy.ResetAnim();
        guy.transform.position = transform.position;
        guy.transform.rotation = transform.rotation;
        Guys.Add(guy);
    }
    public void ReturnGuyWithDelay(Guy guy, float delay)
    {
        StartCoroutine(StartReturnGuyWithDelay(guy, delay));

    }   
    
    private IEnumerator StartReturnGuyWithDelay(Guy guy, float delay)
    {
        yield return new WaitForSeconds(delay);
        guy.ResetAnim();
        
        guy.transform.position = transform.position;
        guy.transform.rotation = transform.rotation;
        Guys.Add(guy);
    }
    public void ReturnBomb(GameObject obj)
    {
        obj.transform.parent = transform;
        obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;

        Bombs.Add(obj);
    }
    public void SpawnExplosion(Vector3 position)
    {
        StartCoroutine(StartSpawnExplosion(position));

    }
    public IEnumerator StartSpawnExplosion(Vector3 position)
    {
        fxs[0].transform.position = position;
        fxs[0].Play();
        ParticleSystem _fx;
        _fx = fxs[0];
        fxs.Remove(fxs[0]);
        yield return new WaitForSeconds(2f);
        _fx.Clear();
        _fx.Stop();
        _fx.time = 0;
        _fx.transform.position = transform.position;
        fxs.Add(_fx);
    }
}
