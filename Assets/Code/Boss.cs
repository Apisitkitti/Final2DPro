
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static OpenSimplex2S;
public class Boss : MonoBehaviour
{
    Animator anim;
    public int boss_HP = 1000;
    public Slider bossui;
    public float movementSpeed = 10f;
    [SerializeField] GameObject[] UIanything;
    [SerializeField] GameObject winmenu;
    [SerializeField] Transform boss;
    private float noiseScale = 0.1f; // adjust to control the scale of the noise
    private float noiseThreshold = 0.6f; // adjust to control the threshold for the noise
    Rigidbody2D rb;
    BoxCollider2D col;
    [SerializeField] Transform pos;
    public int point = 100;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        
    }
    void Update()
    {
        if(winmenu.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
       
    
     public void TakeDamage(int damage)
    {   
        
            anim.SetTrigger("TakeDam");
            boss_HP -= damage/2;
        
        
        if(boss_HP <=0)
        {
            ScoreManager.score += point;
            StartCoroutine(ShakeCamera(0.5f, 0.1f)); // shake the camera for 0.5 seconds with 0.1 intensity

            StartCoroutine(FadeOut(1f)); // fade out the screen for 1 second

                StartCoroutine(Explode(2f));
            anim.SetBool("Death",true);
            
        }
         bossui.value = boss_HP;
         if (boss_HP == 500)
{
    rb.simulated = false;
    col.enabled = false;
    FireBallSpawnner spawnner = FindObjectOfType<FireBallSpawnner>();
if (spawnner != null)
{
    spawnner.RestartSpawning();
}
    Vector3 movement = Vector3.up * movementSpeed * Time.deltaTime;
    transform.Translate(movement);
    

    if (transform.position.y <= pos.position.y)
    {
        transform.position = new Vector3(transform.position.x, pos.position.y, transform.position.z);
    }
}
    
    }

    private IEnumerator ShakeCamera(float duration, float intensity)
    {
        float endTime = Time.time + duration;
        while (Time.time < endTime)
        {
            float x = OpenSimplex2S.Noise2(0,Time.time * 50f * intensity, 0f) * intensity;
            float y = OpenSimplex2S.Noise2(0,0, Time.time * 50f * intensity) * intensity;
            Camera.main.transform.position += new Vector3(x, y, 0f);
            yield return null;
        }
    }

    private IEnumerator FadeOut(float duration)
{
    float startTime = Time.time;
    float startAlpha = 0f;
    float endAlpha = 1f;
    Image fadePanel = FindObjectOfType<Image>();
    while (Time.time < startTime + duration && fadePanel != null)
    {
        float t = (Time.time - startTime) / duration;
        float alpha = Mathf.Lerp(startAlpha, endAlpha, t);
        Color color = new Color(0f, 0f, 0f, alpha);
        fadePanel.color = color;
        yield return null;
    }
}

    private IEnumerator Explode(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject explosion = Instantiate(Resources.Load<GameObject>("Resources"));
        explosion.transform.position = transform.position;
        Destroy(gameObject);
        winmenu.SetActive(true);
    }
     public void BossDeath()
{
    foreach(GameObject item in UIanything)
            {
                item.SetActive(false);
            }

            Destroy(gameObject);
            winmenu.SetActive(true);
     
}


}