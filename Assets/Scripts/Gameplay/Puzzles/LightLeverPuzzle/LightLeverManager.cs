using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.LeverLight {

    public class LightLeverManager: ILightLeverManager
    {
        private int colorLastIndex;
        private int digitsInSolution = 4;
        private List<SolutionData> solutionDatas;
        private List<SolutionData> setUpData;
        private string colorHelper;
        private List<int> solution;
        private int lightsNeeded;
        private List<LightColor> lightColours;
        private List<LightColor> allColors;
        private Dictionary<LightColor, List<ILight>> colourLightDictonary;
        private List<ILight> lightSet1, lightSet2, lightSet3, lightSet4;
        private int listIndex;

        public LightLeverManager() {
            colourLightDictonary = new Dictionary<LightColor, List<ILight>>();
            lightSet1 = new List<ILight>();
            lightSet2 = new List<ILight>();
            lightSet3 = new List<ILight>();
            lightSet4 = new List<ILight>();
            solutionDatas = new List<SolutionData>();
            colorHelper = "";
            Setup();
        }

        private void Setup() {
            PickSolutionColors();
            SetSolution();
        }

        private void PickSolutionColors() {
            colorLastIndex = System.Enum.GetValues(typeof(LightColor)).Length;
            List<int> availableIndex = new List<int>();
            for (int i = 0; i < colorLastIndex; i++)
                availableIndex.Add(i);
            for (int i = 0; i < digitsInSolution; i++) {
                SolutionData data = new SolutionData();
                int rand = Random.Range(0, availableIndex.Count);
                data.color = (LightColor)availableIndex[rand];
                solutionDatas.Add(data);
                availableIndex.RemoveAt(rand);
            }
        }

        private void SetSolution() {
            solution = new List<int>();
            foreach(SolutionData data in solutionDatas) {
                int rndValue = Random.Range(2, 10);
                data.amount = rndValue;
                solution.Add(rndValue);
                lightsNeeded += data.amount;

                colorHelper += $"{data.color.ToString()[0]} ";
            }
        }

        public int GetLightsNeeded() {
            return lightsNeeded;
        }

        public LightColor GetAvailableLeverColor() {
            if(lightColours == null) {
                lightColours = new List<LightColor>();
                allColors = new List<LightColor>();
                foreach(SolutionData data in solutionDatas) {
                    lightColours.Add(data.color);
                    allColors.Add(data.color);
                }
            }
            int rand = Random.Range(0, lightColours.Count);
            LightColor colourToReturn = lightColours[rand];
            lightColours.RemoveAt(rand);
            return colourToReturn;
        }

        public LightColor GetAvailableLightColor(ILight lightObj) {
            if(setUpData == null)
                setUpData = new List<SolutionData>(solutionDatas);
            int rand = Random.Range(0, setUpData.Count);
            LightColor colorToReturn = setUpData[rand].color;
            setUpData[rand].amount--;
            if(setUpData[rand].amount == 0)
                setUpData.RemoveAt(rand);
            
            AddLightToList(colorToReturn, lightObj);
            return colorToReturn;
        }

        public List<ILight> GetLightsOfColor(LightColor colour) {
            return colourLightDictonary[colour];
        }

        private void AddLightToList(LightColor colour, ILight lightObj) {
            if(!colourLightDictonary.ContainsKey(colour))
                AddToDictionary(colour);
            colourLightDictonary[colour].Add(lightObj);
        }

        private void AddToDictionary(LightColor colour) {
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

        public void SubstractNeededLights(int value) {
            lightsNeeded -= value;
        }

        public List<int> GetSolution() {
            return solution;
        }

        public string GetColorHelper() {
            return colorHelper;
        }

        public List<LightColor> GetAllAvailableColors() {
            return allColors;
        }

        private class SolutionData {
            public LightColor color;
            public int amount;
        }
    }
}