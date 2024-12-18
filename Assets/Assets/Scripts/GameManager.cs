using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static int gameMode = 0;

    public float yourTime;

    public TMP_InputField searchBox;
    public MemberManager memberManager;
    public Timer timer;
    public List<Member> memberList;

    public int doneMember = 0;

    public Image cloudImage;
    public TextMeshProUGUI cloudTxt;
    public TextMeshProUGUI memLeft;

    public Image pfp;
    public TextMeshProUGUI chName;

    public TextMeshProUGUI scoreDesc;

    public GameObject errorPanel;

    public GameObject subBtn;
    public Button searchBtn;

    public GameObject winPanel;
    public TextMeshProUGUI curScoreTxt;
    public TextMeshProUGUI hsTxt;

    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreTxt;
    public TextMeshProUGUI goHS;

    public bool isCorrect;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        if (gameMode == 0)
        {
            foreach (Member m in memberManager.Members)
            {
                memberList.Add(m);
            }

            for (int i = 0; i < memberList.Count; i++)
            {
                Member temp = memberList[i];
                int randomIndex = Random.Range(i, memberList.Count);
                memberList[i] = memberList[randomIndex];
                memberList[randomIndex] = temp;
            }
            memLeft.text = memberList.Count.ToString();
        }
        else
        {
            scoreDesc.text = "Members Subbed: ";
            memberList.Add(memberManager.Members[Random.Range(0, memberManager.Members.Length)]);
            memLeft.text = doneMember.ToString();
        }

        cloudImage.sprite = memberList[0].memPic;
        cloudTxt.text = memberList[0].memName;
    }

    // Update is called once per frame
    void Update()
    {
        //checks if win
        if (gameMode == 0)
        {
            if (doneMember == memberManager.Members.Length)
            {
                Time.timeScale = 0;
                winPanel.SetActive(true);
                CheckHighScore();
                curScoreTxt.text = FormatScore(yourTime);
                hsTxt.text = FormatScore(PlayerPrefs.GetFloat("HighScore"));
            }
        }
        else
        {
            if (timer.currentTime <= 0)
            {
                Time.timeScale = 0;
                gameOverPanel.SetActive(true);
                CheckHighScore();
                scoreTxt.text = doneMember.ToString();
                goHS.text = PlayerPrefs.GetInt("Highscore1").ToString();
            }
        }       
    }

    public void CheckWord()
    {
        if (searchBox.text.ToLower() == memberList[doneMember].memName.ToLower())
        {
            isCorrect = true;
        }
        else
        {
            isCorrect = false;
        }
        searchBtn.interactable = true;
    }

    public void ShowResult()
    {
        if (isCorrect)
        {
            errorPanel.SetActive(false);
            pfp.sprite = memberList[doneMember].memPic;
            chName.text = memberList[doneMember].memName;
            subBtn.GetComponent<Button>().interactable = true;
            subBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Subscribe";
        }
        else
        {
            errorPanel.SetActive(true);
        }
        searchBtn.interactable = false;
    }
    public void Subscribe()
    {
        doneMember = doneMember + 1;

        if (gameMode == 0)
        {
            if (doneMember == memberList.Count)
            {
                Time.timeScale = 0;
                yourTime = timer.currentTime;
            }
            else
            {
                cloudImage.GetComponent<Image>().sprite = memberList[doneMember].memPic;
                cloudTxt.text = memberList[doneMember].memName;
                subBtn.GetComponent<Button>().interactable = false;
                subBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Subscribed";

                memLeft.text = (memberList.Count - doneMember).ToString();
            }
        }
        else
        {
            AddCheckDuplicate();
            cloudImage.GetComponent<Image>().sprite = memberList[doneMember].memPic;
            cloudTxt.text = memberList[doneMember].memName;
            subBtn.GetComponent<Button>().interactable = false;
            subBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Subscribed";

            timer.currentTime = timer.currentTime + 3;
            memLeft.text = doneMember.ToString();        
        }
        
    }

    public void SearchClear()
    {
        searchBox.text = string.Empty;
    }

    public void CheckHighScore()
    {
        if (gameMode == 0)
        {
            if (yourTime < PlayerPrefs.GetFloat("HighScore", 3600))
            {
                PlayerPrefs.SetFloat("HighScore", yourTime);
            }
        }
        else
        {
            if (doneMember > PlayerPrefs.GetInt("Highscore1", 0))
            {
                PlayerPrefs.SetInt("Highscore1", doneMember);
            }
        }
        
    }

    public string FormatScore(float score)
    {
        int minutes = Mathf.FloorToInt(score / 60);
        int seconds = Mathf.FloorToInt(score % 60);
        int milliseconds = Mathf.FloorToInt(((score * 100) % 100));
        string scoreTxt = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        return scoreTxt;
    }

    public void AddCheckDuplicate()
    {
        int num = Random.Range(0, memberManager.Members.Length - 1);
        if (memberList[memberList.Count - 1].memName == memberManager.Members[num].memName)
        {
            AddCheckDuplicate();
        }
        else
        {
            memberList.Add(memberManager.Members[num]);
        }   
    }
}
