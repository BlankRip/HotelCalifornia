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
        [SerializeField] List<MapConnection> connections;
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
                piece.FlipValues();
            }
        }

        public void Solved()
        {
            foreach (MapPiece piece in pieces)
            {
                piece.tag = "Untagged";
                piece.gameObject.layer = 0;
            }
            foreach (MapConnection connection in connections)
            {
                connection.tag = "Untagged";
                connection.gameObject.layer = 0;
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
                pieces[i].interactable = false;
                if (solution[i])
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
        List<bool> conekshuns = new List<bool>();
        public List<bool> GetConnectionValues()
        {
            conekshuns.Clear();
            foreach (MapConnection connection in connections)
            {
                conekshuns.Add(connection.GetValue());
            }
            return conekshuns;
        }
    }
}