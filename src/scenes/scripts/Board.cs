using Godot;
using System;
using System.Collections.Generic;

public partial class Board : Area2D
{
	private enum Winner
	{
		White,
		Black,
		None
	}
	
	private struct BoardState
	{
		// need to know: - where each piece is on the board
		//               - whether each piece has moved
		//               - whether each piece is captured
		//               - whose turn it is
		//               - move history (for threefold repetition and fifty-move rule)
		//              - whether the game is over and who won
		public Dictionary<ChessPiece, ChessPiece.State> pieces;
		public bool whiteToMove;
		public String moveHistory;
		public Winner winner;
	}
	private BoardState state;
	private BoardState getInitialState()
	{
		BoardState initialState = new BoardState();
		initialState.pieces = new Dictionary<ChessPiece, ChessPiece.State>();
		initialState.whiteToMove = true;
		initialState.moveHistory = "";
		initialState.winner = Winner.None;
		return initialState;
	}

	private void _SetupBoard()
	{
	}
	public override void _Ready()
	{

	}

	public override void _Process(double delta)
	{
		
	}
}
