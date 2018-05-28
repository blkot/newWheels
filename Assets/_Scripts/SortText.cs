using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;



public class SortText : MonoBehaviour {

    //需要排序的stringText组件
    public Text arraytoSort;
    //排序完成后的stringText组件
    public Text sortedArray;
    
    //原string
    private string str;
    //分隔后的string[]
    private string[] strings;
    //首次处理后的string
    private string tmpString = "";
    //排序完成的string[]
    private string[] newstring;
    //Join后的长string
    private string newlongstr;

    //忽略串前字符数
    
    public uint reserved_char_Count;
    //分隔符
    public char[] seperator ={};
    
    
	// Use this for initialization
	void Start () {
        
        //引用组件和文本

        arraytoSort = GameObject.Find("Canvas/Array").GetComponent<Text>();
        sortedArray = GameObject.Find("Canvas/Sort").GetComponent<Text>();
        str = arraytoSort.text;

        //忽略开头       
        tmpString = str.Substring((int)reserved_char_Count);
        
        
        //以seperator拆分string
        strings = tmpString.Split(seperator);
        
        //debug一下
        for (int i = 0; i < strings.Length; i++)
        {
            Debug.Log(strings[i]);
        }
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    //排序-----前大后小------------------待扩充小大
    string[] SortArray(string[] _strings)
    {
        string tmpstr;
        bool _compare;
        for(int i = 0; i < strings.Length-1; i++)
        {
            while (strings[i].ToCharArray().Length < strings[i + 1].ToCharArray().Length)
            {
                //string字数相同时比较字母数

                if(strings[i].ToCharArray().Length == strings[i + 1].ToCharArray().Length)
                {
                    _compare = CompareSameLength(strings[i], strings[i + 1]);
                    if (_compare)
                    {

                        i++;
                        continue;
                    }
                }
                tmpstr = strings[i];
                strings[i] = strings[i + 1];
                strings[i + 1] = tmpstr;
                i++;
            }
        }
        


        return _strings;
    }

    //比较ab字母个数多少
    bool CompareSameLength(string a,string b)
    {
        //string tmp;
        int ch_a = 0;
        int ch_b = 0;
        ch_a = GetStringCharCount(a);
        ch_b = GetStringCharCount(b);
        if (ch_a > ch_b)
        {
            return true;
        }
        else
        {
            return false;
        }
  
    }


    //返回字母个数
    int GetStringCharCount(string a)
    {
        int count = 0;
        char[] c = a.ToCharArray();
        foreach (char ch in c)
        {
            if (ch >= 'a' && ch <= 'z' || ch >= 'a' && ch <= 'Z')
            {
                count++;
            }

        }
        return count;
    }

    //debug输出有序strings
    public void PrintStrings()
    {
        newstring = SortArray(strings);
        
        for (int i = 0; i < strings.Length; i++)
        {
            Debug.Log(newstring[i]);
        }
    }

    //输出到Text
    public void WriteToOutputText()
    {
        newlongstr = String.Join("，", SortArray(strings));
        sortedArray.text += newlongstr;
    }

}
