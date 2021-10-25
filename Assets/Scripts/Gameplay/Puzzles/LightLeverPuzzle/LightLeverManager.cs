using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.LevelLight {

    public class LightLeverManager: ILightLeverManager
    {
        private int colorLastIndex;
        private int digitsInSolution = 4;
        private List<SolutionData> solutionDatas;
        private List<SolutionData> setUpData;
        private List<int> solution;
        private int lightsNeeded;
        private List<LightColour> lightColours;
        private Dictionary<LightColour, List<ILight>> colourLightDictonary;
        private List<ILight> lightSet1, lightSet2, lightSet3, lightSet4;
        private int listIndex;

        public LightLeverManager() {
            colourLightDictonary = new Dictionary<LightColour, List<ILight>>();
            lightSet1 = lightSet2 = lightSet3 = lightSet4 = new List<ILight>();
            solutionDatas = new List<SolutionData>();
        }

        private void PickSolutionColors() {
            colorLastIndex = System.Enum.GetValues(typeof(LightColour)).Length;
            List<int> availableIndex = new List<int>();
            for (int i = 0; i < colorLastIndex; i++)
                availableIndex.Add(i);
            for (int i = 0; i < digitsInSolution; i++) {
                SolutionData data = new SolutionData();
                int rand = Random.Range(0, availableIndex.Count);
                data.colour = (LightColour)rand;
                solutionDatas.Add(data);
            }
        }

        private void SetSolution() {
            solution = new List<int>();
            foreach(SolutionData data in solutionDatas) {
                int rndValue = Random.Range(3, 11);
                data.amount = rndValue;
                solution.Add(rndValue);
                lightsNeeded += data.amount;
            }
        }

        public int GetLightsNeeded() {
            return lightsNeeded;
        }

        public LightColour GetAvailableLeverColor() {
            if(lightColours == null) {
                lightColours = new List<LightColour>();
                foreach(SolutionData data in solutionDatas)
                    lightColours.Add(data.colour);
            }
            int rand = Random.Range(0, lightColours.Count);
            LightColour colourToReturn = lightColours[rand];
            lightColours.RemoveAt(rand);
            return colourToReturn;
        }

        public LightColour GetAvailableLightColor(ILight lightObj) {
            if(setUpData == null)
                setUpData = new List<SolutionData>(solutionDatas);
            int rand = Random.Range(0, setUpData.Count);
            LightColour colorToReturn = setUpData[rand].colour;
            setUpData[rand].amount--;
            if(setUpData[rand].amount == 0)
                setUpData.RemoveAt(rand);
            
            AddLightToList(colorToReturn, lightObj);
            return colorToReturn;
        }

        public List<ILight> GetLightsOfClour(LightColour colour) {
            return colourLightDictonary[colour];
        }

        private void AddLightToList(LightColour colour, ILight lightObj) {
            if(!colourLightDictonary.ContainsKey(colour))
                AddToDictionary(colour);
            colourLightDictonary[colour].Add(lightObj);
        }

        private void AddToDictionary(LightColour colour) {
            listIndex++;
            switch (listIndex) {
                case 1:
                    colourLightDictonary.Add(colour, lightSet1);
                    break;
                case 2:
                    colourLightDictonary.Add(colour, lightSet2);
                    break;
                case 3:
                    colourLightDictonary.Add(colour, lightSet3);
                    break;
                case 4:
                    colourLightDictonary.Add(colour, lightSet4);
                    break;
            }
        }
    }
}