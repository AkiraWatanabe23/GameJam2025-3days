using UnityEngine;
using UnityEngine.UI;

enum Week
{
    ŒŽ—j“ú,
    ‰Î—j“ú,
    …—j“ú,
    –Ø—j“ú,
    ‹à—j“ú,
    “y—j“ú,
    “ú—j“ú
}

public class DowCounter : MonoBehaviour
{
    [SerializeField] Animator animator;
    Week week;
    [SerializeField]Sprite[] sprites = new Sprite[7];
    string readDay;
    bool isStop = false;
    int spriteNum = 0;
    float time = 0.0f;
    float changeTime = 0.0f;
    void Start()
    {
        week = Week.ŒŽ—j“ú;
        readDay = $"{week}";
    }

    private void Update()
    {
        Debug.Log ($"{week}“Ë“üI");
        sprites.GetValue(spriteNum);
        if (isStop){ changeTime += Time.deltaTime; }
        else { time += Time.deltaTime; }

        if (time >= 0.8)
        {
            NextDay();
            time = 0;
        }

        if (changeTime >= 0.8)
        {
            animator.SetBool("Change", false);
            isStop = false;
            changeTime = 0;
        }
    }
    public void NextDay()
    {
        if (week == Week.“ú—j“ú) 
        {
            week = Week.ŒŽ—j“ú;
            spriteNum = 0;
        }
        else 
        {
            week++;
            spriteNum++;
        }
        animator.SetBool("Change", true);
        isStop = true;
        readDay = $"{week}";
    }

    public string Result()
    {
        return readDay;
    }
}
