using TMPro;
using UnityEngine;

public class CountdownUI : MonoBehaviour
{

    private const string POPUP_ANIMATION = "PopupNumber";

    [SerializeField] private TextMeshProUGUI countdownTextUI;

    private Animator animator;
    private int previousCountdownNumber;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.Instance.OnStateChange += GameManager_OnStateChange;
        Hide();
    }

    private void Update()
    {
        int countdownNumber = Mathf.CeilToInt(GameManager.Instance.GetCountdownToStartTimer());

        countdownTextUI.text = countdownNumber.ToString();

        if(previousCountdownNumber != countdownNumber)
        {
            previousCountdownNumber = countdownNumber;
            animator.SetTrigger(POPUP_ANIMATION);
            SoundManager.Instance.PlayCountdownSound();
        }
    }
    private void GameManager_OnStateChange(object sender, System.EventArgs e)
    {
        if(GameManager.Instance.IsCountdownToStartActive())
        {
            Show();
        } else
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true); 
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
