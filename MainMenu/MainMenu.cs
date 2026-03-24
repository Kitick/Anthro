namespace UI.MainMenu {
	using Godot;
	using Root;

	public partial class MainMenu : Control {
		[ExportCategory("Buttons")]
		[Export] private Button NewGame = null!;
		[Export] private Button LoadGame = null!;
		[Export] private Button Database = null!;
		[Export] private Button Options = null!;
		[Export] private Button Settings = null!;
		[Export] private Button Exit = null!;

		public override void _Ready() {
			NewGame.Pressed += Root.Instance.ChangeToNewGame;
			LoadGame.Pressed += () => GD.Print("Load Game pressed");
			Database.Pressed += Root.Instance.ChangeToWiki;
			Options.Pressed += () => GD.Print("Options pressed");
			Settings.Pressed += () => GD.Print("Settings pressed");
			Exit.Pressed += () => GetTree().Quit();
		}
	}
}
