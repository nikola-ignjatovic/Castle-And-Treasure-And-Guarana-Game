using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedBullControler : MonoBehaviour
{
    public GameObject Krila1;
    public GameObject Krila2;
    public int BullHp;
    public DetectSove DS;
    public Slider BullHpSlider;

    public BullControler BullControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PrvoKrilo());
    }

    // Update is called once per frame
    void Update()
    {
        BullHpSlider.value = BullHp;
    }
    IEnumerator PrvoKrilo()
    {
        yield return new WaitForSeconds(0.1f);
        Krila1.active = true;
        Krila2.active = false;
        StartCoroutine(DrugoKrilo());
    }
    IEnumerator DrugoKrilo()
    {
        yield return new WaitForSeconds(0.1f);
        Krila1.active = false;
        Krila2.active = true;
        StartCoroutine(PrvoKrilo());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Zid")
        {
            Debug.Log("Rotate");
            BullControllerScript.Rotate();
            //    Debug.Log("sova");
            //    rb.velocity = new Vector3(0, 0, 0);
            //    transform.rotation = Quaternion.Euler(0, 180 * k, 0);
            //    k++;
            //    k %= 2;
        }
        if (collision.gameObject.tag == "Laser")
        {
            if (BullHp > 0)
            {
                BullHp--;
                Destroy(collision.gameObject);
            }
            else
            {
                Destroy(collision.gameObject);
                DS.Walls = false;
                Destroy(GameObject.Find("NewSprite"));
            }
        }
    }


}
