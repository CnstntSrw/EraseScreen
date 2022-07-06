using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Eraser))]
public class IcePlacer : MonoBehaviour
{
    [SerializeField]
    [Range(0, 1)]
    private float FrostSpeed = 0.1f;
    private float AlphaValue;
    private SpriteRenderer _Image;
    private Eraser _Eraser;
    private void Start()
    {
        _Image = GetComponent<SpriteRenderer>();
        StartCoroutine(PlaceIce());
        _Eraser = GetComponent<Eraser>();
    }
    IEnumerator PlaceIce()
    {
        while (AlphaValue <= 1)
        {
            yield return new WaitForEndOfFrame();

            AlphaValue += FrostSpeed * Time.deltaTime;
            _Image.color = new Color(_Image.color.r, _Image.color.g, _Image.color.b, AlphaValue);
        }
        _Eraser.enabled = true;
    }
}
