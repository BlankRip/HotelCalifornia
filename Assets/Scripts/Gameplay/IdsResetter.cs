using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Gameplay.Puzzle.ChemicalRoom;
using Knotgames.Gameplay.Puzzle.XO;
using Knotgames.Gameplay.Puzzle.Morse;
using Knotgames.Gameplay.Puzzle.QuickDelivery;
using Knotgames.Gameplay;

public class IdsResetter
{
    public static void ResetIDs() {
        PortionObj.ResetIds();
        PuzzleSolvedObj.ResetIDs();
        BoardPiece.ResetIDs();
        MorseButton.ResetIDs();
        DeliveryItem.RestIDs();
    }
}
