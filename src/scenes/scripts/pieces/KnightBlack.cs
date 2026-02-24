using Godot;
using System;

public partial class KnightBlack : Knight
{

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		this.color = PieceColor.Black;
		this.state = new State(this.Position, false, false);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
