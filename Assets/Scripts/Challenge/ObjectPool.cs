using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    List<GameObject> pool = new List<GameObject>();
    ObjectPool<GameObject> pool2; 
    public GameObject PoolObjectPrefab;
    private const int minSize = 50;
    private const int maxSize = 300;

    private void Start()
    {        
        for (int i = 0; i < minSize; i++) //1
        {
            pool.Add(Instantiate(PoolObjectPrefab));
        }

        pool2 = new ObjectPool<GameObject>(CreateObject, ActiveObject, DisableObject, DestroyObject, true, 0, 100);
    }

    //50개를 기본적으로 스타트에서 생성하고, 이후 최고치를 넘지않는 선에서 리스트에 등록 나머지는 등록하지 않고 반환
    public GameObject Spec01()
    {
        foreach (GameObject temp in pool)
        {
            if(temp.activeSelf == false)
            {
                temp.SetActive(true);
                return temp;
            }
        }

        GameObject newObject = Instantiate(PoolObjectPrefab);
        if(pool.Count < maxSize)
        {
            pool.Add(newObject);
        }
        return newObject;
    }

    //50개로 시작해서 가장 오래된 것을 반환하고 재사용
    public GameObject Spec02()
    {
        foreach(GameObject temp in pool)
        {
            if (temp.activeSelf == false)
            {
                temp.SetActive(true);
                return temp;
            }
        }

        if(pool.Count >= maxSize)
        {
            GameObject temp = pool[0];
            pool.RemoveAt(0);
            pool.Add(temp);
            temp.SetActive(true);
            return temp;
        }
        
        GameObject newObject = Instantiate(PoolObjectPrefab);
        pool.Add(newObject);
        return newObject;
    }

    public void Spec03()
    {
        //유니티 풀링 사용! 
    }

    public void ReleaseObject(GameObject obj)
    {
        //1
        {
            if (pool.Count >= maxSize)
            {
                Destroy(obj);
            }
            else obj.SetActive(false);
        }

        //2
        {
            obj.SetActive(false);
        }
    }
    
    private GameObject CreateObject()
    {
        GameObject newOne = Instantiate(PoolObjectPrefab);
        newOne.SetActive(false);
        return newOne;
    }
    private void ActiveObject(GameObject obj)
    {
        obj.SetActive(true);
    }
    private void DisableObject(GameObject obj)
    {
        obj.SetActive(false);
    }
    private void DestroyObject(GameObject obj)
    {
        Destroy(obj);
    }

    public GameObject UnityGetObject()
    {
        GameObject temp = null;
        if (pool.Count >= maxSize)
        {
            temp = CreateObject();
            temp.tag = "OverFlow";
        }
        else pool2.Get();

        return temp;
    }

    public void  UnityReleaseObjet(GameObject obj)
    {
        if (obj.CompareTag("OverFlow"))
        {
            Destroy(obj);
        }
        else pool2.Release(obj); 
    }
}