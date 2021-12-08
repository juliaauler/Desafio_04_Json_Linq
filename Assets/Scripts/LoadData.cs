using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadData : MonoBehaviour{
    private void Start(){
        ReadJson();
    }

    public WrapperData ReadJson(){
        string gameData = ReadFile();

        if(string.IsNullOrEmpty(gameData) || gameData == "{}"){
            return new WrapperData();
        }

        WrapperData res = JsonUtility.FromJson<WrapperData>(gameData);
        return res;
    }

    private static string ReadFile(){
        string savePath = Application.dataPath + "/Resources/Saves/data6.json";

        if(File.Exists(savePath)){
            using (StreamReader reader = new StreamReader(savePath)){
                string gameData = reader.ReadToEnd();
                return gameData;
            }
        }

        else{
            return "";
        }
    }
}