using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HouseController : MonoBehaviour
{
    public int HouseHealth = 26;
    private TMP_Text HouseHealthText;
    // Start is called before the first frame update
    void Start()
    {
        HouseHealthText = GameObject.Find("HouseHealthText").GetComponent<TMP_Text>();
        HouseHealth = HouseHealth / PlayerPrefs.GetInt("Difficulty");
        HouseHealthText.text = "" + HouseHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HouseHurtOrDieFunction()
    {
            if (HouseHealth > 1)
            {
                Hurt();
            }
            else
            {
                Death();
            }
    }
    private void Hurt()
    {
        HouseHealth--;
        HouseHealthText.text = "" + HouseHealth;
    }
    private void Death()
    {
        SceneManager.LoadScene("LoseScene");
    }
}
