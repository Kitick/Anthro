namespace UI.MainMenu {
	using Godot;

	public partial class MainMenu : Control {
		[Export] private Button StartButton = null!;
		[Export] private Button ExitButton = null!;

		public override void _Ready() {
			StartButton.Pressed += () => GD.Print("Start button pressed");
			ExitButton.Pressed += () => GetTree().Quit();
		}
	}
}
