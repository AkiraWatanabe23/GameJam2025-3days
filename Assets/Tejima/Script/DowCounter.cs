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
    [SerializeField] Text text;
    [SerializeField] Animator animator;
    Week week;
    string readDay;
    bool isStop = false;
    float changeTime = 0.0f;
    void Start()
    {
        week = Week.���j��;
        readDay = $"{week}";
    }

    private void Update()
    {
        text.text = $"{week}�˓��I";
        if (isStop){ changeTime += Time.deltaTime; }

        if (changeTime >= 2)
        {
            animator.SetBool("Change", false);
            isStop = false;
            changeTime = 0;
        }
    }
    public void NextDay()
    {
        if (week == Week.���j��) { week = Week.���j��; }
        else { week++; }
        animator.SetBool("Change", true);
        isStop = true;
        readDay = $"{week}";
    }

    public string Result()
    {
        return readDay;
    }
}
