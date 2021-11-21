using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.LevelGen;

public class MazeTVRoom : MonoBehaviour, IPairPuzzleSetup
{
    [SerializeField] Transform tvObj;
    [SerializeField] List<Transform> tvSpots;

    public void Link(GameObject obj, bool initiator) {
        //Nothing to link for now
    }

    private void Start() {
        int rand = KnotRandom.theRand.Next(0, tvSpots.Count);
        tvObj.position = tvSpots[rand].position;
        tvObj.rotation = tvSpots[rand].rotation;
        tvObj.gameObject.SetActive(true);
    }
}
