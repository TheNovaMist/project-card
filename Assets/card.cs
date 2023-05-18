using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class card : MonoBehaviour
{
    [SerializeField]
    GameObject[] bulletPrefab = null;

    public GameObject root = null;

    private GameObject[] list = null;

    // Start is called before the first frame update
    void Start()
    {
        int[] map = randomMap();
        for (int i = 0; i < map.Length; i++)
        {
            Debug.Log(map[i]);
        }

        // init object array
        list= new GameObject[map.Length]; 


        Quaternion rotation = new Quaternion(0, 0,0,0);
        int size = root.transform.childCount;
        Debug.Log("size: " + size);

        for (int i=0; i<size; i++)
        {
            Vector3 tmp = root.transform.GetChild(i).position;
            GameObject newCard = Instantiate(bulletPrefab[map[i] -1], tmp, rotation, root.transform);
            newCard.transform.Rotate(new Vector3(180, 0, 0));

            // save with index
            list[i]= newCard;

            // add tag
            newCard.tag = "t"+map[i];
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int[] randomMap()
    {
        int[] map = new int[] { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6 };
        int len = map.Length;
        var rand = new System.Random();
        for (int k = 0; k < 100; k++)
        {
            int a = rand.Next(0, len-1);
            int b = rand.Next(0, len-1);

            int tmp = map[a];
            map[a] = map[b];
            map[b] = tmp;
        }

        return map;
    }
}
