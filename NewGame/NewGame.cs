namespace UI.NewGame {
	using Godot;

	public partial class NewGame : Control {
		[ExportCategory("Scenes")]
		[Export] private PackedScene NewGameScene = null!;
		[Export] private PackedScene MainMenuScene = null!;

		[ExportCategory("Buttons")]
		[Export] private LineEdit FileName = null!;
		[Export] private Button StartButton = null!;
		[Export] private Button BackButton = null!;

		public override void _Ready() {
			StartButton.Pressed += () => GetTree().ChangeSceneToPacked(NewGameScene);
			BackButton.Pressed += () => GetTree().ChangeSceneToPacked(MainMenuScene);
		}
	}
}
