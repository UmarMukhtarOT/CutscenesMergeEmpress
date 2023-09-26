using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class YoyoScale : MonoBehaviour
{
    public bool OnEnabled;
    public float dur;
    public Vector3 MinScale;
    public Vector3 MaxScale;
    public Ease Etype = Ease.Linear;



    // Start is called before the first frame update
    void OnEnable()
    {
        if (OnEnabled)
        {
            DoScale();
        }
    }








    void DoScale()
    {


        transform.DOScale(MaxScale, dur).SetEase(Etype).OnComplete(() => {

            transform.DOScale(MinScale, dur).SetEase(Etype).OnComplete(() =>
            {
                DoScale();

            });




        });



    }




}
