using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{

    private string SavePath => $"{Application.persistentDataPath}/save.txt";

    [ContextMenu("Save")]
    public void Save()
    {
        //The file must be loaded up into a variable before we capture the current state, because if you don't then it will load what you just saved.
        //This is why we have loading the file and applying the data to the object seperated
        var state = LoadFile();
        CaptureState(state);
        SaveFile(state);
    }

    [ContextMenu("Load")]
    public void Load()
    {
        var state = LoadFile();
        RestoreState(state);
    }


    private void SaveFile(object state)
    {
        using (var stream = File.Open(SavePath, FileMode.Create))
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, state);
        }
    }

    private Dictionary<string, object> LoadFile()
    {
        //if the file doesn't exist, return an empty dictionary
        if (!File.Exists(SavePath)) 
        {
            return new Dictionary<string, object>();
        }
        
        //get the dictionary in question and return it
        using (FileStream stream = File.Open(SavePath, FileMode.Open))
        {
            var formatter = new BinaryFormatter();
            return (Dictionary<string, object>)formatter.Deserialize(stream);
        }
    
    }

    private void CaptureState(Dictionary<string, object> state)
    {
        foreach (var savable in FindObjectsOfType<SavableEntity>())
        {
            state[savable.Id] = savable.CaptureState();
        }
    }

    private void RestoreState(Dictionary<string, object> state)
    {
        foreach (var savable in FindObjectsOfType<SavableEntity>())
        {
            if (state.TryGetValue(savable.Id, out object value))
            {
                savable.RestoreState(value);
            }
        }
    }

}
