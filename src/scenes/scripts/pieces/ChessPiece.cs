using System;
using Godot;

public partial class ChessPiece : Area2D
{
    private Vector2 _positionBeforeDrag;
    private Vector2 _dragOffset;

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
        if (!state._IsMouseOver || state.isCaptured) return;

        if (@event is InputEventMouseButton mouseEvent && mouseEvent.ButtonIndex == MouseButton.Left)
        {
            if (mouseEvent.Pressed && !state.isBeingDragged)
            {
                state.isBeingDragged = true;
                _positionBeforeDrag = this.GlobalPosition;
                _dragOffset = this.GlobalPosition - mouseEvent.GlobalPosition;
            }
            else if (!mouseEvent.Pressed && state.isBeingDragged)
            {
                state.isBeingDragged = false;
                if (IsValidMove(_positionBeforeDrag, this.GlobalPosition) && IsWithinBounds(this.GlobalPosition))
                {
                    SnapToSquare();
                    state.position = this.GlobalPosition;
                    state.hasMoved = true;
                }
                else
                {
                    this.GlobalPosition = _positionBeforeDrag; // snap back to original position
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
        float squareSize = 64f;
        float snappedX = Mathf.Round(this.GlobalPosition.X / squareSize) * squareSize;
        float snappedY = Mathf.Round(this.GlobalPosition.Y / squareSize) * squareSize;
        this.GlobalPosition = new Vector2(snappedX, snappedY);
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
        if (state.isBeingDragged)
        {
            Vector2 mousePosition = GetGlobalMousePosition() + _dragOffset;
            this.GlobalPosition = mousePosition;
        }
    }
}