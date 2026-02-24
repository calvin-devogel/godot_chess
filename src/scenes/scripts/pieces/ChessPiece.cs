using System;
using Godot;

public partial class ChessPiece : Area2D
{
    private Vector2 _positionBeforeDrag;

    public enum PieceColor
    {
        White,
        Black
    }

    public struct State
    {
        public Vector2 position;
        public bool hasMoved;
        public bool isCaptured;
        public bool isBeingDragged = false;
        public bool _IsMouseOver = false;
        public bool _IsSnapped = false;

        public State(Vector2 position, bool hasMoved, bool isCaptured)
        {
            this.position = position;
            this.hasMoved = hasMoved;
            this.isCaptured = isCaptured;
            this.isBeingDragged = false;
            this._IsMouseOver = false;
            this._IsSnapped = false;
        }
    }

    public PieceColor color;
    public State state;

    public override void _Ready()
    {
    }

    public override void _Input(InputEvent @event)
    {
        if (state._IsMouseOver && @event is InputEventMouseButton mouseButtonEvent)
        {
            if (mouseButtonEvent.Pressed)
            {
                GD.Print("Mouse button pressed on piece at position: " + this.Position);
                state.isBeingDragged = true;
                _positionBeforeDrag = this.GlobalPosition;
            }
            else if (mouseButtonEvent.IsReleased() && state.isBeingDragged)
            {
                state.isBeingDragged = false;
                if (IsValidMove(_positionBeforeDrag, this.Position))
                {
                    SnapToSquare();
                    state.position = this.Position;
                    state.hasMoved = true;
                }
                else
                {
                    this.Position = _positionBeforeDrag;
                }   
            }
        }
    }

    public override void _Process(double delta)
    {
        _FollowMouse();
    }

    protected virtual void SnapToSquare()
    {
        // snap the piece to the nearest square
    }

    protected bool IsWithinBounds(Vector2 position)
    {
        return position.X >= -256 && position.X <= 256 && position.Y >= -256 && position.Y <= 256;
    }

    protected virtual bool IsValidMove(Vector2 from, Vector2 to) => true;

    public void _OnCapture()
    {
        this.state.isCaptured = true;
        this.Visible = false;
    }

    protected void _OnMouseEntered() => this.state._IsMouseOver = true;
    protected void _OnMouseExited() => this.state._IsMouseOver = false;

    protected void _FollowMouse()
    {
        if (this.state.isBeingDragged)
        {
            GD.Print("Dragging piece at position: " + this.Position);
            Vector2 mousePosition = GetGlobalMousePosition();
            Vector2 newPosition = mousePosition;
            if (IsWithinBounds(newPosition))
            {
                this.Position = newPosition;
            }
        }
    }
}