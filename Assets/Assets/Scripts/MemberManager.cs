using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemberManager : MonoBehaviour
{
    public Member[] Members;
    private string folderPath = "Member/ENmember"; // Folder name inside the Assets/Resources folder

    void Awake()
    {
        Members = Resources.LoadAll<Member>(folderPath);
    }
}