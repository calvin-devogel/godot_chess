using Godot;
using System;

public partial class KnightWhite : Knight
{
	public override void _Ready()
	{
		base._Ready();
		this.color = PieceColor.White;
		this.state = new State(this.Position, false, false);
	}
}
