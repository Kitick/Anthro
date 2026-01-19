namespace UI.MainMenu {
	using Godot;

	public partial class MainMenu : Control {
		[Export] private Button NewGame = null!;
		[Export] private Button LoadGame = null!;
		[Export] private Button Options = null!;
		[Export] private Button Settings = null!;
		[Export] private Button Exit = null!;
		
		public override void _Ready() {
			NewGame.Pressed += () => GD.Print("Start button pressed");
			LoadGame.Pressed += () => GD.Print("Load button pressed");
			Options.Pressed += () => GD.Print("Options button pressed");
			Settings.Pressed += () => GD.Print("Settings button pressed");
			Exit.Pressed += () => GetTree().Quit();
		}	
	}
}
