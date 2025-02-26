using UnityEngine;
using UnityEngine.UI;

enum Week
{
    ���j��,
    �Ηj��,
    ���j��,
    �ؗj��,
    ���j��,
    �y�j��,
    ���j��
}

public class DowCounter : MonoBehaviour
{
    [SerializeField] Animator animator;
    Week week;
    [SerializeField]Sprite[] sprites = new Sprite[7];
    Image image;
    string readDay;
    bool isStop = false;
    int spriteNum = 0;
    float time = 0.0f;
    float changeTime = 0.0f;
    void Start()
    {
        image = GetComponent<Image>();
        week = Week.���j��;
        readDay = $"{week}";
    }

    private void Update()
    {
        Debug.Log ($"{week}�˓��I:{spriteNum}");
        sprites.GetValue(spriteNum);
        image.sprite = sprites[spriteNum];
        if (isStop){ changeTime += Time.deltaTime; }
        else { time += Time.deltaTime; }

        if (time >= 48)
        {
            NextDay();
            time = 0;
        }

        if (changeTime >= 1)
        {
            animator.SetBool("Change", false);
            isStop = false;
            changeTime = 0;
        }
    }
    public void NextDay()
    {
        if (week == Week.���j��) 
        {
            week = Week.���j��;
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
