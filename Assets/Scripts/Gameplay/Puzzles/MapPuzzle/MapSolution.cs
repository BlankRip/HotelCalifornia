using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Knotgames.Gameplay.Puzzle.Map
{
    public class MapSolution : MonoBehaviour, IMapSolution
    {
        [SerializeField] GameplayEventCollection eventCollection;
        private bool screwed;
        [SerializeField] List<MapPiece> pieces;
        List<bool> solution = new List<bool>();

        private void Start()
        {
            screwed = false;
            eventCollection.twistVision.AddListener(Messup);
            eventCollection.fixVision.AddListener(Fix);
        }

        private void OnDestroy()
        {
            eventCollection.twistVision.RemoveListener(Messup);
            eventCollection.fixVision.RemoveListener(Fix);
        }

        private void Messup()
        {
            screwed = true;
            FlipValues();
        }

        private void Fix()
        {
            screwed = false;
            FlipValues();
        }

        private void FlipValues()
        {
            foreach (MapPiece piece in pieces)
            {
                FlipValues();
            }
        }

        public List<bool> BuildNewSolution(Transform newSpot)
        {
            transform.position = newSpot.position;
            transform.rotation = newSpot.rotation;
            return SetSolution();
        }

        private List<bool> SetSolution()
        {
            for (int i = 0; i < pieces.Count; i++)
                solution.Add(GetRandomBool());
            if (screwed)
                FlipValues();
            
            SetupMap();
            return solution;
        }

        public void SetupMap()
        {
            for (int i = 0; i < pieces.Count; i++)
            {
                if(solution[i])
                    pieces[i].TurnOn();
            }
        }

        bool GetRandomBool()
        {
            int rand = Random.Range(0, 2);
            if (rand == 0)
                return false;
            else
                return true;
        }
    }
}