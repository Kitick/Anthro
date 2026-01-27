namespace UI.NewGame {
	using Godot;

	public partial class NewGame : Control {
		[Export] private Button StartButton = null!;
		[Export] private Button ExitButton = null!;
		
		[Export] private LineEdit FileName = null!;

		public override void _Ready() {
			StartButton.Pressed += () => GD.Print("Start button pressed");
			ExitButton.Pressed += () => GetTree().Quit();
		}
	}
}
