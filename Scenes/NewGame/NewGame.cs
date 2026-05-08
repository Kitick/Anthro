namespace UI.NewGame {
	using Godot;
	using Root;

	public partial class NewGame : Control {
		[ExportCategory("Buttons")]
		[Export] private Button StartButton = null!;
		[Export] private Button BackButton = null!;

		[ExportCategory("Parameters")]
		[Export] private LineEdit FileName = null!;

		public override void _Ready() {
			StartButton.Pressed += () => GD.Print($"Starting new game with file name: {FileName.Text}");
			BackButton.Pressed += Root.Instance.ChangeToMainMenu;
		}
	}
}
