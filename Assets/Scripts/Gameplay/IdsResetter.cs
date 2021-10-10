using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Gameplay.Puzzle.ChemicalRoom;
using Knotgames.Gameplay.Puzzle.XO;
using Knotgames.Gameplay;

public class IdsResetter
{
    public static void ResetIDs() {
        PortionObj.ResetIds();
        PuzzleSolvedObj.ResetIDs();
        BoardPiece.ResetIDs();
    }
}
