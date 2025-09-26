using System;

namespace Unisaw.Core;

public static class CutPlanner
{
    /// <summary>
    /// Calculates how many pieces of length `piece` can be cut from `stock`
    /// considering saw kerf `kerf` between pieces (no kerf after the last piece).
    /// All numbers are in the same unit (e.g., mm).
    /// </summary>
    public static int MaxPiecesFromStock(double stock, double piece, double kerf)
    {
        if (stock <= 0) throw new ArgumentOutOfRangeException(nameof(stock));
        if (piece <= 0) throw new ArgumentOutOfRangeException(nameof(piece));
        if (kerf < 0) throw new ArgumentOutOfRangeException(nameof(kerf));
        if (piece > stock) return 0;

        int count = 0;
        double remaining = stock;

        while (remaining >= piece)
        {
            count++;
            remaining -= piece;
            // subtract kerf only if we can still attempt another piece
            if (remaining >= piece)
                remaining -= kerf;
        }

        return count;
    }
}
