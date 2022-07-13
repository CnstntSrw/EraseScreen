using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefabs;


    private void Awake()
    {
        var rt = GetComponent<RectTransform>();

        for (int i = 0; i <= rt.sizeDelta.x; i += 50)
        {
            for (int j = 0; j <= rt.sizeDelta.y; j += 50)
            {
                var prefab = prefabs[Random.Range(0, prefabs.Length - 1)];
                var instance = Instantiate(prefab, rt);
                var rti = instance.GetComponent<RectTransform>();
                rti.localPosition = new Vector3(i, -j, 0);

            }
        }
    }
}
