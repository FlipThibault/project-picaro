using UnityEngine;
using System.Collections.Generic;

public interface IDataPersistence
{
    void LoadData (GameData data);
    void SaveData (ref GameData data);
}
