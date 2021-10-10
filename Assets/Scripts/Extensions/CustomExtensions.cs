using System.Collections;
using System.Collections.Generic;
using Knotgames.CharacterData;
using Knotgames.Network;
using UnityEngine;

public class CustomExtensions
{
    public static string GetModelType(ModelType modelType)
    {
        if (modelType == ModelType.Nada)
            return "nada";
        else if (modelType == ModelType.Ghost1 || modelType == ModelType.Ghost2 || modelType == ModelType.Ghost3)
            return "ghost";
        else
            return "human";
    }

    public static GameObject ReturnModelObject(ModelType type)
    {
        switch (type)
        {
            case ModelType.Human1:
                return NetGameManager.instance.allSpawnData.human1Data.model;
            case ModelType.Human2:
                return NetGameManager.instance.allSpawnData.human2Data.model;
            case ModelType.Human3:
                return NetGameManager.instance.allSpawnData.human3Data.model;
            case ModelType.Human4:
                return NetGameManager.instance.allSpawnData.human4Data.model;
            case ModelType.Human5:
                return NetGameManager.instance.allSpawnData.human5Data.model;
            case ModelType.Human6:
                return NetGameManager.instance.allSpawnData.human6Data.model;
            case ModelType.Ghost1:
                return NetGameManager.instance.allSpawnData.ghost1Data.model;
            case ModelType.Ghost2:
                return NetGameManager.instance.allSpawnData.ghost2Data.model;
            case ModelType.Ghost3:
                return NetGameManager.instance.allSpawnData.ghost3Data.model;
        }
        return null;
    }
}