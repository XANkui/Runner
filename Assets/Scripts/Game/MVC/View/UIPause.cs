using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPause : View
{
    public Button buttonResume;

    public Text textCoin;
    public Text textDistance;
    public Text textScore;

    int m_coin = 0;
    int m_distance = 0;
    int m_score = 0;

    public override string Name => Consts.V_Pause;

    public int Coin { get => m_coin;
        set {
            m_coin = value;
            textCoin.text = value.ToString();
        }
    }
    public int Distance { get => m_distance;
        set
        {
            m_distance = value;
            textDistance.text = value.ToString();
        }
    }
    public int Score { get => m_score;
        set
        {
            m_score = value;
            textScore.text = value.ToString();
        }
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public override void HandleEvent(string name, object data)
    {
        throw new System.NotImplementedException();
    }

    public void OnResumeClick() {
        Hide();
        SendEvent(Consts.E_ResumeGame);
    }


    private void Awake()
    {
        buttonResume.onClick.AddListener(OnResumeClick);
    }
}
