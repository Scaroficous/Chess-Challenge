using ChessChallenge.API;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

public class MyBot : IChessBot
{
    public Move Think(Board board, Timer timer)
    {
        Move[] moves = board.GetLegalMoves();
        List<Move> moves2 = new List<Move>();

        //Debug.WriteLine(moves.Length);
        for (int i = 0; i < moves.Length; i++)
        {
            //Board nextBoard = board;

            
            board.MakeMove(board.GetLegalMoves()[i]);
            if (board.IsInCheckmate())
            {
                board.UndoMove(moves[i]);
                return moves[i];
            }

            for (int j = 0; j < 10; j++)
            {
                Move nextMove = TryToGetCheckmate(board);
                moves2.Add(nextMove);
                board.MakeMove(nextMove);
                if (board.IsInCheckmate())
                {
                    if (board.IsWhiteToMove)
                    {
                        for (int k = moves2.Count - 1; k > 0; k--)
                        {
                            //board.UndoMove(moves2[k]);
                            moves2.Remove(moves2[k]);
                        }
                        board.UndoMove(moves[i]);
                        return moves[i];
                    }
                    else
                    {
                        //board.UndoMove(nextMove);
                        moves2.Remove(nextMove);
                        break;
                    }
                }
            }


            //board.UndoMove(moves[i]);

            for (int k = moves2.Count - 1; k > 0; k--)
            {
                //board.UndoMove(moves2[k]);
                moves2.Remove(moves2[k]);
            }

            if (board.GetPiece(moves[i].TargetSquare).PieceType != 0)
            {
                return moves[i];
            }
            
            
        }
        //Debug.WriteLine(board.IsWhiteToMove);
        return moves[0];
    }

    public Move TryToGetCheckmate(Board board)
    {
        Move[] moves = board.GetLegalMoves();

        for (int i = 0; i < moves.Length; i++)
        {
            if (board.IsInCheckmate())
            {
                return moves[i];
            }

            if (board.GetPiece(moves[i].TargetSquare).PieceType != 0)
            {
                return moves[i];
            }
        }

        return board.GetLegalMoves()[0];
    }
}