using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    public List<GameObject> pooledObjects;
    public List<int[]> cromosomas = new List<int[]>();
    public GameObject objectToPool;
    public int amountToPool;
    public Algoritmo alg;
    public Transform conjuntoFlores;

    public void Awake()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        float x = objectToPool.transform.position.x;
        for(int i = 0; i < amountToPool; i++)
        {
            tmp = GameObject.Instantiate(objectToPool, conjuntoFlores);
            tmp.transform.position = new Vector2(x, objectToPool.transform.position.y);
            x += 100;

            int centroFlor = Random.Range(5,16);
            tmp.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().size = new Vector2(centroFlor, centroFlor);
            int tamanioFlor = Random.Range(20,81);
            int colorFlor = Random.Range(0,3);
            tmp.transform.GetChild(0).GetComponent<SpriteRenderer>().size = new Vector2(tamanioFlor, tamanioFlor);
            tmp.transform.GetChild(0).GetComponent<SpriteRenderer>().color = alg.colorFlor[colorFlor];
            int tamanioTalloX = Random.Range(5,16);
            int tamanioTalloY = Random.Range(50,301);
            int colorTallo = Random.Range(0,4);
            tmp.transform.GetChild(1).GetComponent<SpriteRenderer>().size = new Vector2(tamanioTalloX, tamanioTalloY);
            tmp.transform.GetChild(1).GetComponent<SpriteRenderer>().color = alg.colorTallo[colorTallo];
            
            int[] unCromosoma = new int[]{tamanioTalloY, colorFlor, colorTallo, tamanioTalloX, tamanioFlor, centroFlor};
            cromosomas.Add(unCromosoma);

            tmp.SetActive(true);
            pooledObjects.Add(tmp);
        }
    }
}
