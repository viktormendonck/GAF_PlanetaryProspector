using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialVisualmanager : MonoBehaviour
{
    [System.Serializable]
    public struct VisualInfo
    {
        public string Text;
        public GameObject VisualObject;
    }

    [SerializeField] List<VisualInfo> TutorialPages = new List<VisualInfo>();
    [SerializeField] private TextMeshProUGUI tutorialText;
    private int currentPage = 0;

    private TutorialCondition condition;
    // Start is called before the first frame update
    void Start()
    {
        OpenPage(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (condition.Condition())
        {
            ClosePage();
            currentPage++;
            if (currentPage < TutorialPages.Count)
            {
                OpenPage(currentPage);
            }
            else
            {
                //load screen to go back to main menu
                SceneManager.LoadScene(4);
            }
        }
    }
    void OpenPage(int index)
    {
        TutorialPages[index].VisualObject.SetActive(true);
        tutorialText.text = TutorialPages[index].Text;
        condition = TutorialPages[index].VisualObject.GetComponent<TutorialCondition>();
        if (condition == null)
        {
            Debug.LogError("Tutorial couldnt find condition to continue");
        }
    }
    void ClosePage()
    {
        TutorialPages[currentPage].VisualObject.SetActive(false);
    }
}
