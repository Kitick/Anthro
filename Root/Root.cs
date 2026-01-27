namespace Root {
	using Godot;

	public sealed partial class Root : Node {
		public static Root Instance { get; private set; } = null!;

		[ExportCategory("Scenes")]
		[Export] private PackedScene MainMenuScene = null!;
		[Export] private PackedScene NewGameScene = null!;
		[Export] private PackedScene LoadGameScene = null!;
		[Export] private PackedScene SettingsScene = null!;

		private Node? CurrentScene = null;

		public override void _Ready() {
			Instance = this;
			ChangeToMainMenu();
		}

		private void ChangeScene(PackedScene scene) {
			Node instance = this.AddScene<Node>(scene);
			CurrentScene?.QueueFree();
			CurrentScene = instance;
		}

		public void ChangeToMainMenu() => ChangeScene(MainMenuScene);
		public void ChangeToNewGame() => ChangeScene(NewGameScene);
	}

	public static class RootExtensions {
		public static void AddScene(this Node parent, PackedScene scene) {
			parent.AddChild(scene.Instantiate());
		}

		public static T AddScene<T>(this Node parent, PackedScene scene) where T : Node {
			T instance = scene.Instantiate<T>();
			parent.AddChild(instance);
			return instance;
		}
	}
}