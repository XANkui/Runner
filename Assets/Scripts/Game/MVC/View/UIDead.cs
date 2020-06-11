using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDead : View
{

    private  int m_BriberyTime =1;

    public Button btnCancel;
    public Button btnBuy;
    public Text textBribery;

    public override string Name =>Consts.V_Dead;

    public int BriberyTime { get => m_BriberyTime; set => m_BriberyTime = value; }

    public override void HandleEvent(string name, object data)
    {
        
    }

    public void Hide() {

        gameObject.SetActive(false);

    }


    public void Show() {
        textBribery.text = (BriberyTime * 500).ToString();
        gameObject.SetActive(true);
    }

    public void OnCancelClick()
    {
        SendEvent(Consts.E_FinalShowUI);
    }

    public void OnBriberyClick() {

        CoinArgs coin = new CoinArgs
        {
            coin = m_BriberyTime * 500
        };

        SendEvent(Consts.E_BriberyClick, coin);
    }


    private void Awake()
    {
        btnCancel.onClick.AddListener(OnCancelClick);
        btnBuy.onClick.AddListener(OnBriberyClick);
        m_BriberyTime = 1;
    }

}
