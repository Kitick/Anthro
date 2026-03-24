namespace UI.Wiki {
	using System.Text;
	using System.Text.RegularExpressions;

	public static class MarkdownParser {
		public static string ToBBCode(string markdown) {
			var lines = markdown.Split('\n');
			var sb = new StringBuilder();
			bool inList = false;

			foreach (string rawLine in lines) {
				string line = rawLine.TrimEnd();

				// Horizontal rule
				if (Regex.IsMatch(line, @"^-{3,}$") || Regex.IsMatch(line, @"^\*{3,}$")) {
					if (inList) { sb.AppendLine(); inList = false; }
					sb.AppendLine("[color=#555555]────────────────────────────────────────[/color]");
					continue;
				}

				// H3
				if (line.StartsWith("### ")) {
					if (inList) { sb.AppendLine(); inList = false; }
					sb.AppendLine($"\n[font_size=15][b]{ProcessInline(line[4..])}[/b][/font_size]");
					continue;
				}
				// H2
				if (line.StartsWith("## ")) {
					if (inList) { sb.AppendLine(); inList = false; }
					sb.AppendLine($"\n[font_size=17][b]{ProcessInline(line[3..])}[/b][/font_size]");
					continue;
				}
				// H1
				if (line.StartsWith("# ")) {
					if (inList) { sb.AppendLine(); inList = false; }
					sb.AppendLine($"\n[font_size=20][b]{ProcessInline(line[2..])}[/b][/font_size]\n");
					continue;
				}

				// List item
				if (Regex.IsMatch(line, @"^[-*] ")) {
					inList = true;
					sb.AppendLine($"• {ProcessInline(line[2..])}");
					continue;
				} else if (inList && !string.IsNullOrEmpty(line)) {
					inList = false;
				}

				// Empty line
				if (string.IsNullOrWhiteSpace(line)) {
					sb.AppendLine();
					continue;
				}

				// Trailing backslash = hard line break
				if (line.EndsWith('\\')) {
					sb.AppendLine(ProcessInline(line[..^1]));
				} else {
					sb.AppendLine(ProcessInline(line));
				}
			}

			return sb.ToString();
		}

		private static string ProcessInline(string text) {
			// Bold-italic ***text***
			text = Regex.Replace(text, @"\*\*\*(.+?)\*\*\*", "[b][i]$1[/i][/b]");
			// Bold **text**
			text = Regex.Replace(text, @"\*\*(.+?)\*\*", "[b]$1[/b]");
			// Italic *text*
			text = Regex.Replace(text, @"\*(.+?)\*", "[i]$1[/i]");
			// Italic _text_ (not mid-word)
			text = Regex.Replace(text, @"(?<!\w)_(.+?)_(?!\w)", "[i]$1[/i]");
			// Inline code `text`
			text = Regex.Replace(text, @"`(.+?)`", "[code]$1[/code]");
			// Links [text](path) — rendered as colored clickable meta
			text = Regex.Replace(text, @"\[([^\]]+)\]\(([^\)]+)\)",
				"[color=#7ec8e3][url=$2]$1[/url][/color]");

			return text;
		}
	}
}
