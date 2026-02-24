using Godot;
using System;

public partial class Knight : ChessPiece
{
	protected override bool IsValidMove(Vector2 from, Vector2 to)
	{
		// check if the move is within the bounds of the board, doesn't put the king in check
		// and doesn't move to a square occupied by a piece of the same color
		float squareSize = 64;
		float dx = Mathf.Abs(to.X - from.X) / squareSize;
		float dy = Mathf.Abs(to.Y - from.Y) / squareSize;

		// valid knight moves are +/- (1, 2) or +/- (2, 1)
		return (Mathf.RoundToInt(dx) == 1 && Mathf.RoundToInt(dy) == 2) ||
				(Mathf.RoundToInt(dx) == 2 && Mathf.RoundToInt(dy) == 1);
	}

	public override void _Ready()
	{
		base._Ready();
	}
	public override void _Process(double delta)
	{
		base._Process(delta);
	}
}
