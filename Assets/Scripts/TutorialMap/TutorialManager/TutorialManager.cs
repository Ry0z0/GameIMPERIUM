using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public TextMeshProUGUI guideText;
    public GameObject guidePanel;
    public Button moveForwardButton;
    public Button moveBackwardButton;
    public SceneController sceneController;
    public GameObject buttonManager; // Tham chiếu đến UIButtonManager
    public EnemyTutorialHealth enemyHealth;
    public Button continueButton;


    private int step = 0;

    private void Awake()
    {

        // Tắt script UIButtonManager khi khởi chạy
        if (buttonManager != null)
        {
            buttonManager.SetActive(false);
        }
    }

    void Start()
    {
        // Ẩn nút Continue khi bắt đầu
        if (continueButton != null)
        {
            continueButton.gameObject.SetActive(false);
        }
        ShowStepOne();
    }

    void Update()
    {
        if (guidePanel.activeSelf && Input.GetMouseButtonDown(0))
        {
            if (step == 0)
            {
                ShowStepTwo();
            }
            else if (step == 1)
            {
                ShowStepThree();
            }
            else if (step == 2)
            {
                guidePanel.SetActive(false);
            }
            else if (step == 3)
            {
                guidePanel.SetActive(false);
            }
            else if (step == 4)
            {
                guidePanel.SetActive(false);
            }
            else if (step == 5)
            {              
                guidePanel.SetActive(false);
            }else if (step == 6)
            {
                guidePanel.SetActive(false);
                // Hiện nút Continue khi đến step 6
                if (continueButton != null)
                {
                    continueButton.gameObject.SetActive(true);
                }
            }
        }
    }

    public int GetCurrentStep()
    {
        return step; 
    }

    public void SetCurrentStep(int newStep)
    {
        step = newStep;
        if (step == 4)
        {
            guidePanel.SetActive(true);
            guideText.text = "Bạn đã đến gần kẻ thù, hãy ấn nút tấn công để tấn công nó!";
        }if (step == 3)
        {
            guidePanel.SetActive(true);
            guideText.text = "Khi thể lực xuống dưới 50%, bạn có thể ấn để hồi thể lực";
        }
        if (step == 5 )
        {
            guidePanel.SetActive(true);
            guideText.text = "Kẻ địch đã yếu, hãy kết liễu chúng bằng đón tấn công mạnh hơn!";
        }
        if (step == 6)
        {
            guidePanel.SetActive(true);
            guideText.text = "Chúc mừng, bạn đã dành chiến thắng";
            Time.timeScale = 0;
            // Hiện nút Continue khi đến step 6
            if (continueButton != null)
            {
                continueButton.gameObject.SetActive(true);
            }

        }
       
    }


    void ShowStepOne()
    {
        guidePanel.SetActive(true);
        guideText.text = "Chào mừng bạn đến với phòng tân thủ, đây là nơi để bạn làm quen với các nút.";
    }

    void ShowStepTwo()
    {
        guideText.text = "Đây là 2 nút tiến lên và lùi xuống, dùng để di chuyển. Hãy sử dụng hợp lý để tiếp cận kẻ thù";
        ShowMovementButtons(true);

        // Vô hiệu hóa các nút để không cho phép nhấn
        if (moveForwardButton != null)
        {
            moveForwardButton.interactable = false;
        }
        if (moveBackwardButton != null)
        {
            moveBackwardButton.interactable = false;
        }
        step = 1;
    }

    void ShowStepThree()
    {
        guideText.text = "Hãy ấn nút tiến lên để tiếp cận kẻ thù";
        if (buttonManager != null)
        {
            buttonManager.SetActive(true);
        }
        if (moveForwardButton != null)
        {
            moveForwardButton.interactable = true;
        }
        if (moveBackwardButton != null)
        {
            moveBackwardButton.interactable = true;
        }
        step = 2;
    }

    private void ShowMovementButtons(bool isActive)
    {
        if (moveForwardButton != null)
        {
            moveForwardButton.gameObject.SetActive(isActive);
        }
        if (moveBackwardButton != null)
        {
            moveBackwardButton.gameObject.SetActive(isActive);  
        }
    }
}
