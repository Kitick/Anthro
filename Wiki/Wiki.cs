namespace UI.Wiki {
	using System.Collections.Generic;
	using Godot;
	using Root;

	public partial class Wiki : Control {
		private Tree _entryTree = null!;
		private LineEdit _searchBox = null!;
		private Button _backBtn = null!;
		private Button _forwardBtn = null!;
		private Button _backToMenuBtn = null!;
		private Label _entryTitle = null!;
		private RichTextLabel _content = null!;

		//[Export] private string[] ExcludedFolders = null!;

		private readonly Dictionary<string, List<WikiEntry>> _entries = [];
		private readonly Stack<string> _backHistory = new();
		private readonly Stack<string> _forwardHistory = new();
		private string? _currentPath;

		public override void _Ready() {
			_entryTree     = GetNode<Tree>("%EntryTree");
			_searchBox     = GetNode<LineEdit>("%SearchBox");
			_backBtn       = GetNode<Button>("%BackBtn");
			_forwardBtn    = GetNode<Button>("%ForwardBtn");
			_backToMenuBtn = GetNode<Button>("%BackToMenu");
			_entryTitle    = GetNode<Label>("%EntryTitle");
			_content       = GetNode<RichTextLabel>("%Content");

			ScanLoreFiles();
			PopulateSidebar();

			_entryTree.ItemSelected     += OnEntrySelected;
			_searchBox.TextChanged      += OnSearchChanged;
			_backBtn.Pressed            += NavigateBack;
			_forwardBtn.Pressed         += NavigateForward;
			_backToMenuBtn.Pressed      += Root.Instance.ChangeToMainMenu;
			_content.MetaClicked        += OnLinkClicked;

			UpdateNavButtons();
		}

		// ── Scanning ──────────────────────────────────────────────────────────

		private void ScanLoreFiles() {
			_entries.Clear();
			ScanDirectory("res://Lore", "res://Lore");
		}

		private void ScanDirectory(string path, string loreRoot) {
			using var dir = DirAccess.Open(path);
			if (dir == null) return;

			dir.ListDirBegin();
			string name = dir.GetNext();
			while (name != "") {
				if (name != "." && name != "..") {
					string fullPath = $"{path}/{name}";
					if (dir.CurrentIsDir())
						ScanDirectory(fullPath, loreRoot);
					else if (name.EndsWith(".md"))
						RegisterEntry(fullPath, loreRoot);
				}
				name = dir.GetNext();
			}
		}

		private void RegisterEntry(string filePath, string loreRoot) {
			string relative = filePath[(loreRoot.Length + 1)..]; // e.g. "Characters/Arwyn.md"
			string[] parts  = relative.Split('/');
			string category = parts.Length > 1 ? parts[^2] : "General";

			string raw   = FileAccess.GetFileAsString(filePath);
			string title = ExtractTitle(raw, filePath.GetFile().GetBaseName());

			if (!_entries.ContainsKey(category))
				_entries[category] = [];

			_entries[category].Add(new WikiEntry(title, category, filePath, raw));
		}

		// ── Sidebar ───────────────────────────────────────────────────────────

		private void PopulateSidebar(string filter = "") {
			_entryTree.Clear();
			var root = _entryTree.CreateItem();

			foreach (var (category, entries) in _entries) {
				var catItem = _entryTree.CreateItem(root);
				catItem.SetText(0, category);
				catItem.SetSelectable(0, false);

				foreach (var entry in entries) {
					if (!string.IsNullOrEmpty(filter) &&
						!entry.Title.Contains(filter, System.StringComparison.OrdinalIgnoreCase))
						continue;

					var item = _entryTree.CreateItem(catItem);
					item.SetText(0, entry.Title);
					item.SetMeta("file_path", entry.FilePath);
				}
			}
		}

		private void OnEntrySelected() {
			var item = _entryTree.GetSelected();
			if (item == null || !item.HasMeta("file_path")) return;
			NavigateTo((string)item.GetMeta("file_path"));
		}

		private void OnSearchChanged(string text) => PopulateSidebar(text);

		// ── Navigation ────────────────────────────────────────────────────────

		private void NavigateTo(string filePath) {
			if (_currentPath != null)
				_backHistory.Push(_currentPath);
			_forwardHistory.Clear();
			SetEntry(filePath);
		}

		private void NavigateBack() {
			if (_backHistory.Count == 0) return;
			if (_currentPath != null) _forwardHistory.Push(_currentPath);
			SetEntry(_backHistory.Pop());
		}

		private void NavigateForward() {
			if (_forwardHistory.Count == 0) return;
			if (_currentPath != null) _backHistory.Push(_currentPath);
			SetEntry(_forwardHistory.Pop());
		}

		private void SetEntry(string filePath) {
			_currentPath = filePath;
			string raw   = FileAccess.GetFileAsString(filePath);
			_entryTitle.Text = ExtractTitle(raw, filePath.GetFile().GetBaseName());
			_content.Text    = MarkdownParser.ToBBCode(raw);
			UpdateNavButtons();
		}

		private void UpdateNavButtons() {
			_backBtn.Disabled    = _backHistory.Count == 0;
			_forwardBtn.Disabled = _forwardHistory.Count == 0;
		}

		// ── Link clicks ───────────────────────────────────────────────────────

		private void OnLinkClicked(Variant meta) {
			string link = meta.AsString();
			if (string.IsNullOrEmpty(link) || _currentPath == null) return;

			string currentDir = _currentPath[.._currentPath.LastIndexOf('/')];
			string resolved   = ResolvePath(currentDir, link);

			if (FileAccess.FileExists(resolved))
				NavigateTo(resolved);
			else
				GD.PrintErr($"[Wiki] Could not resolve link '{link}' from '{_currentPath}'");
		}

		private static string ResolvePath(string baseDir, string relativePath) {
			// Strip anchor fragments
			int hash = relativePath.IndexOf('#');
			if (hash >= 0) relativePath = relativePath[..hash];

			var parts = new List<string>(baseDir.Split('/'));
			foreach (string part in relativePath.Split('/')) {
				if (part == "..") { if (parts.Count > 0) parts.RemoveAt(parts.Count - 1); }
				else if (part != ".") parts.Add(part);
			}
			return string.Join("/", parts);
		}

		// ── Helpers ───────────────────────────────────────────────────────────

		private static string ExtractTitle(string content, string fallback) {
			foreach (string line in content.Split('\n')) {
				string trimmed = line.Trim();
				if (trimmed.StartsWith("# "))
					return trimmed[2..].Trim();
			}
			return fallback;
		}
	}

	public record WikiEntry(string Title, string Category, string FilePath, string RawContent);
}
