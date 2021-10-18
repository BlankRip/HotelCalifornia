using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Gameplay;
using Knotgames.Gameplay.Puzzle.ChemicalRoom;
using Knotgames.Gameplay.Puzzle.XO;
using Knotgames.Gameplay.Puzzle.Radio;
using Knotgames.Gameplay.Puzzle.Morse;
using Knotgames.Gameplay.Puzzle.QuickDelivery;
using Knotgames.Gameplay.Puzzle.Replicate;
using Knotgames.Gameplay.Puzzle.Riddler;

public class IdsResetter
{
    public static void ResetIDs() {
        PortionObj.ResetIds();
        PuzzleSolvedObj.ResetIDs();
        BoardPiece.ResetIDs();
        TuningPiece.ResetIDs();
        MorseButton.ResetIDs();
        DeliveryItem.RestIDs();
        ReplicateObject.ResetIds();
        RiddleSolutionPad.ResetID();
        RiddleBoard.ResetIDs();
    }
}
