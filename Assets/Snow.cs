using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snow : MonoBehaviour
{
    private Image _Image;
    private float AlphaValue = 1f;
    private float FrostSpeed = 0.03f;
    private float MeltSpeed = 0.4f;

    bool canFreeze = false;
    private void Awake()
    {
        _Image = GetComponent<Image>();
        //StartCoroutine(Show());
    }
    public void ChangeColor()
    {
        _Image.color = new Color(_Image.color.r, _Image.color.g, _Image.color.b, 0);
    }
    public void Hide()
    {
        StopAllCoroutines();
        StartCoroutine(StartHiding());
    }
    private IEnumerator StartHiding()
    {
        while (AlphaValue >= 0)
        {
            yield return new WaitForEndOfFrame();

            AlphaValue -= MeltSpeed * Time.deltaTime;
            _Image.color = new Color(_Image.color.r, _Image.color.g, _Image.color.b, AlphaValue);
        }
        StartCoroutine(Show());
    }
    public IEnumerator Show()
    {
            while (AlphaValue <= 1)
            {
                yield return new WaitForEndOfFrame();

                AlphaValue += FrostSpeed * Time.deltaTime;
                _Image.color = new Color(_Image.color.r, _Image.color.g, _Image.color.b, AlphaValue);
            }
        
    }
}
