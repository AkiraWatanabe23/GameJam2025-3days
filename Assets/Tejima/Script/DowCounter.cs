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
    [SerializeField] Text text;
    [SerializeField] Animator animator;
    Week week;
    string readDay;
    bool isStop = false;
    float changeTime = 0.0f;
    void Start()
    {
        week = Week.ŒŽ—j“ú;
        readDay = $"{week}";
    }

    private void Update()
    {
        text.text = $"{week}“Ë“üI";
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
        if (week == Week.“ú—j“ú) { week = Week.ŒŽ—j“ú; }
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
