namespace UI.MainMenu {
	using Godot;

	public partial class MainMenu : Control {
		[ExportCategory("Scenes")]
		[Export] private PackedScene NewGameScene = null!;
		[Export] private PackedScene LoadGameScene = null!;
		[Export] private PackedScene SettingsScene = null!;

		[ExportCategory("Buttons")]
		[Export] private Button NewGame = null!;
		[Export] private Button LoadGame = null!;
		[Export] private Button Options = null!;
		[Export] private Button Settings = null!;
		[Export] private Button Exit = null!;

		public override void _Ready() {
			NewGame.Pressed += () => GetTree().ChangeSceneToPacked(NewGameScene);
			LoadGame.Pressed += () => GetTree().ChangeSceneToPacked(LoadGameScene);
			Options.Pressed += () => GetTree().ChangeSceneToPacked(SettingsScene);
			Settings.Pressed += () => GetTree().ChangeSceneToPacked(SettingsScene);
			Exit.Pressed += () => GetTree().Quit();
		}
	}
}
