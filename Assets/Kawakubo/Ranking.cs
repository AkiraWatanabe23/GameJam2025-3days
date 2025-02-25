using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ranking : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<float> Scores = new List<float>()
        {
            PlayerPrefs.GetFloat("1", 0), 
            PlayerPrefs.GetFloat("2", 0),
            PlayerPrefs.GetFloat("3", 0), GameManager.Instance.GetTime()
        };
        IEnumerable Ranking = Scores.OrderByDescending(x => x);
        int num = 0;
        foreach (float r in Ranking)
        {
            if(num < 3)
            {
                num++;
                PlayerPrefs.SetFloat(num.ToString(), r);
            }
        }
    }
}
