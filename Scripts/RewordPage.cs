using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewordPage : MonoBehaviour
{
    Transform weaponParent;
    private void Awake()
    {
        weaponParent = transform.Find("WeaponParent");

    }
    [SerializeField]
    GameObject map;
    public void CloseThis()
    {
        gameObject.SetActive(false);
        //给玩家激活武器
        for (int i = 0; i < weaponParent.childCount; i++)
        {
            if (weaponParent.GetChild(i).GetComponent<RewordChoose>())
            {
                if (weaponParent.GetChild(i).GetComponent<RewordChoose>().isChoose)
                {
                    GameObject.FindObjectOfType<PlayerSC>().AddWeapon(weaponParent.GetChild(i).GetComponent<RewordChoose>().weaponID);
                }
                //LocalData.activeWeaopnList.Add(weaponParent.GetChild(i).GetComponent<RewordChoose>().weaponID);
                
            }
            
        }
        //添加地图
        map.SetActive(true);
        //添加药水
        GameObject.FindObjectOfType<AddHPProp>().AddCount();
      
    }
}
