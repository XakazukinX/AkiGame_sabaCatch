using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MissLoader : MonoBehaviour
{

    private List<string[]> Datas = new List<string[]>();
    
    //データ読み込み
    public List<string[]> LoadData()
    {
        string filePath = "Data/";
        string fileName = "miss";
        TextAsset csv = Resources.Load(filePath + fileName) as TextAsset;
        StringReader reader = new StringReader(csv.text);
        // csvファイルの内容を一行ずつ末尾まで取得しリストを作成
        while (reader.Peek() > -1)
        {
            // 一行読み込む
            var lineData = reader.ReadLine();
            // カンマ(,)区切りのデータを文字列の配列に変換
            var address = lineData.Split(',');
            // リストに追加
            Datas.Add(address);
            // 末尾まで繰り返し...

        }
        return Datas;
    }
}
