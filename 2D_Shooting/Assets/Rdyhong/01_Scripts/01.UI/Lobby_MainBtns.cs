using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby_MainBtns : MonoBehaviour
{
    [SerializeField] List<Button> mainBtns = new List<Button>();

    private void Awake()
    {
        //BtnBinding();
    }

    private void BtnBinding()
    {
        foreach(Button _btn in mainBtns)
        {
            switch(_btn.name)
            {
                case "Btn_QuickMatch":
                    _btn.onClick.AddListener(() => {
                        
                    });
                    break;

                case "Btn_Room":
                    _btn.onClick.AddListener(() => {

                    });
                    break;

                default:

                    break;
            }
        }
    }
}
