using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class DelayDisable : MonoBehaviour
{
   
    public float Delay;   
    // Start is called before the first frame update
    void OnEnable()
    {
        CancelInvoke();
        Invoke(nameof(Disable), Delay);
    }

    public void Disable()
    {
       gameObject.SetActive(false);

    }



}
