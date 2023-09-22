using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CutScenesCommon : MonoBehaviour
{

  public float  NarrTypingSpeed=0.05f;
    public Transform Cam;
    public AudioClip[] NarrationClips;
    public string[] Narrations;
    public TextMeshProUGUI Narrtext;
    public AudioSource Narr_AS;
    
    // Start is called before the first frame update
  public  virtual void Start()
    {
        Narrtext.text = "";

    }

    // Update is called once per frame
    void Update()
    {

    }



    public IEnumerator TypingText(string str)
    {
        Narrtext.text = "";
        
        for (int i = 0; i < str.Length; i++)
        {
            Narrtext.text += str[i];

            yield return new WaitForSeconds(NarrTypingSpeed);


        }



    }
}
