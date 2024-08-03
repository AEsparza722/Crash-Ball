using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashPool : MonoBehaviour
{
    public static SplashPool instance;
    List<GameObject> splashList = new List<GameObject>();
    [SerializeField] int initialSpash;
    [SerializeField] GameObject splashPrefab;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitialSplash();
    }

    void InitialSplash()
    {
        for (int i = 0; i < initialSpash; i++) 
        {
            GameObject splashClone = Instantiate(splashPrefab, transform);
            splashClone.SetActive(false);
            splashList.Add(splashClone);
        }        
    }

    public GameObject GetPooledSplash()
    {
        foreach (GameObject splashClone in splashList)
        {
            if (!splashClone.activeSelf)
            {
                return splashClone;
            }
        }
        return AddNewSplash();
    }

    GameObject AddNewSplash()
    {
        GameObject splashClone = Instantiate(splashPrefab, transform);
        splashClone.SetActive(false);
        splashList.Add(splashClone);
        return splashClone;
    }
}
