using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Eraser : MonoBehaviour
{

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), 3, Vector2.zero);
            
            if (hits != null)
            {
                foreach (var hit in hits)
                {
                    hit.collider.GetComponent<Snow>().Hide();
                }
            }
        }
    }
}
