using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionFX : MonoBehaviour
{
    private AudioSource audio;
    
    private Renderer rend;
    private float timeStart;
    
    //private float timeExist;

    public AudioClip soundFX;
    public Color color1;
    public Color color2;
    public float maxSize = 10f;
    public float timeLenght = 1f;
    // Start is called before the first frame update
    void Start()
    {
        timeStart = Time.time;
        //timeLenght = soundFX.length;
        audio = GetComponent<AudioSource>();
        audio.PlayOneShot(soundFX, 0.7F);
        rend = gameObject.GetComponentInChildren<Renderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float u = Mathf.Sin(Mathf.PI*(Time.time - timeStart)/timeLenght);
        transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * maxSize, u);
        rend.material.color = Color.Lerp(color1, color2, u);
        if (Time.time - timeStart >= timeLenght) { Destroy(this.gameObject); }
    }
}
